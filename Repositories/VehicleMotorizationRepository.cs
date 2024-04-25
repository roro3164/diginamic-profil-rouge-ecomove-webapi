using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.VehicleMotorizationDTOs;
using ecomove_back.DTOs.VehicleVehicleCategoryDTOs;
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

        public async Task<Response<VehicleMotorizationDTO>> CreateVehicleMotorizationAsync(VehicleMotorizationDTO motorizationDTO)
        {
            Motorization motorizationVehicle = new Motorization
            {
                MotorizationLabel = motorizationDTO.MotorizationLabel,
            };

            try
            {

                await _ecoMoveDbContext.Motorizations.AddAsync(motorizationVehicle);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleMotorizationDTO>
                {
                    Message = $"La motorisation {motorizationVehicle.MotorizationLabel} a bien été créée",
                    Data = motorizationDTO,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<VehicleMotorizationDTO>
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
        public async Task<Response<List<Motorization>>> GetAllVehicleMotorizationsAsync()
        {
            List<Motorization> motorizations = await _ecoMoveDbContext.Motorizations.ToListAsync();

            if (motorizations.Count > 0)
            {
                return new Response<List<Motorization>>
                {
                    IsSuccess = true,
                    Data = motorizations,
                    Message = null,
                    CodeStatus = 200,
                };
            }
            else if (motorizations.Count == 0)
            {
                return new Response<List<Motorization>>
                {
                    IsSuccess = false,
                    Message = "La liste des motorisations est vide",
                    CodeStatus = 404
                };
            }
            else
            {
                return new Response<List<Motorization>>
                {
                    IsSuccess = false,
                };
            }
        }
        public async Task<Response<int>> GetVehicleMotorizationByIdAsync(int id)
        {
            Motorization? motorization = await _ecoMoveDbContext
            .Motorizations.FirstOrDefaultAsync(motorization => motorization.MotorizationId == id);

            if (motorization is null)
            {
                return new Response<int>
                {
                    Message = "La motorisation que vous voulez trouver n'existe pas.",
                    IsSuccess = false,
                    CodeStatus = 404
                };
            }

            try
            {
                _ecoMoveDbContext.Motorizations.Remove(motorization);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<int>
                {
                    Message = $"La motorisation {motorization.MotorizationLabel} a été trouvée avec succés.",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response<int>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }
        public async Task<Response<VehicleMotorizationDTO>> UpdateVehicleMotorizationByIdAsync(int motorizationId, VehicleMotorizationDTO VehicleMotorizationDTO)
        {
            try
            {
                Motorization? motorization = await _ecoMoveDbContext.Motorizations.FirstOrDefaultAsync(motorization => motorization.MotorizationId == motorizationId);

                if (motorization is null)
                {
                    return new Response<VehicleMotorizationDTO>
                    {
                        CodeStatus = 404,
                        Message = "La motorisation que vous recherez n'existe pas",
                        IsSuccess = false,
                    };
                }

                motorization.MotorizationLabel = VehicleMotorizationDTO.MotorizationLabel;
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleMotorizationDTO>
                {
                    Message = $"La motorisation a été bien modifiéée",
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception ex)
            {
                return new Response<VehicleMotorizationDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500,
                };
            }
        }
    }
}
