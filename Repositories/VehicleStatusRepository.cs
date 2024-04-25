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

        public async Task<Response<VehicleStatusDTO>> CreateVehicleStatusAsync(VehicleStatusDTO statusDTO)
        {

            Status statusVehicle = new Status
            {
                StatusLabel = statusDTO.StatusLabel,
            };

            try
            {
                await _ecoMoveDbContext.Status.AddAsync(statusVehicle);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<VehicleStatusDTO>
                {
                    Message = $"Le statut {statusVehicle.StatusLabel} a bien ét� cr��",
                    Data = statusDTO,
                    IsSuccess = true
                };

            }
            catch (Exception e)
            {
                return new Response<VehicleStatusDTO>
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

        public async Task<Response<List<VehicleStatusDTO>>> GetAllVehicleStatusAsync()
        {
            try
            {
                List<Status> listStatus = await _ecoMoveDbContext.Status.ToListAsync();


                if (listStatus.Count > 0)
                {


                    List<VehicleStatusDTO> vehicleStatusDTO = new();

                    foreach (var status in listStatus)
                    {
                        vehicleStatusDTO.Add(new VehicleStatusDTO { StatusLabel = status.StatusLabel });
                    }

                    return new Response<List<VehicleStatusDTO>>
                    {
                        IsSuccess = true,
                        Data = vehicleStatusDTO,
                        Message = null,
                        CodeStatus = 200,
                    };
                }
                else
                {
                    return new Response<List<VehicleStatusDTO>>
                    {
                        IsSuccess = false,
                        Message = "La liste des status est vide",
                        CodeStatus = 404
                    };
                }

            }
            catch (Exception e)
            {
                return new Response<List<VehicleStatusDTO>>
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

        }

        public async Task<Response<VehicleStatusDTO>> GetByIdVehicleStatusAsync(int id)
        {
            try
            {
                Status? status = await _ecoMoveDbContext.Status.FirstOrDefaultAsync(s => s.StatusId == id);

                if (status == null)
                {
                    return new Response<VehicleStatusDTO>
                    {
                        Message = "Le status que vous chercher n'existe pas.",
                        IsSuccess = false,
                        CodeStatus = 404,
                    };
                }

                VehicleStatusDTO vehicleStatusDTO = new VehicleStatusDTO
                {
                    StatusLabel = status.StatusLabel,
                };

                return new Response<VehicleStatusDTO>
                {
                    IsSuccess = true,
                    Data = vehicleStatusDTO,
                    Message = null,
                    CodeStatus = 200,
                };

            }

            catch (Exception e)
            {
                return new Response<VehicleStatusDTO>
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

        }
    }
}
