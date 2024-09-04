using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.ViewModel.User
{
	public class PermissionViewModel
	{
		public Guid Id { get; set; }
		public Guid PermissionId { get; set; }
		public string PermissionName { get; set; }
	}
	public class UserPermissionViewModel
	{
		public Guid Id { get; set; }
		public Guid	UserPermissionId { get; set; }
		public string ListPermission { get; set; }
		public string UserId { get; set; }
	}
	
}
