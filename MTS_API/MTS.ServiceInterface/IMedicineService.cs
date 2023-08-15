using MTS.Contracts.Request;
using MTS.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.ServiceInterface
{
    public interface IMedicineService
    {
        Task<List<MedicineResponseModel>> GetMedicineList();
        MedicineResponseModel GetMedicineById(int id);
        Task<int> AddMedicine(MedicineRequestModel medicineRequestModel);
        Task<int> UpdateMedicine(MedicineRequestModel medicineRequestModel);
        Task<bool> DeleteMedicine(int id);
    }
}
