using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Permissions
{
	public class CreatePermissionCommand : IRequest<bool>
	{
		public string PermissionName { get; set; }
	}
}
