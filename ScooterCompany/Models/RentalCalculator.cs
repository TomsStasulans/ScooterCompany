using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(RentedScooters scooter)
        {
            decimal minutesInADAy = 1440;
            decimal calculation = 0;

            var time =  scooter.RentEnded - scooter.RentStarted;
            var days = Math.Ceiling(time.Value.TotalMinutes / 1440);

            if (days == 1)
            {
                calculation = Math.Round((decimal)time.Value.TotalMinutes * scooter.Price, 2);
                return CalculateDayPrice(calculation);
            }

            decimal firstDayMinutes = minutesInADAy - (scooter.RentStarted.Minute + scooter.RentStarted.Hour * 60);
            decimal lastDayMinutes = scooter.RentEnded.Value.Hour * 60 + scooter.RentEnded.Value.Minute;

            decimal firstDayPrice = Math.Round(firstDayMinutes * scooter.Price, 2);
            decimal lastDayPrice = Math.Round(lastDayMinutes * scooter.Price, 2);

            calculation += CalculateDayPrice(firstDayPrice);
            calculation += CalculateDayPrice(lastDayPrice);
            calculation += (int)days - 2 * 20.0m;

            return calculation;
        }

        public decimal CalculateDayPrice(decimal calculation)
        {
            return calculation > 20.0m ? 20.0m : calculation;
        }
    }
}
