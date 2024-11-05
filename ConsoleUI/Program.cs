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
           

            carManager.Add(new Car() { CarId = 2,BrandId= 2,ColorId= 2, Description = "Havali Araba", ModelYear="2016",  
                DailyPrice = 500 , CarName = "Araba 2" , ColorName = "Kirmizi", Brandname ="BMW"});


            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.CarName + " / " + car.Brandname + " / " + car.ColorName);
            }


        }




        
    }
}
