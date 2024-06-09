using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.ModelDTOs;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Repositories
{
    public class ModelRepository(EcoMoveDbContext ecoMoveDbContext) : IModelRepository
    {
        public async Task<ErrorOr<Created>> CreateModelAsync(ModelFKeyDTO modelFKeyDTO)
        {
            try
            {
                Model model = new Model { ModelLabel = modelFKeyDTO.ModelLabel };

                await ecoMoveDbContext.Models.AddAsync(model);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Created;

            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<Deleted>> DeleteModelAsync(int id)
        {
            try
            {
                Model? model = await ecoMoveDbContext.Models.FindAsync(id);

                if (model is null) return Error.NotFound(description: $"Model with ID {id} not found.");

                ecoMoveDbContext.Models.Remove(model);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Deleted;

            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }
        public async Task<ErrorOr<List<Model>>> GetAllModelsAsync()
        {
            try
            {
                return await ecoMoveDbContext.Models.ToListAsync();
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<Model>> GetModelByIdAsync(int id)
        {
            try
            {
                Model? model = await ecoMoveDbContext.Models.FindAsync(id);

                if (model is null) return Error.NotFound(description: $"Model with ID {id} is not found.");

                return model;
            }
            catch (Exception e)
            {
                return Error.Unexpected(e.Message);
            }
        }
        public async Task<ErrorOr<Updated>> UpdateModelByIdAsync(int id, ModelDTO modelLabelDTO)
        {
            try
            {
                Model? model = await ecoMoveDbContext.Models.FindAsync(id);

                if (model is null) return Error.NotFound(description: $"Model with ID {id} is not found.");

                return Result.Updated;
            }
            catch (Exception e)
            {
                return Error.Unexpected(e.Message);
            }
        }
    }
}