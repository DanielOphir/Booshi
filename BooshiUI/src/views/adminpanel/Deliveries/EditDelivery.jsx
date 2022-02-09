import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router-dom';
import { loadingOff, loadingOn } from '../../../redux/loading/loadingActions';
import CreateDelivery from '../../userpanel/CreateDelivery';

const EditDelivery = (props) => {
	const { id } = useParams();
	useEffect(() => {}, [id]);

	return (
		<div>
			<div className='ml-5 mt-4'>
				<h4 className='text-booshi'>Edit delivery #{id}</h4>
			</div>
			<CreateDelivery id={id} mode='edit'></CreateDelivery>
		</div>
	);
};

const mapDispatchToProps = (dispatch) => {
	return {
		loadingOn: () => dispatch(loadingOn()),
		loadingOff: () => dispatch(loadingOff()),
	};
};

export default connect(null, mapDispatchToProps)(EditDelivery);
