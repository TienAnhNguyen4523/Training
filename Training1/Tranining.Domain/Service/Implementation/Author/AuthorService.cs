using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.Author;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Implementation.Author
{
	public class AuthorService : IAuthorService
	{
		private readonly IMediator _mediator;
		public AuthorService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateAuthor(CreateAuthorCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateAuthor(UpdateAuthorCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteAuthor(DeleteAuthorCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<PaginationSet<AuthorViewModel>> Listing(ListingAuthorCommand request)
		{
			try
			{
				return await _mediator.Send(request);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.ToString());
			}
		}
	}
}
