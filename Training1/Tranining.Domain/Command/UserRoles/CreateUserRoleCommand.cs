using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.UserRoles
{
	public class CreateUserRoleCommand : IRequest<bool>
	{
		[Required]
		public Guid UserId { get; set; }
		[Required]
		public Guid RoleId { get; set; }
	}
}
