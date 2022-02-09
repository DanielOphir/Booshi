import React from 'react';
import { Link } from 'react-router-dom';
import PrimaryButton from '../../../components/design/buttons/PrimaryButton';
import images from '../../../utils/constants/images';
const Unauthorized = () => {
	return (
		<div
			className='d-flex flex-column align-items-center justify-content-center unauthorized'
			style={{ width: '100vw', height: '100vh' }}>
			<h1 className='mb-4 text-center'>
				Oops...<br></br> you should not get in here
			</h1>
			<img src={images.unauthorized} alt='Unauthorized' />
			<Link to='/' className='link'>
				<PrimaryButton className='col-auto btn-lg mt-4'>
					<i className='fas fa-chevron-left'></i> Home
				</PrimaryButton>
			</Link>
		</div>
	);
};

export default Unauthorized;
