import React from 'react';
import DeliveriesTable from '../../../userpanel/DeliveriesTable';

export const AllDeliveries = () => {
	return <DeliveriesTable admin apiUrl='api/deliveries/page/' />;
};

export const NewDeliveries = () => {
	return (
		<DeliveriesTable admin apiUrl='api/deliveries/new-deliveries/page/' />
	);
};

export const PendingDeliveries = () => {
	return <DeliveriesTable admin apiUrl='api/deliveries/status/1/page/' />;
};

export const ShippedDeliveries = () => {
	return <DeliveriesTable admin apiUrl='api/deliveries/status/2/page/' />;
};

export const CompletedDeliveries = () => {
	return <DeliveriesTable admin apiUrl='api/deliveries/status/3/page/' />;
};

export const CancelledDeliveries = () => {
	return <DeliveriesTable admin apiUrl='api/deliveries/status/4/page/' />;
};
