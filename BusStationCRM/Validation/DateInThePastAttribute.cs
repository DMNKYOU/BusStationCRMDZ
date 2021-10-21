using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusStationCRM.Areas.Identity.Pages.Account
{
    public class DateInThePastAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes.Add("data-val", "true");
        }

        public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
        {

            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now.AddYears(-110) && d <= DateTime.Now.AddYears(-1); //Dates Greater than or equal to today are valid (true)

        }
    }
}