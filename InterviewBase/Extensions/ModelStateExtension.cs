using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewBase.Extensions
{
    public static class ModelStateExtension
    {
        public static string GetParams(this ICollection<ModelState> states)
        {
            var errors = string.Empty;
            foreach (var item in states)
            {
                errors = $"{errors} {item.Errors[0].ErrorMessage}";
            }

            return errors;
        }
    }
}