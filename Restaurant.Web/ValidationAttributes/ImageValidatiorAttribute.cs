using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.ValidationAttributes
{
    public class ImageValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var image = value as IFormFile;

            if (image == null)
            {
                return ValidationResult.Success;
            }
            

            if (image.ContentType == null ||
                image.ContentType != "image/png" &&
                image.ContentType != "image/jpeg" &&
                image.ContentType != "image/jpg")
            {
                return new ValidationResult("Only .jpeg, .jpg and .png formats are accepted.");
            }

            return ValidationResult.Success;
        }
    }
}
