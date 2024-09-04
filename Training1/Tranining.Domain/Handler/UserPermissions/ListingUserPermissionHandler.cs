using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Permissions;
using Tranining.Domain.Command.UserPermissions;
using Tranining.Domain.Extensions;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Handler.UserPermissions
{
	public class ListingUserPermissionHandler : BaseHandler,IRequestHandler<ListingUserPermissionCommand,PaginationSet<UserPermissionViewModel>>
	{
		public ListingUserPermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<UserPermissionViewModel>> Handle(ListingUserPermissionCommand request, CancellationToken cancellationToken)
		{

			//var query = _unitOfWork.UserPermission.GetQueryable()
			//	.Select(x => new UserPermissionViewModel
			//	{
			//		Id = x.Id,
			//		UserPermissionId = x.Id,
			//		ListPermission = x.ListPermission,
			//		UserId = x.User.UserId
			//	});
			var userPermissions = _unitOfWork.UserPermission.GetQueryable();
			var query = from userPermission in userPermissions
						select new UserPermissionViewModel
						{
							Id = userPermission.Id,
							UserPermissionId = userPermission.Id,
							ListPermission = userPermission.ListPermission,
							UserId = userPermission.User.UserId
						};
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
