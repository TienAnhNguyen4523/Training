using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.UserPermissions
{
	public class DeleteUserPermissionCommand :IRequest<bool>
	{
		public Guid Id { get; set; }
	}
}
