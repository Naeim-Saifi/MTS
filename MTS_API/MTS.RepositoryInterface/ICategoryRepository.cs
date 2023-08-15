using MTS.DataAccess.DBContext;
using MTS.DataAccess.Entities;
using MTS.DataAccess.Repository;
using MTS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.RepositoryInterface
{
    public interface ICategoryRepository : IRepository<Category,MTSDBContext>
    {
        Task<List<CategoryModel>> GetCategoryList();
        Task<int> AddCategory(CategoryModel categoryModel);

    }
}
