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
        private EcoMoveDbContext _ecoMoveDbContext;
        public VehicleMotorizationRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }

        public async Task<Response<VehicleMotorizationForCreationDTO>> CreateVehicleMotorizationAsync(VehicleMotorizationForCreationDTO motorizationDTO)
        {
            Motorization motorizationVehicle = new Motorization
            {
                MotorizationLabel = motorizationDTO.MotorizationLabel,
            };

            try
            {

                await _ecoMoveDbContext.Motorizations.AddAsync(motorizationVehicle);
                await _ecoMoveDbContext.SaveChangesAsync();

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
        public async Task<Response<string>> DeleteVehicleMotorizationAsync(int motorizationId)
        {
            Motorization? motorization = await _ecoMoveDbContext
            .Motorizations.FirstOrDefaultAsync(motorization => motorization.MotorizationId == motorizationId);

            if (motorization is null)
            {
                return new Response<string>
                {
                    Message = "La motorisation que vous voulez supprimer n'existe pas.",
                    IsSuccess = false,
                    CodeStatus = 404
                };
            }

            try
            {
                _ecoMoveDbContext.Motorizations.Remove(motorization);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = $"La motorisation {motorization.MotorizationLabel} a été supprimée avec succés.",
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
