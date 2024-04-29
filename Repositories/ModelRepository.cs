using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.DTOs.ModelDTOs;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private EcoMoveDbContext _ecoMoveDbContext;
        public ModelRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }
        public async Task<Response<ModelLabelDTO>> CreateModelAsync(ModelFKeyDTO modelFKeyDTO)
        {
            try
            {
                var brand = await _ecoMoveDbContext.Brands.FirstOrDefaultAsync(b => b.BrandId == modelFKeyDTO.BrandId);

                    if (brand != null)
                {
                    var newModel = new Model
                    {
                        ModelLabel = modelFKeyDTO.ModelLabel,
                        BrandId = modelFKeyDTO.BrandId,
                    };

                    await _ecoMoveDbContext.Models.AddAsync(newModel);
                    await _ecoMoveDbContext.SaveChangesAsync();

                    return new Response<ModelLabelDTO>
                    {
                        Message = $"Le modèle {newModel.ModelLabel} a bien été créé",
                        IsSuccess = true,
                        Data = new ModelLabelDTO { ModelLabel = newModel.ModelLabel },
                        CodeStatus = 201
                    };
                }
                else
                {
                    return new Response<ModelLabelDTO>
                    {
                        Message = "La marque spécifiée n'est pas trouvée dans la base de données",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

            }
            catch (DbUpdateException ex) when (ex.InnerException is UniqueConstraintException)
            {
               return new Response<ModelLabelDTO>
               {
                   Message = "Erreur",
                   IsSuccess = false,
                   CodeStatus = 400 
               };
            }
            catch (Exception e)
            {
                return new Response<ModelLabelDTO>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response<string>> DeleteModelAsync(int modelId)
        {
            Model? model = await _ecoMoveDbContext
            .Models.FirstOrDefaultAsync(model => model.ModelId == modelId);

            if (model == null)
            {
                return new Response<string>
                {
                    Message = "Le modèle que vous voulez supprimer n'existe pas.",
                    IsSuccess = false,
                    CodeStatus = 404
                };
            }
            try
            {
                _ecoMoveDbContext.Models.Remove(model);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = $"Le modèle {model.ModelLabel} a été supprimé avec succès",
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
        public async Task<Response<List<ModelLabelDTO>>> GetAllModelsAsync()
        {
            List<Model> models = await _ecoMoveDbContext.Models.ToListAsync();

            List<ModelLabelDTO> modelLabelDTOs = new();

            if (models.Count > 0)
            {
                foreach (Model model in models)
                {
                    modelLabelDTOs.Add(new ModelLabelDTO { ModelLabel = model.ModelLabel });
                }

                return new Response<List<ModelLabelDTO>>
                {
                    IsSuccess = true,
                    Data = modelLabelDTOs,
                    Message = null,
                    CodeStatus = 201,
                };
            }
            else if (modelLabelDTOs.Count == 0)
            {
                return new Response<List<ModelLabelDTO>>
                {
                    IsSuccess = false,
                    Message = "La liste des modèles est vide",
                    CodeStatus = 404
                };
            }
            else
            {
                return new Response<List<ModelLabelDTO>>
                {
                    IsSuccess = false,
                };
            }
        }

        public async Task<Response<ModelLabelDTO>> GetModelByIdAsync(int id)
        {
            try
            {
                Model? model = await _ecoMoveDbContext.Models.FirstOrDefaultAsync(model => model.ModelId == id);

                if (model is null)
                {
                    return new Response<ModelLabelDTO>
                    {
                        Message = "Le modèle que vous voulez trouver n'existe pas.",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                ModelLabelDTO ModelLabelDTO = new ModelLabelDTO
                {
                    ModelLabel = model.ModelLabel
                };

                return new Response<ModelLabelDTO>
                {
                    Data = ModelLabelDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<ModelLabelDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<ModelLabelDTO>> UpdateModelByIdAsync(int modelId, ModelLabelDTO modelLabelDTO)
        {
            try
            {
                Model? model = await _ecoMoveDbContext.Models.FirstOrDefaultAsync(model => model.ModelId == modelId);

                if (model is null)
                {
                    return new Response<ModelLabelDTO>
                    {
                        CodeStatus = 404,
                        Message = "Le modèle que vous recherchez n'existe pas",
                        IsSuccess = false,
                    };
                }

                model.ModelLabel = modelLabelDTO.ModelLabel;
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<ModelLabelDTO>
                {
                    Message = $"Le modèle a été bien modifié",
                    IsSuccess = true,
                    CodeStatus = 201,
                    Data = modelLabelDTO 
                };
            }
            catch (Exception ex)
            {
                return new Response<ModelLabelDTO>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500,
                };
            }
        }
    }
}