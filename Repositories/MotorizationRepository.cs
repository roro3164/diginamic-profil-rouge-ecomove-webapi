using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.BrandDTOs;
using ecomove_back.DTOs.MotorizationDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class MotorizationRepository : IMotorizationRepository
    {
        private EcoMoveDbContext _ecoMoveDbContext;
        public MotorizationRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }

        public async Task<Response<MotorizationDTO>> CreateMotorizationAsync(MotorizationDTO motorizationDTO)
        {
            Motorization motorization = new Motorization
            {
                MotorizationLabel = motorizationDTO.MotorizationLabel,
            };

            try
            {
                await _ecoMoveDbContext.Motorizations.AddAsync(motorization);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<MotorizationDTO>
                {
                    Message = $"La motorisation {motorization.MotorizationLabel} a bien ï¿½tï¿½ crï¿½ï¿½e",
                    Data = motorizationDTO,
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<MotorizationDTO>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<string>> DeleteMotorizationAsync(int motorizationId)
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
                    Message = $"La motorisation {motorization.MotorizationLabel} a Ã©tÃ© supprimÃ©e avec succÃ©s.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }
        public async Task<Response<List<MotorizationDTO>>> GetAllMotorizationsAsync()
        {
            List<Motorization> motorizations = await _ecoMoveDbContext.Motorizations.ToListAsync();

            List<MotorizationDTO> motorizationDTOs = new();

            if (motorizations.Count > 0)
            {
                foreach (Motorization motorization in motorizations)
                {
                    motorizationDTOs.Add(new MotorizationDTO { MotorizationLabel = motorization.MotorizationLabel });
                }

                return new Response<List<MotorizationDTO>>
                {
                    IsSuccess = true,
                    Data = motorizationDTOs,
                    Message = null,
                    CodeStatus = 201,
                };
            }
            else if (motorizationDTOs.Count == 0)
            {
                return new Response<List<MotorizationDTO>>
                {
                    IsSuccess = false,
                    Message = "La liste des motorisations est vide",
                    CodeStatus = 404
                };
            }
            else
            {
                return new Response<List<MotorizationDTO>>
                {
                    IsSuccess = false,
                };
            }
        }

        public async Task<Response<MotorizationDTO>> GetMotorizationByIdAsync(int id)
        {
            try
            {
                Motorization? motorization = await _ecoMoveDbContext.Motorizations.FirstOrDefaultAsync(motorization => motorization.MotorizationId == id);

                if (motorization is null)
                {
                    return new Response<MotorizationDTO>
                    {
                        Message = "La motorisation que vous voulez trouver n'existe pas.",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                MotorizationDTO MotorizationDTO = new MotorizationDTO
                {
                    MotorizationLabel = motorization.MotorizationLabel
                };

                return new Response<MotorizationDTO>
                {
                    Data = MotorizationDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<MotorizationDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<MotorizationDTO>> UpdateMotorizationByIdAsync(int motorizationId, MotorizationDTO MotorizationDTO)
        {
            try
            {
                Motorization? motorization = await _ecoMoveDbContext.Motorizations.FirstOrDefaultAsync(motorization => motorization.MotorizationId == motorizationId);

                if (motorization is null)
                {
                    return new Response<MotorizationDTO>
                    {
                        CodeStatus = 404,
                        Message = "La motorisation que vous recherez n'existe pas",
                        IsSuccess = false,
                    };
                }

                motorization.MotorizationLabel = MotorizationDTO.MotorizationLabel;
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<MotorizationDTO>
                {
                    Message = $"La motorisation a Ã©tÃ© bien modifiÃ©e",
                    IsSuccess = true,
                    CodeStatus = 201,
                };
            }
            catch (Exception ex)
            {
                return new Response<MotorizationDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500,
                };
            }
        }
    }
}
