import { NavLink, Routes, Route, Navigate } from 'react-router-dom';
import * as FilteredDeliveries from './DeliveriesFiltered/AllDeliveries';

const Deliveries = () => {

	return (
		<div>
			<div className='container mt-2'>
				<ul className='nav nav-tabs' id='myTab' role='tablist'>
					<li className='nav-item' role='presentation'>
						<NavLink
							to={''}
							className='nav-link'
							id='all-tab'
							data-toggle='tab'
							role='tab'
							end>
							All
						</NavLink>
					</li>
					<li className='nav-item' role='presentation'>
						<NavLink
							to={'new'}
							className='nav-link'
							id='new-tab'
							data-toggle='tab'
							role='tab'>
							New
						</NavLink>
					</li>
					<li className='nav-item' role='presentation'>
						<NavLink
							to={'pending'}
							className='nav-link'
							id='pending-tab'
							data-toggle='tab'
							role='tab'
							end>
							Pending
						</NavLink>
					</li>
					<li className='nav-item' role='presentation'>
						<NavLink
							to={'shipped'}
							className='nav-link'
							id='shipped-tab'
							data-toggle='tab'
							role='tab'>
							Shipped
						</NavLink>
					</li>
					<li className='nav-item' role='presentation'>
						<NavLink
							to={'completed'}
							className='nav-link'
							id='completed-tab'
							data-toggle='tab'
							role='tab'>
							Completed
						</NavLink>
					</li>
					<li className='nav-item' role='presentation'>
						<NavLink
							to={'cancelled'}
							className='nav-link'
							id='cancelled-tab'
							data-toggle='tab'
							role='tab'>
							Cancelled
						</NavLink>
					</li>
				</ul>
			</div>
			<Routes>
				<Route
					path='/'
					element={<FilteredDeliveries.AllDeliveries />}
				/>
				<Route
					path='new'
					element={<FilteredDeliveries.NewDeliveries />}
				/>
				<Route
					path='pending'
					element={<FilteredDeliveries.PendingDeliveries />}
				/>
				<Route
					path='shipped'
					element={<FilteredDeliveries.ShippedDeliveries />}
				/>
				<Route
					path='completed'
					element={<FilteredDeliveries.CompletedDeliveries />}
				/>
				<Route
					path='cancelled'
					element={<FilteredDeliveries.CancelledDeliveries />}
				/>
				<Route
					path='*'
					element={<Navigate to='/adminpanel/deliveries' replace />}
				/>
			</Routes>
		</div>
	);
};

export default Deliveries;
