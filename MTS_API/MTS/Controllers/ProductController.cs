using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.Contracts.Request;
using MTS.Contracts.Response;
using MTS.ServiceInterface;
using System;
using System.Threading.Tasks;

namespace MTS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
   
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMTSLogger _logger;
        public ProductController(IProductService ProductService, IMTSLogger logger)
        {
            _logger = logger;
            _productService = ProductService;
        }
        [HttpGet]
        [ActionName("ProductList")]
        public async Task<Response> GetProductList()
        {
            Response response = new Response();
            try
            {
                var data = await Task.Run(() => _productService.GetProductList()).ConfigureAwait(true);
                if (data.Count > 0)
                {
                    response.Data = data;
                    response.IsError = false;
                    response.Message = data.Count + " records successfully loaded";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.IsError = false;
                    response.Message = "records not found";
                    response.ErrorCode = 400;
                }

            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message.ToString();
                response.ErrorCode = 500;
            }

            return response;
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<Response> AddProduct(ProductRequestModel productRequestModel)
        {
            Response response = new Response();

            try
            {
                var res = await _productService.AddProduct(productRequestModel);
                if (res >0)
                {
                    response.Data = productRequestModel;
                    response.IsError = false;
                    response.Message = "Record successfully added";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.Data = productRequestModel;
                    response.IsError = false;
                    response.Message = "Something went wrong";
                    response.ErrorCode = 400;
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message.ToString();
                response.ErrorCode = 500;
            }

            return response;
        }


        [HttpPost]
        [ActionName("Update")]
        public async Task<Response> UpdateProduct(ProductRequestModel productRequestModel)
        {
            Response response = new Response();

            try
            {
                var res = await _productService.UpdateProduct(productRequestModel);
                if (res > 0)
                {
                    response.Data = productRequestModel;
                    response.IsError = false;
                    response.Message = "Record successfully updated";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.Data = productRequestModel;
                    response.IsError = false;
                    response.Message = "Something went wrong";
                    response.ErrorCode = 400;
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message.ToString();
                response.ErrorCode = 500;
            }

            return response;
        }
    }
}