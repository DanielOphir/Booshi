import * as authTypes from './authTypes';

export const authRequest = () => {
	return {
		type: authTypes.GET_AUTH_REQUEST,
	};
};

export const authSuccess = (userData) => {
	return {
		type: authTypes.GET_AUTH_SUCCESS,
		payload: userData,
	};
};

export const authFailed = (error) => {
	return {
		type: authTypes.GET_AUTH_FAILED,
		payload: error,
	};
};
