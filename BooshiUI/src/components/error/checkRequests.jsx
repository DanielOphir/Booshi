import React, { useEffect, useState } from 'react';
import { api } from '../../utils/helpers/AxiosHelper';
import NetworkErrorPage from '../../views/errors/NetworkError/NetworkErrorPage';
import Unauthorized from '../../views/errors/Unauthorized/Unauthorized';

// Check every api call if it has been successful or not, if not, display the error page.
const checkRequests = (Wrapped) => {
	function CheckRequests(props) {
		const [networkError, setNetworkError] = useState(false);
		const [unauthorizedError, setUnauthorizedError] = useState(false);

		useEffect(() => {
			api.interceptors.response.use(
				function (response) {
					return response;
				},
				function (error) {
					if (error.message === 'Network Error') {
						console.log('Network Error');
						setNetworkError(true);
					}
					if (
						error.response.state === 401 ||
						error.response.state === 403
					) {
						setUnauthorizedError(true);
					}
					return Promise.reject(error);
				},
			);
		}, []);
		if (networkError) {
			return <NetworkErrorPage />;
		}
		if (unauthorizedError) {
			return <Unauthorized />;
		}
		return <Wrapped {...props} />;
	}
	return CheckRequests;
};

export default checkRequests;
