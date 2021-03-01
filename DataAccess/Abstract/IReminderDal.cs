using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IReminderDal:IEntityRepository<Reminder>
    {
        Reminder GetById(int id);
        Reminder GetByCategoryId(int categoryId);
    }
}
