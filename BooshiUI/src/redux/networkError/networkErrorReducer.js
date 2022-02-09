import { SET_ERROR } from './networkErrorTypes';

const initializedState = {
	networkError: false,
};

export const networkErrorReducer = (state = initializedState, action) => {
	switch (action.type) {
		case SET_ERROR:
			return { ...state, networkError: true };
		default:
			return state;
	}
};
