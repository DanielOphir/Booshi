import React, { useState, useEffect } from 'react';
import PrimaryButton from '../design/buttons/PrimaryButton';
import { rows as registerRows, schema as registerSchema } from './RegisterForm';
import { rows as updateRows, schema as updateSchema } from './UpdateForm';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as $ from 'jquery';
import { api } from '../../utils/helpers/AxiosHelper';
import { SetLocalToken } from '../../utils/helpers/TokenHelper';
import { connect } from 'react-redux';
import * as loadingActions from '../../redux/loading/loadingActions';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';

const RegisterTable = (props) => {
	const navigate = useNavigate();
	const [generalError, setGeneralError] = useState('');
	const [id, setId] = useState(undefined);

	// Get the rules schema wether it's register or update
	let schema =
		props.mode === 'update' || props.mode === 'settings'
			? updateSchema
			: registerSchema;

	const {
		register,
		reset,
		handleSubmit,
		formState: { errors, dirtyFields },
	} = useForm({
		resolver: yupResolver(schema),
	});

	useEffect(() => {
		if (props.mode === 'update' || props.mode === 'settings') {
			props.loadingOn();
			const url =
				props.mode === 'update'
					? `api/users/${props.id}`
					: 'api/auth/fulluser';
			api.get(url)
				.then((response) => {
					const user = response.data;
					setId(user?.id);
					// If its update form, automaticly fill the form with the user data
					reset({
						userName: user.userName,
						firstName: user.firstName,
						lastName: user.lastName,
						email: user.email,
						phoneNumber: user.phoneNumber,
						street: user.street,
						city: user.city,
						zipCode: user.zipCode,
					});
					props.loadingOff();
				})
				.catch((error) => {
					if (
						error?.response?.status === 400 ||
						error?.response?.status === 404
					) {
						loadingActions.loadingOff();
						navigate('..');
					}
				});
		}
	}, []);

	const onSubmit = (data) => {
		$('#success-alert').addClass('d-none');
		$('#error-alert').addClass('d-none');
		if (props.mode === 'update' || props.mode === 'settings') {
			console.log(id);
			data['id'] = id;
			props.loadingOn();
			api.patch('/api/users/update', data)
				.then((response) => {
					if (response.status === 200) {
						if (props.mode === 'update') {
							if (response.data.roleId === 2) {
								navigate('/adminpanel/deliverypersons');
							} else {
								navigate('..');
							}
						}
						if (props.mode === 'settings') {
							$('#success-alert').removeClass('d-none');
						}
						props.loadingOff();
					}
				})
				.catch((error) => {
					if (error.response.data.type === 'username') {
						$('#userName').addClass('is-invalid');
						$('#feedback-userName').text(
							error.response.data.message,
						);
					} else if (error.response.data.type === 'email') {
						$('#email').addClass('is-invalid');
						$('#feedback-email').text(error.response.data.message);
					} else {
						if (props.mode === 'update') {
							error.response &&
								setGeneralError(error.response.data.message);
						}
						if (props.mode === 'settings') {
							$('#error-alert').removeClass('d-none');
						}
					}
					props.loadingOff();
				});
		} else {
			props.loadingOn();
			api.post('/api/auth/register', data)
				.then((response) => {
					if (response.status === 201 && !props.adminpanel) {
						api.post(
							'/api/auth/login',
							JSON.stringify({
								userName: data.userName,
								password: data.password,
							}),
						).then((response) => {
							console.log(response);
							if (response.status === 200) {
								SetLocalToken(response.data.token);
							}
							navigate('/');
							window.location.reload();
							props.loadingOff();
						});
					}
					if (response.status === 201) {
						api.post('api/deliveryperson', response.data.id)
							.then((response) => {
								if (response.status === 201) {
									navigate('/adminpanel/deliverypersons');
									props.loadingOff();
								}
							})
							.catch((error) => {
								setGeneralError(error.message);
								props.loadingOff();
							});
					}
				})
				.catch((error) => {
					if (error.response.data.type === 'username') {
						$('#userName').addClass('is-invalid');
						$('#feedback-userName').text(
							error.response.data.message,
						);
					} else if (error.response.data.type === 'email') {
						$('#email').addClass('is-invalid');
						$('#feedback-email').text(error.response.data.message);
					} else {
						error.response &&
							setGeneralError(error.response.data.message);
					}
					props.loadingOff();
				});
		}
	};

	// Get the input rows wether it's register or update
	let rows =
		props.mode === 'update' || props.mode === 'settings'
			? updateRows
			: registerRows;
	return (
		<div>
			<div
				className={
					'container px-5 py-4 shadow-lg register-form-width ' +
					props.className
				}>
				{props.mode === 'settings' && (
					<div id='success-alert' className='px-4 mb-5 d-none'>
						<div className='alert alert-success'>
							<i className='fas fa-check-circle'></i> The changes
							was saved successfully.
						</div>
					</div>
				)}
				{props.mode === 'settings' && (
					<div id='error-alert' className='px-4 mb-5 d-none'>
						<div className='alert alert-danger'>
							<i className='fas fa-exclamation-circle'></i> Error
							occured while saving changes.
						</div>
					</div>
				)}
				{props.mode === 'settings' && (
					<h3 className='row px-5'>
						<i className='fas fa-user-cog h4 mr-2'></i> Settings
					</h3>
				)}
				<form onSubmit={handleSubmit(onSubmit)} noValidate>
					{rows.map((row, i) => {
						return (
							<div key={i} className='row px-4'>
								{row.formControls.map((formControl, i) => {
									return (
										<div
											key={i}
											className={
												'form-group ' + formControl.col
											}>
											<label htmlFor={formControl.id}>
												{formControl.required && (
													<span className='text-danger'>
														*
													</span>
												)}{' '}
												{formControl.label}
											</label>
											<input
												id={formControl.id}
												{...register(formControl.id)}
												name={formControl.id}
												type={
													formControl.type
														? formControl.type
														: 'text'
												}
												disabled={
													props.mode === 'settings' &&
													formControl.disabled
												}
												className={
													errors[formControl.id]
														? 'is-invalid form-control'
														: 'form-control'
												}></input>
											<small
												id={
													'feedback-' + formControl.id
												}
												className='invalid-feedback'>
												{
													errors[formControl.id]
														?.message
												}
											</small>
										</div>
									);
								})}
							</div>
						);
					})}
					{props.mode === 'settings' && (
						<div className='row px-4'>
							<Link
								to='change-password'
								className='link col-12 col-sm-auto'>
								<button className='col-12 col-sm-auto btn btn-dark'>
									Change password
								</button>
							</Link>
						</div>
					)}
					<p className='text-center text-danger'>{generalError}</p>
					<div className='row px-4 mt-4 align-items-center justify-content-center'>
						<div className='col-auto text-big'>
							<PrimaryButton
								className='btn-block'
								disabled={
									(props.mode === 'update' ||
										props.mode === 'settings') &&
									Object.keys(dirtyFields).length === 0
								}>
								{props.mode === 'update' ||
								props.mode === 'settings' ? (
									<div>
										<i className='fas fa-save'></i> Save
										changes
									</div>
								) : (
									'Register'
								)}
							</PrimaryButton>
						</div>
					</div>
				</form>
			</div>
			{props.mode === 'settings' && (
				<div className='container p-3 register-form-width'>
					<div className='row mt-2'>
						<div className='col-auto ml-3 p-0 m-0'>
							<Link to='/' className='link'>
								<PrimaryButton
									style={{ fontSize: '20px' }}
									className='btn-block py-1'>
									<i className='fas fa-chevron-left px-1'></i>{' '}
									Back
								</PrimaryButton>
							</Link>
						</div>
					</div>
				</div>
			)}
		</div>
	);
};

const stateDispatchToProps = (dispatch) => {
	return {
		loadingOn: () => dispatch(loadingActions.loadingOn()),
		loadingOff: () => dispatch(loadingActions.loadingOff()),
	};
};

export default connect(null, stateDispatchToProps)(RegisterTable);
