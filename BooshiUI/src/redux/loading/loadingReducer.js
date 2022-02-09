import * as loadingTypes from './loadingTypes';

const initializedState = {
	loading: false,
};

export const loadingReducer = (state = initializedState, action) => {
	switch (action.type) {
		case loadingTypes.LOADING_ON:
			return { ...state, loading: true };
		case loadingTypes.LOADING_OFF:
			return { ...state, loading: false };
		default:
			return state;
	}
};
