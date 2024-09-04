using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Roles
{
	public class UpdateRoleCommand : IRequest<bool>
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string RoleName { get; set; }
		[Required]
		public bool IsActive { get; set; }
	}
}
