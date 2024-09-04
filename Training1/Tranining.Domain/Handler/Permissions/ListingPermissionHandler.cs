using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;
using MediatR;
using Tranining.Domain.Command.Permissions;
using Tranining.Domain.Extensions;

namespace Tranining.Domain.Handler.Permissions
{
	public class ListingPermissionHandler : BaseHandler,IRequestHandler<ListingPermissionCommand,PaginationSet<PermissionViewModel>>
	{
		public ListingPermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<PermissionViewModel>> Handle(ListingPermissionCommand request, CancellationToken cancellationToken)
		{

			//var query = _unitOfWork.Permission.GetQueryable()
			//	.Select(x => new PermissionViewModel
			//	{
			//		Id = x.Id,
			//		PermissionId = x.Id,
			//		PermissionName = x.PermissionName
			//	});
			var permissions = _unitOfWork.Permission.GetQueryable();
			var query = from permisson in permissions
						select new PermissionViewModel
						{
							Id = permisson.Id,
							PermissionId = permisson.Id,
							PermissionName = permisson.PermissionName
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
