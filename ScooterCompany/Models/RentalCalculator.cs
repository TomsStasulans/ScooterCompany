using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(RentedScooters scooter)
        {
            const int minutesInADay = 1440;
            decimal totalPrice = 0;

            foreach (DateTime day in EachCalendarDay(scooter.RentStarted, scooter.RentEnded.Value))
            {
                decimal calculation = 0;
                if (day.DayOfYear == scooter.RentEnded.Value.DayOfYear)
                {
                    var time = scooter.RentEnded - scooter.RentStarted;
                    calculation = Math.Round((decimal)time.Value.TotalMinutes * scooter.Price, 2);
                    totalPrice += FinalPricePerDay(calculation);
                    return totalPrice;
                }

                if (day.DayOfYear == scooter.RentStarted.DayOfYear)
                {
                    var time = minutesInADay - (int)scooter.RentStarted.TimeOfDay.TotalMinutes;
                    calculation = Math.Round((decimal)time * scooter.Price, 2);
                    totalPrice += FinalPricePerDay(calculation);
                }
                else
                {
                    totalPrice += 20.0m;
                }
            }

            return totalPrice;
        }

        public IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
                return date;
        }

        public static decimal FinalPricePerDay(decimal calculation)
        {
            return calculation > 20.0m ? 20.0m : calculation;
        }
    }
}
