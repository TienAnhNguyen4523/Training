using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Interface.Role
{
	public interface IRoleServices
	{
		Task<bool> CreateRole(CreateRoleCommand request);
		Task<bool> DeleteRole(DeleteRoleCommand request);
		Task<bool> UpdateRole(UpdateRoleCommand request);

		Task<PaginationSet<RoleBViewModel>> ListingRole(GetRolePaginationCommand request);
	}
}
