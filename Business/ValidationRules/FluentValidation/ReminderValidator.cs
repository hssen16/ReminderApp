using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ReminderValidator:AbstractValidator<Reminder>
    {
        public ReminderValidator()
        {
            RuleFor(r => r.CategoryId).NotEmpty();
            RuleFor(r => r.CategoryId).GreaterThan(0);
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.DateTime).NotEmpty();
            RuleFor(r => r.Priority).NotEmpty();
        }
    }
}
