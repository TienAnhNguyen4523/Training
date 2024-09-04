using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.BorrowAndReturnBooks;

namespace Tranining.Domain.Handler.BorrowAndReturnBooks
{
	public class DeleteBorrowAndReturnBookHandler : BaseHandler,IRequestHandler<DeleteBorrowAndReturnBookCommand,bool>
	{
		public DeleteBorrowAndReturnBookHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : base(unitOfWork, httpContext)
		{
		}
		//public DeleteBorrowAndReturnBookHandler(IUnitOfWork unitOfWork, ) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteBorrowAndReturnBookCommand request,CancellationToken cancellationToken)
		{
			var borrowAndReturnBook = await _unitOfWork.BorrowAndReturnBook.GetById(request.Id);
			if (borrowAndReturnBook == null)
			{
				throw new ArgumentException("Not Found");
			}
			var listRole = String.Join(",", GetCurrentRoles());

			if (GetCurrentUserId() != borrowAndReturnBook.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}

			await _unitOfWork.BorrowAndReturnBook.Delete(borrowAndReturnBook);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
