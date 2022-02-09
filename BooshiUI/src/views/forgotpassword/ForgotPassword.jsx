import React, { useState } from 'react';
import PrimaryButton from '../../components/design/buttons/PrimaryButton';
import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { Link } from 'react-router-dom';
import { api } from '../../utils/helpers/AxiosHelper';
import { loadingOff, loadingOn } from '../../redux/loading/loadingActions';
import { connect } from 'react-redux';

const ForgotPassword = (props) => {
	const [mode, setMode] = useState('in-proccess');

	const schema = yup.object().shape({
		userName: yup.string().required('* This field is required'),
	});
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm({
		resolver: yupResolver(schema),
	});

	const submitForm = (data) => {
		props.loadingOn();
		api.post('api/auth/forgot-password', JSON.stringify(data.userName))
			.then((response) => {
				if (response.status === 200) {
					setMode('success');
				}
				props.loadingOff();
			})
			.catch((error) => {
				if (error.response.status === 400) {
					setMode('failed');
					props.loadingOff();
				}
			});
	};

	return (
		<div>
			<div className='container form-max-width p-0 shadow-lg'>
				{mode === 'in-proccess' && (
					<form
						className='p-5 my-5'
						noValidate
						onSubmit={handleSubmit(submitForm)}>
						<div className='row'>
							<div className='col-12 text-center h4'>
								Forgot your password?
							</div>
						</div>
						<div className='row align-items-center justify-content-center form-group mx-5 mt-4'>
							<label
								htmlFor='userName'
								className='form-label md-text-big col-12 p-0'>
								<span className='text-danger'>*</span> Username
							</label>
							<input
								id='userName'
								name='userName'
								className={
									errors && errors['userName']
										? 'is-invalid form-control col-12'
										: 'form-control col-12'
								}
								autoComplete='off'
								placeholder='Enter your username'
								{...register('userName')}
							/>
							<small className='invalid-feedback'>
								{errors ? errors['userName']?.message : null}
							</small>
						</div>
						<div className='row justify-content-center mt-4'>
							<PrimaryButton
								className='col-auto'
								style={{ fontSize: '18px' }}>
								Reset password
							</PrimaryButton>
						</div>
					</form>
				)}
				{mode === 'success' && (
					<div className='p-5 my-5'>
						<div className='alert alert-success'>
							<i className='fas fa-check-circle'></i> Your
							temporary password was sent to your email.
						</div>
						<Link className='link' to='/signin'>
							<PrimaryButton className='col-auto'>
								<i className='fas fa-chevron-left'></i> Back
							</PrimaryButton>
						</Link>
					</div>
				)}
				{mode === 'failed' && (
					<div className='p-5 my-5'>
						<div className='alert alert-danger'>
							<i className='fas fa-exclamation-circle'></i>{' '}
							Something went wrong, please try again.
						</div>
						<Link className='link' to='/signin'>
							<PrimaryButton className='col-auto'>
								<i className='fas fa-chevron-left'></i> Back
							</PrimaryButton>
						</Link>
					</div>
				)}
			</div>
		</div>
	);
};

const mapDispatchToProps = (dispatch) => {
	return {
		loadingOn: () => dispatch(loadingOn()),
		loadingOff: () => dispatch(loadingOff()),
	};
};

export default connect(null, mapDispatchToProps)(ForgotPassword);
