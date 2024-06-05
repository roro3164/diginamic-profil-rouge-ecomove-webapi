using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.MotorizationDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Services.Motorizations;

public class MotorizationService(IMotorizationRepository motorizationRepository, ILogger<MotorizationService> logger) : IMotorizationService
{
    // Create a new motorization
    public async Task<Response<bool>> CreateMotorizationAsync(MotorizationDTO motorizationDTO)
    {
        ErrorOr<Created> createMotorizationResult = await motorizationRepository.CreateMotorizationAsync(motorizationDTO);

        return createMotorizationResult.MatchFirst(created =>
        {
            logger.LogInformation($"Motorization {motorizationDTO.MotorizationLabel} created successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                Message = "La motorisation a bien été créée avec succès",
                CodeStatus = 201,
                Data = true
            };
        }, error =>
        {
            logger.LogError(createMotorizationResult.FirstError.Description);

            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la création de la motorisation",
                CodeStatus = 500,
                Data = false,
            };
        });
    }
    // Get all motorizations from the database
    public async Task<Response<List<MotorizationDTO>>> GetAllMotorizationsAsync()
    {
        ErrorOr<List<Motorization>> getMotorizationsResult = await motorizationRepository.GetAllMotorizationsAsync();

        return getMotorizationsResult.MatchFirst(motorizations =>
        {
            logger.LogInformation("Motorizations fetched successfully !");

            return new Response<List<MotorizationDTO>>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Data = getMotorizationsResult.Value.Adapt<List<MotorizationDTO>>()
            };

        }, error =>
        {
            logger.LogError(getMotorizationsResult.FirstError.Description);

            return new Response<List<MotorizationDTO>>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la récupération des motorizations",
                CodeStatus = 500
            };
        });
    }
    // Get a motorization by ID
    public async Task<Response<MotorizationDTO>> GetMotorizationAsync(int id)
    {
        ErrorOr<Motorization> getMotorizationResult = await motorizationRepository.GetMotorizationAsync(id);

        return getMotorizationResult.MatchFirst(motorization =>
        {
            logger.LogInformation($"Motorization with ID {id} fetched successfully !");

            return new Response<MotorizationDTO>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Data = getMotorizationResult.Value.Adapt<MotorizationDTO>()
            };

        }, error =>
        {
            logger.LogError(getMotorizationResult.FirstError.Description);

            if (getMotorizationResult.FirstError.Type == ErrorType.NotFound)
            {
                return new Response<MotorizationDTO>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune motorisation n'a été trouvée"
                };
            }

            return new Response<MotorizationDTO>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la récupération de la motorisation",
                CodeStatus = 500
            };
        });

    }
    // Update a motorization
    public async Task<Response<bool>> UpdateMotorizationAsync(int id, MotorizationDTO motorizationDTO)
    {
        ErrorOr<Updated> updateMotorizationResult = await motorizationRepository.UpdateMotorizationAsync(id, motorizationDTO);

        return updateMotorizationResult.MatchFirst(motorization =>
        {
            logger.LogInformation($"Motorization with ID {id} had been updated successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                CodeStatus = 201,
                Message = "La motorisation a bien été mise à jour"
            };

        }, error =>
        {
            logger.LogError(updateMotorizationResult.FirstError.Description);

            if (updateMotorizationResult.FirstError.Type == ErrorType.NotFound)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune motorisation n'a été trouvée",
                    Data = false,
                };
            }


            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la mise à jour de la motorisation",
                CodeStatus = 500,
                Data = false,
            };
        });
    }
    // Delete a motorization
    public async Task<Response<bool>> DeleteMotorizationAsync(int id)
    {
        ErrorOr<Deleted> deleteMotorizationResult = await motorizationRepository.DeleteMotorizationAsync(id);

        return deleteMotorizationResult.MatchFirst(motorization =>
        {
            logger.LogInformation($"Motorization with ID {id} had been deleted successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Message = "La motorisation a bien été supprimée"
            };

        }, error =>
        {
            logger.LogError(deleteMotorizationResult.FirstError.Description);

            if (deleteMotorizationResult.FirstError.Type == ErrorType.NotFound)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune motorisation n'a été trouvée",
                    Data = false,
                };
            }

            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la suppression de la motorisation",
                CodeStatus = 500,
                Data = false
            };
        });
    }
}