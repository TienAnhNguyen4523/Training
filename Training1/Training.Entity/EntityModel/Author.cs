using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("Author")]
	public class Author : EntityBase
	{
		public string AuthorName { get; set; }
		//public virtual Book Book { get; set; }
	}
}
