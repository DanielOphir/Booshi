import React from 'react';
import { useState } from 'react';
import ReactDatePicker from 'react-datepicker';
import '../../../../node_modules/react-datepicker/dist/react-datepicker.css';
import PrimaryButton from '../../../components/design/buttons/PrimaryButton';
import { Bar } from 'react-chartjs-2';
import { api } from '../../../utils/helpers/AxiosHelper';

const Reports = () => {
	const [loading, setLoading] = useState(false);
	const [fromDate, setFromDate] = useState(null);
	const [toDate, setToDate] = useState(null);
	const [datasetData, setDatasetData] = useState([]);

	const labels = ['Pending', 'In process', 'Completed', 'Cancelled'];
	const data = {
		labels: labels,
		datasets: [
			{
				data: datasetData,
				backgroundColor: [
					'rgba(54, 162, 235, 0.2)',
					'rgba(255, 159, 64, 0.2)',
					'rgba(75, 192, 192, 0.2)',
					'rgba(255, 99, 132, 0.2)',
				],
				borderColor: [
					'rgb(54, 162, 235)',
					'rgb(255, 205, 86)',
					'rgb(75, 192, 192)',
					'rgb(255, 99, 132)',
				],
				borderWidth: 1,
			},
		],
	};
	const options = {
		scales: {
			y: {
				ticks: {
					stepSize: 1,
				},
			},
		},
		plugins: {
			title: {
				display: true,
				text: 'Deliveries',
			},
			legend: {
				display: false,
			},
		},
	};

	const onToDateChange = (date) => {
		date.setHours(23, 59, 59, 999);
		setToDate(date);
    }

	const onSubmit = (event) => {
		event.preventDefault();
		setLoading(true);
		api.post('/api/analytics/reports', JSON.stringify({ fromDate, toDate }))
			.then((response) => {
				setDatasetData(response.data);
				setLoading(false);
			})
			.catch((error) => {
				setDatasetData([]);
				setLoading(false);
			});
	};

	return (
		<div className='container mt-3'>
			<div className='row justify-content-center'>
				<div className='col-8 col-md-12 col-lg-11 col-xl-9'>
					<div className='h3'>Deliveries reports</div>
				</div>
			</div>
			<form
				onSubmit={onSubmit}
				action=''
				className='form-row justify-content-center align-items-center'>
				<div className='col-8 col-md-6 col-lg-5 col-xl-4 form-group'>
					<label htmlFor='from'>From:</label>
					<ReactDatePicker
						id='from'
						selected={fromDate}
						placeholderText='Select a date'
						onChange={(date) => setFromDate(date)}
						maxDate={new Date()}
						showYearDropdown
						dateFormat='dd/MM/yyyy'
						className='form-control date-picker'></ReactDatePicker>
				</div>
				<div className='col-8 col-md-6 col-lg-5 col-xl-4 form-group'>
					<label htmlFor='to'>To:</label>
					<ReactDatePicker
						selected={toDate}
						placeholderText='Select a date'
						onChange={(date) => onToDateChange(date)}
						maxDate={new Date()}
						minDate={fromDate}
						disabled={!fromDate}
						dateFormat='dd/MM/yyyy'
						className='form-control'></ReactDatePicker>
				</div>
				<div className='col-5 col-lg-auto text-center mt-3'>
					<PrimaryButton
						className='py-2 btn-block'
						disabled={loading || !fromDate || !toDate}>
						{loading && (
							<span>
								<span
									className='spinner-border spinner-border-sm'
									role='status'
									aria-hidden='true'></span>{' '}
							</span>
						)}
						{'Submit'}
					</PrimaryButton>
				</div>
			</form>
			<div className='row mt-3 px-3'>
				{!loading && datasetData.length > 0 && (
					<Bar data={data} options={options} className='col-12'></Bar>
				)}
			</div>
		</div>
	);
};

export default Reports;
