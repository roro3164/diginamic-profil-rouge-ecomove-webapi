using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.ModelDTOs;
using ErrorOr;

namespace Ecomove.Api.Interfaces.IRepositories
{
    public interface IModelRepository
    {
        Task<ErrorOr<Created>> CreateModelAsync(ModelFKeyDTO modelFKeyDTO);
        Task<ErrorOr<Deleted>> DeleteModelAsync(int id);
        Task<ErrorOr<List<Model>>> GetAllModelsAsync();
        Task<ErrorOr<Model>> GetModelByIdAsync(int id);
        Task<ErrorOr<Updated>> UpdateModelByIdAsync(int id, ModelDTO modelDTO);
    }
}