using ecomove_back.Data.Models;
using ecomove_back.Data;
using ecomove_back.DTOs.RentalVehicleDTO;
using ecomove_back.Helpers;
using ecomove_back.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ecomove_back.Repositories
{
    public class RentalVehicleRepository : IRentalVehicleRepository
    {
        private EcoMoveDbContext _ecoMoveDbContext;

        public RentalVehicleRepository(
            EcoMoveDbContext ecoMoveDbContext
        )
        {
            _ecoMoveDbContext = ecoMoveDbContext;
        }



        // Manque verification sur les reservations futures changer userID dans le controller par le user connecte
        public async Task<Response<string>> CreateRentalVehicleAsync(string userId, Guid vehicleId, RentalVehicleDTO rentalVehicleDTO)
        {
            // Vérification que le vehicule existe bien en BDD
            Vehicle? vehicle = await _ecoMoveDbContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

            if (vehicle == null)
            {
                return new Response<string>
                {
                    Message = "Le véhicle n'existe pas",
                    IsSuccess = false,
                    CodeStatus = 404
                };
            }

            // À refactorer
            if (rentalVehicleDTO.EndDate < rentalVehicleDTO.StartDate)
            {
                return new Response<string>
                {
                    Message = "La date de fin ne peut pas être inférieur à la date de début",
                    IsSuccess = false,
                    CodeStatus = 400
                };
            } 
            else if (rentalVehicleDTO.EndDate.ToString("d") == DateTime.Now.ToString("d"))
            {
                return new Response<string>
                {
                    Message = "La date minimale d'une réservation est de 1 jour",
                    IsSuccess = false,
                    CodeStatus = 400
                };
            } 
            else if (rentalVehicleDTO.StartDate.Date < DateTime.Now.Date)
            {
                return new Response<string>
                {
                    Message = "La date de début ne peut pas être antérieur à la date du jour",
                    IsSuccess = false,
                    CodeStatus = 400
                };
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
                await _ecoMoveDbContext.RentalVehicles.AddAsync(newRentalVehicle);
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = $"Votre réservation pour le {rentalVehicleDTO.StartDate} au {rentalVehicleDTO.EndDate} a bien été crée",
                    IsSuccess = true,
                    CodeStatus = 201
                };
            }
            catch (Exception e)
            {
                return new Response<string>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }

        // Manque verification sur les reservations presentes en BDD et verifier aussi que le user est bien celui qui a fait la reservation
        public async Task<Response<RentalVehicleDTO>> UpdateRentalVehicleAsync(int rentalId, RentalVehicleDTO rentalVehicleDTO)
        {
            try
            {
                RentalVehicle? rentalVehicle = await _ecoMoveDbContext.RentalVehicles.FirstOrDefaultAsync(r => r.RentalVehicleId == rentalId);   

                if (rentalVehicle == null)
                {
                    return new Response<RentalVehicleDTO>
                    {
                        Message = "Aucune location ne correspond à cette ID",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                // À refactorer
                if (rentalVehicleDTO.EndDate < rentalVehicleDTO.StartDate)
                {
                    return new Response<RentalVehicleDTO>
                    {
                        Message = "La date de fin ne peut pas être inférieur à la date de début",
                        IsSuccess = false,
                        CodeStatus = 400
                    };
                }
                else if (rentalVehicleDTO.EndDate.ToString("d") == DateTime.Now.ToString("d"))
                {
                    return new Response<RentalVehicleDTO>
                    {
                        Message = "La date minimale d'une réservation est de 1 jour",
                        IsSuccess = false,
                        CodeStatus = 400
                    };
                }
                else if (rentalVehicleDTO.StartDate.Date < DateTime.Now.Date)
                {
                    return new Response<RentalVehicleDTO>
                    {
                        Message = "La date de début ne peut pas être antérieur à la date du jour",
                        IsSuccess = false,
                        CodeStatus = 400
                    };
                }

                rentalVehicle.StartDate = rentalVehicleDTO.StartDate;
                rentalVehicle.EndDate = rentalVehicleDTO.EndDate;
                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<RentalVehicleDTO>
                {
                    Message = "Votre réservatiopn a bien été modifiée",
                    IsSuccess = true,
                    CodeStatus = 201
                };

            }
            catch (Exception e)
            {
                return new Response<RentalVehicleDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }


        // Verifier également que la réservation n'a pas de réservation de covoiturage
        public async Task<Response<string>> CancelRentalVehicleAsync(int rentalId)
        {
            try
            {
                RentalVehicle? rentalVehicle = await _ecoMoveDbContext.RentalVehicles.FirstOrDefaultAsync(r => r.RentalVehicleId == rentalId);

                if (rentalVehicle == null)
                {
                    return new Response<string>
                    {
                        Message = "Aucune location ne correspond à cette ID",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                rentalVehicle.Confirmed = false;

                await _ecoMoveDbContext.SaveChangesAsync();

                return new Response<string>
                {
                    Message = "Votre réservatiopn a bien été annulée",
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<string>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }


        // Ajout plus de vérification
        public async Task<Response<List<AllRentalVehicles>>> GetAllRentalVehiclesAysnc()
        {
            try
            {
                List<RentalVehicle> rentalVehicles = await _ecoMoveDbContext.RentalVehicles.ToListAsync();

                if (rentalVehicles.Count == 0)
                {
                    return new Response<List<AllRentalVehicles>>
                    {
                        Message = "Aucune réservation trouvée",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                List<AllRentalVehicles> rentalvehiclesDTO = new List<AllRentalVehicles>();

                foreach (RentalVehicle rentalVehicle in rentalVehicles)
                {
                    rentalvehiclesDTO.Add(new AllRentalVehicles
                    {
                        RentalVehicleId = rentalVehicle.RentalVehicleId,
                        StartDate = rentalVehicle.StartDate,
                        EndDate = rentalVehicle.EndDate,
                    });
                }

                return new Response<List<AllRentalVehicles>>
                {
                    Data = rentalvehiclesDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<List<AllRentalVehicles>>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }
        }


        // Ajouter plus d'éléments à renvoyer
        public async Task<Response<SingleRentalVehicleDTO>> GetRentalVehicleByIdAysnc(int rentalId)
        {
            try
            {
                RentalVehicle? rentalVehicle = await _ecoMoveDbContext.RentalVehicles.FirstOrDefaultAsync(r => r.RentalVehicleId == rentalId);

                if (rentalVehicle == null)
                {
                    return new Response<SingleRentalVehicleDTO>
                    {
                        Message = "La réservation que vous voulez n'existe pas",
                        IsSuccess = false,
                        CodeStatus = 404
                    };
                }

                SingleRentalVehicleDTO rentalVehicleDTO = new SingleRentalVehicleDTO
                {
                    RentalVehicleId = rentalVehicle.RentalVehicleId,
                    StartDate = rentalVehicle.StartDate,
                    EndDate = rentalVehicle.EndDate,
                };

                return new Response<SingleRentalVehicleDTO>
                {
                    Data = rentalVehicleDTO,
                    IsSuccess = true,
                    CodeStatus = 200
                };
            }
            catch (Exception e)
            {
                return new Response<SingleRentalVehicleDTO>
                {
                    Message = e.Message,
                    IsSuccess = false,
                    CodeStatus = 500
                };
            }


        }




    }
}