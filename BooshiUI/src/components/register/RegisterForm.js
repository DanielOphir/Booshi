import * as yup from 'yup';

const requiredError = 'This field is required.';

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

export const schema = yup.object().shape({
	userName: yup
		.string()
		.required(requiredError)
		.min(1, 'Username must be between 4-12 characters')
		.max(12, 'Username must be between 4-12 characters'),
	email: yup.string().required(requiredError).email('The email is not valid'),
	password: yup
		.string()
		.required(requiredError)
		.min(1, 'Password must be between 6-16 characters')
		.max(16, 'Password must be between 6-16 characters'),
	confirmPassword: yup
		.string()
		.required(requiredError)
		.oneOf([yup.ref('password'), null], 'Passwords must match'),
	firstName: yup.string().required(requiredError),
	lastName: yup.string().required(requiredError),
	phoneNumber: yup
		.string()
		.required(requiredError)
		.matches(
			/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/,
			'The phone number is not valid',
		),
	street: yup.string().required(requiredError),
	city: yup.string().required(requiredError),
	zipCode: yup.string().required(requiredError),
});
