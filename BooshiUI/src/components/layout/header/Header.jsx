import React, { Component } from 'react';
import Navbar from './navbar/Navbar';

class Header extends Component {
	constructor(props) {
		super(props);

		this.state = {};
	}

	render() {
		return (
			<div className={'shadow-sm container-fluid bg-white'}>
				<Navbar></Navbar>
			</div>
		);
	}
}

export default Header;
