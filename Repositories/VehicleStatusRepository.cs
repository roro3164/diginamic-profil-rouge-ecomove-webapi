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
        private EcoMoveDbContext _ecoMoveDbContext;

        public VehicleStatusRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }

        public async Task<Response<VehicleStatusForCreationDTO>> CreateVehicleStatusAsync(VehicleStatusForCreationDTO statusDTO)
        {

            Status statusVehicle = new Status
            {
                StatusLabel = statusDTO.StatusLabel,
            };

            try
            {
                await _ecoMoveDbContext.Status.AddAsync(statusVehicle);
                await _ecoMoveDbContext.SaveChangesAsync();

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

        public async Task<Response<string>> DeleteVehicleStatusAsync(int statusId)
        {
            Status? status = await _ecoMoveDbContext
            .Status.FirstOrDefaultAsync(status => status.StatusId == statusId);

            if (status is null)
            {
                return new Response<string>
                {
                    Message = "Le status que vous voulez supprimer n'existe pas.",
                    IsSuccess = false,
                    CodeStatus = 404,
                };
            }

            try
            {
                _ecoMoveDbContext.Status.Remove(status);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = $"Le status {status.StatusLabel} a été supprimée avec succés.",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<string>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }

        }

    }
}
