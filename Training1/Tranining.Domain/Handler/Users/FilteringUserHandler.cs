using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Extensions;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Handler.Users
{
	public class FilteringUserHandler : UserBaseHandler, IRequestHandler<GetUserFilteringCommand, List<AccountViewmodel>>
	{
		public FilteringUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<List<AccountViewmodel>> Handle(GetUserFilteringCommand request,CancellationToken cancellationToken)
		{
			var query = _unitOfWork.User.GetQueryable()
				.Select(x => new AccountViewmodel
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
				});
			if (request.FilterConditions != null )
			{
				query = query.Filter(request.FilterConditions);
			}

			var result = await query.ToListAsync();
			return result;
		}
	}
}
