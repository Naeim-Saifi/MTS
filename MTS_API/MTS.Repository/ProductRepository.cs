using MTS.CommonLibrary.Logger.Abstraction;
using MTS.CommonLibrary.Logger.Implementation;
using AutoMapper;
using MTS.DataAccess.DBContext;
using MTS.DataAccess.Entities;
using MTS.DataAccess.Repository;
using MTS.RepositoryInterface;
using System;
using MTS.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MTS.Repository
{
    public class ProductRepository : Repository<Product, MTSDBContext>, IProductRepository
    {
        private readonly IMTSLogger _logger;
        public ProductRepository(MTSDBContext context, IMapper mapper, IMTSLogger logger) : base(context, mapper)
        {
            _logger = logger;
        }
        public async Task<List<ProductModel>> GetProductList()
        {
            List<ProductModel> products = null;

            try
            {
                var ProductList = await _context.Product.ToListAsync();
                products = _mapper.Map<List<ProductModel>>(ProductList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return products;
        }
        public async Task<int> AddProduct(ProductModel productModel)
        {
            int response = 0;
            try
            {
                var Product =_mapper.Map<Product>(productModel);
                await _context.AddAsync(Product);
                response = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }

            return response;
        }
        public async Task<int> UpdateProduct(ProductModel productModel)
        {
            int response = 0;
            try
            {
                var Product = _mapper.Map<Product>(productModel);
                await Task.Run(() => _context.Update(Product));
                response = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }

            return response;
        }
    }
}
