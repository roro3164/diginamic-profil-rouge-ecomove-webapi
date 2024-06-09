using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.ModelDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Services.Models;

public class ModelService(IModelRepository modelRepository, ILogger<ModelService> logger) : IModelService
{
    // Create a new model
    public async Task<Response<bool>> CreateModelAsync(ModelFKeyDTO modelFKeyDTO)
    {
        ErrorOr<Created> createModelResult = await modelRepository.CreateModelAsync(modelFKeyDTO);

        return createModelResult.MatchFirst(created =>
        {
            logger.LogInformation($"Model {modelFKeyDTO.ModelLabel} created successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                Message = "Le modèle a été créée avec succès",
                CodeStatus = 201,
                Data = true
            };
        }, error =>
        {
            logger.LogError(createModelResult.FirstError.Description);

            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la création du modèle",
                CodeStatus = 500,
                Data = false,
            };
        });
    }
    // Get all models from the database
    public async Task<Response<List<ModelDTO>>> GetAllModelsAsync()
    {
        ErrorOr<List<Model>> getModelsResult = await modelRepository.GetAllModelsAsync();

        return getModelsResult.MatchFirst(models =>
        {
            logger.LogInformation("Models fetched successfully !");

            return new Response<List<ModelDTO>>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Data = getModelsResult.Value.Adapt<List<ModelDTO>>()
            };

        }, error =>
        {
            logger.LogError(getModelsResult.FirstError.Description);

            return new Response<List<ModelDTO>>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la récupération des modèles",
                CodeStatus = 500
            };
        });
    }
    // Get a model by ID
    public async Task<Response<ModelDTO>> GetModelByIdAsync(int id) 
    {
        ErrorOr<Model> getModelResult = await modelRepository.GetModelByIdAsync(id);

        return getModelResult.MatchFirst(model =>
        {
            logger.LogInformation($"Model with ID {id} fetched successfully !");

            return new Response<ModelDTO>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Data = getModelResult.Value.Adapt<ModelDTO>()
            };

        }, error =>
        {
            logger.LogError(getModelResult.FirstError.Description);

            if (getModelResult.FirstError.Type == ErrorType.NotFound)
            {
                return new Response<ModelDTO>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune modèle n'a été trouvée"
                };
            }

            return new Response<ModelDTO>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la récupération du modèle",
                CodeStatus = 500
            };
        });

    }
    // Update a model
    public async Task<Response<bool>> UpdateModelByIdAsync(int id, ModelDTO modelDTO)
    {
        ErrorOr<Updated> updateModelResult = await modelRepository.UpdateModelByIdAsync(id, modelDTO);

        return updateModelResult.MatchFirst(model =>
        {
            logger.LogInformation($"Model with ID {id} had been updated successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                CodeStatus = 201,
                Message = "Le modèle a bien été mise à jour"
            };

        }, error =>
        {
            logger.LogError(updateModelResult.FirstError.Description);

            if (updateModelResult.FirstError.Type == ErrorType.NotFound)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucun modèle n'a été trouvé",
                    Data = false,
                };
            }


            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la mise à jour du modèle",
                CodeStatus = 500,
                Data = false,
            };
        });
    }
    // Delete a model
    public async Task<Response<bool>> DeleteModelAsync(int id)
    {
        ErrorOr<Deleted> deleteModelResult = await modelRepository.DeleteModelAsync(id);

        return deleteModelResult.MatchFirst(model =>
        {
            logger.LogInformation($"Model with ID {id} had been deleted successfully !");

            return new Response<bool>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Message = "Le modèle a bien été supprimé"
            };

        }, error =>
        {
            logger.LogError(deleteModelResult.FirstError.Description);

            if (deleteModelResult.FirstError.Type == ErrorType.NotFound)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucun modèle n'a été trouvé",
                    Data = false,
                };
            }

            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Une erreur est survenue lors de la suppression du modèle",
                CodeStatus = 500,
                Data = false
            };
        });
    }
}