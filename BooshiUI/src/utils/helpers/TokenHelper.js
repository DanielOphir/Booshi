//Checks if the jwt token is in the local storage or session storage and gets it.
export const GetToken = () => {
	return window.localStorage.getItem('jwt')
		? window.localStorage.getItem('jwt')
		: window.sessionStorage.getItem('jwt')
		? window.sessionStorage.getItem('jwt')
		: '';
};

// Setting the jwt token in the local storage.
export const SetLocalToken = (token) => {
	window.localStorage.setItem('jwt', token);
};

// Setting the token in the session storage.
export const SetSessionToken = (token) => {
	window.sessionStorage.setItem('jwt', token);
};

// Remove the token from the storage.
export const RemoveToken = () => {
	window.localStorage.removeItem('jwt');
	window.sessionStorage.removeItem('jwt');
};
