using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Users
{
	public class UpdateUserCommand : IRequest<bool>
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string UserId { get; set; }
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public DateTime BirthOfDate { get; set; }
		[Required]
		public int NationalId { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Address { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }
		[Required]
		public string Gender { get; set; }
		[Required]
		public string National { get; set; }
		[Required]
		public bool IsActive { get; set; }
	}
}
