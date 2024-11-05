using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentCarContext>, ICarDal
    {
        public List<CarDetailsDto> GetCarDetails()
        {
            using (RentCarContext context = new RentCarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.Brandname equals b.BrandName
                             join col in context.Colors
                             on c.ColorName equals col.ColorName
                             select new CarDetailsDto
                             {
                                 ColorName = col.ColorName,
                                 BrandName = b.BrandName,
                                 CarName = c.CarName,
                             };

           
                      return result.ToList();
                             
                            

            }
        }
    }
}
