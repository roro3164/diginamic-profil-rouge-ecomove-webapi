using Ecomove.Api.Data;
using Ecomove.Api.DTOs.BrandDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Services.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    [Authorize(Roles = $"{Roles.ADMIN}")]
    public class BrandController(IBrandService brandService) : ControllerBase
    {
        /// <summary>
        /// Permet de créer une marque
        /// </summary>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBrand(BrandDTO brandDTO)
        {
            Response<bool> createBrandResult = await brandService.CreateBrandAsync(brandDTO);

            return new JsonResult(createBrandResult) { StatusCode = createBrandResult.CodeStatus };
        }


        /// <summary>
        /// Permet de supprimer une marque de véhicules par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            Response<bool> deleteBrandResult = await brandService.DeleteBrandAsync(id);

            return new JsonResult(deleteBrandResult) { StatusCode = deleteBrandResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer toutes les marques de véhicules
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllBrands()
        {
            Response<List<BrandDTO>> getAllBrandsResult = await brandService.GetAllBrandsAsync();

            return new JsonResult(getAllBrandsResult) { StatusCode = getAllBrandsResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer une marque de véhicules par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBrandById(int id)
        {
            Response<BrandDTO> getBrandByIdResult = await brandService.GetBrandByIdAsync(id);

            return new JsonResult(getBrandByIdResult) { StatusCode = getBrandByIdResult.CodeStatus };
        }


        /// <summary>
        /// Permet de mettre à jour une marque de véhicules par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBrand(int id, BrandDTO brandDTO)
        {
            Response<bool> updateBrandResult = await brandService.UpdateBrandAsync(id, brandDTO);

            return new JsonResult(updateBrandResult) { StatusCode = updateBrandResult.CodeStatus };
        }
    }
}