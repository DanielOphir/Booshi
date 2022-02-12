import React from 'react';
import images from '../../utils/constants/images';

// Displaying that there's no deliveries.
const NoDeliveries = () => {
	return (
		<div className='container'>
			<div className='row justify-content-center'>
				<img
					className='col-12 col-md-7 col-lg-6 p-0 m-0'
					src={images.cardboard_box}
					alt=''
				/>
			</div>
			<div className='row justify-content-center text-center mt-3'>
				<h1>No delvieries found...</h1>
			</div>
		</div>
	);
};

export default NoDeliveries;
