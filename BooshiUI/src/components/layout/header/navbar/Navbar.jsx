import React from 'react';
import { connect } from 'react-redux';
import { Link, useNavigate } from 'react-router-dom';
import { RemoveToken } from '../../../../utils/helpers/TokenHelper';
import PrimaryButton from '../../../design/buttons/PrimaryButton';
import NavLinks from './NavLinks';
import BooshiIcon from '../../../../assets//Booshi_Icon.svg';

const Navbar = (props) => {
	const navigate = useNavigate();

	const logOut = () => {
		RemoveToken();
		navigate('/');
		window.location.reload();
	};

	return (
		<nav
			className={
				'navbar navbar-expand-sm navbar-light row justify-content-between align-items-center header'
			}>
			<Link to='/' className='navbar-brand m-0 px-0 col-auto'>
				<img
					className='booshi-icon'
					src={BooshiIcon}
					alt='Booshi icon'></img>
			</Link>
			<div className='row d-sm-none justify-content-end p-0'>
				<button
					className='btn btn-transparent p-0 mx-3 m-0 pt-2'
					type='button'
					data-toggle='collapse'
					data-target='#collapsibleNavId'
					aria-controls='collapsibleNavId'
					aria-expanded='false'
					aria-label='Toggle navigation'>
					<span className='h5 fas fa-bars'></span>
				</button>
			</div>
			<div
				className='collapse navbar-collapse col justify-content-center row m-0 p-0'
				id='collapsibleNavId'>
				<ul className='navbar-nav mt-lg-0 col-12 col-sm justify-content-center gap-8 text-center p-0 m-0'>
					<NavLinks></NavLinks>
				</ul>
				{/* Checks if theres user connected, if not displaying the register and sign in buttons, else, displaying the user functionalities */}
				{!props.auth.userData ? (
					<div className='col-12 col-sm-auto row text-center p-0 m-0 justify-content-center align-items-center'>
						<div className='col-12 col-sm-auto mb-2 mb-sm-0 p-1 mx-0 mx-sm-1 mr-lg-3'>
							<Link to='/register'>
								<PrimaryButton>Register</PrimaryButton>
							</Link>
						</div>
						<div className='col-12 col-sm-auto mx-0 p-1 mx-sm-1 ml-lg-3'>
							<Link to='/signin'>
								<PrimaryButton>Sign in</PrimaryButton>
							</Link>
						</div>
					</div>
				) : (
					<div className='navbar-nav col-12 col-sm-auto m-0 p-0 row text-center justify-content-center align-items-center'>
						<div className='nav-item dropdown col-6 col-sm-auto align-items-center justify-content-center'>
							<button
								className='btn btn-transparent col-12 col-sm-auto text-center nav-link'
								id='navbarDropdown'
								data-toggle='dropdown'
								aria-expanded='false'>
								Hello, {props.auth.userData.userName}{' '}
								<i className='fas fa-chevron-down'></i>
							</button>
							<div
								className='dropdown-menu dropdown-menu-right'
								aria-labelledby='navbarDropdown'>
								{/* Only for delivery person */}
								{props.auth.userData.roleId === 2 && (
									<Link
										to='deliverypanel/new-deliveries'
										className='dropdown-item'>
										<i className='fas fa-motorcycle'></i>{' '}
										Deliveries
									</Link>
								)}
								<Link
									to={
										props.auth.userData.roleId === 1
											? 'adminpanel'
											: props.auth.userData.roleId === 2
											? 'deliverypanel'
											: 'mypanel'
									}
									className='dropdown-item'>
									<i className='fas fa-columns mr-2'></i> My
									panel
								</Link>
								<Link
									to={'/settings'}
									className='dropdown-item'>
									<i className='fas fa-cogs mr-2'></i>{' '}
									Settings
								</Link>
								<div className='dropdown-divider'></div>
								<button
									className='dropdown-item'
									onClick={logOut}>
									<i className='fas fa-sign-out-alt mr-2'></i>{' '}
									Sign out
								</button>
							</div>
						</div>
					</div>
				)}
			</div>
		</nav>
	);
};

const mapStateToProps = (state) => {
	return {
		auth: state.auth,
	};
};

export default connect(mapStateToProps)(Navbar);

//<div className="col-12 col-sm-auto m-0 row text-center justify-content-center align-items-center">
//<div className="navbar-nav mt-lg-0 justify-content-center text-center p-0 m-0">
