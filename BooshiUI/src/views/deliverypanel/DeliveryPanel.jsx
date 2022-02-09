import React, { Component } from 'react';
import NewDeliveries from './NewDeliveries';
import { Routes, Route } from 'react-router-dom';
import DeliveriesTable from '../userpanel/DeliveriesTable';

class DeliveryPanel extends Component {
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
						<DeliveriesTable
							deliveryPerson
							apiUrl='/api/deliveryperson/deliveries/page/'
						/>
					}
				/>
				<Route path='/new-deliveries' element={<NewDeliveries />} />
			</Routes>
		);
	}
}

export default DeliveryPanel;
