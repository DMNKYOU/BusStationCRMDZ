using System;
using System.ComponentModel.DataAnnotations;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusStationCRM.Validation
{
    public class DateCorrectRangeAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes.Add("data-val", "true");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as VoyageModel;

            if (model != null)
            {
                if (model.ArrivalInfo >= model.DepartureInfo
                    && model.DepartureInfo >= DateTime.Now.Date )
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Should be more then departure date");
            }

            return new ValidationResult("Can't validate");
        }
    }
}