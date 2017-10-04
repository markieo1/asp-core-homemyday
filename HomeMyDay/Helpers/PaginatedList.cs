using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Helpers
{
	/// <summary>
	/// List with support for pagination.
	/// Source: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page#add-paging-functionality-to-the-students-index-page
	/// </summary>
	public class PaginatedList<T> : List<T>
	{
		/// <summary>
		/// Gets the current index
		/// </summary>
		public int PageIndex { get; private set; }

		/// <summary>
		/// Gets the total pages.
		/// </summary>
		public int TotalPages { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="count">The count.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);

			this.AddRange(items);
		}

		/// <summary>
		/// Gets a value indicating whether this instance has previous page.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
		/// </value>
		public bool HasPreviousPage
		{
			get
			{
				return (PageIndex > 1);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has next page.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
		/// </value>
		public bool HasNextPage
		{
			get
			{
				return (PageIndex < TotalPages);
			}
		}

		/// <summary>
		/// Creates the asynchronous.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex = 1, int pageSize = 10)
		{
			int count = await source.CountAsync();
			List<T> items = await source
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize).ToListAsync();

			return new PaginatedList<T>(items, count, pageIndex, pageSize);
		}
	}
}
