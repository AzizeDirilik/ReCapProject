using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        [SecuredOperation("admin,car.add")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [PerformanceAspect(5)]
        public IResult Add(Car car)
        {
           
            _carDal.Add(car);
            return new SuccessResult(Messages.AddedCarMessage);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.DeletedCarMessage);
        }
        
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());  
        }

        [CacheAspect]
        [PerformanceAspect(2)]
        public IDataResult<Car> GetCar(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            return new ErrorDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails());    
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            Add(car);
            if (car.DailyPrice <= 100)
            {
                throw new Exception("Günlük kiralama ücreti 100 tl'den küçük olamaz."); 
            }
            Update(car);
            return new SuccessResult("Ürün eklendi.");
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.UpdatedCarMessage);
        }
    }
}
