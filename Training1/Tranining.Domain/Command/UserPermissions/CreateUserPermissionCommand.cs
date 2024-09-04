using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.UserPermissions
{
	public class CreateUserPermissionCommand : IRequest<bool>
	{
		public Guid UserId { get; set; }
		public string ListPermission { get; set; }
		[JsonIgnore]
		public List<string> ListRole { get; set;} = new List<string>();
	}
}
