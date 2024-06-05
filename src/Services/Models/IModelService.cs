using Ecomove.Api.DTOs.ModelDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Services.Models;

public interface IModelService
{
    Task<Response<bool>> CreateModelAsync(ModelFKeyDTO modelFKeyDto);
    Task<Response<List<ModelDTO>>> GetAllModelsAsync();
    Task<Response<ModelDTO>> GetModelByIdAsync(int id);
    Task<Response<bool>> UpdateModelByIdAsync(int id, ModelDTO modelDTO);
    Task<Response<bool>> DeleteModelAsync(int id);
}
