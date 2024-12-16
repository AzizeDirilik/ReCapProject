using Business.Abstract;
using Business.Constans;
using Core.Utilities.Business;
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
            IResult businessResult = BusinessRules.Run(CheckCarReturned(rental));

            if (businessResult == null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(Rental rental)
        {
            IResult result = BusinessRules.Run(CheckDeletable());
            if (result != null)
            {
                _rentalDal.Delete(rental);
                return new SuccessResult();
            }
            return new ErrorResult("Arac kiradayken silinemez");
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
            var rentalToUpdate = _rentalDal.GetAll(r => r.RentalId == rental.RentalId && r.ReturnDate == null).FirstOrDefault();

            if (rentalToUpdate != null)
            {
                rentalToUpdate.ReturnDate = DateTime.Now;
                _rentalDal.Update(rentalToUpdate);
                Console.WriteLine("Araba teslim edildi");
            }
            else
            {
                Console.WriteLine("Araba zaten teslim edilmiş.");
                return new ErrorResult();
            }

            return new SuccessResult();
        }


        private IResult CheckCarReturned(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId && r.ReturnDate == null).FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine("Hata");

                return new ErrorResult();
            }
            else
            {
                rental.RentDate = DateTime.Now;
                _rentalDal.Add(rental);
                return new SuccessResult();
            }
        }

        private IResult CheckCarReturnDate(int day)
        {
            var result = _rentalDal.GetAll(r => r.ReturnDate.HasValue && r.ReturnDate.Value.Day == day);
            if (result != null && result.Any())
            {
                return new ErrorResult("Ayni arac ayni gun icinde kirilanamaz");
            }
            return new SuccessResult();
        }

        private IResult CheckDeletable()
        {
            var result = _rentalDal.GetAll(r => r.ReturnDate == null).FirstOrDefault();

            if (result != null)
            {
                return new ErrorResult("Arac kiradayken silinemez");
            }
            return new SuccessResult();
        }



    }
}
