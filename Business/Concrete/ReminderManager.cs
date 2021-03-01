using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
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
       // [CacheAspect]
       // [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Reminder>> GetAll(Expression<Func<Reminder, bool>> filter = null)
        {
            return filter == null
                ? new SuccessDataResult<List<Reminder>>(_reminderDal.GetAll(), Messages.AllReminderListed)
                : new SuccessDataResult<List<Reminder>>(_reminderDal.GetAll(filter), Messages.RemindersMatchingTheFilterListed);
        }

        public IDataResult<Reminder> Get(Expression<Func<Reminder, bool>> filter)
        {
            return new SuccessDataResult<Reminder>(_reminderDal.Get(filter), Messages.ReminderMatchingTheFilterListed);
        }

        public IDataResult<Reminder> GetById(int id)
        {
            return new SuccessDataResult<Reminder>(_reminderDal.GetById(id), Messages.ReminderMatchingTheFilterListed);
        }

        public IDataResult<Reminder> GetByCategoryId(int categoryId)
        {
            return new SuccessDataResult<Reminder>(_reminderDal.GetByCategoryId(categoryId), Messages.ReminderMatchingTheFilterListed);
        }

        //[CacheRemoveAspect("IReminderService.Get")]
        [ValidationAspect(typeof(ReminderValidator))]
        public IResult Add(Reminder reminder)
        {
            IResult result = BusinessRules.Run(CheckIfReminderNameExists(reminder.Title));

            if (result != null)
            {
                return result;
            }

            _reminderDal.Add(reminder);

            return new SuccessResult(Messages.ReminderAdded);
        }

       // [LogAspect(typeof(FileLogger))]
       // [CacheRemoveAspect("IReminderService.Get")]
        [ValidationAspect(typeof(ReminderValidator))]
        public IResult Update(Reminder reminder)
        {
            _reminderDal.Update(reminder);
            return new SuccessResult(Messages.ReminderUpdated);
        }

       // [CacheRemoveAspect("IReminderService.Get")]
        public IResult Delete(Reminder reminder)
        {
            _reminderDal.Delete(reminder);
            return new SuccessResult(Messages.ReminderDeleted);
        }


        private IResult CheckIfReminderNameExists(string title)
        {
            var result = _reminderDal.GetAll(r => r.Title == title).Any();
            if (result)
            {
                return new ErrorResult(Messages.ReminderNameAlreadyExists);
            }
            return new SuccessResult();
        }



    }
}
