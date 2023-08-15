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
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IMTSLogger _logger;
        public CategoryService(ICategoryRepository CategoryRepository, IMapper mapper, IMTSLogger logger)
        {

            _categoryRepository = CategoryRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<List<CategoryResponse>> GetCategoryList()
        {
            List<CategoryResponse> categories = null;
            try
            {
                var CategoryList = await _categoryRepository.GetCategoryList();
                categories = _mapper.Map<List<CategoryResponse>>(CategoryList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return categories;
        }
      
        public async Task<int> AddCategory(CategoryRequestModel categoryRequestModel)
        {
            int response = 0;
            try
            {
                var category = _mapper.Map<CategoryModel>(categoryRequestModel);
                response = await _categoryRepository.AddCategory(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return response;
        }
    }
}
