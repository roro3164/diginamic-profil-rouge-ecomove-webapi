using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleMotorizationDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class VehicleMotorizationRepository : IVehicleMotorizationRepository
    { 
        private EcoMoveDbContext _dbContext;
        public VehicleMotorizationRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _dbContext = ecoMoveDbContext;
        }

        public async Task<Response<VehicleMotorizationForCreationDTO>> CreateVehicleMotorizationAsync(VehicleMotorizationForCreationDTO motorizationDTO)
        {
            Motorization motorizationVehicle = new Motorization
            {
                MotorizationLabel = motorizationDTO.MotorizationLabel,
            };

            try
            {

                await _dbContext.Motorizations.AddAsync(motorizationVehicle);
                await _dbContext.SaveChangesAsync();

                return new Response<VehicleMotorizationForCreationDTO> 
                { 
                    Message = $"La motorisation {motorizationVehicle.MotorizationLabel} a bien été créée",
                    Data = motorizationDTO,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleMotorizationForCreationDTO> 
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }
    }
}