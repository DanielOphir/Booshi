import React from 'react';
import { useParams } from 'react-router-dom';
import RegisterTable from '../../../components/register/RegisterTable';

const UpdateUser = () => {
	const { id } = useParams();

	return (
		<div className='container'>
			<div className='row justify-content-center p-0 mt-3'>
				<div className='col-auto p-0 m-0'>
					<h2 className='color-booshi'>Update user</h2>
				</div>
			</div>
			<RegisterTable mode='update' className='my-3' id={id} />
		</div>
	);
};

export default UpdateUser;
