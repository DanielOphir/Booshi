import React from 'react';
import './deliveryStatus.css';

// Delivery status alerts
function DeliveryStatus(props) {
	switch (props.statusId) {
		case 1:
			return <PendingStatus />;
		case 2:
			return <ShippedStatus />;
		case 3:
			return <CompleteStatus />;
		case 4:
			return <CancelledStatus />;
		default:
			return <PendingStatus />;
	}
}

const PendingStatus = () => {
	return (
		<div
			className='alert alert-primary px-0 py-1 m-0 text-center pending-status'
			role='alert'>
			Pending
		</div>
	);
};

const CompleteStatus = () => {
	return (
		<div
			className='alert alert-success px-0 py-1 m-0 text-center complete-status'
			role='alert'>
			Complete
		</div>
	);
};

const ShippedStatus = () => {
	return (
		<div
			className='alert alert-warning px-0 py-1 m-0 text-center shipped-status'
			role='alert'>
			In process
		</div>
	);
};

const CancelledStatus = () => {
	return (
		<div
			className='alert alert-danger px-0 py-1 m-0 text-center cancelled-status'
			role='alert'>
			Cancelled
		</div>
	);
};

export default DeliveryStatus;
