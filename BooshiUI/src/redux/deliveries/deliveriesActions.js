import * as types from './deliveriesTypes';

export const RequestDeliveries = () => {
	return {
		type: types.REQUEST_DELIVERIES,
	};
};

export const DeliveriesSuccess = (deliveries, totalDeliveries) => {
	return {
		type: types.DELIVERIES_SUCCESS,
		payload: { deliveries, totalDeliveries },
	};
};

export const DeliveriesError = (error) => {
	return {
		type: types.REQUEST_DELIVERIES,
		payload: error,
	};
};

export const UpdateTotalDeliveries = (totalDeliveriesNum) => {
	return {
		type: types.TOTAL_DELIVERIES,
		payload: totalDeliveriesNum,
	};
};
