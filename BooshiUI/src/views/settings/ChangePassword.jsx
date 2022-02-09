import { yupResolver } from '@hookform/resolvers/yup';
import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { connect } from 'react-redux';
import * as yup from 'yup';
import PrimaryButton from '../../components/design/buttons/PrimaryButton';
import { loadingOff, loadingOn } from '../../redux/loading/loadingActions';
import { api } from '../../utils/helpers/AxiosHelper';
import * as $ from 'jquery';
import { Link } from 'react-router-dom';

const ChangePassword = () => {
	const [showPassword, setShowPassword] = useState(false);
	const [showConfirmPassword, setShowConfirmPassword] = useState(false);

	const schema = yup.object().shape({
		password: yup
			.string()
			.required('* This field is required')
			.min(8, `* Password must be at least ${8} characters long`),
		confirmPassword: yup
			.string()
			.required('* This field is required')
			.oneOf([yup.ref('password'), null], '* Passwords must match'),
	});
	const {
		register,
		handleSubmit,
		formState: { errors, dirtyFields },
	} = useForm({
		resolver: yupResolver(schema),
	});

	const submitForm = (data) => {
		!$('#success-alert').hasClass('d-none') &&
			$('#success-alert').addClass('d-none');
		!$('#error-alert').hasClass('d-none') &&
			$('#error-alert').addClass('d-none');
		api.post('api/auth/change-password', JSON.stringify(data.password))
			.then((response) => {
				$('#success-alert').removeClass('d-none');
			})
			.catch((error) => {
				$('#error-alert').removeClass('d-none');
			});
	};

	return (
		<div className='pt-3'>
			{' '}
			<div className={'container p-5 register-form-width shadow-lg mt-5'}>
				<div
					id='success-alert'
					className='row justify-content-center align-items-center mb-4 d-none'>
					<div className='px-4 col-auto'>
						<div className='alert alert-success m-0'>
							<i className='fas fa-check-circle'></i> The password
							changed successfully.
						</div>
					</div>
				</div>
				<div
					id='error-alert'
					className='row justify-content-center align-items-center mb-4 d-none'>
					<div className='px-4 col-auto'>
						<div className='alert alert-danger m-0'>
							<i className='fas fa-exclamation-circle'></i> Error
							occured while changing password.
						</div>
					</div>
				</div>
				<form onSubmit={handleSubmit(submitForm)} noValidate>
					<h3 className='text-center'>
						<i className='fas fa-key h4'></i> Change password
					</h3>

					<div className='row px-4 justify-content-center align-items-center mt-4'>
						<div className='form-group col-12 col-md-8 col-xl-6'>
							<label htmlFor='password' className=''>
								<span className='text-danger'>* </span> New
								password
							</label>
							<input
								type={showPassword ? 'text' : 'password'}
								id='password'
								name='password'
								className={
									errors && errors['password']
										? 'invalid-form-control form-control password'
										: 'form-control password'
								}
								placeholder='Enter new password'
								{...register('password')}
								autoComplete='off'
							/>
							<small
								id={'passwordFeedback'}
								className='text-danger'>
								{' '}
								{errors['password']?.message}
							</small>
							{!showPassword ? (
								<i
									className='fas fa-eye eye'
									onClick={() => setShowPassword(true)}></i>
							) : (
								<i
									className='fas fa-eye-slash eye'
									onClick={() => setShowPassword(false)}></i>
							)}
						</div>
					</div>
					<div className='row px-4 justify-content-center align-items-center mt-2'>
						<div className='form-group col-12 col-md-8 col-xl-6'>
							<label htmlFor='confirmPassword' className=''>
								<span className='text-danger'>* </span>Confirm
								password
							</label>
							<input
								type={showConfirmPassword ? 'text' : 'password'}
								id='confirmPassword'
								className={
									errors && errors['confirmPassword']
										? 'invalid-form-control form-control password'
										: 'form-control password'
								}
								placeholder='Confirm new password'
								{...register('confirmPassword')}
								autoComplete='off'></input>
							<small
								id={'confirmPasswordFeedback'}
								className='text-danger'>
								{' '}
								{errors['confirmPassword']?.message}
							</small>
							{!showConfirmPassword ? (
								<i
									className='fas fa-eye eye'
									onClick={() =>
										setShowConfirmPassword(true)
									}></i>
							) : (
								<i
									className='fas fa-eye-slash eye'
									onClick={() =>
										setShowConfirmPassword(false)
									}></i>
							)}
						</div>
					</div>
					<div className='row px-4 justify-content-center align-items-center mt-2'>
						<div className='col-auto p-0 m-0 mx-2'>
							<PrimaryButton
								style={{ fontSize: '20px' }}
								className='btn-block py-1'>
								<i className='fas fa-save px-1'></i> Save
								changes
							</PrimaryButton>
						</div>
					</div>
				</form>
			</div>
			<div className='container p-3 register-form-width'>
				<div className='row mt-2'>
					<div className='col-auto ml-3 p-0 m-0'>
						<Link to='/settings' className='link'>
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
		</div>
	);
};

const mapDispatchToProps = (dispatch) => ({
	loadingOn: () => dispatch(loadingOn()),
	loadingOff: () => dispatch(loadingOff()),
});

export default connect(null, mapDispatchToProps)(ChangePassword);
