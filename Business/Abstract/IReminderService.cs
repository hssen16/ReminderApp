using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IReminderService
    {
        IDataResult<List<Reminder>> GetAll(Expression<Func<Reminder, bool>> filter = null);
        IDataResult<Reminder> Get(Expression<Func<Reminder, bool>> filter);
        IDataResult<Reminder> GetById(int id);
        IDataResult<Reminder> GetByCategoryId(int categoryId);
        IResult Add(Reminder reminder);
        IResult Update(Reminder reminder);
        IResult Delete(Reminder reminder);
    }
}
