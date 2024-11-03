using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
           

            carManager.Add(new Car() { CarId = 3,BrandId= 1,ColorId= 1, Description = "as", ModelYear="2334",  DailyPrice = 500 , CarName = "araba3"});

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.CarName);
            }


        }




        
    }
}
