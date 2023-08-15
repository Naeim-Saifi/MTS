using AutoMapper;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.CommonLibrary.Logger.Implementation;
using MTS.Contracts.Request;
using MTS.Contracts.Response;
using MTS.Models;
using MTS.RepositoryInterface;
using MTS.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTS.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;
        private readonly IMTSLogger _logger;

        public MedicineService(IMedicineRepository MedicineRepository, IMapper mapper, IMTSLogger logger)
        {

            _medicineRepository = MedicineRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<List<MedicineResponseModel>> GetMedicineList()
        {
            List<MedicineResponseModel> medicines = null;
            try
            {
                var MedicineList = await _medicineRepository.GetMedicineList();
                medicines = _mapper.Map<List<MedicineResponseModel>>(MedicineList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return medicines;
        }
        public MedicineResponseModel GetMedicineById(int id)
        {
            MedicineResponseModel medicine = null;
            try
            {
                var result = _medicineRepository.GetMedicineById(id);
                medicine = _mapper.Map<MedicineResponseModel>(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return medicine;
        }
        public async Task<int> AddMedicine(MedicineRequestModel medicineRequestModel)
        {
            int response = 0;
            try
            {
                var medicine = _mapper.Map<MedicineModel>(medicineRequestModel);
                response = await _medicineRepository.AddMedicine(medicine);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return response;
        }
        public async Task<int> UpdateMedicine(MedicineRequestModel medicineRequestModel)
        {
            int response = 0;

            try
            {
                var medicine = _mapper.Map<MedicineModel>(medicineRequestModel);
                response = await _medicineRepository.UpdateMedicine(medicine);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return response;
        }
        public async Task<bool> DeleteMedicine(int id)
        {
            bool result = false;
            try
            {
                result = await _medicineRepository.DeleteMedicine(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return result;
        }

    }
}
