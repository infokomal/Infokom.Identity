using Infokom.Identity.App.Models;
using Infokom.Identity.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infokom.Identity.App.Queries
{

	public abstract record Request<TResponse> : IRequest<TResponse>
	{

	}


	public class SearchQueryRequest<TItem> : IRequest<SearchQueryResponse<TItem>>
	{
		public SearchQueryRequest(int shift = 0, int limit = 25)
		{
			this.Shift = shift;
			this.Limit = limit;
		}

		public int Shift { get; }

		public int Limit { get; }
	}

	public class SearchQueryResponse<TItem>
	{
		public int TotalCount { get; set; }

		public List<TItem> Items { get; set; } = new List<TItem>();
	}








	public class UserSearchQueryRequest : SearchQueryRequest<UserInfo>
	{
		public UserSearchQueryRequest(string userName, int shift = 0, int limit = 25) : base(shift, limit)
		{
			ArgumentNullException.ThrowIfNull(userName, nameof(userName));

			this.UserName = userName;
		}

		public string UserName { get; }
	}


	internal class UserSearchQueryHandler : IRequestHandler<UserSearchQueryRequest, SearchQueryResponse<UserInfo>>
	{
		private readonly UserManager<User> _userManager;

		public UserSearchQueryHandler(UserManager<User> userManager)
		{
			ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

			_userManager = userManager;
		}


		public async Task<SearchQueryResponse<UserInfo>> Handle(UserSearchQueryRequest request, CancellationToken cancellationToken)
		{
			var query = _userManager.Users;

			if(!string.IsNullOrWhiteSpace(request.UserName))
			{
				query = query.Where(x => EF.Functions.Like(x.UserName, $"{request.UserName}%"));
			}

			query = query.OrderBy(x => x.UserName);

			var totalCount = await query.CountAsync();

			query = query
				.Skip(request.Shift)
				.Take(request.Limit);


			var items = await query.Select(x => new UserInfo()
			{
				Name = x.UserName,
				Email = new ()
				{
					Address = x.Email ?? string.Empty,
					IsConfirmed = x.EmailConfirmed
				},
				Phone = new ()
				{
					Number = x.PhoneNumber,
					IsConfirmed = x.PhoneNumberConfirmed
				},
				IsLocked = x.LockoutEnabled,
				Has2FA = x.TwoFactorEnabled
			}).ToListAsync(cancellationToken);

			return new SearchQueryResponse<UserInfo>
			{
				TotalCount = totalCount,
				Items = items
			};


		}
	}
}
