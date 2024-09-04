using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("UserDetail")]
	public class UserDetail : EntityBase
	{
		[Required]
		public Guid UserId { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string FullName { get; set; }
		[Required]
		public DateTime BirthOfDate { get; set; }
		[Required]
		public int NationalId { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Address { get; set; }
		public string Password { get; set; }
		public string Phone { get;set; }
		[Required]
		public string Gender { get; set; }
		[Required]
		public string National { get; set; }
		public virtual User User { get; set; }
	}
}
