using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Permissions
{
	public class DeletePermissionCommand : IRequest<bool>
	{
		public Guid Id { get; set; }
	}
}
