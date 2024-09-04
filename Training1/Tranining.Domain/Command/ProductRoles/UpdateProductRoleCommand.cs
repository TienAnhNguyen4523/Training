using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.ProductRoles
{
	public class UpdateProductRoleCommand : IRequest<bool>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string ListRole { get; set; }
	}

}
