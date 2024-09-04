using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.Role;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Implementation.Role
{
	public class RoleServices : IRoleServices
	{
		private readonly IMediator _mediator;
		public RoleServices(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateRole(CreateRoleCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteRole(DeleteRoleCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateRole(UpdateRoleCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<PaginationSet<RoleBViewModel>> ListingRole(GetRolePaginationCommand request)
		{
			try
			{
				return await _mediator.Send(request);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.ToString());
			}
		}
	}
}
