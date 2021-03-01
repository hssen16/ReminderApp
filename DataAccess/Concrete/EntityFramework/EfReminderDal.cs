using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReminderDal:EfEntityRepositoryBase<Reminder,ReminderDbContext>,IReminderDal
    {
        public Reminder GetById(int id)
        {
            using (ReminderDbContext context=new ReminderDbContext())
            {
                return context.Set<Reminder>().SingleOrDefault(r => r.Id==id);
            }
        }

        public Reminder GetByCategoryId(int categoryId)
        {
            using (ReminderDbContext context = new ReminderDbContext())
            {
                return context.Set<Reminder>().SingleOrDefault(r => r.CategoryId == categoryId);
            }
        }
    }
}
