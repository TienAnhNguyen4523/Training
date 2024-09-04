using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Permissions
{
	public class UpdatePermissionCommand : IRequest<bool>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
