import React, { Component } from 'react';
import { connect } from 'react-redux';
import DeliveryDetailsModal from '../../components/design/deliveryStatus/DeliveryDetailsModal';
import Paginator from '../../components/design/paginator/Paginator';
import * as deliveriesActions from '../../redux/deliveries/deliveriesActions';
import * as loadingActions from '../../redux/loading/loadingActions';
import { DateToString } from '../../utils/helpers/DateHelper';
import { api } from '../../utils/helpers/AxiosHelper';

class NewDeliveries extends Component {
	constructor(props) {
		super(props);
	}

	state = {
		deliveryDetails: {},
	};

	componentDidMount() {
		this.getUserDeliveriesByPage();
	}

	getUserDeliveriesByPage = (pageNumber = 1) => {
		this.props.RequestDeliveries();
		this.props.LoadingOn();
		api.get('api/deliveries/new-deliveries/page/' + pageNumber)
			.then((response) => {
				this.props.DeliveriesSuccess(
					response.data.deliveries,
					response.data.totalDeliveries,
				);
				this.props.LoadingOff();
			})
			.catch((error) => {
				this.props.DeliveriesError(error.response);
				this.props.LoadingOff();
			});
	};

	PaginatorOnClick = (pageNum) => {
		this.getUserDeliveriesByPage(pageNum);
	};

	render() {
		return (
			<div className='container my-3'>
				{this.props.deliveries ? (
					<div>
						<table className='table table-hover'>
							<thead>
								<tr className='row'>
									<th className='col'>Delivery number</th>
									<th className='col'>From</th>
									<th className='col'>To</th>
									<th className='col'>Created at</th>
								</tr>
							</thead>
							<tbody>
								{this.props.deliveries.map((delivery) => {
									return (
										<tr
											onClick={() =>
												this.setState({
													deliveryDetails: delivery,
												})
											}
											data-toggle='modal'
											data-target='#detailsModal'
											className='row table-row'
											key={delivery.delivery.id}>
											<td className='col d-flex align-items-center'>
												{delivery.delivery.id}
											</td>
											<td className='col d-flex align-items-center'>
												{delivery.origin.city}
											</td>
											<td className='col d-flex align-items-center'>
												{delivery.destination.city}
											</td>
											<td className='col d-flex align-items-center'>
												{DateToString(
													delivery.delivery.created,
												)}
											</td>
										</tr>
									);
								})}
							</tbody>
						</table>
						<DeliveryDetailsModal
							takeDelivery
							getDeliveries={this.getUserDeliveriesByPage}
							deliveryDetails={
								this.state.deliveryDetails
							}></DeliveryDetailsModal>
					</div>
				) : (
					<div>No new deliveries...</div>
				)}
				<Paginator
					pages={Math.ceil(this.props.totalDeliveries / 10)}
					onClick={(pageNum) =>
						this.PaginatorOnClick(pageNum)
					}></Paginator>
			</div>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		deliveries: state.deliveries.deliveries,
		totalDeliveries: state.deliveries.totalDeliveries,
	};
};
const mapDispatchToProps = (dispatch) => {
	return {
		RequestDeliveries: () =>
			dispatch(deliveriesActions.RequestDeliveries()),
		DeliveriesSuccess: (deliveries, totalDeliveriesNum) =>
			dispatch(
				deliveriesActions.DeliveriesSuccess(
					deliveries,
					totalDeliveriesNum,
				),
			),
		DeliveriesError: (error) =>
			dispatch(deliveriesActions.DeliveriesError(error)),
		LoadingOn: () => dispatch(loadingActions.loadingOn()),
		LoadingOff: () => dispatch(loadingActions.loadingOff()),
	};
};
export default connect(mapStateToProps, mapDispatchToProps)(NewDeliveries);
