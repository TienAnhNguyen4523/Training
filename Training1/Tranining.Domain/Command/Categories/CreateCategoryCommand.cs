using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Categories
{
	public class CreateCategoryCommand : IRequest<bool>
	{
		[Required]
		public string Name { get; set; }
	}
}
