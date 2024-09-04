using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.UserRoles;

namespace Tranining.Domain.Service.Interface.UserRole
{
	public interface IUserRoleService
	{
		Task<bool> CreateUserRole(CreateUserRoleCommand request);
		Task<bool> UpdateUserRole(UpdateUserRoleCommand request);
		Task<bool> DeleteUserRole(DeleteUserRoleCommand request);
	}
}
