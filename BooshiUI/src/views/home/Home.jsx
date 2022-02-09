import React from 'react';
import { connect } from 'react-redux';
import DeliveryPersonImg from '../../assets/3683242.png';
import DeliveryPersonImg2 from '../../assets/Deliveryman.png';
import DeliveryPersonImg3 from '../../assets/3333446.png';
import FAQImg from '../../assets/faq.png';
import PrimaryButton from '../../components/design/buttons/PrimaryButton';
import { Link } from 'react-router-dom';
import Footer from '../../components/layout/footer/Footer';

const Home = ({ user, ...props }) => {
	return (
		<div>
			<div id='join-us' className='container-fluid p-0'>
				<div className='container'>
					<div className='row p-0 m-0 py-3 justify-content-home align-items-center'>
						<div className='order-2 order-lg-1 col-12 col-md-8 col-lg-6 col-xl-5 text-center'>
							{!user ? (
								<div className='h2'>Not a member? Join us!</div>
							) : (
								<div className='h2'>Your panel is here!</div>
							)}
							<div className='p'>
								Lorem, ipsum dolor sit amet consectetur
								adipisicing elit. Quam excepturi fuga
								voluptatibus porro molestias deserunt! Et
								aspernatur, quis adipisci vero magni quas.
								Aperiam veniam consequatur quam ea enim?
								Distinctio, velit?
							</div>
							{!user ? (
								<Link to='register' className='link'>
									<PrimaryButton className='w-75 btn-lg mt-4'>
										Get started here{' '}
										<i className='fas fa-chevron-right'></i>
									</PrimaryButton>
								</Link>
							) : (
								<Link
									to={
										user.roleId === 3
											? 'mypanel'
											: user.roleId === 2
											? 'deliverypanel'
											: user.roleId === 1
											? 'adminpanel'
											: ''
									}
									className='link'>
									<PrimaryButton className='w-50 btn-lg mt-4'>
										My panel{' '}
										<i className='fas fa-chevron-right'></i>
									</PrimaryButton>
								</Link>
							)}
						</div>
						<div className='order-1 order-lg-2 col-12 col-lg-6 text-center'>
							<img
								className='home-img'
								src={DeliveryPersonImg}
								alt='deliveryperson'
							/>
						</div>
					</div>
				</div>
			</div>
			<div id='about' className='container-fluid p-0'>
				<div className='container'>
					<div className='row p-0 m-0 py-3 justify-content-home align-items-center'>
						<div className='col-12 col-lg-6 text-center'>
							<img
								className='home-img'
								src={DeliveryPersonImg2}
								alt='deliveryperson'
							/>
						</div>
						<div className='col-12 col-md-8 col-lg-6 col-xl-5 text-center mt-4 mt-lg-0'>
							<div className='h2'>Booshi delivery systems</div>
							<div className='p'>
								Lorem ipsum dolor sit, amet consectetur
								adipisicing elit. Laboriosam animi officia
								voluptatem omnis tempora assumenda temporibus
								placeat mollitia hic? Vero unde est molestias
								nihil, expedita magnam dicta repudiandae iure
								magni corrupti. Ea, quas. Quo reprehenderit esse
								veritatis quaerat rem vitae reiciendis similique
								ducimus odio nisi natus molestias consectetur
								assumenda, sequi consequatur placeat voluptatum
								culpa iure. Deserunt ratione nostrum ea neque!
							</div>
						</div>
					</div>
				</div>
			</div>
			<div id='features' className='container-fluid p-0 py-2'>
				<div className='container'>
					<div className='row p-0 m-0 py-3 justify-content-home align-items-center'>
						<div className='order-2 order-lg-1 col-12 col-md-8 col-lg-6 col-xl-5 text-center mt-4 mt-lg-0'>
							<div className='h2'>Our features</div>
							<ul>
								<li>
									Lorem ipsum dolor sit amet consectetur
									adipisicing elit. Animi, quibusdam?
								</li>
								<li className='mt-2'>
									Sequi minus similique ullam excepturi
									adipisci dolorem voluptates commodi.
								</li>
								<li className='mt-2'>
									Exercitationem nihil nostrum accusamus esse
									saepe alias fuga?
								</li>
								<li className='mt-2'>
									Eius tempora doloremque est assumenda sequi,
									quibusdam expedita fugit repellat
									recusandae!
								</li>
							</ul>
						</div>
						<div className='order-1 order-lg-2 col-12 col-lg-6 text-center'>
							<img
								className='home-img'
								src={DeliveryPersonImg3}
								alt='deliveryperson'
							/>
						</div>
					</div>
				</div>
			</div>
			<div id='faq' className='container-fluid p-0 py-2'>
				<div className='container'>
					<div className='row p-0 m-0 py-3 justify-content-home align-items-center'>
						<div className='col-12 col-lg-6 text-center'>
							<img
								className='faq-img'
								src={FAQImg}
								alt='deliveryperson'
							/>
						</div>
						<div className='col-12 col-md-8 col-lg-6 col-xl-5 text-center mt-4 mt-lg-0'>
							<div className='h2'>FAQ</div>
							<div
								className='accordion mt-4 shadow-md'
								id='accordionExample'>
								<div className='card'>
									<div
										className='card-header bg-white'
										id='headingOne'>
										<h2 className='mb-0'>
											<button
												className='btn btn-trabsparent btn-block collapsed'
												type='button'
												data-toggle='collapse'
												data-target='#collapseOne'>
												Lorem ipsum dolor sit amet?
											</button>
										</h2>
									</div>

									<div
										id='collapseOne'
										className='collapse'
										aria-labelledby='headingOne'
										data-parent='#accordionExample'>
										<div className='card-body bg-light-gray p-0 m-0 py-2'>
											Lorem ipsum dolor sit amet
											consectetur, adipisicing elit.
										</div>
									</div>
								</div>
								<div className='card'>
									<div
										className='card-header bg-white'
										id='headingTwo'>
										<h2 className='mb-0'>
											<button
												className='btn btn-transparent btn-block collapsed'
												type='button'
												data-toggle='collapse'
												data-target='#collapseTwo'
												aria-expanded='false'
												aria-controls='collapseTwo'>
												Nisi reprehenderit obcaecati
												laboriosam?
											</button>
										</h2>
									</div>
									<div
										id='collapseTwo'
										className='collapse'
										aria-labelledby='headingTwo'
										data-parent='#accordionExample'>
										<div className='card-body bg-light-gray'>
											Unde laudantium corrupti
											voluptatibus ipsum eos aperiam nobis
											quo incidunt veritatis.
										</div>
									</div>
								</div>
								<div className='card'>
									<div
										className='card-header bg-white'
										id='headingThree'>
										<h2 className='mb-0'>
											<button
												className='btn btn-transparent btn-block collapsed'
												type='button'
												data-toggle='collapse'
												data-target='#collapseThree'
												aria-expanded='false'
												aria-controls='collapseThree'>
												Dolorem nostrum eveniet quia?
											</button>
										</h2>
									</div>
									<div
										id='collapseThree'
										className='collapse'
										aria-labelledby='headingThree'
										data-parent='#accordionExample'>
										<div className='card-body bg-light-gray'>
											Laborum consectetur dolorem corporis
											commodi eligendi in!
										</div>
									</div>
								</div>
								<div className='card'>
									<div
										className='card-header bg-white'
										id='headingFour'>
										<h2 className='mb-0'>
											<button
												className='btn btn-transparent btn-block collapsed'
												type='button'
												data-toggle='collapse'
												data-target='#collapseFour'
												aria-expanded='false'
												aria-controls='collapseFour'>
												Vero at alias quaerat tenetur?
											</button>
										</h2>
									</div>
									<div
										id='collapseFour'
										className='collapse'
										aria-labelledby='headingFour'
										data-parent='#accordionExample'>
										<div className='card-body bg-light-gray'>
											Lorem ipsum dolor sit amet
											consectetur adipisicing elit.
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div id='pricing' className='container-fluid p-0 py-2 mb-3'>
				<div className='container'>
					<div className='text-center mt-2'>
						<div className='h2'>Our pricing</div>
						<p>
							Lorem ipsum dolor sit amet, consectetur adipisicing
							elit. Doloremque, ipsum!
						</p>
					</div>
					<div className='row p-0 m-0 py-3 justify-content-home align-items-center'>
						<div className='col-12 p-0 col-lg d-flex align-items-center justify-content-center'>
							<div
								className='card shadow-md'
								style={{ width: '17rem', height: '15rem' }}>
								<div className='card-body d-flex flex-column justify-content-between'>
									<div className=''>
										<h5 className='card-title text-center'>
											1 Delivery package
										</h5>
										<p className='card-text'>
											Lorem ipsum dolor sit, amet
											consectetur adipisicing elit. Quo
											recusandae corporis officiis dolore
											incidunt!
										</p>
									</div>
									<div className=''>
										<div className='text-center text-booshi'>
											<span className='h5'>12.99$ </span>
											<span>/ Delivery</span>
										</div>
									</div>
								</div>
							</div>
						</div>{' '}
						<div className='col-12 p-0 col-lg d-flex align-items-center justify-content-center my-4 my-lg-0'>
							<div
								className='card shadow-md'
								style={{ width: '17rem', height: '15rem' }}>
								<div className='card-body d-flex flex-column justify-content-between'>
									<div className=''>
										<h5 className='card-title text-center'>
											3 Deliveries package
										</h5>
										<p className='card-text'>
											Lorem ipsum dolor sit, amet
											consectetur adipisicing elit. Quo
											recusandae corporis officiis dolore
											incidunt!
										</p>
									</div>
									<div className=''>
										<div className='text-center text-booshi'>
											<span className='h5'>10.99$ </span>
											<span>/ Delivery</span>
										</div>
									</div>
								</div>
							</div>
						</div>{' '}
						<div className='col-12 p-0 col-lg d-flex align-items-center justify-content-center'>
							<div
								className='card shadow-md'
								style={{ width: '17rem', height: '15rem' }}>
								<div className='card-body d-flex flex-column justify-content-between'>
									<div className=''>
										<h5 className='card-title text-center'>
											5 Deliveries package
										</h5>
										<p className='card-text mt-3'>
											Lorem ipsum dolor sit, amet
											consectetur adipisicing elit. Quo
											recusandae corporis officiis dolore
											incidunt!
										</p>
									</div>
									<div className=''>
										<div className='text-center text-booshi'>
											<span className='h5'>8.99$ </span>
											<span>/ Delivery</span>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<Footer></Footer>
		</div>
	);
};

const mapStateToProps = (state) => {
	return {
		user: state.auth.userData,
	};
};

export default connect(mapStateToProps)(Home);
