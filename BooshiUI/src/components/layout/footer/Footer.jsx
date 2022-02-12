import React from 'react';
import './Footer.css';

// Footer component
const Footer = () => {
	return (
		<div
			className='container-fluid text-white p-0 m-0 px-4 pt-4 pb-2'
			style={{ backgroundColor: 'rgb(35, 35, 35)' }}>
			<div className='row m-0 p-0 justify-content-center'>
				<div className='col-12 col-md-6 col-lg-4'>
					<div className='row justify-content-center align-items-center'>
						<div className='col-12'>
							<div
								className='text-center'
								style={{ fontSize: '25px' }}>
								Check us on social media!
							</div>
						</div>
						<div className='col-12 row justify-content-center icon-container'>
							<a
								href='https://github.com/DanielOphir/'
								target={'_blank'}
								rel='noreferrer'
								className='col-auto text-center'>
								<i className='fab fa-github'></i>
							</a>
							<a
								href='https://www.linkedin.com/in/danielophir/'
								target={'_blank'}
								rel='noreferrer'
								className='col-auto text-center'>
								<i className='fab fa-linkedin'></i>
							</a>
							<a
								href='https://www.facebook.com/daniel.ophir.5'
								target={'_blank'}
								rel='noreferrer'
								className='col-auto text-center'>
								<i className='fab fa-facebook'></i>
							</a>
							<a
								href='https://www.instagram.com/daniel.ophir/'
								target={'_blank'}
								rel='noreferrer'
								className='col-auto text-center'>
								<i className='fab fa-instagram'></i>
							</a>
						</div>
					</div>
				</div>
				<div className='col-12 col-md-6 col-lg-4 text-center mt-4 mt-md-0 '>
					{' '}
					<div
						className='text-center'
						style={{
							fontSize: '25px',
						}}>
						Concact us
					</div>
					<p className='mt-2 p-0'>
						<i className='fas fa-phone'></i> 050-570-0082
					</p>
					<p className='my-2 p-0'>
						<i className='fas fa-envelope'></i> opird1@gmail.com
					</p>
					<p className='m-0 p-0'>
						<i className='far fa-clock'></i> 09:00 - 17:00
					</p>
				</div>
			</div>
			<div className='row mt-3'>
				<div
					className='col-12 text-white text-center'
					style={{ fontSize: '15px' }}>
					&copy; Booshi delivery systems 2022
				</div>
			</div>
		</div>
	);
};

export default Footer;
