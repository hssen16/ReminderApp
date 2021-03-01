using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null
                ? new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.AllCategoryListed)
                : new SuccessDataResult<List<Category>>(_categoryDal.GetAll(filter), Messages.CategoriesMatchingTheFilterListed);
        }

        public IDataResult<Category> Get(Expression<Func<Category, bool>> filter)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(filter), Messages.CategoryMatchingTheFilterListed);
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.GetById(id), Messages.CategoryMatchingTheFilterListed);
        }

        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryNameExists(category.Name));

            if (result != null)
            {
                return result;
            }
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        private IResult CheckIfCategoryNameExists(string title)
        {
            var result = _categoryDal.GetAll(r => r.Name == title).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
