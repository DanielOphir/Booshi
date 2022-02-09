import * as authTypes from './authTypes';

const initializedState = {
	loading: false,
	userData: null,
	error: null,
};

export const authReducer = (state = initializedState, action) => {
	switch (action.type) {
		case authTypes.GET_AUTH_REQUEST:
			return { ...state, loading: true, userData: null, error: null };
		case authTypes.GET_AUTH_SUCCESS:
			return {
				...state,
				loading: false,
				userData: action.payload,
				error: null,
			};
		case authTypes.GET_AUTH_FAILED:
			return {
				...state,
				loading: false,
				userData: null,
				error: action.payload,
			};
		default:
			return state;
	}
};
