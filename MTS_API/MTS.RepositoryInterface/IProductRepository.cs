using MTS.DataAccess.DBContext;
using MTS.DataAccess.Entities;
using MTS.DataAccess.Repository;
using MTS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.RepositoryInterface
{
    public interface IProductRepository:IRepository<Product, MTSDBContext>
    {
        Task<List<ProductModel>> GetProductList();
        Task<int> AddProduct(ProductModel ProductModel);
        Task<int> UpdateProduct(ProductModel ProductModel);
    }
}
