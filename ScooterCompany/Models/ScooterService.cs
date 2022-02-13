using ScooterCompany.Exception;
using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class ScooterService : IScooterService
    {
        private IList<Scooter> _scooterList = new List<Scooter>();
        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute <= 0)
            {
                throw new InvalidPriceException();
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid id provided");
            }

            if (_scooterList.Any(s => s.Id == id))
            {
                throw new ScooterDuplicateException();
            }

            _scooterList.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            if (_scooterList.All(s => s.Id != id))
            {
                throw new ScooterNotFoundException();
            }

            _scooterList.Remove(_scooterList.First(s => s.Id == id));
        }

        public IList<Scooter> GetScooters()
        {
            return _scooterList.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooterList.FirstOrDefault(s => s.Id == scooterId);
            if (scooter == null)
            {
                throw new ScooterNotFoundException();
            }

            return scooter;
        }
    }
}