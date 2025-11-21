using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Common.Attributes
{
    public class MaxFileSizeAttribute(long maxFileSizeInBytes) : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null && file.Length > maxFileSizeInBytes)
            {
                return new ValidationResult($"Max file size is {AppConstants.MaxFileSizeInMB} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
