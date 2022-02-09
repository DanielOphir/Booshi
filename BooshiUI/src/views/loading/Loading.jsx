import React, { Component } from 'react';
import './Loading.css';
import icon from '../../assets/Booshi_Icon.svg';

class Loading extends Component {
	constructor(props) {
		super(props);
	}

	render() {
		return (
			<div className='loading-container'>
				<div className='animate-pulse-slow container-fluid'>
					<div className='row text-center row justify-content-center'>
						<img
							className='col-8 col-sm-6 col-md-5 col-lg-4 col-xl-3'
							src={icon}
							alt='Booshi icon'
						/>
					</div>
					<div className='row text-center row justify-content-center'>
						<div className='h1'>Loading...</div>
					</div>
				</div>
			</div>
		);
	}
}

export default Loading;
