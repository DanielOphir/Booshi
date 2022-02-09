import * as formTypes from './formTypes';

export const SetForm = (data) => {
	return {
		type: formTypes.SET_FORM,
		payload: data,
	};
};
