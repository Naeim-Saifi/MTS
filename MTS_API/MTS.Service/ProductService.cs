using AutoMapper;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.Contracts.Request;
using MTS.Contracts.Response;
using MTS.Models;
using MTS.RepositoryInterface;
using MTS.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Service
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMTSLogger _logger;
        public ProductService(IProductRepository ProductRepository, IMapper mapper, IMTSLogger logger)
        {

            _productRepository = ProductRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<List<ProductResponseModel>> GetProductList()
        {
            List<ProductResponseModel> product = null;
            try
            {
                var ProductList = await _productRepository.GetProductList();
                product = _mapper.Map<List<ProductResponseModel>>(ProductList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return product;
        }
        public async Task<int> AddProduct(ProductRequestModel productRequestModel)
        {
            int response = 0;
            try
            {
                var product = _mapper.Map<ProductModel>(productRequestModel);
                response = await _productRepository.AddProduct(product);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return response;
        }
        public async Task<int> UpdateProduct(ProductRequestModel productRequestModel)
        {
            int response = 0;

            try
            {
                var product = _mapper.Map<ProductModel>(productRequestModel);
                response = await _productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return response;
        }
    }
}
