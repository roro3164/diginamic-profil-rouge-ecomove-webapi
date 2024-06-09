using Ecomove.Api.Data;
using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecomove.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = $"{Roles.ADMIN}")]
    public class AppUserController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Permet de créer un utilisateur avec le rôle USER
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser(CreateAppUserDTO userDTO)
        {
            Response<bool> createUserResult = await userService.CreateUserAsync(userDTO);

            return new JsonResult(createUserResult) { StatusCode = createUserResult.CodeStatus };
        }


        /// <summary>
        /// Permet de supprimer un utilisateur
        /// </summary>
        /// <param name="id">string : identifiant de l'utilisateur (Guid)</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            Response<bool> deleteUserResult = await userService.DeleteUserAsync(id);

            return new JsonResult(deleteUserResult) { StatusCode = deleteUserResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récupérer la liste de tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllUsers()
        {
            Response<List<AllUsersDTO>> getAllUsersResult = await userService.GetAllUsersAysnc();

            return new JsonResult(getAllUsersResult) { StatusCode = getAllUsersResult.CodeStatus };
        }


        /// <summary>
        /// Permet de récuperer un utilisateur avec son id
        /// </summary>
        /// <param name="id">string : identifiant de l'utilisateur (Guid)</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserById(string id)
        {
            Response<UserDTO> getUserByIdResult = await userService.GetUserByIdAysnc(id);

            return new JsonResult(getUserByIdResult) { StatusCode = getUserByIdResult.CodeStatus };
        }


        /// <summary>
        /// Permet de modifier les infos d'un utilisateur
        /// </summary>
        /// <param name="id">string : identifiant de l'utilisateur (Guid)</param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserById(string id, UpdateUserDTO userDTO)
        {
            Response<bool> updateUserResult = await userService.UpdateUserAysnc(id, userDTO);

            return new JsonResult(updateUserResult) { StatusCode = updateUserResult.CodeStatus };
        }
    }
}