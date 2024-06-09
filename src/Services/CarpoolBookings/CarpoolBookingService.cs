using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CarpoolBookingDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;
using ErrorOr;
using Mapster;

namespace Ecomove.Api.Services.CarpoolBookings
{
    public class CarpoolBookingService(ICarpoolBookingRepository carpoolBookingRepository, ILogger<CarpoolBookingService> logger) : ICarpoolBookingService
    {
        // Create a new carpool booking 
        public async Task<Response<bool>> CreateCarpoolBookingAsync(CarpoolBookingDTO bookingDTO, string appUserId)
        {
            ErrorOr<Created> createCarpoolBookingResult = await carpoolBookingRepository.CreateCarpoolBookingAsync(bookingDTO, appUserId);

            return createCarpoolBookingResult.MatchFirst(succes =>
            {
                logger.LogInformation($"Carpool booking for user {appUserId} created successfully!");

                return new Response<bool>
                {
                    IsSuccess = true,
                    Message = "La réservation du covoiturage a bien été créée avec succès",
                    CodeStatus = 201,
                    Data = true
                };
            }, error =>
            {
                logger.LogError(createCarpoolBookingResult.FirstError.Description);

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la création de la réservation du covoiturage",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }

        // Get all carpool bookings from the database by user
        public async Task<Response<List<CarpoolBookingDTO>>> GetAllCarpoolBookingsByUserIdAsync(string appUserId)
        {
            ErrorOr<List<CarpoolBookingDTO>> getAllCarpoolBookingsResult = await carpoolBookingRepository.GetAllCarpoolBookingsByUserIdAsync(appUserId);

            return getAllCarpoolBookingsResult.MatchFirst(carpoolBookings =>
            {
                logger.LogInformation($"Carpool bookings for user {appUserId} fetched successfully!");

                return new Response<List<CarpoolBookingDTO>>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = carpoolBookings
                };
            }, error =>
            {
                logger.LogError(getAllCarpoolBookingsResult.FirstError.Description);

                return new Response<List<CarpoolBookingDTO>>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération des réservations du covoiturage",
                    CodeStatus = 500
                };
            });
        }

        // Get all future carpool bookings from the database by user
        public async Task<Response<List<CarpoolBookingDTO>>> GetFutureCarpoolBookingsByUserIdAsync(string appUserId)
        {
            ErrorOr<List<CarpoolBookingDTO>> getFutureCarpoolBookingsResult = await carpoolBookingRepository.GetFutureCarpoolBookingsByUserIdAsync(appUserId);

            return getFutureCarpoolBookingsResult.MatchFirst(carpoolBookings =>
            {
                logger.LogInformation($"Future carpool bookings for user {appUserId} fetched successfully!");

                return new Response<List<CarpoolBookingDTO>>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = carpoolBookings
                };
            }, error =>
            {
                logger.LogError(getFutureCarpoolBookingsResult.FirstError.Description);

                return new Response<List<CarpoolBookingDTO>>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération des réservations du covoiturage",
                    CodeStatus = 500
                };
            });
        }

        // Get all past carpool bookings from the database by user
        public async Task<Response<List<CarpoolBookingDTO>>> GetPastCarpoolBookingsByUserIdAsync(string appUserId)
        {
            ErrorOr<List<CarpoolBookingDTO>> getPastCarpoolBookingsResult = await carpoolBookingRepository.GetPastCarpoolBookingsByUserIdAsync(appUserId);

            return getPastCarpoolBookingsResult.MatchFirst(carpoolBookings =>
            {
                logger.LogInformation($"Past carpool bookings for user {appUserId} fetched successfully!");

                return new Response<List<CarpoolBookingDTO>>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = carpoolBookings
                };
            }, error =>
            {
                logger.LogError(getPastCarpoolBookingsResult.FirstError.Description);

                return new Response<List<CarpoolBookingDTO>>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération des réservations du covoiturage",
                    CodeStatus = 500
                };
            });
        }

        // Get a carpool booking by Announcement ID
        public async Task<Response<CarpoolBookingDTO>> GetCarpoolBookingByAnnouncementIdAsync(Guid carpoolAnnouncementId, string appUserId)
        {
            ErrorOr<CarpoolBookingDTO> getCarpoolBookingResult = await carpoolBookingRepository.GetCarpoolBookingByAnnouncementIdAsync(carpoolAnnouncementId, appUserId);

            return getCarpoolBookingResult.MatchFirst(carpoolBooking =>
            {
                logger.LogInformation($"Carpool booking with announcement ID {carpoolAnnouncementId} for user {appUserId} fetched successfully!");

                return new Response<CarpoolBookingDTO>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = carpoolBooking
                };

            }, error =>
            {
                logger.LogError(getCarpoolBookingResult.FirstError.Description);

                if (getCarpoolBookingResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<CarpoolBookingDTO>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune réservation du covoiturage n'a été trouvée"
                    };
                }

                return new Response<CarpoolBookingDTO>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de la récupération de la réservation du covoiturage",
                    CodeStatus = 500
                };
            });
        }

        // Cancel a carpool booking  
        public async Task<Response<bool>> CancelCarpoolBookingAsync(Guid carpoolAnnouncementId, string appUserId)
        {
            ErrorOr<Updated> cancelCarpoolBookingResult = await carpoolBookingRepository.CancelCarpoolBookingAsync(carpoolAnnouncementId, appUserId);

            return cancelCarpoolBookingResult.MatchFirst(success =>
            {
                logger.LogInformation($"Carpool booking for announcement {carpoolAnnouncementId} and user {appUserId} canceled successfully!");

                return new Response<bool>
                {
                    IsSuccess = true,
                    CodeStatus = 201,
                    Message = "La réservation du covoiturage est annulée",
                    Data = true
                };
            }, error =>
            {
                logger.LogError(cancelCarpoolBookingResult.FirstError.Description);

                if (cancelCarpoolBookingResult.FirstError.Type == ErrorType.NotFound)
                {
                    return new Response<bool>
                    {
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune réservation du covoiturage n'a été trouvée",
                        Data = false,
                    };
                }

                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Une erreur est survenue lors de l'annulation de la réservation du covoiturage",
                    CodeStatus = 500,
                    Data = false,
                };
            });
        }
    }
}