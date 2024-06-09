using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.MotorizationDTOs;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Repositories
{
    public class MotorizationRepository(EcoMoveDbContext ecoMoveDbContext) : IMotorizationRepository
    {
        public async Task<ErrorOr<Created>> CreateMotorizationAsync(MotorizationDTO motorizationDTO)
        {
            try
            {
                Motorization motorization = new Motorization { MotorizationLabel = motorizationDTO.MotorizationLabel };

                await ecoMoveDbContext.Motorizations.AddAsync(motorization);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Created;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<Deleted>> DeleteMotorizationAsync(int id)
        {
            try
            {
                Motorization? motorization = await ecoMoveDbContext.Motorizations.FindAsync(id);

                if (motorization is null) return Error.NotFound(description: $"Motorization with ID {id} not found.");

                ecoMoveDbContext.Motorizations.Remove(motorization);

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Deleted;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }
        public async Task<ErrorOr<List<Motorization>>> GetAllMotorizationsAsync()
        {
            try
            {
                return await ecoMoveDbContext.Motorizations.ToListAsync();
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }

        public async Task<ErrorOr<Motorization>> GetMotorizationAsync(int id)
        {
            try
            {
                Motorization? motorization = await ecoMoveDbContext.Motorizations.FindAsync(id);

                if (motorization is null) return Error.NotFound(description: $"Motorization with ID {id} is not found.");

                return motorization;

            }
            catch (Exception e)
            {
                return Error.Unexpected(e.Message);
            }

        }

        public async Task<ErrorOr<Updated>> UpdateMotorizationAsync(int id, MotorizationDTO motorizationDTO)
        {
            try
            {
                Motorization? motorization = await ecoMoveDbContext.Motorizations.FindAsync(id);

                if (motorization is null) return Error.NotFound(description: $"Motorization with ID {id} not found.");

                motorization.MotorizationLabel = motorizationDTO.MotorizationLabel;

                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Updated;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);

            }

        }
    }
}

