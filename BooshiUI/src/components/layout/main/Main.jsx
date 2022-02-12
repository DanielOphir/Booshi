import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { Navigate, Route, Routes } from 'react-router-dom';
import Home from '../../../views/home/Home';
import Register from '../../../views/register/Register';
import SignIn from '../../../views/signin/SignIn';
import AdminPanel from '../../../views/adminpanel/AdminPanel';
import DeliveryPanel from '../../../views/deliverypanel/DeliveryPanel';
import UserPanel from '../../../views/userpanel/UserPanel';
import Settings from '../../../views/settings/Settings';
import ChangePassword from '../../../views/settings/ChangePassword';
import ForgotPassword from '../../../views/forgotpassword/ForgotPassword';
import Unauthorized from '../../../views/errors/Unauthorized/Unauthorized';

class Main extends PureComponent {
	constructor(props) {
		super(props);
	}

	render() {
		return (
			<Routes>
				<Route path='/' element={<Home />} />
				<Route
					path='register'
					element={!this.props.user ? <Register /> : <Unauthorized />}
				/>
				<Route
					path='signin'
					element={!this.props.user ? <SignIn /> : <Unauthorized />}
				/>
				<Route
					path='forgot-password'
					element={
						!this.props.user ? <ForgotPassword /> : <Unauthorized />
					}
				/>
				<Route
					path='settings'
					element={this.props.user ? <Settings /> : <Unauthorized />}
				/>
				<Route
					path='settings/change-password'
					element={
						this.props.user ? <ChangePassword /> : <Unauthorized />
					}
				/>
				<Route
					path='adminpanel/*'
					element={
						this.props.user?.roleId === 1 ? (
							<AdminPanel />
						) : (
							<Unauthorized />
						)
					}
				/>
				<Route
					path='deliverypanel/*'
					element={
						this.props.user?.roleId === 2 ? (
							<DeliveryPanel />
						) : (
							<Unauthorized />
						)
					}
				/>
				<Route
					path='mypanel/*'
					element={
						this.props.user?.roleId === 3 ? (
							<UserPanel />
						) : (
							<Unauthorized />
						)
					}
				/>
				<Route path='*' element={<Navigate to='/' />} />
			</Routes>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		user: state.auth.userData,
	};
};

export default connect(mapStateToProps)(Main);
