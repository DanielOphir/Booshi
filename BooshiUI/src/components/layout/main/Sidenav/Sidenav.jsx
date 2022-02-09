import React, { Component } from 'react';
import './Sidenav.css';
import { NavLink } from 'react-router-dom';

class Sidenav extends Component {
	constructor(props) {
		super(props);

		this.state = {};
	}

	render() {
		return (
			<nav className='bg-booshi sidenav py-4'>
				<div className='container-fluid'>
					<NavLink to='users' className='row p-2 mt-5 sidenav-item'>
						<div className='col-lg-auto col-md-12 m-0 text-center p-0 mx-2 mx-md-0 mr-lg-2'>
							<i className='fas fa-user text-white'></i>
						</div>
						<div className='col-lg col-md-12 m-0 lg-text-left d-none d-md-flex p-0'>
							<div className='text-white w-100'>Users</div>
						</div>
					</NavLink>
					<NavLink
						to='deliverypersons'
						className='row p-2 mt-5 sidenav-item align-items-center'>
						<div className='col-lg-auto col-md-12 m-0 text-center p-0 mx-2 mx-md-0 mr-lg-2'>
							<i className='fas fa-motorcycle text-white'></i>
						</div>
						<div className='col-lg col-md-12 m-0 lg-text-left d-none d-md-flex p-0'>
							<div className='text-white w-100'>
								Delivery<br></br>persons
							</div>
						</div>
					</NavLink>
					<NavLink
						to='deliveries'
						className='row p-2 mt-5 sidenav-item'>
						<div className='col-lg col-md-12 m-0 text-center p-0 mx-2 mx-md-0 mr-lg-2'>
							<i className='fas fa-box-open text-white text-center'></i>
						</div>
						<div className='col-lg col-md-12 m-0 text-center d-none d-md-flex p-0'>
							<div className='text-white w-100 '>Deliveries</div>
						</div>
					</NavLink>
					<NavLink to='reports' className='row p-2 mt-5 sidenav-item'>
						<div className='col-lg-auto col-md-12 m-0 text-center p-0 mx-2 mx-md-0 mr-lg-2'>
							<i className='fas fa-chart-bar text-white'></i>
						</div>
						<div className='col-lg col-md-12 m-0 lg-text-left d-none d-md-flex p-0'>
							<div className='text-white w-100'>Reports</div>
						</div>
					</NavLink>
					<NavLink
						to='analytics'
						className='row p-2 mt-5 sidenav-item'>
						<div className='col-lg-auto col-md-12 m-0 text-center p-0 mx-2 mx-md-0 mr-lg-2'>
							<i className='fas fa-chart-line text-white'></i>
						</div>
						<div className='col-lg col-md-12 m-0 lg-text-left d-none d-md-flex p-0'>
							<div className='text-white w-100'>Analitycs</div>
						</div>
					</NavLink>
				</div>
			</nav>
		);
	}
}

export default Sidenav;
