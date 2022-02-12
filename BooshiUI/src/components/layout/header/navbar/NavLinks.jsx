import React, { Component } from 'react';
import { HashLink as Link } from 'react-router-hash-link';

// All of the navbar links.
class NavLinks extends Component {
	links = [
		{
			label: 'About',
			path: 'about',
		},
		{
			label: 'Features',
			path: 'features',
		},
		{
			label: 'FAQ',
			path: 'faq',
		},
		{
			label: 'Pricing',
			path: 'pricing',
		},
	];

	render() {
		return (
			<React.Fragment>
				{/* Mapping through all the links and returning the list for the links. */}
				{this.links.map((link) => {
					return (
						<li
							key={link.path}
							className='nav-item booshi-nav-item col-auto p-0 mx-0 mx-lg-4'>
							<Link
								className='nav-link text-color-link p-1'
								to={{ pathname: '/', hash: `#${link.path}` }}>
								{link.label}
							</Link>
						</li>
					);
				})}
			</React.Fragment>
		);
	}
}

export default NavLinks;
