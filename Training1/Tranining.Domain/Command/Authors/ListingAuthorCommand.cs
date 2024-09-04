using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Command.Authors
{
	public class ListingAuthorCommand : IRequest<PaginationSet<AuthorViewModel>>
	{
		public List<SearchObjForCondition>? SearchString { get; set; }
		public List<SearchObjForCondition>? Filter { get; set; }
		public SearchObjForCondition? Sort { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		
	}

}
