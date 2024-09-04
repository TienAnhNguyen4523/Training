using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.ViewModel.User;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;
using Tranining.Domain.Command.Authors;

namespace Tranining.Domain.Service.Interface.Author
{
	public interface IAuthorService
	{
		Task<bool> CreateAuthor(CreateAuthorCommand request);
		Task<bool> UpdateAuthor(UpdateAuthorCommand request);
		Task<bool> DeleteAuthor(DeleteAuthorCommand request);
		Task<PaginationSet<AuthorViewModel>> Listing(ListingAuthorCommand request);


	}
}
