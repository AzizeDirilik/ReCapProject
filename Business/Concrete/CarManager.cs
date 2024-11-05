using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public void Add(Car car)
        {

            if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            {
                Console.WriteLine("Araba eklendi");
                _carDal.Add(car);
            }
            else if (car.DailyPrice <= 0 && car.CarName.Length >= 2)
            {
                Console.WriteLine("Fiyat 0'dan kucuk olamaz");

            }
            else if (car.CarName.Length <= 2 && car.DailyPrice > 0)
            {
                Console.WriteLine("Araba ismi minimum 2 karakter olmali");
            }
            else
            {
                Console.WriteLine("Araba ismi minimum 2 karakter olmali ve fiyat 0'dan buyuk olmali");
            }

        }

        public void Update(Car car)
        {
            _carDal.Update(car);

        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<CarDetailsDto> GetCarDetails()
        {
           return _carDal.GetCarDetails();
        }
    }
}
