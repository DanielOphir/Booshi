import * as loadingTypes from './loadingTypes';

export const loadingOn = () => {
	return {
		type: loadingTypes.LOADING_ON,
	};
};

export const loadingOff = () => {
	return {
		type: loadingTypes.LOADING_OFF,
	};
};
