using Ecomove.Api.DTOs.ModelDTOs;
using Ecomove.Api.Helpers;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IModelRepository
    {
        Task<Response<ModelLabelDTO>> CreateModelAsync(ModelFKeyDTO modelFKeyDTO);
        Task<Response<string>> DeleteModelAsync(int id);
        Task<Response<List<ModelLabelDTO>>> GetAllModelsAsync();
        Task<Response<ModelLabelDTO>> GetModelByIdAsync(int modelId);
        Task<Response<ModelLabelDTO>> UpdateModelByIdAsync(int modelId, ModelLabelDTO modelLabelDTO);
    }
}