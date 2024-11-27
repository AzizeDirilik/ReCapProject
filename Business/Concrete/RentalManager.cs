using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {



            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId && r.ReturnDate == null).FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine("Hata");

                return new ErrorResult();
            }
            else
            {
                _rentalDal.Add(rental);
                return new SuccessResult();   
            }


            //var carIsNull = _rentalDal.GetAll().FirstOrDefault(r => r.ReturnDate == null 
            //&& r.CarId == rental.CarId 
            //&& r.CustomerId ==rental.CustomerId);
            //if (carIsNull != null)
            //{
            //    rental.ReturnDate = DateTime.Now;   
            //    _rentalDal.Add(rental);
            //    return new SuccessResult();
            //}
            //var result = _rentalDal.GetAll().FirstOrDefault(r => r.ReturnDate !=  null 
            //&& r.CarId == rental.CarId 
            //&& r.CustomerId == rental.CustomerId );
            //if (result != null)
            //{
            //    _rentalDal.Add(rental);
            //    return new SuccessResult();
            //}



            //else
            //{
            //    return new ErrorResult();
            //}

        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == rentalId));  
        }

        public IResult Update(Rental rental)
        {

            var result = _rentalDal.GetAll(r => r.RentalId == rental.RentalId && r.ReturnDate == null).FirstOrDefault();

            if (result != null)
            {

                rental.ReturnDate = DateTime.Now;
                _rentalDal.Update(rental);
                Console.WriteLine("Araba teslim edildi");
            }
            else
            {
                Console.WriteLine("Araba Teslim Edilmis");
            }

            return new SuccessResult();

          
        }
    }
}
