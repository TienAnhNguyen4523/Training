using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.ProductRoles;

namespace Tranining.Domain.Service.Interface.IProductRoleService
{
	public interface IProductRoleService
	{
		Task<bool> CreateProductRole(CreateProductRoleCommand request);
		Task<bool> DeleteProductRole(DeleteProductRoleCommand request);
		Task<bool> UpdateProductRole(UpdateProductRoleCommand request);
	}
}
