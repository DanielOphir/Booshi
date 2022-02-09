import React, { Component } from 'react';
import PrimaryButton from '../../components/design/buttons/PrimaryButton';
import * as $ from 'jquery';
import { api } from '../../utils/helpers/AxiosHelper';
import {
	SetLocalToken,
	SetSessionToken,
} from '../../utils/helpers/TokenHelper';
import { useNavigate } from 'react-router-dom';
import * as loadingActions from '../../redux/loading/loadingActions';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';

class SignInClass extends Component {
	state = {
		userName: '',
		password: '',
		generalError: '',
		rememberMe: true,
	};

	SignInFunction = () => {
		this.props.loadingOn();
		api.post('/api/auth/login', JSON.stringify(this.state))
			.then((response) => {
				if (response?.status === 200) {
					const token = response.data.token;
					if (this.state.rememberMe) {
						SetLocalToken(token);
					} else {
						SetSessionToken(token);
					}
				}
				this.props.navigate('/');
				window.location.reload();
				window.addEventListener('load', () => {
					this.props.loadingOff();
				});
			})
			.catch((error) => {
				this.setState({
					...this.state,
					generalError: `* ${error?.response?.data?.message}`,
				});
				this.props.loadingOff();
			});
	};

	OnInputChange = (event) => {
		const eventId = event.target.id;

		$(`#${eventId}`).hasClass('is-invalid') &&
			$(`#${eventId}`).removeClass('is-invalid');

		eventId === 'userName'
			? this.setState({ userName: event.target.value })
			: eventId === 'password' &&
			  this.setState({ password: event.target.value });
	};

	onFormSubmit = (event) => {
		event.preventDefault();

		let isFormValid = true;

		for (let i = 0; i < event.target.length; i++) {
			if (!event.target[i].checkValidity()) {
				event.target[i].classList.add('is-invalid');
				isFormValid = false;
			}
		}

		if (!isFormValid) return;

		this.SignInFunction();
	};

	render() {
		return (
			<div className='container form-max-width p-0'>
				<form
					className='p-5 my-5'
					id='sign-in-form'
					noValidate
					onSubmit={this.onFormSubmit}>
					<div className='px-5 py-4 shadow-lg'>
						<h3 className='mb-3'>
							<i className='fas fa-user h4'></i> Sign in
						</h3>
						<div className='form-group mt-2'>
							<label
								htmlFor='userName'
								className='form-label md-text-big'>
								<span className='text-danger'>*</span> Username
							</label>
							<input
								id='userName'
								className='form-control'
								onChange={this.OnInputChange}
								required
								autoComplete='off'
							/>
							<small className='invalid-feedback'>
								* Username is required
							</small>
						</div>
						<div className='form-group mt-4'>
							<label
								htmlFor='password'
								className='form-label md-text-big'>
								<span className='text-danger'>*</span> Password
							</label>
							<input
								id='password'
								type='password'
								className='form-control'
								onChange={this.OnInputChange}
								required
								autoComplete='off'
							/>
							<small className='invalid-feedback'>
								* Password is required
							</small>
						</div>
						<div className='text-danger'>
							{this.state.generalError}
						</div>
						<div className='row justify-content-center'>
							<div className='form-group col-12 m-0 ml-5 mt-1'>
								<input
									type='checkbox'
									name='rememberMe'
									id='rememberMe'
									className='form-check-input'
									defaultChecked={true}
									onChange={() =>
										this.setState({
											...this.state,
											rememberMe: !this.state.rememberMe,
										})
									}
								/>
								<label htmlFor='rememberMe' className=''>
									Remember me
								</label>
							</div>
							<div className='text-center m-0'>
								<Link to='/forgot-password'>
									Forgot your password?
								</Link>
							</div>
						</div>
						<div
							className='text-center mt-2'
							style={{ fontSize: '14px' }}>
							<Link to='/register'>Create account</Link> and join
							us
						</div>
						<div className='row justify-content-center text-center mt-3'>
							<div className='text-big w-10'>
								<PrimaryButton
									type='submit'
									className='btn-block'>
									Sign in
								</PrimaryButton>
							</div>
						</div>
					</div>
				</form>
			</div>
		);
	}
}

function SignIn(props) {
	let navigate = useNavigate();
	return <SignInClass {...props} navigate={navigate} />;
}

const stateDispatchToProps = (dispatch) => {
	return {
		loadingOn: () => dispatch(loadingActions.loadingOn()),
		loadingOff: () => dispatch(loadingActions.loadingOff()),
	};
};

export default connect(null, stateDispatchToProps)(SignIn);
