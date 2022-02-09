export const rows = [
	{
		formControls: [
			{
				id: 'userName',
				label: 'Username',
				col: 'col-12 col-md-6',
				required: true,
			},
			{
				id: 'email',
				label: 'Email',
				col: 'col-12 col-md-6',
				required: true,
				type: 'email',
			},
		],
	},
	{
		formControls: [
			{
				id: 'password',
				label: 'Password',
				col: 'col-12 col-md-6',
				required: true,
				type: 'password',
			},
			{
				id: 'confirmPassword',
				label: 'Confirm password',
				col: 'col-12 col-md-6',
				required: true,
				type: 'password',
			},
		],
	},
	{
		formControls: [
			{
				id: 'firstName',
				label: 'First name',
				col: 'col-12 col-md-4',
				required: true,
			},
			{
				id: 'lastName',
				label: 'Last name',
				col: 'col-12 col-md-4',
				required: true,
			},
			{
				id: 'phoneNumber',
				label: 'Phone number',
				col: 'col-12 col-md-4',
				required: true,
				type: 'tel',
			},
		],
	},
	{
		formControls: [
			{
				id: 'street',
				label: 'Street',
				col: 'col-12 col-lg-5',
				required: true,
			},
			{
				id: 'city',
				label: 'City',
				col: 'col-12 col-md-6 col-lg-4',
				required: true,
			},
			{
				id: 'zipCode',
				label: 'Zip code',
				col: 'col-12 col-md-6 col-lg-3',
				required: true,
			},
		],
	},
];
