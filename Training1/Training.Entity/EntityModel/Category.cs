using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("Category")]
	public class Category : EntityBase
	{
		public string CategoryName { get; set; }
		//public virtual Book Book { get; set; }
	}
}
