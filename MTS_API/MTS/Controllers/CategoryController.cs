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
    
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMTSLogger _logger;
        public CategoryController(ICategoryService CategoryService, IMTSLogger logger)
        {
            _logger = logger;
            _categoryService = CategoryService;
        }
        [HttpGet]
        [ActionName("CategoryList")]
        public async Task<Response> GetCategoryList()
        {
            Response response = new Response();
            try
            {
                var data = await Task.Run(() => _categoryService.GetCategoryList()).ConfigureAwait(true);
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
        public async Task<Response> AddCategory(CategoryRequestModel categoryRequestModel)
        {
            Response response = new Response();

            try
            {
                var res = await _categoryService.AddCategory(categoryRequestModel);
                if (res > 0)
                {
                    response.Data = categoryRequestModel;
                    response.IsError = false;
                    response.Message = "Record successfully added";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.Data = categoryRequestModel;
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
