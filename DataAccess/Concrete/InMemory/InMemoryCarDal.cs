using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>()
            {
            new Car { CarId = 1, BrandId = 1, ColorId = 1, DailyPrice = 1200, Description = "Havali Araba", ModelYear = "2016" },
            new Car { CarId = 2, BrandId = 2, ColorId = 2, DailyPrice = 1300, Description = "Beyaz Araba", ModelYear = "2017" },
            new Car { CarId = 3, BrandId = 3, ColorId = 3, DailyPrice = 1400, Description = "Kırmızı Araba", ModelYear = "2018" },
            new Car { CarId = 4, BrandId = 4, ColorId = 4, DailyPrice = 1400, Description = "Siyah Araba", ModelYear = "2019" },
            };
        }

      
        public void Add(Car car)
        {
           _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int CarId)
        {
             return _cars.Where(c => c.CarId == CarId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
        }
    }
}
