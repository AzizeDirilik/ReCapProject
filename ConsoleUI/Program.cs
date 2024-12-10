using Business.Concrete;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // NewCustomerAdd();

            // NewUserAdd();

            RentalManager rentalManager = new RentalManager(new EfRentalDal());



            rentalManager.Add(new Rental
            {

                CarId = 1,
                CustomerId = 1,
                RentDate = DateTime.Now,


            });


            //rentalManager.Update(new Rental
            //{
            //    RentalId = 23,
            //    CarId = 1,
            //    CustomerId = 1,
            //});

            CarManager carManager = new CarManager(new EfCarDal());

            // CarGetAll(carManager);

            // CarGetAll(carManager);

            // DeleteBrand();

            //GetCarDetails(carManager);

        }

        private static void GetCarDetails(CarManager carManager)
        {
            var result = carManager.GetCarDetails();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.BrandName + " / " + item.ColorName + " / " + item.CarName + " / " + item.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void DeleteBrand()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Delete(new Brand { BrandId = 6 });
        }

        private static void CarGetAll(CarManager carManager)
        {
            var result = carManager.GetAll();
            foreach (var item in result.Data)
            {
                Console.WriteLine(item.Description);
            }
        }

        private static void NewCarAdded(CarManager carManager)
        {
            carManager.Add(new Car
            {
                BrandId = 11,
                ColorId = 1,
                DailyPrice = 2000,
                Description = "Mercedes",
                ModelYear = 2024
            });
        }

        private static void NewCustomerAdd()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            customerManager.Add(new Customer
            {
                UserId = 3,
                CompanyName = "Test",
            });
        }

        private static void NewUserAdd()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            userManager.Add(new User
            {
                FirstName = "Azize",
                LastName = "Dirilik",
                Email = "Azizedirilik",
                Password = "123",

            });
        }
    }
}
