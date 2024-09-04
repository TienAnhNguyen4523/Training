using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Handler.Roles;

namespace Tranining.Domain.Handler.Authors
{
	public class DeleteAuthorHandler : AuthorBaseHandler, IRequestHandler<DeleteAuthorCommand, bool>
	{

		public DeleteAuthorHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
		{
			var CurrentUserId = request.CurrentUserId;
			var roles = request.CurrentRole;
			var listRole = String.Join(",", roles);
			var author = await _unitOfWork.Author.GetOne(x => x.Id == request.Id);
			var book = await _unitOfWork.Book.GetOne(x => x.Author.Id == author.Id);
			if (author == null)
			{
				throw new ArgumentException("Author doesn't exist");
			}
			if (CurrentUserId != author.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			else
			{
				if(book != null)
				{
					_unitOfWork.Book.Delete(book);
					//_unitOfWork.Author.Delete(author);
				}
				_unitOfWork.Author.Delete(author);
			}
			
			return await _unitOfWork.CommitAsync() > 0;
		}

	}
}
