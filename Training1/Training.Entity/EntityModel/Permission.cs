using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("Permission")]
	public class Permission : EntityBase
	{
		public string PermissionName { get; set; }
	}
}
