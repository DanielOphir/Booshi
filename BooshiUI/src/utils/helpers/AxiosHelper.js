import axios from 'axios';
import { GetToken } from './TokenHelper';
import config from '../../config.json';

const token = GetToken();

// Configuring axios for all of the application.
export const api = axios.create({
	baseURL: config.apiUrl,
	headers: {
		'Content-type': 'application/json',
		Authorization: 'Bearer ' + token,
	},
});
