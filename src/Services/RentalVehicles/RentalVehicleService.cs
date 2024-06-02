using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.RentalVehicleDTO;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Services.RentalVehicles
{
    public class RentalVehicleService(IRentalVehicleRepository rentalVehicleRepository, ILogger<RentalVehicleService> logger) : IRentalVehicleService
    {
        public async Task<Response<bool>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO)
        {
            ErrorOr<Created> createRentalVehicleResult = await rentalVehicleRepository.CreateRentalVehicleAsync(userId, vehicleId, rentalVehicleDTO);

            return createRentalVehicleResult.MatchFirst(created =>
            {
                logger.LogInformation($"Rental vehicle created successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "La réservation a bien été créée avec succès",
                    CodeStatus = 201,
                    Data = true
                };
            }, error =>
            {
                logger.LogError(createRentalVehicleResult.FirstError.Description);

                if (createRentalVehicleResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = createRentalVehicleResult.FirstError.Description
                    };
                }

                if (createRentalVehicleResult.FirstError.Type == ErrorType.Conflict)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 409,
                        Message = createRentalVehicleResult.FirstError.Description
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la réservation",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }


        public async Task<Response<bool>> CancelRentalVehicleAsync(string idUserConnect, Guid rentalId)
        {
            ErrorOr<Deleted> cancelRentalVehicleResult = await rentalVehicleRepository.CancelRentalVehicleAsync(idUserConnect, rentalId);

            return cancelRentalVehicleResult.MatchFirst(rentalVehicle =>
            {
                logger.LogInformation($"Rental Vehicle with ID had been deleted successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Message = "La réservation a bien été annulée"
                };

            }, error =>
            {
                logger.LogError(cancelRentalVehicleResult.FirstError.Description);

                if (cancelRentalVehicleResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = cancelRentalVehicleResult.FirstError.Description,
                        Data = false,
                    };
                }

                if (cancelRentalVehicleResult.FirstError.Type == ErrorType.Conflict)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 409,
                        Message = cancelRentalVehicleResult.FirstError.Description,
                        Data = false,
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de l'annulation de la réservation'",
                    CodeStatus = 500,
                    Data = false
                };
            });
        }


        public async Task<Response<List<AllRentalVehicles>>> GetAllRentalVehiclesAysnc(string idUserConnect)
        {
            ErrorOr<List<RentalVehicle>> getRentalVehiclesResult = await rentalVehicleRepository.GetAllRentalVehiclesAysnc(idUserConnect);

            return getRentalVehiclesResult.MatchFirst(rentals =>
            {
                logger.LogInformation("Rental Vehicles fetched successfully !");

                return new Response<List<AllRentalVehicles>>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = getRentalVehiclesResult.Value.Adapt<List<AllRentalVehicles>>()
                };

            }, error =>
            {
                logger.LogError(getRentalVehiclesResult.FirstError.Description);

                return new Response<List<AllRentalVehicles>>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération des réservation",
                    CodeStatus = 500
                };
            });
        }


        public async Task<Response<SingleRentalVehicleDTO>> GetRentalVehicleByIdAysnc(string idUserConnect, Guid rentalId)
        {
            ErrorOr<RentalVehicle> getRentalVehicleResult = await rentalVehicleRepository.GetRentalVehicleByIdAysnc(idUserConnect, rentalId);

            return getRentalVehicleResult.MatchFirst(rental =>
            {
                logger.LogInformation($"Rental with ID {rentalId} fetched successfully !");

                return new Response<SingleRentalVehicleDTO>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = getRentalVehicleResult.Value.Adapt<SingleRentalVehicleDTO>()
                };

            }, error =>
            {
                logger.LogError(getRentalVehicleResult.FirstError.Description);

                if (getRentalVehicleResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<SingleRentalVehicleDTO>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = getRentalVehicleResult.FirstError.Description
                    };
                }

                return new Response<SingleRentalVehicleDTO>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération de la réservation",
                    CodeStatus = 500
                };
            });
        }


        public async Task<Response<bool>> UpdateRentalVehicleAsync(string idUserConnect, Guid rentalId, RentalVehicleDTO rentalVehicleDTO)
        {
            ErrorOr<Updated> updateRentalResult = await rentalVehicleRepository.UpdateRentalVehicleAsync(idUserConnect, rentalId, rentalVehicleDTO);

            return updateRentalResult.MatchFirst(rental =>
            {
                logger.LogInformation($"Rental with ID {rentalId} had been updated successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    CodeStatus = 201,
                    Message = "La réservation a bien été mise à jour"
                };

            }, error =>
            {
                logger.LogError(updateRentalResult.FirstError.Description);

                if (updateRentalResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = updateRentalResult.FirstError.Description,
                        Data = false,
                    };
                }

                if (updateRentalResult.FirstError.Type == ErrorType.Conflict)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 409,
                        Message = updateRentalResult.FirstError.Description,
                        Data = false,
                    };
                }

                if (updateRentalResult.FirstError.Type == ErrorType.Forbidden)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 403,
                        Message = updateRentalResult.FirstError.Description,
                        Data = false,
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la mise à jour de la réservation",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }
    }
}
