import React from 'react';
import { Link } from 'react-router-dom';
import * as $ from 'jquery';

function UserModal({ user, ...props }) {
	return (
		<div>
			<div className='modal fade' id='userModal' tabIndex='-1'>
				<div className='modal-dialog'>
					<div className='modal-content container'>
						<div className='modal-header row m-0 container'>
							<div className='row p-0 m-0 w-100'>
								<h5 className='color-booshi col m-0 p-0'>
									<i className='fas fa-user'></i>{' '}
									{user.firstName} {user.lastName}
								</h5>
								<div className='col-auto p-0 m-0'>
									<button
										type='button'
										className='close m-0 p-0'
										data-dismiss='modal'
										aria-label='Close'>
										<span aria-hidden='true'>&times;</span>
									</button>
								</div>
							</div>
							<div className='row p-0 m-0  w-100'>
								<p className='p-0 m-0'>
									{user.roleId === 1
										? 'Admin'
										: user.roleId === 2
										? 'Delivery person'
										: user.roleId === 3
										? 'User'
										: 'Unknown'}
								</p>
							</div>
							<div className='row p-0 m-0 w-100'>
								<small>{user.id}</small>
							</div>
						</div>
						<div className='modal-body'>
							<div className='container'>
								<div className='row mb-2'>
									<p>Username: {user.userName}</p>
								</div>
								<div className='row mb-2'>
									<p>Email: {user.email}</p>
								</div>
								<div className='row mb-2'>
									<p>Phone number: {user.phoneNumber}</p>
								</div>
								<div className='row mb-2'>
									<p>Street : {user.street}</p>
								</div>
								<div className='row mb-2'>
									<p>City : {user.city}</p>
								</div>
								<div className='row'>
									<p>Zipcode : {user.zipCode}</p>
								</div>
							</div>
						</div>
						{user.roleId !== 1 && (
							<div className='modal-footer'>
								<div className='container'>
									<div className='row justify-content-center'>
										{user.roleId === 3 && (
											<div className='col-6'>
												<Link
													to={user.id}
													className='link'>
													<button
														className='btn btn-block btn-danger'
														onClick={() =>
															$('.modal').modal(
																'hide',
															)
														}>
														<i className='fas fa-motorcycle'></i>{' '}
														User deliveries
													</button>
												</Link>
											</div>
										)}
										{user.roleId === 2 && (
											<div className='col-6'>
												<Link
													to={user.id}
													className='link'>
													<button
														onClick={() =>
															$('.modal').modal(
																'hide',
															)
														}
														className='btn btn-block btn-danger'>
														<i className='fas fa-motorcycle'></i>{' '}
														Deliveries
													</button>
												</Link>
											</div>
										)}
										<div className='col-6'>
											<Link
												to={`/adminpanel/users/edit/${user.id}`}
												className='link'>
												<button
													onClick={() =>
														$('.modal').modal(
															'hide',
														)
													}
													className='btn btn-block btn-danger h-100'>
													<i className='fas fa-edit'></i>{' '}
													Edit user
												</button>
											</Link>
										</div>
									</div>
								</div>
							</div>
						)}
					</div>
				</div>
			</div>
		</div>
	);
}

export default UserModal;
