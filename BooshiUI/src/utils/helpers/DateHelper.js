export const DateToString = (dateString) => {
	const date = new Date(dateString);
	let addZero = (stringToAddZero) => {
		return stringToAddZero.toString().length > 1
			? stringToAddZero.toString()
			: '0' + stringToAddZero.toString();
	};
	const minutes =
		date.getMinutes().toString().length !== 1
			? date.getMinutes()
			: '0' + date.getMinutes().toString();
	const month =
		date.getMinutes().toString().length !== 1
			? date.getMinutes()
			: '0' + date.getMinutes().toString();
	return `${addZero(date.getDate())}/${addZero(
		date.getMonth() + 1,
	)}/${date.getFullYear()} ${addZero(date.getHours())}:${addZero(
		date.getMinutes(),
	)}`;
};
