import React from 'react';
import images from '../../../utils/constants/images';

const NetworkErrorPage = () => {
	return (
		<div
			className='d-flex flex-column align-items-center justify-content-center'
			style={{ width: '100vw', height: '100vh' }}>
			<img
				style={{ maxWidth: '75%', maxHeight: '75%' }}
				src={images.time_out}
				alt='timeOut'
			/>
		</div>
	);
};

export default NetworkErrorPage;
