import React, { Component } from 'react';
import Sidenav from '../../components/layout/main/Sidenav/Sidenav';
import { Routes, Route, Navigate } from 'react-router-dom';
import Users from './Users/Users';
import Deliveries from './Deliveries/Deliveries';
import UserDeliveries from './Users/UserDeliveries';
import Register from '../register/Register';
import RegisterTable from '../../components/register/RegisterTable';
import UpdateUser from './Users/UpdateUser';
import EditDelivery from './Deliveries/EditDelivery';
import DeliveriesTable from '../userpanel/DeliveriesTable';
import Analytics from './Analytics/Analytics';
import DeliveryPerosns from './DeliveryPersons/DeliveryPersons';
import DeliveryPersonDeliveries from './DeliveryPersons/DeliveryPersonDeliveries';
import Reports from './Reports/Reports';
import NewDeliveryPerson from './DeliveryPersons/NewDeliveryPerson';
class AdminPanel extends Component {
	constructor(props) {
		super(props);

		this.state = {};
	}

	render() {
		return (
			<div className='container-fluid'>
				<div className='row'>
					<div className='col-2 col-md-auto p-0 m-0'>
						<Sidenav></Sidenav>
					</div>
					<div className='col-10 col-md p-0'>
						<Routes>
							<Route path='/' element={<Navigate to='users' />} />
							<Route
								path='users'
								element={
									<Users
										apiUrl='/api/users/page/'
										searchUrl='/api/users/'
									/>
								}
							/>
							<Route
								path='users/:id'
								element={
									<UserDeliveries apiUrl='/api/deliveries/' />
								}
							/>
							<Route
								path='users/edit/:id'
								element={<UpdateUser />}
							/>
							<Route
								path='deliverypersons'
								element={<DeliveryPerosns />}
							/>
							<Route
								path='deliverypersons/new'
								element={<NewDeliveryPerson />}
							/>
							<Route
								path='deliverypersons/:id'
								element={<DeliveryPersonDeliveries />}
							/>
							<Route
								path='deliverypersons/*'
								element={
									<Navigate
										to='/adminpanel/deliverypersons'
										replace
									/>
								}
							/>
							<Route
								path='deliveries/*'
								element={<Deliveries />}
							/>
							<Route
								path='deliveries/edit/:id'
								element={<EditDelivery />}
							/>
							<Route path='analytics' element={<Analytics />} />
							<Route path='reports' element={<Reports />} />
							<Route path='*' element={<Navigate to='/' />} />
						</Routes>
					</div>
				</div>
			</div>
		);
	}
}

export default AdminPanel;
