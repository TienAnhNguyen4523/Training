using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Categories
{
	public class DeleteCategoryCommand : IRequest<bool>
	{
		[Required]
		public Guid Id { get; set; }
		[JsonIgnore]
		public Guid CurrentUserId { get; set; }
		[JsonIgnore]
		public List<string> CurrentRole { get; set; } = new List<string>();

	}
}
