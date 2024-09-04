using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("UserPermission")]
	public class UserPermission : EntityBase
	{
		public Guid UserId { get; set; }
		public virtual User User { get; set; }
		public string ListPermission { get; set; }
	}
}
