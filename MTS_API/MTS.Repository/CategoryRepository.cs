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
    public class CategoryRepository : Repository<Category, MTSDBContext>, ICategoryRepository
    {
           private readonly IMTSLogger _logger;
    public CategoryRepository(MTSDBContext context, IMapper mapper, IMTSLogger logger) : base(context, mapper)
    {
        _logger = logger;
    }
        public async Task<List<CategoryModel>> GetCategoryList()
        {
            List<CategoryModel> categories = null;
            try
            {
                var CategoryList = await _context.Category.ToListAsync();
                categories = _mapper.Map<List<CategoryModel>>(CategoryList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return categories;
        }
        public async Task<int> AddCategory(CategoryModel categoryModel)
        {
            int response = 0;
            try
            {
                var category = _mapper.Map<Category>(categoryModel);
                await _context.AddAsync(category);
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
