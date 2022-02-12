// Turning date time that we get from the server into a readable format
export const DateToString = (dateString) => {
	const date = new Date(dateString);
	let addZero = (stringToAddZero) => {
		return stringToAddZero.toString().length > 1
			? stringToAddZero.toString()
			: '0' + stringToAddZero.toString();
	};
	return `${addZero(date.getDate())}/${addZero(
		date.getMonth() + 1,
	)}/${date.getFullYear()} ${addZero(date.getHours())}:${addZero(
		date.getMinutes(),
	)}`;
};
