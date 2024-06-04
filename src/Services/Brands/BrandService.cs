using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.BrandDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Services.Brands
{
    public class BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger) : IBrandService
    {
        public async Task<Response<bool>> CreateBrandAsync(BrandDTO brandDTO)
        {
            ErrorOr<Created> createBrandResult = await brandRepository.CreateBrandAsync(brandDTO);

            return createBrandResult.MatchFirst(created =>
            {
                logger.LogInformation($"Brand {brandDTO.BrandLabel} created successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "La marque a bien été créée avec succès",
                    CodeStatus = 201,
                    Data = true
                };
            }, error =>
            {
                logger.LogError(createBrandResult.FirstError.Description);

                if (createBrandResult.FirstError.Type == ErrorType.Conflict)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 409,
                        Message = "La marque existe déjà"
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la création de la marque",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }


        public async Task<Response<bool>> DeleteBrandAsync(int id)
        {
            ErrorOr<Deleted> deleteBrandResult = await brandRepository.DeleteBrandAsync(id);

            return deleteBrandResult.MatchFirst(brand =>
            {
                logger.LogInformation($"Brand with ID {id} had been deleted successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Message = "La marque a bien été supprimée"
                };

            }, error =>
            {
                logger.LogError(deleteBrandResult.FirstError.Description);

                if (deleteBrandResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune marque n'a été trouvée",
                        Data = false,
                    };
                }

                if (deleteBrandResult.FirstError.Type == ErrorType.Conflict)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 409,
                        Message = "Vous ne pouvez pas supprimer cette marque car des modèles y sont associés",
                        Data = false,
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la suppression de la marque",
                    CodeStatus = 500,
                    Data = false
                };
            });
        }

        public async Task<Response<List<BrandDTO>>> GetAllBrandsAsync()
        {
            ErrorOr<List<Brand>> getBrandsResult = await brandRepository.GetAllBrandsAsync();

            return getBrandsResult.MatchFirst(brands =>
            {
                logger.LogInformation("Brand fetched successfully !");

                return new Response<List<BrandDTO>>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = getBrandsResult.Value.Adapt<List<BrandDTO>>()
                };

            }, error =>
            {
                logger.LogError(getBrandsResult.FirstError.Description);

                return new Response<List<BrandDTO>>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération des marques",
                    CodeStatus = 500
                };
            });
        }

        public async Task<Response<BrandDTO>> GetBrandByIdAsync(int id)
        {
            ErrorOr<Brand> getBrandResult = await brandRepository.GetBrandByIdAsync(id);

            return getBrandResult.MatchFirst(brand =>
            {
                logger.LogInformation($"Brand with ID {id} fetched successfully !");

                return new Response<BrandDTO>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = getBrandResult.Value.Adapt<BrandDTO>()
                };

            }, error =>
            {
                logger.LogError(getBrandResult.FirstError.Description);

                if (getBrandResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<BrandDTO>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune marque n'a été trouvée"
                    };
                }

                return new Response<BrandDTO>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération de la marque",
                    CodeStatus = 500
                };
            });
        }

        public async Task<Response<bool>> UpdateBrandAsync(int id, BrandDTO brandDTO)
        {
            ErrorOr<Updated> updateBrandResult = await brandRepository.UpdateBrandAsync(id, brandDTO);

            return updateBrandResult.MatchFirst(brand =>
            {
                logger.LogInformation($"Brand with ID {id} had been updated successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    CodeStatus = 201,
                    Message = "La marque a bien été mise à jour"
                };

            }, error =>
            {
                logger.LogError(updateBrandResult.FirstError.Description);

                if (updateBrandResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune marque n'a été trouvée",
                        Data = false,
                    };
                }


                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la mise à jour de la marque",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }
    }
}
