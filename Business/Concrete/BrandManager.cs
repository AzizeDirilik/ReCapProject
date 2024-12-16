﻿using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckBrandExists(brand.BrandName));

            if (result != null)
            {
                return result;
            }
            _brandDal.Add(brand);
            return new SuccessResult();
        }

        public IResult Delete(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckBrandExistence(brand.BrandName , brand.BrandId));
            if (result != null)
            {
                return result;
            }
            _brandDal.Delete(brand);
            return new SuccessResult();
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.UpdatedBrandMessage);
        }

        private IResult CheckBrandExists(string brandName)
        {
            var result = _brandDal.GetAll(b => b.BrandName == brandName).Any();

            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
       
        private IResult CheckBrandExistence(string brandName, int brandId)
        {
            var result = _brandDal.GetAll(b => b.BrandName == brandName && b.BrandId == brandId).Any();
            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
