using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Entity.EntityBase;

namespace Training.Entity.EntityModel
{
	public class EntityBase : IEntityBase
	{
		[Key] 
		public Guid Id { get; set; }
		[Required]
		public bool isActive { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		[Required]
		public Guid CreatedBy { get; set; }
		public Guid? UpdatedBy { get; set; }
		
	}
}
