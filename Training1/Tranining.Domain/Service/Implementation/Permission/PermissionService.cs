using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Command.Permissions;
using Tranining.Domain.Service.Interface.Permission;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Implementation.Permission
{
	public class PermissionService : IPermissionService
	{
		private readonly IMediator _mediator;
		public PermissionService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreatePermission(CreatePermissionCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdatePermission(UpdatePermissionCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeletePermission(DeletePermissionCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<PaginationSet<PermissionViewModel>> ListingPermission(ListingPermissionCommand request)
		{
			return await _mediator.Send(request);
		}
	}
}
