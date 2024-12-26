using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetModelStateError(this ModelStateDictionary dictionary)
        {
            var errors = dictionary.SelectMany(x=>x.Value?.Errors).Select(x=>x.ErrorMessage).ToList();
            return errors;
        }
    }
}
