import { SET_PAGE } from './paginatorTypes';

const initialState = {
	page: 1,
};

export const paginatorReducer = (state = initialState, action) => {
	switch (action.type) {
		case SET_PAGE:
			return { ...state, page: action.payload };
		default:
			return state;
	}
};
