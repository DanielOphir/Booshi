import * as types from './deliveriesTypes';

const initialState = {
	loading: false,
	deliveries: [],
	totalDeliveries: 0,
	error: {},
};

export const DeliveriesReducer = (state = initialState, action) => {
	switch (action.type) {
		case types.REQUEST_DELIVERIES:
			return {
				...state,
				loading: true,
				deliveries: [],
				totalDeliveries: 0,
				error: {},
			};
		case types.DELIVERIES_SUCCESS:
			return {
				...state,
				loading: false,
				deliveries: action.payload.deliveries,
				totalDeliveries: action.payload.totalDeliveries,
				error: {},
			};
		case types.DELIVERIES_ERROR:
			return {
				...state,
				loading: false,
				deliveries: [],
				totalDeliveries: 0,
				error: action.payload,
			};
		default:
			return state;
	}
};
