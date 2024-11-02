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
            GetAll(carManager);

            carManager.Add(new Car() { CarId = 1, DailyPrice = 0 , CarName = "1"});

            

        }




        private static void GetAll(CarManager carManager)
        {
            var products = carManager.GetAll();
            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product.Description);
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }
    }
}
