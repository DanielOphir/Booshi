import React from 'react';
import RegisterTable from '../../../components/register/RegisterTable';

const NewDeliveryPerson = () => {
	return (
		<div>
			<div className='container register-form-width p-0 mt-4 ml-2 ml-sm-auto'>
				<h4 className=''>
					New delivery person <i className='fas fa-motorcycle h4'></i>
				</h4>
			</div>
			<RegisterTable adminpanel></RegisterTable>
		</div>
	);
};

export default NewDeliveryPerson;
