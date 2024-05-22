namespace Ecomove.Api.Helpers
{
    public class Tools
    {

        /// <summary>
        /// Permet de mettre la premier lettre en majuscule
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstLetterToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string upper = s.ToUpper();
            string lower = s.ToLower();

            string result = upper[0] + lower[1..];

            return result;
        }






        public string TestDate(int newStartDate, int newEndDate)
        {
            // Recuperation de toutes les locations dont la startdate est >= à la nouvelle startdate 

            //RentalVehicle re = new RentalVehicle();

            ////List<RentalVehicle> rentals = _ecoMoveDbContext.RentalVehicles.Where(r => r.VehicleId == re.VehicleId && r.StartDate >= re.StartDate && r.StartDate <= r.EndDate).ToList();
            //List<RentalVehicle> rentals = _ecoMoveDbContext.RentalVehicles.Where(r => r.StartDate >= re.StartDate && r.StartDate <= r.EndDate).ToList();
            int[][] rentalDates = [[2, 5], [8, 10], [20, 23]];

            foreach (var rentalDate in rentalDates)
            {
                if (rentalDate[0] >= newStartDate && rentalDate[0] <= newEndDate && newStartDate > rentalDate[1])
                {
                    return "Pas possible";
                }
            }

            return "Possible";
        }
    }
}


