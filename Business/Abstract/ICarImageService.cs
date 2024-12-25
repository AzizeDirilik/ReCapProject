using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile formfile, CarImage carImage);
        IResult Delete(CarImage carImage);
        IResult Update(IFormFile formfile, CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int id);
        IDataResult<List<CarImage>> GetCarById(int carId);

    }
}
