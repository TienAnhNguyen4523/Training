using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.UserPermissions;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Interface.UserPermission
{
	public interface IUserPermissionService
	{
		Task<bool> CreateUserPermission(CreateUserPermissionCommand request);
		Task<bool> UpdateUserPermission(UpdateUserPermissionCommand request);
		Task<bool> DeleteUserPermission(DeleteUserPermissionCommand request);
		Task<PaginationSet<UserPermissionViewModel>> ListingUserPermission(ListingUserPermissionCommand request);
	}
}
