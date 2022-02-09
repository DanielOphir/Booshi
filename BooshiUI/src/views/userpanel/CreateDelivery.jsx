import React, {useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { connect } from 'react-redux';
import PrimaryButton from '../../components/design/buttons/PrimaryButton';
import { SetForm } from '../../redux/form/formActions';
import { loadingOff, loadingOn } from '../../redux/loading/loadingActions';
import { api } from '../../utils/helpers/AxiosHelper';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { rows } from './NewDeliveryForm';
import DeliveryCreated from './DeliveryCreated';
import { useNavigate } from 'react-router-dom';
import { DeliveryStatuses } from '../../utils/constants/DeliveryStatuses';

const CreateDelivery = (props) => {
	const requiredError = 'This field is required.';
	const schemaBase = yup.string().required(requiredError);
	const schema = yup.object().shape({
		'origin-street': schemaBase,
		'origin-city': schemaBase,
		'origin-zipCode': schemaBase,
		'destination-firstName': schemaBase,
		'destination-lastName': schemaBase,
		'destination-email': schemaBase.email('The email is not valid.'),
		'destination-phoneNumber': schemaBase.matches(
			/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/,
			'The phone number is not valid.',
		),
		'destination-street': schemaBase,
		'destination-city': schemaBase,
		'destination-zipCode': schemaBase,
	});

	const navigate = useNavigate();

	const [isCreated, setIsCreated] = useState({
		created: false,
	});

	const {
		register,
		handleSubmit,
		reset,
		formState: { errors, dirtyFields },
	} = useForm({
		resolver: yupResolver(schema),
	});

	useEffect(() => {
		props.loadingOn();
		if (props.id) {
			api.get('/api/deliveries/' + props.id)
				.then((response) => {
					if (response.status === 200) {
						const delivery = response.data;
						reset({
							'origin-firstName': delivery?.origin?.firstName,
							'origin-lastName': delivery?.origin?.lastName,
							'origin-email': delivery?.origin?.email,
							'origin-phoneNumber': delivery?.origin?.phoneNumber,
							'origin-street': delivery?.origin?.street,
							'origin-city': delivery?.origin?.city,
							'origin-zipCode': delivery?.origin?.zipCode,
							'destination-firstName':
								delivery?.destination?.firstName,
							'destination-lastName':
								delivery?.destination?.lastName,
							'destination-email': delivery?.destination?.email,
							'destination-phoneNumber':
								delivery?.destination?.phoneNumber,
							'destination-street': delivery?.destination?.street,
							'destination-city': delivery?.destination?.city,
							'destination-zipCode':
								delivery?.destination?.zipCode,
							deliveryStatusId:
								delivery?.delivery?.deliveryStatusId,
							notes: delivery?.delivery?.notes,
						});
						props.loadingOff();
					}
				})
				.catch((error) => {
					if (error.response.status === 404) {
						props.loadingOff();
						navigate('../../adminpanel/deliveries');
					}
				});
		} else {
			api.get('api/auth/fulluser').then((response) => {
				const user = response.data;
				reset({
					'origin-firstName': user.firstName,
					'origin-lastName': user.lastName,
					'origin-email': user.email,
					'origin-phoneNumber': user.phoneNumber,
					'origin-street': user.street,
					'origin-city': user.city,
					'origin-zipCode': user.zipCode,
				});
				props.loadingOff();
			});
		}
	}, []);

	const onSubmit = (formData) => {
		const data = {
			delivery: {},
			origin: {},
			destination: {},
		};
		data['delivery']['userId'] = props.user.id;
		data['delivery']['notes'] = formData.notes;
		Object.keys(formData).forEach((key) => {
			if (key.includes('origin')) {
				data['origin'][key.toString().replace('origin-', '')] =
					formData[key];
			}
			if (key.includes('destination')) {
				data['destination'][
					key.toString().replace('destination-', '')
				] = formData[key];
			}
		});
		if (props.mode === 'edit') {
			data['delivery']['deliveryStatusId'] = formData.deliveryStatusId;
			data['delivery']['Id'] = props.id;
			data['origin']['deliveryId'] = props.id;
			data['destination']['deliveryId'] = props.id;
			api.patch('api/deliveries', JSON.stringify(data)).then((res) => {
				if (res.status === 200) {
					navigate('../../adminpanel/deliveries');
				}
			});
			return;
		}
		api.post('api/deliveries', JSON.stringify(data))
			.then((res) => {
				console.log(res.data);
				setIsCreated({
					created: true,
					orderNumber: res.data.delivery.id,
				});
			})
			.catch((error) => console.log(error.response));
	};

	const DeliveryForm = (props) => {
		const CreateInputId = (name) => {
			return props.origin ? `origin-${name}` : `destination-${name}`;
		};

		return (
			<div className='container-lg'>
				{rows.map((row, i) => {
					return (
						<div
							key={i}
							className='row px-0 px-md-5 px-lg-0 px-xl-4'>
							{row.formControls.map((formControl, i) => {
								return (
									<div
										key={i}
										className={
											'form-group form-float' +
											formControl.col
										}>
										<label
											htmlFor={CreateInputId(
												formControl.id,
											)}>
											{formControl.label}
										</label>
										<input
											id={CreateInputId(formControl.id)}
											name={CreateInputId(formControl.id)}
											{...register(
												CreateInputId(formControl.id),
											)}
											className={
												errors[
													CreateInputId(
														formControl.id,
													)
												]
													? 'is-invalid form-control'
													: 'form-control'
											}
											disabled={
												props.origin &&
												formControl.disabled &&
												!props.id
											}
										/>
										<small className='invalid-feedback'>
											{
												errors[
													CreateInputId(
														formControl.id,
													)
												]?.message
											}
										</small>
									</div>
								);
							})}
						</div>
					);
				})}
			</div>
		);
	};

	return (
		<div>
			{!isCreated.created ? (
				<div className='mx-1 mx-sm-2 mx-lg-4 mx-xl-5 px-3 py-3 mt-3 shadow-lg'>
					<form
						className='row justify-content-around'
						onSubmit={handleSubmit(onSubmit)}
						noValidate>
						<div className='col-12 col-lg-6 container p-4 origin-container'>
							<div className='row align-items-center justify-content-center'>
								<div className='col-12 text-center h4 mb-4 text-booshi'>
									<i className='fas fa-map-marker-alt'></i>{' '}
									Origin
								</div>
							</div>
							<div className='row'>
								<div className='col-12'>
									{DeliveryForm({
										...props,
										origin: true,
									})}
								</div>
							</div>
						</div>
						<div className='col-12 col-lg-6 container p-4 destination-container'>
							<div className='row align-items-center justify-content-center'>
								<div className='col-12 text-center h4 mb-4 text-booshi'>
									<i className='fas fa-map-marker-alt'></i>{' '}
									Destination
								</div>
							</div>
							<div className='row'>
								<div className='col-12'>
									{DeliveryForm({
										...props,
									})}
								</div>
							</div>
						</div>
						<div className='col-12 p-4'>
							<div className='row px-0 px-sm-5 justify-content-center align-items-start'>
								<div className='form-group col-12 col-sm-12 col-md-8 col-lg-6 col-xl-4'>
									<label htmlFor=''>Notes :</label>
									<textarea
										className='form-control'
										id='notes'
										name='notes'
										{...register('notes')}
									/>
								</div>
								{props.mode === 'edit' && (
									<div className='form-group col-12 col-sm-12 col-md-8 col-lg-6 col-xl-2'>
										<label htmlFor=''>
											Delivery status :
										</label>
										<select
											className='form-control'
											id='deliveryStatusId'
											name='deliveryStatusId'
											{...register('deliveryStatusId')}>
											{Object.keys(DeliveryStatuses).map(
												(statusId) => (
													<option
														key={statusId}
														value={statusId}>
														{
															DeliveryStatuses[
																statusId
															]
														}
													</option>
												),
											)}
										</select>
									</div>
								)}
							</div>
							<div className='row justify-content-center align-items-center'>
								<div className='col-auto text-big'>
									<PrimaryButton
										disabled={
											props.id &&
											Object.keys(dirtyFields).length ===
												0
										}
										className='btn-block'>
										{props.mode !== 'edit' ? (
											<i className='fas fa-plus-circle'></i>
										) : (
											<i className='fas fa-save'></i>
										)}
										{props.mode !== 'edit'
											? ' Create'
											: ' Save changes'}
									</PrimaryButton>
								</div>
							</div>
						</div>
					</form>
				</div>
			) : (
				<DeliveryCreated orderNumber={isCreated.orderNumber} />
			)}
		</div>
	);
};

const mapDispatchToProps = (dispatch) => {
	return {
		loadingOn: () => dispatch(loadingOn()),
		loadingOff: () => dispatch(loadingOff()),
		setForm: (data) => dispatch(SetForm(data)),
	};
};

const mapStateToProps = (state) => {
	return {
		loading: state.loading.loading,
		user: state.auth.userData,
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(CreateDelivery);
