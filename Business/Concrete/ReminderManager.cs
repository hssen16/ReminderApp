using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using DataAccess.Abstract;
namespace Business.Concrete
{
    public class ReminderManager : IReminderService
    {
        private IReminderDal _reminderDal;

        public ReminderManager(IReminderDal reminderDal)
        {
            _reminderDal = reminderDal;
        }

        public IDataResult<List<Reminder>> GetAll(Expression<Func<Reminder, bool>> filter = null)
        {
            return filter == null
                ? new SuccessDataResult<List<Reminder>>(_reminderDal.GetAll(), "Tüm hatırlatmalar listelendi")
                : new SuccessDataResult<List<Reminder>>(_reminderDal.GetAll(filter), "Filtreye uyan tüm hatırlatmalar listelendi");
        }

        public IDataResult<Reminder> Get(Expression<Func<Reminder, bool>> filter)
        {
            return new SuccessDataResult<Reminder>(_reminderDal.Get(filter), "Filtreye uyan hatırlatma listelendi");
        }

        public IDataResult<Reminder> GetById(int id)
        {
            return new SuccessDataResult<Reminder>(_reminderDal.GetById(id), "Filtreye uyan hatırlatma listelendi");
        }

        public IDataResult<Reminder> GetByCategoryId(int categoryId)
        {
            return new SuccessDataResult<Reminder>(_reminderDal.GetByCategoryId(categoryId), "Filtreye uyan hatırlatma listelendi");
        }

        public IResult Add(Reminder reminder)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(reminder.Title));

            if (result != null)
            {
                return result;
            }

            _reminderDal.Add(reminder);

            return new SuccessResult(Messages.ReminderAdded);
        }

        public IResult Update(Reminder reminder)
        {
            _reminderDal.Update(reminder);
            return new SuccessResult(Messages.ReminderUpdated);
        }

        public IResult Delete(Reminder reminder)
        {
            _reminderDal.Delete(reminder);
            return new SuccessResult(Messages.ReminderDeleted);
        }
        

        private IResult CheckIfProductNameExists(string title)
        {
            var result = _reminderDal.GetAll(r =>r.Title  == title).Any();
            if (result)
            {
                return new ErrorResult(Messages.ReminderNameAlreadyExists);
            }
            return new SuccessResult();
        }

        

    }
}
