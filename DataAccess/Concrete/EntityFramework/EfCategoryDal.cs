using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal:EfEntityRepositoryBase<Category,ReminderDbContext>,ICategoryDal
    {
        public Category GetById(int id)
        {
            using (ReminderDbContext context = new ReminderDbContext())
            {
                return context.Set<Category>().SingleOrDefault(c => c.CategoryId == id);
            }
        }
    }
}
