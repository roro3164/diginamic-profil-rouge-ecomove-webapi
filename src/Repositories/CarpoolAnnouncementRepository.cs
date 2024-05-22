using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CapoolAnnouncementDTOs;
using Ecomove.Api.DTOs.CarpoolAnnouncementDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecomove.Api.Repositories
{
    public class CarpoolAnnouncementRepository : ICarpoolAnnouncementRepository
    {
        private readonly EcoMoveDbContext _ecoMoveDbContext;
        public CarpoolAnnouncementRepository(EcoMoveDbContext ecoMoveDbContext)
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }

        public async Task<Response<CarpoolAnnouncement>> CreateCarpoolAnnouncement(string userId, CarpoolAnnouncementInGoingDTO carpoolAnnouncementDTO)
        {
            try
            {
                // Récupérer la location véhicule choisie par l'utilisateur pour la création d'une annonce
                RentalVehicle? rentalVehicle = await _ecoMoveDbContext.RentalVehicles
                    .FirstOrDefaultAsync(r => r.RentalVehicleId == carpoolAnnouncementDTO.RentalVehicleId);

                // Vérifier si le locataire du véhicule est bien celui qui est connecté
                if (rentalVehicle?.AppUserId != userId)
                {
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Vous devriez louer un véhicule avant de créer une annonce de covoiturage",
                        CodeStatus = 403,
                    };
                }

                // Chercher si l'utilisateur a bien une location voiture avec des dattes correctes
                RentalVehicle? userRentalVehicle = await _ecoMoveDbContext.RentalVehicles.FirstOrDefaultAsync(r => r.AppUserId == userId);

                // Vérifier que la fourchette des dattes de l'annonce de covoiturage est incluse dans la fourchettes des dattes
                // de la location voitures et que la location véhicule n'est pas annulée

                if (userRentalVehicle?.StartDate > carpoolAnnouncementDTO.StartDate)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 403,
                        Message = "La date de début de covoiturage doit être supérieure ou égale à la date de début de location du véhicule"
                    };


                if (carpoolAnnouncementDTO.StartDate < DateTime.Now)
                    return new Response<CarpoolAnnouncement>
                    {
                        Message = "La date de début du covoiturage ne doit pas être antérieure à la date d'aujourd'hui",
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 403,
                    };

                if (carpoolAnnouncementDTO.PickupAddressId == carpoolAnnouncementDTO.DropOffAddressId)
                    return new Response<CarpoolAnnouncement>
                    {
                        Message = "L'addresse de départ doit être différente de l'addresse d'arrivée",
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 403,
                    };

                CarpoolAnnouncement carpoolAnnouncement = new CarpoolAnnouncement
                {
                    StartDate = carpoolAnnouncementDTO.StartDate,
                    RentalVehicleId = carpoolAnnouncementDTO.RentalVehicleId,
                    PickupAddressId = carpoolAnnouncementDTO.PickupAddressId,
                    DropOffAddressId = carpoolAnnouncementDTO.DropOffAddressId
                };

                _ecoMoveDbContext.CarpoolAnnouncements.Add(carpoolAnnouncement);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = "L'annonce de covoiturage a bien été créée",
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        public async Task<Response<CarpoolAnnouncement>> DeleteCarpoolAnnouncement(Guid id, string userId)
        {
            try
            {
                CarpoolAnnouncement? carpoolAnnouncement = await _ecoMoveDbContext.CarpoolAnnouncements.FindAsync(id);

                if (carpoolAnnouncement is null)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        Message = "Aucune annonce de covoiturage n'a été trouvée",
                        IsSuccess = false,
                        CodeStatus = 404
                    };

                var carpoolOrganizer = _ecoMoveDbContext.CarpoolAnnouncements
                    .Include(c => c.RentalVehicle)
                    .Where(c => c.CarpoolAnnouncementId == id)
                    .Select(c => new { c.RentalVehicle.AppUserId }).First();


                // Vérifier si l'utilisateur connecté est celui le créateur de l'annonce pour pouvoir la supprimer par la suite

                if (userId != carpoolOrganizer.AppUserId)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        Message = "Vous ne pouvez pas supprimer cette annonce de covoiturage",
                        IsSuccess = false,
                        CodeStatus = 403
                    };

                _ecoMoveDbContext.CarpoolAnnouncements.Remove(carpoolAnnouncement);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = "L'annonce du covoiturage a été supprimée avec succés",
                    IsSuccess = true,
                    CodeStatus = 204
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            };

        }
        public async Task<Response<List<CarpoolAnnouncementOutGoingDTO>>> GetAllCarpoolAnnouncements()
        {
            try
            {
                List<CarpoolAnnouncementOutGoingDTO> carpoolAnnouncements = await _ecoMoveDbContext.CarpoolAnnouncements
                    .Include(c => c.PickupAddress)
                    .Include(c => c.DropOffAddress)
                    .Select(c => new CarpoolAnnouncementOutGoingDTO
                    {
                        StartDate = c.StartDate,
                        PickupAddress = c.PickupAddress,
                        DropOffAddress = c.DropOffAddress,
                    }).ToListAsync();

                if (carpoolAnnouncements.Count > 0)
                    return new Response<List<CarpoolAnnouncementOutGoingDTO>>()
                    {
                        Data = carpoolAnnouncements,
                        IsSuccess = true,
                        CodeStatus = 200,
                        Message = null
                    };


                return new Response<List<CarpoolAnnouncementOutGoingDTO>>
                {
                    Data = null,
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = "Aucune annonce de covoiturage n'a été trouvée"
                };
            }
            catch (Exception ex)
            {
                return new Response<List<CarpoolAnnouncementOutGoingDTO>>
                {
                    Data = null,
                    IsSuccess = false,
                    CodeStatus = 404,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<CarpoolAnnouncementOutGoingDTO>> GetCarpoolAnnouncementById(Guid id)
        {
            try
            {
                CarpoolAnnouncement? carpoolAnnouncement = (await _ecoMoveDbContext.CarpoolAnnouncements
                    .Include(c => c.PickupAddress)
                    .Include(c => c.DropOffAddress)
                    .Where(c => c.CarpoolAnnouncementId == id)
                    .ToListAsync())
                    .First();

                CarpoolAnnouncementOutGoingDTO carpoolAnnouncementOutGoingDTO = new()
                {
                    StartDate = carpoolAnnouncement.StartDate,
                    PickupAddress = carpoolAnnouncement.PickupAddress,
                    DropOffAddress = carpoolAnnouncement.DropOffAddress
                };


                if (carpoolAnnouncement is null)
                    return new Response<CarpoolAnnouncementOutGoingDTO>
                    {
                        Data = null,
                        Message = "Aucune annonce de covoiturage n'a été trouvée",
                        IsSuccess = false,
                        CodeStatus = 404
                    };

                return new Response<CarpoolAnnouncementOutGoingDTO>
                {
                    Data = carpoolAnnouncementOutGoingDTO,
                    Message = null,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncementOutGoingDTO>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            };
        }

        public async Task<Response<CarpoolAnnouncement>> UpdateCarpoolAnnouncement(Guid id, string userId, CarpoolAnnouncementDTO carpoolAnnouncementDTO)
        {
            try
            {

                CarpoolAnnouncement? carpoolAnnouncement = await _ecoMoveDbContext.CarpoolAnnouncements.FindAsync(id);
                RentalVehicle? rentalVehicle = await _ecoMoveDbContext.RentalVehicles.FirstOrDefaultAsync(r => r.RentalVehicleId == carpoolAnnouncement.RentalVehicleId);

                if (carpoolAnnouncement is null)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 404,
                        Message = "Aucune annonce de covoiturage n'a été trouvée"
                    };


                var carpoolOrganizer = _ecoMoveDbContext.CarpoolAnnouncements
                                   .Include(c => c.RentalVehicle)
                                   .Where(c => c.CarpoolAnnouncementId == id)
                                   .Select(c => new { c.RentalVehicle.AppUserId }).First();


                // Impossible de modifier l'annonce de covoiturage que par son créateur
                if (userId != carpoolOrganizer.AppUserId)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        Message = "Vous ne pouvez pas modifier cette annonce de covoiturage",
                        IsSuccess = false,
                        CodeStatus = 403
                    };

                bool carpoolAnnouncementIsBooked = await _ecoMoveDbContext.CarpoolBookings.AnyAsync(cb => cb.CarpoolAnnouncementId == id);


                // Impossible de supprimer un covoiturage déja réservé
                if (carpoolAnnouncementIsBooked)
                    return new Response<CarpoolAnnouncement>
                    {
                        Data = null,
                        Message = "Vous ne pouvez pas modifier cette annonce, certains passagers on déja réservé des places sur ce covoiturage",
                        IsSuccess = false,
                        CodeStatus = 403
                    };

                if (carpoolAnnouncement.PickupAddress == carpoolAnnouncement.DropOffAddress)
                    return new Response<CarpoolAnnouncement>
                    {
                        Message = "L'addresse de départ doit être différente de l'addresse d'arrivée",
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 403,
                    };

                if (carpoolAnnouncement.StartDate < DateTime.Now)
                    return new Response<CarpoolAnnouncement>
                    {
                        Message = "La date ne doit pas être antérieure à la date d'aujourd'hui",
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 403,
                    };


                if (carpoolAnnouncement.StartDate < rentalVehicle.StartDate)
                    return new Response<CarpoolAnnouncement>
                    {
                        Message = "La date de début du covoiturage ne doit pas être antérieure à la date de départ de la location du véhicule.",
                        Data = null,
                        IsSuccess = false,
                        CodeStatus = 403,
                    };


                carpoolAnnouncement.StartDate = carpoolAnnouncementDTO.StartDate;
                carpoolAnnouncement.PickupAddressId = carpoolAnnouncementDTO.PickupAddressId;
                carpoolAnnouncement.DropOffAddressId = carpoolAnnouncementDTO.DropOffAddressId;


                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = "L'annonce a été bien mise à jour avec succès",
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception ex)
            {
                return new Response<CarpoolAnnouncement>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }
    }
}