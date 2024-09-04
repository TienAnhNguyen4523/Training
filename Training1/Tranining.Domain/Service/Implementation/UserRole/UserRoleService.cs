using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.UserRoles;
using Tranining.Domain.Service.Interface.UserRole;

namespace Tranining.Domain.Service.Implementation.UserRole
{
	public class UserRoleService : IUserRoleService
	{
		private readonly IMediator _mediator;
		public UserRoleService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateUserRole(CreateUserRoleCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateUserRole(UpdateUserRoleCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteUserRole(DeleteUserRoleCommand request)
		{
			return await _mediator.Send(request);
		}
	}
}
