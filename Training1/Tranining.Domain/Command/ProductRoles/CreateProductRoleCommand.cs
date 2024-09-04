using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.ProductRoles
{
	public class CreateProductRoleCommand : IRequest<bool>
	{
		public string BookName { get; set; }
		public string ListRole { get; set; }

		
	}
}
