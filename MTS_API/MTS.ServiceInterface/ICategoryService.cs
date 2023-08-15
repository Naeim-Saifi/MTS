using MTS.Contracts.Request;
using MTS.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.ServiceInterface
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetCategoryList();
        Task<int> AddCategory(CategoryRequestModel categoryRequestModel);
    }
}
