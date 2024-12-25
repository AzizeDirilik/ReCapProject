﻿using Business.Abstract;
using Business.Constans;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _carImageDal = carImageDal;
        }
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult? result = BusinessRules.Run(CountByCarId(carImage));
            if (result != null)
            {
                return result;
            }

            string guid = _fileHelper.Add(file);
            carImage.ImagePath = guid;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessDataResult<CarImage>(carImage);

        }

        public IResult Delete(CarImage carImage)
        {
            var result = _carImageDal.GetAll(c => c.CarImageId == carImage.CarImageId && c.ImagePath == carImage.ImagePath);
            if (result != null && result.Any())
            {
                _carImageDal.Delete(carImage);
                _fileHelper.Delete(carImage.ImagePath!);
                return new SuccessResult();
        }
            return new ErrorResult();
    }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            if (carImage == null)
            {
                return new ErrorResult("Car image data is null.");
            }

            if (string.IsNullOrEmpty(carImage.ImagePath))
            {
                return new ErrorResult("ImagePath cannot be null or empty.");
            }

            _fileHelper.Update(file, carImage.ImagePath);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessDataResult<CarImage>(carImage);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.CarImageId == id));
        }


        public IDataResult<List<CarImage>> GetCarById(int carId)
        {
            var carImages = _carImageDal.GetAll(c => c.CarId == carId);
            if (carImages.Count == 0)
            {
                carImages.Add(new CarImage() { CarId = carId, ImagePath = "default.jpeg" });
                return new SuccessDataResult<List<CarImage>>(carImages);
            }
            return new SuccessDataResult<List<CarImage>>(carImages);
        }

        private IResult CountByCarId(CarImage carImage)
        {
            if (_carImageDal.GetAll(c => c.CarId == carImage.CarId).Count >= 5)
            {
                return new ErrorResult();
            }
            else
            {
                return new SuccessResult();
            }
        }


    }
}
