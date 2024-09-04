using MediatR;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Extensions;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;


namespace Tranining.Domain.Handler.Users
{
	public class ListingUserHandler : UserBaseHandler,IRequestHandler<GetUserPaginationCommand,PaginationSet<AccountViewmodel>>
	{
		public ListingUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<AccountViewmodel>> Handle(GetUserPaginationCommand request, CancellationToken cancellationToken)
		{
			var Roles = _unitOfWork.Role.GetQueryable();
			var UserRoles = _unitOfWork.UserRole.GetQueryable();
			var Users = _unitOfWork.User.GetQueryable();
			var query = from user in Users
						join userRole in
							(from ur in UserRoles
							 select ur.UserId).Distinct()
						on user.Id equals userRole
						select new AccountViewmodel
						{
							Id = user.Id,
							UserId = user.UserId,
							Password = user.UserDetails.Password,
							Email = user.UserDetails.Email,
							FullName = user.UserDetails.FullName,
							Address = user.UserDetails.Address,
							PhoneNumber = user.UserDetails.Phone,
							Roles = (from role in Roles
									 join userRoles in UserRoles on role.Id equals userRoles.RoleId into RoleDefault
									 from roleDefault in RoleDefault.DefaultIfEmpty()
									 where roleDefault.UserId == user.Id
									 select new RoleBViewModel
									 {
										 Id = role.Id,
										 Name = role.RoleName
									 }).ToList(),
							BirthOfDate = user.UserDetails.BirthOfDate,
							Gender = user.UserDetails.Gender,
							National = user.UserDetails.National,
							NationalId = user.UserDetails.NationalId,
							Permissions = user.UserPermission.ListPermission
						};

			//var query = _unitOfWork.User.GetQueryable()
			//	.Select(x => new AccountViewmodel
			//	{
			//		Id = x.Id,
			//		UserId = x.UserId,
			//		Password = x.UserDetails.Password,
			//		Email = x.UserDetails.Email,
			//		FullName = x.UserDetails.FullName,
			//		Address = x.UserDetails.Address,
			//		PhoneNumber = x.UserDetails.Phone,
			//		Roles = x.UserRoles
			//		.Select(r => new RoleBViewModel
			//		{
			//			Id = r.Id,
			//			Name = r.Role.RoleName
			//		}).ToList(),
			//		BirthOfDate = x.UserDetails.BirthOfDate,
			//		Gender = x.UserDetails.Gender,
			//		National = x.UserDetails.National,
			//		NationalId = x.UserDetails.NationalId,
			//		Permissions = x.UserPermission.ListPermission

			//	});
			if (request.Filter != null)
			{
				query = query.Filter(request.Filter);
			}
			if (request.SearchString != null)
			{
				query = query.Search(request.SearchString);
			}

			var result = await query.Pagination(request.PageIndex, request.PageSize, request.Sort);
			return result;
		}
	}

}
