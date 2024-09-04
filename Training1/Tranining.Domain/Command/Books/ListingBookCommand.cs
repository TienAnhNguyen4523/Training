using AutoMapper.Configuration.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Command.Books
{
	public class ListingBookCommand : IRequest<PaginationSet<BookViewModel>>
	{
		public List<SearchObjForCondition>? SearchString { get; set; }
		public List<SearchObjForCondition>? Filter { get; set; }
		public SearchObjForCondition? Sort { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		[JsonIgnore]
		public List<string> UserRoles { get; set; } = new List<string>();
	}
}
