import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadingOn, loadingOff } from '../../../redux/loading/loadingActions';
import { api } from '../../../utils/helpers/AxiosHelper';
import Paginator from '../../../components/design/paginator/Paginator';
import * as $ from 'jquery';
import UserModal from './UserModal';
import PrimaryButton from '../../../components/design/buttons/PrimaryButton';
import { Link } from 'react-router-dom';

class Users extends Component {
	state = {
		users: [],
		totalUsers: 0,
		windowWidth: 0,
		userToDisplay: {},
		searchTerm: '',
	};

	_componentMount = false;

	componentDidMount() {
		this._componentMount = true;
		this.GetUsers();
		this.setState({ ...this.state, windowWidth: $(document).width() });
	}

	componentWillUnmount() {
		this._componentMount = false;
	}

	GetUsers = (pageNumber = 1) => {
		this.props.loadingOn();
		api.get(this.props.apiUrl + pageNumber).then((response) => {
			const data = response?.data;
			this.setState({
				...this.state,
				users: data?.users,
				totalUsers: data?.totalCount,
			});
			this.props.loadingOff();
		});
	};

	PaginatorOnClick = (pageNum) => {
		if (this.state.searchTerm === '') {
			this.GetUsers(pageNum);
			return;
		}
		this.SearchUsers(pageNum);
		return;
	};

	SearchUsers = (pageNumber = 1) => {
		if (!this.state.searchTerm) {
			this.GetUsers();
			return;
		}
		this.props.loadingOn();
		api.get(
			`${this.props.searchUrl}${this.state.searchTerm}/page/${pageNumber}`,
		)
			.then(({ data }) => {
				this.setState({
					...this.state,
					users: data.users,
					totalUsers: data.totalCount,
				});
				this.props.loadingOff();
			})
			.catch((error) => {
				if (error.response.status === 404) {
					this.setState({ ...this.state, users: [] });
				} else {
					alert(error.message);
				}
				this.props.loadingOff();
			});
	};

	onSearchClick = (event) => {
		event.preventDefault();
		this.SearchUsers();
	};

	componentDidCatch(error) {
		console.log(error);
	}

	render() {
		return (
			<div className='container-fluid container-lg mt-4'>
				{this.state.users ? (
					<div className='p-3'>
						{this.props.deliveryPersons && (
							<div className='row'>
								<div className='col-auto p-0 m-0 mb-3'>
									<Link to='new' className='link'>
										<PrimaryButton className='btn-block'>
											New delivery person
										</PrimaryButton>
									</Link>
								</div>
							</div>
						)}
						<form className=''>
							<div className='input-group row'>
								<input
									type='text'
									className='form-control col-8 col-xs-5 col-sm-4 col-lg-3'
									placeholder='Username'
									onChange={(e) =>
										this.setState({
											...this.state,
											searchTerm: e.target.value,
										})
									}
								/>
								<button
									onClick={(e) => this.onSearchClick(e)}
									className='btn btn-dark ml-1 col-auto'>
									<i className='fas fa-search'></i>
								</button>
							</div>
						</form>
						<table className='table table-hover my-3'>
							<thead>
								<tr className='row'>
									<th className='col-3 col-md-2 text-break'>
										User
									</th>
									<th className='col-3'>Name</th>
									<th className='col-6 d-flex col-md'>
										Email
									</th>
									<th className='col-2 col-xl-3 d-none d-md-flex'>
										Phone number
									</th>
								</tr>
							</thead>
							<tbody>
								{this.state.users.map((user) => {
									return (
										<tr
											onClick={() =>
												this.setState({
													...this.state,
													userToDisplay: user,
												})
											}
											data-toggle='modal'
											data-target='#userModal'
											className='row table-row'
											key={user.id}>
											<td className='col-3 col-md-2 d-flex align-items-center text-break'>
												{user.userName}
											</td>
											<td className='col-3 d-flex align-items-center'>
												{user.firstName} {user.lastName}
											</td>
											<td className='col-6 col-md d-flex align-items-center text-break'>
												{user.email}
											</td>
											<td className='col-2 col-xl-3 d-none d-md-flex align-items-center'>
												{user.phoneNumber}
											</td>
										</tr>
									);
								})}
							</tbody>
						</table>
						<UserModal user={this.state.userToDisplay} />
					</div>
				) : (
					<div>No Users</div>
				)}
				<Paginator
					pages={Math.ceil(this.state.totalUsers / 10)}
					onClick={(pageNum) =>
						this.PaginatorOnClick(pageNum)
					}></Paginator>
			</div>
		);
	}
}

const mapDispatchToProps = (dispatch) => {
	return {
		loadingOn: () => dispatch(loadingOn()),
		loadingOff: () => dispatch(loadingOff()),
	};
};
export default connect(null, mapDispatchToProps)(Users);
