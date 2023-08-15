using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.Contracts.Request;
using MTS.Contracts.Response;
using MTS.Repository;
using MTS.ServiceInterface;

namespace MTS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
   
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IMTSLogger _logger;
        public MedicineController(IMedicineService MedicineService, IMTSLogger logger)
        {
            _logger = logger;
            _medicineService = MedicineService;
        }

        [HttpGet]
        [ActionName("MedicineList")]
        public async Task<Response> GetMedicineList()
        {
            Response response = new Response();
            try
            {
                var data = await Task.Run(() => _medicineService.GetMedicineList()).ConfigureAwait(true);
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

        [HttpGet]
        [ActionName("MedicineById")]
        public async Task<Response> GetMedicineById(int id)
        {
            Response response = new Response();
            try
            {
                var data = await Task.Run(() => _medicineService.GetMedicineById(id)).ConfigureAwait(true);
                if (data != null)
                {
                    response.Data = data;
                    response.IsError = false;
                    response.Message = "Record successfully loaded";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.IsError = false;
                    response.Message = "record not found";
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
        public async Task<Response> AddMedicine(MedicineRequestModel medicineRequestModel)
        {
            Response response = new Response();

            try
            {
                var res = await _medicineService.AddMedicine(medicineRequestModel);
                if (res > 0)
                {
                    response.Data = medicineRequestModel;
                    response.IsError = false;
                    response.Message = "Record successfully added";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.Data = medicineRequestModel;
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
        public async Task<Response> UpdateMedicine(MedicineRequestModel medicineRequestModel)
        {
            Response response = new Response();

            try
            {
                var res = await _medicineService.UpdateMedicine(medicineRequestModel);
                if (res > 0)
                {
                    response.Data = medicineRequestModel;
                    response.IsError = false;
                    response.Message = "Record successfully updated";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.Data = medicineRequestModel;
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
        [HttpDelete]
        [ActionName("delete")]
        public async Task<Response> DeleteMedicine(int id)
        {
            Response response = new Response();
            try
            {
                var res = await _medicineService.DeleteMedicine(id);
                if (res)
                {
                    response.Data = id;
                    response.IsError = false;
                    response.Message = "Record successfully Deleted";
                    response.ErrorCode = 200;
                }
                else
                {
                    response.Data = id;
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


