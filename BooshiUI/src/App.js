import { Component } from 'react';
import { connect } from 'react-redux';
import Header from './components/layout/header/Header';
import Main from './components/layout/main/Main';
import { api } from './utils/helpers/AxiosHelper';
import * as authActions from './redux/auth/authActions';
import { GetToken, RemoveToken } from './utils/helpers/TokenHelper';
import Loading from './views/loading/Loading';
import '../node_modules/primereact/resources/themes/bootstrap4-dark-blue/theme.css';
import { SetNetworkError } from './redux/networkError/networkErrorActions';
import {
	Chart as ChartJS,
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
	Title,
	Tooltip,
	Legend,
	BarElement,
} from 'chart.js';
import { loadingOff } from './redux/loading/loadingActions';
import checkRequests from './components/error/checkRequests';

ChartJS.register(
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
	BarElement,
	Title,
	Tooltip,
	Legend,
);

class App extends Component {
	componentDidMount() {
		if (GetToken()) {
			this.props.authRequst();
			api.get('/api/auth/user')
				.then((response) => {
					this.props.authSuccess(response?.data);
				})
				.catch((error) => {
					this.props.authFailed(error.response);
					if (error.response?.data?.type === 'expiredToken') {
						RemoveToken();
					}
				});
		}
	}

	render() {
		return (
			<div
				className=''
				style={{ position: 'relative', minHeight: '100vh' }}>
				{!this.props.auth.loading && (
					<>
						<div>
							{this.props.loading.loading && <Loading />}
							<Header></Header>
							<Main className='main'></Main>
						</div>
					</>
				)}
			</div>
		);
	}
}

const mapStateToProps = (state) => {
	return {
		auth: state.auth,
		loading: state.loading,
		networkError: state.networkError.networkError,
	};
};

const mapDispatchToProps = (dispatch) => {
	return {
		authRequst: () => dispatch(authActions.authRequest()),
		authSuccess: (userData) => dispatch(authActions.authSuccess(userData)),
		authFailed: (error) => dispatch(authActions.authFailed(error)),
		setNetworkError: () => dispatch(SetNetworkError()),
		loadingOff: () => dispatch(loadingOff()),
	};
};
export default checkRequests(connect(mapStateToProps, mapDispatchToProps)(App));
