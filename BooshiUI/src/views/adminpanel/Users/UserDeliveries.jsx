import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import PrimaryButton from '../../../components/design/buttons/PrimaryButton';
import DeliveryDetailsModal from '../../../components/design/deliveryStatus/DeliveryDetailsModal';
import DeliveryStatus from '../../../components/design/deliveryStatus/deliveryStatus';
import Paginator from '../../../components/design/paginator/Paginator';
import * as deliveriesActions from '../../../redux/deliveries/deliveriesActions';
import * as loadingActions from '../../../redux/loading/loadingActions';
import { DateToString } from '../../..//utils/helpers/DateHelper';
import { api } from '../../../utils/helpers/AxiosHelper';
import { useParams } from 'react-router-dom';
import NoDeliveries from '../../../components/no-deliveries/NoDeliveries';

const UserDeliveries = (props) => {
	const { id } = useParams();
	const [delivery, setDelivery] = useState({});
	const [user, setUser] = useState({});
	const navigate = useNavigate();

	useEffect(() => {
		getUserDeliveriesByPage();
	}, []);

	const getUserDeliveriesByPage = (pageNumber = 1) => {
		props.RequestDeliveries();
		props.LoadingOn();
		api.get(props.apiUrl + id + '/page/' + pageNumber)
			.then((response) => {
				props.DeliveriesSuccess(
					response.data.deliveries,
					response.data.totalCount,
				);
				api.get('/api/users/' + id).then((res) => {
					setUser(res.data);
					props.LoadingOff();
				});
			})
			.catch((error) => {
				props.DeliveriesError(error.response);
				props.LoadingOff();
				if (
					error.response.status === 400 ||
					error.response.status === 404
				) {
					navigate('..');
				}
			});
	};

	const PaginatorOnClick = (pageNum) => {
		getUserDeliveriesByPage(pageNum);
	};

	return (
		<div className=''>
			<div className='container-lg my-3'>
				<div className='row justify-content-between mt-4 m-auto'>
					<div className='col-auto p-0 m-0 mb-2'>
						<h5 className='color-booshi'>
							<i className='fas fa-user'></i> {user.firstName}{' '}
							{user.lastName}
						</h5>
					</div>
					<div className='col-auto p-0 m-0 mb-2'>
						<h5 className='color-booshi'>
							<i className='fas fa-envelope'></i> {user.email}
						</h5>
					</div>
					<div className='col-auto p-0 m-0 mb-2'>
						<h5 className='color-booshi'>
							<i className='fas fa-phone'></i> {user.phoneNumber}
						</h5>
					</div>
				</div>
				{props.deliveries ? (
					<div>
						<table className='table table-hover'>
							<thead>
								<tr className='row'>
									<th className='col'>Delivery number</th>
									<th className='col'>From</th>
									<th className='col'>To</th>
									<th className='d-none col-md d-md-flex'>
										Created at
									</th>
									<th className='col'></th>
								</tr>
							</thead>
							<tbody>
								{props.deliveries.map((delivery) => {
									return (
										<tr
											onClick={() =>
												setDelivery(delivery)
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
											<td className='d-none col-md d-md-flex align-items-center'>
												{DateToString(
													delivery.delivery.created,
												)}
											</td>
											<td className='col d-flex align-items-center justify-content-center'>
												<DeliveryStatus
													statusId={
														delivery.delivery
															.deliveryStatusId
													}
												/>
											</td>
										</tr>
									);
								})}
							</tbody>
						</table>
						<DeliveryDetailsModal
							deliveryPerson={props.deliveryPerson}
							getDeliveries={getUserDeliveriesByPage}
							deliveryDetails={delivery}></DeliveryDetailsModal>
					</div>
				) : (
					<div>
						<NoDeliveries />
					</div>
				)}
				<Paginator
					pages={Math.ceil(props.totalDeliveries / 10)}
					onClick={(pageNum) =>
						PaginatorOnClick(pageNum)
					}></Paginator>
				<div className='row p-0 m-0'>
					<div className='col-auto p-0 m-0 mb-2'>
						<PrimaryButton
							className='btn-block'
							onClick={() => {
								user.roleId == 3
									? navigate('/adminpanel/users')
									: navigate('/adminpanel/deliverypersons');
							}}>
							<i className='fas fa-chevron-left'></i>
							{user.roleId == 3
								? ' Back to users'
								: ' Back to delivery persons'}
						</PrimaryButton>
					</div>
				</div>
			</div>
		</div>
	);
};

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
export default connect(mapStateToProps, mapDispatchToProps)(UserDeliveries);
