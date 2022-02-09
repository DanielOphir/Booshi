import React, { useState } from 'react';
import { DateToString } from '../../../utils/helpers/DateHelper';
import DeliveryStatus from '../deliveryStatus/deliveryStatus';
import * as $ from 'jquery';
import { api } from '../../../utils/helpers/AxiosHelper';
import { connect } from 'react-redux';
import { loadingOff, loadingOn } from '../../../redux/loading/loadingActions';
import { useLocation, Link } from 'react-router-dom';

function DeliveryDetailsModal({
	deliveryDetails,
	pageNum,
	getDeliveries,
	...props
}) {
	const location = useLocation();
	const admin = props.user?.roleId === 1;

	const [deliveryPerson, setDeliveryPerson] = useState(null);

	const deleteDialog = () => {
		$('#detailsModal').modal('hide');
	};

	const cancelDelivery = () => {
		props.loadingOn();
		api.patch('/api/deliveries/cancel/' + deliveryDetails?.delivery?.id)
			.then((response) => {
				if (response.status === 200) {
					$('#deleteModal').modal('hide');
					props.loadingOff();
				}
				getDeliveries(pageNum);
			})
			.catch(() => props.loadingOff());
	};

	const takeDelivery = (deliveryId) => {
		api.patch('/api/deliveryperson/asign-self', deliveryId)
			.then((response) => {
				$('.modal').modal('hide');
				getDeliveries(pageNum);
			})
			.catch((error) => {
				$('.modal').modal('hide');
				getDeliveries(pageNum);
			});
	};

	$('#detailsModal')
		.off('show.bs.modal')
		.on('show.bs.modal', function () {
			if (deliveryDetails?.delivery?.deliveryPersonId) {
				props.loadingOn();
				api.get(
					`api/users/${deliveryDetails.delivery.deliveryPersonId}`,
				)
					.then((response) => {
						if (response.status === 200) {
							setDeliveryPerson(response.data);
						}
						props.loadingOff();
					})
					.catch((error) => {
						props.loadingOff();
					});
				return;
			}
			setDeliveryPerson(null);
		});

	const changeDeliveryStatus = (statusId) => {
		props.loadingOn();
		api.patch(
			'/api/deliveries/change-status/' + deliveryDetails?.delivery?.id,
			statusId,
		)
			.then((response) => {
				if (response.status === 200) {
					$('.modal').modal('hide');
					props.loadingOff();
				}
				getDeliveries(pageNum);
			})
			.catch(() => props.loadingOff());
	};
	return (
		<div>
			<div className='modal fade' id='detailsModal' tabIndex='-1'>
				<div className='modal-dialog'>
					<div className='modal-content container'>
						<div className='modal-header row px-4'>
							<div className='col p-0 m-0'>
								<h5
									className='modal-title'
									id='detailsModalLabel'>
									Delivery #{deliveryDetails?.delivery?.id}
								</h5>
								<p className='p-0 m-0'>
									Created at{' '}
									{DateToString(
										deliveryDetails?.delivery?.created,
									)}
								</p>
								<DeliveryStatus
									statusId={
										deliveryDetails?.delivery
											?.deliveryStatusId
									}
								/>
							</div>
							<div className='col-auto p-0 m-0'>
								<button
									type='button'
									className='close m-0 p-0'
									data-dismiss='modal'
									aria-label='Close'>
									<span aria-hidden='true'>&times;</span>
								</button>
							</div>
						</div>
						<div className='modal-body'>
							<div className='container'>
								<div className='row'>
									<h5 className='color-booshi'>
										<i className='fas fa-map-marker-alt'></i>{' '}
										Origin
									</h5>
								</div>
								<div className='row'>
									<p>
										Name :{' '}
										{deliveryDetails?.origin?.firstName}{' '}
										{deliveryDetails?.origin?.lastName}
									</p>
								</div>
								<div className='row'>
									<p>
										Email: {deliveryDetails?.origin?.email}
									</p>
								</div>
								<div className='row'>
									<p>
										Tel:{' '}
										{deliveryDetails?.origin?.phoneNumber}
									</p>
								</div>
								<div className='row'>
									<p>
										Street:{' '}
										{deliveryDetails?.origin?.street}
									</p>
								</div>
								<div className='row'>
									<p>City: {deliveryDetails?.origin?.city}</p>
								</div>
								<div className='row'>
									<p>
										Zipcode:{' '}
										{deliveryDetails?.origin?.zipCode}
									</p>
								</div>
								<div className='row my-2 justify-content-center'>
									<i className='h3 fas fa-arrow-down'></i>
								</div>
								<div className='row'>
									<h5 className='color-booshi'>
										<i className='fas fa-map-marker-alt'></i>{' '}
										Destination
									</h5>
								</div>
								<div className='row'>
									<p>
										Name:{' '}
										{
											deliveryDetails?.destination
												?.firstName
										}{' '}
										{deliveryDetails?.destination?.lastName}
									</p>
								</div>
								<div className='row'>
									<p>
										Email:{' '}
										{deliveryDetails?.destination?.email}
									</p>
								</div>
								<div className='row'>
									<p>
										Tel:{' '}
										{
											deliveryDetails?.destination
												?.phoneNumber
										}
									</p>
								</div>
								<div className='row'>
									<p>
										Street:{' '}
										{deliveryDetails?.destination?.street}
									</p>
								</div>
								<div className='row'>
									<p>
										City:{' '}
										{deliveryDetails?.destination?.city}
									</p>
								</div>
								<div className='row'>
									<p>
										Zipcode:{' '}
										{deliveryDetails?.destination?.zipCode}
									</p>
								</div>
								{deliveryDetails?.delivery?.notes && (
									<div>
										<div className='row mt-4'>
											<h5 className='color-booshi'>
												<i className='fas fa-pen'></i>{' '}
												Notes
											</h5>
										</div>
										<div className='row'>
											<p
												style={{
													whiteSpace: 'pre-wrap',
												}}>
												{
													deliveryDetails?.delivery
														?.notes
												}
											</p>
										</div>
									</div>
								)}
								{admin && (
									<div>
										<div className='row mt-4'>
											<h5 className='color-booshi'>
												<i className='fas fa-motorcycle'></i>{' '}
												Delivery person
											</h5>
										</div>
										{deliveryPerson ? (
											<div>
												<div className='row'>
													<p>
														ID :{' '}
														{deliveryPerson?.id}
													</p>
												</div>
												<div className='row'>
													<p>
														Name :{' '}
														{
															deliveryPerson?.firstName
														}{' '}
														{
															deliveryPerson?.lastName
														}
													</p>
												</div>
											</div>
										) : (
											<div className='row'>
												<p>Not assigned yet</p>
											</div>
										)}
									</div>
								)}
							</div>
						</div>
						{admin && (
							<div className='modal-footer'>
								<Link
									to={`../../../adminpanel/deliveries/edit/${deliveryDetails?.delivery?.id}`}
									className='link'
									onClick={() => $('.modal').modal('hide')}>
									<button className='btn btn-danger'>
										<i className='fas fa-edit'></i> Edit
										delivery
									</button>
								</Link>
							</div>
						)}
						{deliveryDetails?.delivery?.deliveryStatusId === 1 &&
							!props.deliveryPerson &&
							!admin && (
								<div className='modal-footer'>
									{!props.takeDelivery ? (
										<button
											className='btn btn-danger'
											data-toggle='modal'
											data-target='#deleteModal'
											onClick={deleteDialog}>
											Cancel delivery
										</button>
									) : (
										<button
											className='btn btn-danger'
											onClick={() =>
												takeDelivery(
													deliveryDetails.delivery.id,
												)
											}>
											<i className='fas fa-motorcycle'></i>{' '}
											Take delivery
										</button>
									)}
								</div>
							)}
						{props.deliveryPerson &&
							deliveryDetails?.delivery?.deliveryStatusId ===
								1 && (
								<div className='modal-footer justify-content-center'>
									<button
										className='btn shipped-btn btn-lg col-auto'
										onClick={() => changeDeliveryStatus(2)}>
										Delivery in process
									</button>
								</div>
							)}
						{props.deliveryPerson &&
							deliveryDetails?.delivery?.deliveryStatusId ===
								2 && (
								<div className='modal-footer justify-content-center'>
									<button
										className='btn complete-btn btn-lg col-auto'
										onClick={() => changeDeliveryStatus(3)}>
										Delivery completed
									</button>
								</div>
							)}
					</div>
				</div>
			</div>

			<div className='modal fade' id='deleteModal' tabIndex='-1'>
				<div className='modal-dialog'>
					<div className='modal-content container p-4'>
						<div className='modal-header row px-4'>
							<h4 className='text-center'>
								Are you sure you want to cancel delivery #
								{deliveryDetails?.delivery?.id} ?
							</h4>
						</div>
						<div className='modal-body'>
							<div className='row'>
								<div className='col'>
									<button
										className='btn btn-block btn-lg btn-outline-danger'
										onClick={cancelDelivery}>
										Yes
									</button>
								</div>
								<div className='col'>
									<button
										className='btn btn-block btn-lg btn-outline-secondary'
										data-dismiss='modal'>
										No
									</button>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}

const mapStateToProps = (state) => {
	return {
		pageNum: state.paginator.page,
		user: state.auth.userData,
	};
};

const mapDispatchToProps = (dispatch) => {
	return {
		loadingOn: () => dispatch(loadingOn()),
		loadingOff: () => dispatch(loadingOff()),
	};
};

export default connect(
	mapStateToProps,
	mapDispatchToProps,
)(DeliveryDetailsModal);
