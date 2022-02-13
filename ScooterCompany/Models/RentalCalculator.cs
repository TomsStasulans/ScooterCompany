using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(RentedScooters scooter)
        {
            const int minutesInADAy = 1440;
            decimal calculation = 0;

            var time =  scooter.RentEnded - scooter.RentStarted;
            var days = (int)Math.Ceiling(time.Value.TotalMinutes / minutesInADAy);

            if (days == 1)
            {
                calculation = Math.Round((decimal)time.Value.TotalMinutes * scooter.Price, 2);
                return FinalPricePerDay(calculation);
            }

            decimal firstDayMinutes = minutesInADAy - (scooter.RentStarted.Minute + scooter.RentStarted.Hour * 60);
            decimal lastDayMinutes = scooter.RentEnded.Value.Hour * 60 + scooter.RentEnded.Value.Minute;

            decimal firstDayPrice = Math.Round(firstDayMinutes * scooter.Price, 2);
            decimal lastDayPrice = Math.Round(lastDayMinutes * scooter.Price, 2);

            calculation += FinalPricePerDay(firstDayPrice);
            calculation += FinalPricePerDay(lastDayPrice);
            var fullDaysRented = days - 2 * 20.0m;
            calculation += fullDaysRented > 0 ? fullDaysRented : 0;

            return calculation;
        }

        public static decimal FinalPricePerDay(decimal calculation)
        {
            return calculation > 20.0m ? 20.0m : calculation;
        }
    }
}
