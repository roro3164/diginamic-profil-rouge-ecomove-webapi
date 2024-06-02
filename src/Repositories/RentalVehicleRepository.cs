using Ecomove.Api.Data.Models;
using Ecomove.Api.Data;
using Ecomove.Api.DTOs.RentalVehicleDTO;
using Ecomove.Api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using ErrorOr;

namespace Ecomove.Api.Repositories
{
    public class RentalVehicleRepository(EcoMoveDbContext ecoMoveDbContext, ICarpoolAnnouncementRepository carpoolAnnouncementRepository) : IRentalVehicleRepository
    {
        public async Task<ErrorOr<Created>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO)
        {
            Vehicle? vehicle = await ecoMoveDbContext.Vehicles
                .Include(v => v.RentalVehicles)
                .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

            if (vehicle is null) return Error.NotFound(description: $"Le véhicle n'existe pas");
            if (vehicle.StatusId != 1) return Error.NotFound(description: $"Le véhicle que vous voulez n'est pas en service");

            // À refactorer car utilisé dans d'autres méthodes
            if (rentalVehicleDTO.EndDate < rentalVehicleDTO.StartDate)
            {
                return Error.Conflict(description: $"La date de fin ne peut pas être inférieur à la date de début");
            }
            else if (rentalVehicleDTO.StartDate.ToString("d") == rentalVehicleDTO.EndDate.ToString("d"))
            {
                return Error.Conflict(description: $"La date minimale d'une réservation est de 1 jour");
            }
            else if (rentalVehicleDTO.StartDate.Date < DateTime.Now.Date)
            {
                return Error.Conflict(description: $"La date de début ne peut pas être antérieur à la date du jour");
            }

            // À refectoré car utilisé autre part
            if (vehicle.RentalVehicles != null)
            {
                // Récupération des réservations de véhicule qui sont confirmées et que la date de 
                List<RentalVehicle>? rentalVehiclesConfirmed = vehicle.RentalVehicles.Where(d => d.Confirmed == true).ToList();

                // Vérification que les nouvelles ne sont pas déjà réservées
                foreach (var rentalVehicleConfirmed in rentalVehiclesConfirmed)
                {
                    DateTime newStartDate = rentalVehicleDTO.StartDate.Date;
                    DateTime newEndDate = rentalVehicleDTO.EndDate.Date;
                    DateTime currentStartDate = rentalVehicleConfirmed.StartDate.Date;
                    DateTime currentEndDate = rentalVehicleConfirmed.EndDate.Date;

                    if (
                        (newStartDate >= currentStartDate && newStartDate <= currentEndDate) ||
                        (newEndDate >= currentStartDate && newEndDate <= currentEndDate) ||
                        (newStartDate <= currentStartDate && newEndDate >= currentEndDate)
                    )
                    {
                        return Error.Conflict(description: $"Les nouvelles dates que vous voulez ne sont pas disponibles");
                    }
                }
            }

            RentalVehicle newRentalVehicle = new RentalVehicle
            {
                StartDate = rentalVehicleDTO.StartDate,
                EndDate = rentalVehicleDTO.EndDate,
                VehicleId = vehicleId,
                AppUserId = userId,
                Confirmed = true
            };

            try
            {
                await ecoMoveDbContext.RentalVehicles.AddAsync(newRentalVehicle);
                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Created;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        public async Task<ErrorOr<Deleted>> CancelRentalVehicleAsync(string idUserConnect, Guid rentalId)
        {
            try
            {
                RentalVehicle? rentalVehicle = await ecoMoveDbContext.RentalVehicles
                    .Include(r => r.CarpoolAnnouncement)
                    .FirstOrDefaultAsync(r => r.RentalVehicleId == rentalId);

                if (rentalVehicle is null) return Error.NotFound(description: $"Aucune location ne correspond à cette ID");
                if (rentalVehicle.AppUserId != idUserConnect) return Error.Forbidden(description: $"Vous ne pouvez pas annuler cette réservation");

                if (rentalVehicle.CarpoolAnnouncement != null)
                {
                    await carpoolAnnouncementRepository.DeleteCarpoolAnnouncement(rentalId, idUserConnect);
                }

                rentalVehicle.Confirmed = false;
                await ecoMoveDbContext.SaveChangesAsync();

                return Result.Deleted;
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        // Ajout plus de vérification
        public async Task<ErrorOr<List<RentalVehicle>>> GetAllRentalVehiclesAysnc(string idUserConnect)
        {
            try
            {
                return await ecoMoveDbContext.RentalVehicles
                    .Where(r => r.AppUserId == idUserConnect)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return Error.Unexpected(description: e.Message);
            }
        }


        // Ajouter plus d'éléments à renvoyer
        public async Task<ErrorOr<RentalVehicle>> GetRentalVehicleByIdAysnc(string idUserConnect, Guid rentalId)
        {
            try
            {
                RentalVehicle? rentalVehicle = await ecoMoveDbContext.RentalVehicles.FirstOrDefaultAsync(r => r.RentalVehicleId == rentalId);

                if (rentalVehicle is null) return Error.NotFound(description: $"La réservation que vous voulez n'existe pas");
                if (rentalVehicle.AppUserId != idUserConnect) return Error.Forbidden(description: $"Vous ne pouvez pas accéder cette réservation");

                return rentalVehicle;
            }
            catch (Exception e)
            {
                return Error.Unexpected(e.Message);
            }
        }


        // Manque verification sur les reservations presentes en BDD
        public async Task<ErrorOr<Updated>> UpdateRentalVehicleAsync(string idUserConnect, Guid rentalId, RentalVehicleDTO rentalVehicleDTO)
        {

            try
            {
                RentalVehicle? rentalVehicle = await ecoMoveDbContext.RentalVehicles
                   .Include(r => r.CarpoolAnnouncement.Bookings)
                   .FirstOrDefaultAsync(r => r.RentalVehicleId == rentalId);

                if (rentalVehicle is null) return Error.NotFound(description: $"Aucune location ne correspond à cette ID");

                // S'il a un covoiturage, vérifier que la date de covoiturage rentre bien dans les nouvelles dates de réservation
                if (rentalVehicle.CarpoolAnnouncement != null)
                {
                    if (rentalVehicle.CarpoolAnnouncement.StartDate > rentalVehicle.StartDate && rentalVehicle.CarpoolAnnouncement.StartDate < rentalVehicle.EndDate)
                    {
                        return Error.Conflict(description: $"Vous avez un covoiturage avec une date qui ne correspond pas à vos nouvelles dates de réservations");
                    }
                }

                if (rentalVehicle.CarpoolAnnouncement == null)
                { 
                    // À refactorer car utilisé dans d'autres méthodes
                    if (rentalVehicleDTO.EndDate < rentalVehicleDTO.StartDate)
                    {
                        return Error.Conflict(description: $"La date de fin ne peut pas être inférieur à la date de début");
                    }
                    else if (rentalVehicleDTO.StartDate.ToString("d") == rentalVehicleDTO.EndDate.ToString("d"))
                    {
                        return Error.Conflict(description: $"La date minimale d'une réservation est de 1 jour");
                    }
                    else if (rentalVehicleDTO.StartDate.Date < DateTime.Now.Date)
                    {
                        return Error.Conflict(description: $"La date de début ne peut pas être antérieur à la date du jour");
                    }
                }

                // Vérifier que le covoiturage n'a pas de places réservées
                else if (rentalVehicle.CarpoolAnnouncement.Bookings != null)
                {
                    if (rentalVehicle.CarpoolAnnouncement.Bookings.Count != 0)
                    {
                        return Error.Conflict(description: $"Vous ne pouvez modifier cette réservation car vous avez des passagers pour votre covoiturage");
                    }
                }

                // Récupération des réservations de véhicule qui sont confirmées et que la date de 
                List<RentalVehicle>? rentalVehiclesConfirmed = ecoMoveDbContext.RentalVehicles.Where(r => r.VehicleId == rentalVehicle.VehicleId && r.Confirmed == true).ToList();

                // Vérification que les nouvelles ne sont pas déjà réservées
                foreach (var rentalVehicleConfirmed in rentalVehiclesConfirmed)
                {
                    DateTime newStartDate = rentalVehicleDTO.StartDate.Date;
                    DateTime newEndDate = rentalVehicleDTO.EndDate.Date;
                    DateTime currentStartDate = rentalVehicleConfirmed.StartDate.Date;
                    DateTime currentEndDate = rentalVehicleConfirmed.EndDate.Date;

                    if (
                        (newStartDate >= currentStartDate && newStartDate <= currentEndDate) ||
                        (newEndDate >= currentStartDate && newEndDate <= currentEndDate) ||
                        (newStartDate <= currentStartDate && newEndDate >= currentEndDate)
                    )
                    {
                        return Error.Conflict(description: $"Les nouvelles dates que vous voulez ne sont pas disponibles");
                    }
                }

                rentalVehicle.StartDate = rentalVehicleDTO.StartDate;
                rentalVehicle.EndDate = rentalVehicleDTO.EndDate;

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