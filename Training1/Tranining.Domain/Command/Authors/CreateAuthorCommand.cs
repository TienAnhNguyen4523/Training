using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Authors
{
	public class CreateAuthorCommand : IRequest<bool>
	{
		[Required]
		public string AuthorName { get; set; }
	}
}
