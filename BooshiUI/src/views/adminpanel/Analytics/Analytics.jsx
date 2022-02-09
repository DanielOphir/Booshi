import React, { useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2';
import { api } from '../../../utils/helpers/AxiosHelper';
import './Analytics.css';
import { monthLabels, analyticsOptions } from './AnalyticsConsts';
import * as lodash from 'lodash';

const Analytics = () => {
	const [datasetsData, setDatasetsData] = useState({});
	const [totalDeliveries, setTotalDeliveries] = useState(0);
	const [totalUsers, setTotalUsers] = useState(0);

	const usersAnalyticsData = {
		labels: monthLabels,
		datasets: [
			{
				label: 'New users',
				data: datasetsData?.newUsers,
				backgroundColor: 'rgb(255, 99, 132)',
				borderColor: 'rgb(255, 99, 132)',
				borderWidth: 2,
			},
		],
	};

	const deliveriesAnalyticsData = {
		labels: monthLabels,
		datasets: [
			{
				label: 'New deliveries',
				data: datasetsData?.deliveries,
				backgroundColor: 'rgba(80, 80, 80, 1)',
				borderColor: 'rgba(80, 80, 80, 1)',
				borderWidth: 2,
			},
		],
	};
	useEffect(() => {
		api.get('/api/analytics').then((response) => {
			setDatasetsData(response.data);
			setTotalUsers(lodash.sum(response.data?.newUsers));
			setTotalDeliveries(lodash.sum(response.data?.deliveries));
		});
	}, []);

	return (
		<div className='container-lg my-5 my-sm-0'>
			<div className='row align-items-center justify-content-center my-3'>
				<div className='col-12 col-md-10 col-lg-8 text-center'>
					<h3>Users analytics</h3>
					<h3>{new Date(Date.now()).getFullYear()}</h3>
					<Line
						className='analytics'
						data={usersAnalyticsData}
						options={analyticsOptions(
							'New users',
							totalUsers,
						)}></Line>
				</div>
			</div>
			<div className='row align-items-center justify-content-center my-5'>
				<div className='col-12 col-md-10 col-lg-8 text-center'>
					<h3>Deliveries analytics</h3>
					<h3>{new Date(Date.now()).getFullYear()}</h3>

					<Line
						className='analytics'
						data={deliveriesAnalyticsData}
						options={analyticsOptions(
							'New deliveries',
							totalDeliveries,
						)}></Line>
				</div>
			</div>
		</div>
	);
};

export default Analytics;
