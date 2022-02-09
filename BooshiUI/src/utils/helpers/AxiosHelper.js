import axios from 'axios';
import { GetToken } from './TokenHelper';

const token = GetToken();

export const api = axios.create({
	baseURL: 'http://localhost:8000',
	headers: {
		'Content-type': 'application/json',
		Authorization: 'Bearer ' + token,
	},
});
