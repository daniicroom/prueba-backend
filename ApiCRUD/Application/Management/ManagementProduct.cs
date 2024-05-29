using Application.Interface;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Management
{
    public class ManagementProduct: IManagementProduct
    {
        private readonly IProductRepository _productRepository;
        public ManagementProduct(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
