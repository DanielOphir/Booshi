import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { SetPage } from '../../../redux/paginator/paginatorActions';

const Paginator = ({ pageNum, ...props }) => {
	let pages = [];

	for (let index = 1; index <= props.pages; index++) {
		pages.push(index);
	}

	const onPaginatorClick = (pageNumber) => {
		props.setPage(pageNumber);
		props.onClick(pageNumber);
	};

	useEffect(() => {
		props.setPage(1);
	}, []);

	return (
		<div>
			{props.pages > 0 && (
				<nav>
					<ul className='pagination justify-content-center align-items-end'>
						<li
							id='first'
							className={
								pageNum === pages[0]
									? 'page-item disabled'
									: 'page-item'
							}>
							<button
								className='page-link'
								onClick={() => onPaginatorClick(pages[0])}>
								<i className='fas fa-angle-double-left'></i>
							</button>
						</li>
						<li
							id='previous'
							className={
								pageNum === pages[0]
									? 'page-item disabled'
									: 'page-item'
							}>
							<button
								className='page-link'
								onClick={() => onPaginatorClick(pageNum - 1)}>
								<i className='fas fa-angle-left'></i>
							</button>
						</li>
						{pages.map((page) => {
							if (pages.length > 5) {
								if (
									(pageNum < 4 && page <= 5) ||
									(pageNum > pages.length - 2 &&
										page >= pages.length - 4) ||
									(page >= pageNum - 2 && page <= pageNum + 2)
								) {
									return (
										<li
											key={page}
											className={
												page === pageNum
													? 'page-item active'
													: 'page-item'
											}>
											<button
												className='page-link'
												onClick={() =>
													onPaginatorClick(page)
												}>
												{page}
											</button>
										</li>
									);
								}
							} else {
								return (
									<li
										key={page}
										className={
											page === pageNum
												? 'page-item active'
												: 'page-item'
										}>
										<button
											className='page-link'
											onClick={() =>
												onPaginatorClick(page)
											}>
											{page}
										</button>
									</li>
								);
							}
						})}
						<li
							id='next'
							className={
								pageNum === pages[pages.length - 1]
									? 'page-item disabled'
									: 'page-item'
							}>
							<button
								className='page-link'
								onClick={() => onPaginatorClick(pageNum + 1)}>
								<i className='fas fa-angle-right'></i>
							</button>
						</li>
						<li
							id='last'
							className={
								pageNum === pages[pages.length - 1]
									? 'page-item disabled'
									: 'page-item'
							}>
							<button
								className='page-link'
								onClick={() =>
									onPaginatorClick(pages[pages.length - 1])
								}>
								<i className='fas fa-angle-double-right'></i>
							</button>
						</li>
					</ul>
				</nav>
			)}
		</div>
	);
};

const mapStateToProps = (state, props) => {
	return {
		pageNum: state.paginator.page,
		props: props,
	};
};

const mapDispatchToProps = (dispatch) => {
	return {
		setPage: (pageNum) => dispatch(SetPage(pageNum)),
	};
};
export default connect(mapStateToProps, mapDispatchToProps)(Paginator);
