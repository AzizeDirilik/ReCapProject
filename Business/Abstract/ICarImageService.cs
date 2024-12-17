using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add (CarImage carImage);
        IResult Delete (CarImage carImage);
        IResult Update (CarImage carImage);
        IDataResult<CarImage> Get(int carImageId);
        IDataResult<List<CarImage>> GetAll();
        
    }
}
