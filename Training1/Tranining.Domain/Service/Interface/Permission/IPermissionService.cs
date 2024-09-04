using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Command.Permissions;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Interface.Permission
{
	public interface IPermissionService
	{
		Task<bool> CreatePermission(CreatePermissionCommand request);
		Task<bool> UpdatePermission(UpdatePermissionCommand request);
		Task<bool> DeletePermission(DeletePermissionCommand request);
		Task<PaginationSet<PermissionViewModel>> ListingPermission(ListingPermissionCommand request);

	}
}
