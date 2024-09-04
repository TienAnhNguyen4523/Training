using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.UserPermissions;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.UserPermission;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Implementation.UserPermission
{
	public class UserPermissionService : IUserPermissionService
	{
		private readonly IMediator _mediator;
		public UserPermissionService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateUserPermission(CreateUserPermissionCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateUserPermission(UpdateUserPermissionCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteUserPermission(DeleteUserPermissionCommand request)
		{
			return await _mediator.Send(request);
		}

		public async Task<PaginationSet<UserPermissionViewModel>> ListingUserPermission(ListingUserPermissionCommand request)
		{
			return await _mediator.Send(request);
		}
	}
}
