import { SET_PAGE } from './paginatorTypes';

export const SetPage = (pageNum) => {
	return {
		type: SET_PAGE,
		payload: pageNum,
	};
};
