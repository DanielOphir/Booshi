export const monthLabels = [
	'Jan',
	'Feb',
	'Mar',
	'Apr',
	'May',
	'Jun',
	'Jul',
	'Aug',
	'Sep',
	'Oct',
	'Nov',
	'Dec',
];

export const analyticsOptions = (title, count) => {
	return {
		scales: {
			y: {
				ticks: {
					stepSize: 2,
				},
			},
		},
		plugins: {
			title: {
				display: true,
				text: `${title} - ${count}`,
			},
			legend: {
				display: false,
			},
		},
	};
};
