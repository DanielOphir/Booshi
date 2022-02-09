export const GetToken = () => {
	return window.localStorage.getItem('jwt')
		? window.localStorage.getItem('jwt')
		: window.sessionStorage.getItem('jwt')
		? window.sessionStorage.getItem('jwt')
		: '';
};

export const SetLocalToken = (token) => {
	window.localStorage.setItem('jwt', token);
};
export const SetSessionToken = (token) => {
	window.sessionStorage.setItem('jwt', token);
};

export const RemoveToken = () => {
	window.localStorage.removeItem('jwt');
	window.sessionStorage.removeItem('jwt');
};
