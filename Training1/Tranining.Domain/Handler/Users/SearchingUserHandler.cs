using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Extensions;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Handler.Users
{
	public class SearchingUserHandler : UserBaseHandler, IRequestHandler<GetUserSearchingCommand, List<AccountViewmodel>>
	{
		public SearchingUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public async Task<List<AccountViewmodel>> Handle(GetUserSearchingCommand request, CancellationToken cancellationToken)
		{
			var query = _unitOfWork.User.GetQueryable();

			if (!string.IsNullOrEmpty(request.SearchConditions))
			{
				query = query.SearchString(request.SearchConditions);
			}

			var result = await query.Select(x => new AccountViewmodel
			{
				Id = x.Id,
				UserId = x.UserId,
				Password = x.UserDetails.Password,
				Email = x.UserDetails.Email,
				FullName = x.UserDetails.FullName,
				Address = x.UserDetails.Address,
				PhoneNumber = x.UserDetails.Phone,
				Roles = x.UserRoles
					.Select(r => new RoleBViewModel
					{
						Id = r.Id,
						Name = r.Role.RoleName
					}).ToList(),
				BirthOfDate = x.UserDetails.BirthOfDate,
				Gender = x.UserDetails.Gender,
				National = x.UserDetails.National,
				NationalId = x.UserDetails.NationalId
			}).ToListAsync();
			return result;
		}
	}
}
