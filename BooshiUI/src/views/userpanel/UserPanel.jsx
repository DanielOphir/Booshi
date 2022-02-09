import React, { PureComponent } from 'react';
import { Routes, Route } from 'react-router-dom';
import CreateDelivery from './CreateDelivery';
import DeliveriesTable from './DeliveriesTable';

class UserPanel extends PureComponent {
	constructor(props) {
		super(props);

		this.state = {};
	}

	render() {
		return (
			<Routes>
				<Route
					path='/'
					element={
						<DeliveriesTable apiUrl='/api/deliveries/user/page/' />
					}
				/>
				<Route path='/add' element={<CreateDelivery />} />
			</Routes>
		);
	}
}

export default UserPanel;
