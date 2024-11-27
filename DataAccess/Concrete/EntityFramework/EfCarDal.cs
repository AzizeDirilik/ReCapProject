using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarDbContext>, ICarDal
    {
        public List<CarDetailsDto> GetCarDetails()
        {
            using (RentACarDbContext context = new RentACarDbContext())
            {
                var result = from c in context.Cars
                             join col in context.Colors on c.ColorId equals col.ColorId 
                             join b in context.Brands on c.BrandId equals b.BrandId
                             select new CarDetailsDto
                             {
                                 ColorName = col.ColorName, 
                                 BrandName = b.BrandName,
                                 DailyPrice = c.DailyPrice,
                                 CarName = c.Description,
                                

                             };
                return result.ToList();
            }
        }
    }
}
