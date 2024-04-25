using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleStatusDTOs;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using ecomove_back.Helpers;

namespace ecomove_back.Repositories
{
    public class VehicleStatusRepository : IVehicleStatusRepository
    {
        private EcoMoveDbContext _dbContext;

        public VehicleStatusRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _dbContext = ecoMoveDbContext;
        }

        public async Task<Response<VehicleStatusForCreationDTO>> CreateVehicleStatusAsync(VehicleStatusForCreationDTO statusDTO)
        {

            Status statusVehicle = new Status
            {
                StatusLabel = statusDTO.StatusLabel,
            };

            try
            {
                await _dbContext.Status.AddAsync(statusVehicle);
                await _dbContext.SaveChangesAsync();

                return new Response<VehicleStatusForCreationDTO>
                {
                    Message = $"Le statut {statusVehicle.StatusLabel} a bien �t� cr��",
                    Data = statusDTO,
                    IsSuccess = true
                };

            }
            catch (Exception e)
            {
                return new Response<VehicleStatusForCreationDTO>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }


        }


    }
}