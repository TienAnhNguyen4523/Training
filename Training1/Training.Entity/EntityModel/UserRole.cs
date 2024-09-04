using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("UserRole")]
	public class UserRole : EntityBase
	{
		[Required]
		public Guid UserId { get; set; }
		[Required]
		public Guid RoleId { get; set; }
		public virtual User User { get; set; }
		public virtual Role Role { get; set; }
	}
}
