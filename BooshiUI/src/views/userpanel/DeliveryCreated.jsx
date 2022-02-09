import React from 'react';
import PrimaryButton from '../../components/design/buttons/PrimaryButton';
import { Link } from 'react-router-dom';
import Deliveryman from '../../assets/Deliveryman1.svg';
import './UserPanel.css';

function DeliveryCreated(props) {
	return (
		<div className='container p-5 mt-5'>
			<div className='row'>
				<div className='col text-center'>
					<h2> The delivery was created succesfully!</h2>
				</div>
			</div>
			<div className='row mt-3'>
				<div className='col text-center'>
					<p className='text-big'>
						Delivery number : {props.orderNumber}
					</p>
				</div>
			</div>
			<div className='row mt-3 justify-content-center'>
				<div className='col-auto text-center'>
					<Link to='/mypanel' className='link'>
						<PrimaryButton className='btn-block text-big'>
							<i className='fas fa-chevron-circle-left'></i> My
							panel
						</PrimaryButton>
					</Link>
				</div>
			</div>
			<div className='row mt-5 justify-content-center'>
				<div className='col text-center mt-5 mt-lg-2'>
					<img
						style={{ maxHeight: '500px' }}
						src={Deliveryman}
						alt='g'
					/>
				</div>
			</div>
		</div>
	);
}

export default DeliveryCreated;
