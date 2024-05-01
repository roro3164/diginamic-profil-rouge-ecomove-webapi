using ecomove_back.Data.Models;
using ecomove_back.DTOs.CarpoolBookingDTOs;
using ecomove_back.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomove_back.Interfaces.IRepositories
{
    public interface ICarpoolBookingRepository
    {
        Task<Response<CarpoolBookingCreateDTO>> CreateCarpoolBookingAsync(CarpoolBookingCreateDTO bookingCreateDTO);
        Task<Response<string>> CancelCarpoolBookingAsync(int id);

        //Task<List<CarpoolBooking>> GetCarpoolBookingsByUserIdAsync(string userId);
        //Task<List<CarpoolBooking>> GetFutureCarpoolBookingsByUserIdAsync(string userId);
        //Task<List<CarpoolBooking>> GetPastCarpoolBookingsByUserIdAsync(string userId);
    }
}