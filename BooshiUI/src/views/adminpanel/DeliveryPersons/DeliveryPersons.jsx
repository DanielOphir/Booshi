import React from 'react';
import Users from '../Users/Users';

const DeliveryPerosns = () => {
	return (
		<>
			<div className='container'></div>
			<Users
				apiUrl='/api/deliveryperson/page/'
				searchUrl='/api/deliveryperson/'
				deliveryPersons
			/>
		</>
	);
};

export default DeliveryPerosns;
