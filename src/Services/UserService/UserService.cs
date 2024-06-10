using User = Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.AppUserDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Services.UserService
{
    public class UserService(IAppUserRepository appUserRepository, ILogger<UserService> logger) : IUserService
    {
        public async Task<Response<bool>> CreateUserAsync(CreateAppUserDTO userDTO)
        {
            ErrorOr<Created> createAppUserResult = await appUserRepository.CreateUserAsync(userDTO);

            return createAppUserResult.MatchFirst(created =>
            {
                logger.LogInformation($"User {userDTO.Email} created successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "L'utilisaeur a bien été créé avec succès",
                    CodeStatus = 201,
                    Data = true
                };
            }, error =>
            {
                logger.LogError(createAppUserResult.FirstError.Description);

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la création d'utilisateur",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }


        public async Task<Response<List<AllUsersDTO>>> GetAllUsersAysnc()
        {
            ErrorOr<List<User.AppUser>> getUsersResult = await appUserRepository.GetAllUsersAysnc();

            return getUsersResult.MatchFirst(appUser =>
            {
                logger.LogInformation("Users fetched successfully !");

                return new Response<List<AllUsersDTO>>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = getUsersResult.Value.Adapt<List<AllUsersDTO>>()
                };
            }, error =>
            {
                logger.LogError(getUsersResult.FirstError.Description);

                return new Response<List<AllUsersDTO>>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération des utilisateurs",
                    CodeStatus = 500
                };
            });
        }


        public async Task<Response<UserDTO>> GetUserByIdAysnc(string id)
        {
            ErrorOr<User.AppUser> getUserResult = await appUserRepository.GetUserByIdAysnc(id);

            return getUserResult.MatchFirst(user =>
            {
                logger.LogInformation($"User with ID {id} fetched successfully !");

                return new Response<UserDTO>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = getUserResult.Value.Adapt<UserDTO>()
                };

            }, error =>
            {
                logger.LogError(getUserResult.FirstError.Description);

                if (getUserResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<UserDTO>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucun utilisateur n'a été trouvé"
                    };
                }

                return new Response<UserDTO>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération de l'utilisateur",
                    CodeStatus = 500
                };
            });
        }


        public async Task<Response<bool>> UpdateUserAysnc(string id, UpdateUserDTO userDTO)
        {
            ErrorOr<Updated> updateUserResult = await appUserRepository.UpdateUserAysnc(id, userDTO);

            return updateUserResult.MatchFirst(user =>
            {
                logger.LogInformation($"User with ID {id} had been updated successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    CodeStatus = 201,
                    Message = "L'utilisateur a bien été mise à jour"
                };

            }, error =>
            {
                logger.LogError(updateUserResult.FirstError.Description);

                if (updateUserResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucun utilisateur n'a été trouvée",
                        Data = false,
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la mise à jour de l'utilisateur",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }


        public async Task<Response<bool>> DeleteUserAsync(string id)
        {
            ErrorOr<Deleted> deleteUserResult = await appUserRepository.DeleteUserAsync(id);

            return deleteUserResult.MatchFirst(user =>
            {
                logger.LogInformation($"User with ID {id} had been deleted successfully !");

                return new Response<bool>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Message = "L'utilisateur a bien été supprimée"
                };

            }, error =>
            {
                logger.LogError(deleteUserResult.FirstError.Description);

                if (deleteUserResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucun utilisateur n'a été trouvé",
                        Data = false,
                    };
                }

                if (deleteUserResult.FirstError.Type == ErrorType.Conflict)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 409,
                        Message = "Vous ne pouvez pas supprimer cet utilisateur car des réservations y sont associées",
                        Data = false,
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la suppression de l'utilisateur",
                    CodeStatus = 500,
                    Data = false
                };
            });
        }


    }
}
