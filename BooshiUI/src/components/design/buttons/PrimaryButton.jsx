import React, { Component } from 'react';

// Main button component that will be used in all of the project
class PrimaryButton extends Component {
	constructor(props) {
		super(props);
		this.state = {};
	}

	render() {
		return (
			<button
				{...this.props}
				className={'main-btn ' + this.props.className}
				disabled={this.props.disabled}
				style={this.props.style}>
				{this.props.children}
			</button>
		);
	}
}

export default PrimaryButton;
