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
    public class MedicineRepository : Repository<Medicine, MTSDBContext>, IMedicineRepository
    {
        private readonly IMTSLogger _logger;
        public MedicineRepository(MTSDBContext context, IMapper mapper, IMTSLogger logger) : base(context, mapper)
        {
            _logger = logger;
        }

        public async Task< List<MedicineModel>> GetMedicineList()
        {
            List<MedicineModel> medicines = null;

            try
            {
                var MedicineList = await _context.Medicine.ToListAsync();
                medicines = _mapper.Map<List<MedicineModel>>(MedicineList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return medicines;
        }
        public MedicineModel GetMedicineById(int id)
        {
            MedicineModel medicine = null;

            try
            {
                var result = _context.Medicine.Where(a => a.Id == id);
                medicine = _mapper.Map<MedicineModel>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return medicine;
        }
        public async Task<int> AddMedicine(MedicineModel medicineModel)
        {
            int response = 0;
            try
            {
                var Medicine = _mapper.Map<Medicine>(medicineModel);
                await _context.AddAsync(Medicine);
                response = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }

            return response;
        }
        public async Task<int> UpdateMedicine(MedicineModel medicineModel)
        {
            int response = 0;
            try
            {
                var Medicine = _mapper.Map<Medicine>(medicineModel);
                await Task.Run(() => _context.Update(Medicine));
                response = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }

            return response;
        }
        public async Task<bool> DeleteMedicine(int medicineid)
        {
            bool isDeleted = false;
            var medicine = _context.Medicine.Where(a => a.Id == medicineid).FirstOrDefault();
            if (medicine != null && medicine.Id > 0)
            {
                _context.Medicine.Remove(medicine);
                var deletemedicine = _context.SaveChanges();
                if (deletemedicine > 0)
                {
                    isDeleted = true;
                }
                await _context.SaveChangesAsync();
            }
            return isDeleted;
        }
    }
}
