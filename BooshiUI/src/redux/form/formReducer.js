import * as formTypes from './formTypes';

const initialState = {
	form: {},
};

export const formReducer = (state = initialState, action) => {
	switch (action.type) {
		case formTypes.SET_FORM:
			return { ...state, form: action.payload };
		default:
			return state;
	}
};
