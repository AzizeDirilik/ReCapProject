﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IResult TransactionalOperation(Car car);
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetCar(int CarId);
        IDataResult<List<CarDetailsDto>> GetCarDetails();

        

    }
}
