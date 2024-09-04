using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Handler.Users;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Extensions;

namespace Tranining.Domain.Handler.Roles
{
	public class ListingRoleHandler : RoleBaseHandler, IRequestHandler<GetRolePaginationCommand, PaginationSet<RoleBViewModel>>
	{
		public ListingRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<RoleBViewModel>> Handle(GetRolePaginationCommand request, CancellationToken cancellationToken)
		{

			var roles = _unitOfWork.Role.GetQueryable();
			//var query = _unitOfWork.Role.GetQueryable()
			//	.Select(x => new RoleBViewModel
			//	{
			//		Id = x.Id,
			//		Name = x.RoleName
			//	}); 
			var query = from role in roles
						select new RoleBViewModel
						{
							Id = role.Id,
							Name = role.RoleName
						};
			if (request.FilterRole != null)
			{
				query = query.Filter(request.FilterRole);
				

			}
			if (request.SearchRole != null)
			{
				query = query.Search(request.SearchRole);
			}

			var result = await query.Pagination(request.PageIndex, request.PageSize, request.Sort);
			return result;
		}
	}
}
