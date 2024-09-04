using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Implementation;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Roles;

namespace Tranining.Domain.Handler.Authors
{
	public class CreateAuthorHandler : AuthorBaseHandler, IRequestHandler<CreateAuthorCommand,bool>
	{
		public CreateAuthorHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
		{
			var check = await _unitOfWork.Author.CheckExist(x => x.AuthorName == request.AuthorName);
			if(check)
			{
				throw new ArgumentException("Already Exist");
			}

			var id = Guid.NewGuid();
			var author = new Author
			{
				Id = id,
				AuthorName = request.AuthorName
				
			};
			_unitOfWork.Author.Insert(author);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
