using MTS.Contracts.Request;
using MTS.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.ServiceInterface
{
    public interface IProductService
    {
        Task<List<ProductResponseModel>> GetProductList();
        Task<int> AddProduct(ProductRequestModel productRequestModel);
        Task<int> UpdateProduct(ProductRequestModel productRequestModel);
    }
}
