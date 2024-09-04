using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Handler.Roles;

namespace Tranining.Domain.Handler.Authors
{
	public class UpdateAuthorHandler : AuthorBaseHandler, IRequestHandler<UpdateAuthorCommand, bool>
	{
		public UpdateAuthorHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
		{
			var CurrentUserId = request.CurrentUserId;
			var roles = request.CurrentRole;
			var listRole = String.Join(",", roles);
			var author = await _unitOfWork.Author.GetOne(x => x.Id == request.Id);
			if (author == null)
			{
				throw new ArgumentNullException("Author doesn't exist");
			}
			if (CurrentUserId != author.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			else
			{
				author.AuthorName = request.Name;
			}
			_unitOfWork.Author.Update(author);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
