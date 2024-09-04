using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Books;

namespace Tranining.Domain.Handler.Books
{
	public class DeleteBookHandler : BaseHandler,IRequestHandler<DeleteBookCommand,bool>
	{
		public DeleteBookHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteBookCommand request,CancellationToken cancellationToken)
		{
			var CurrentUserId = request.CurrentUserId;
			var roles = request.CurrentRole;
			var listRole = String.Join(",", roles);
			var book = await _unitOfWork.Book.GetOne(x => x.Id == request.Id);
			if(book == null)
			{
				throw new ArgumentException("Not Found");
			}
			if (CurrentUserId != book.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			await _unitOfWork.Book.Delete(book);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
