import { combineReducers } from 'redux';
import { authReducer } from './auth/authReducer';
import { DeliveriesReducer } from './deliveries/deliveriesReducer';
import { formReducer } from './form/formReducer';
import { loadingReducer } from './loading/loadingReducer';
import { paginatorReducer } from './paginator/paginatorReducer';
import { networkErrorReducer } from './networkError/networkErrorReducer';

export const rootReducer = combineReducers({
	auth: authReducer,
	loading: loadingReducer,
	form: formReducer,
	deliveries: DeliveriesReducer,
	paginator: paginatorReducer,
	networkError: networkErrorReducer,
});
