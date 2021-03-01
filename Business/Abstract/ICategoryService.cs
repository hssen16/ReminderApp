using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll(Expression<Func<Category, bool>> filter = null);
        IDataResult<Category> Get(Expression<Func<Category, bool>> filter);
        IDataResult<Category> GetById(int id);
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
    }
}
