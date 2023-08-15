using MTS.DataAccess.DBContext;
using MTS.DataAccess.Entities;
using MTS.DataAccess.Repository;
using MTS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.RepositoryInterface
{
    public interface IMedicineRepository : IRepository<Medicine, MTSDBContext>
    {
        Task<List<MedicineModel>> GetMedicineList();
        MedicineModel GetMedicineById(int id);
        Task<int> AddMedicine(MedicineModel MedicineModel);
        Task<int> UpdateMedicine(MedicineModel MedicineModel);

        Task<bool> DeleteMedicine(int id);
    }
}
