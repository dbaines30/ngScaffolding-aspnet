﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngScaffolding.database.Models;

namespace ngScaffolding.database.ConfigHelpers
{
    public class ReferenceValueHelper
    {
        public static ReferenceValue Add(ngScaffoldingContext context, ReferenceValue reference)
        {
            ReferenceValue newReference = null;

            if (context.ReferenceValues.Any(m => m.Name == reference.Name))
            {
                newReference = context.ReferenceValues.First(m => m.Name == reference.Name);
            }
            else
            {
                newReference = new ReferenceValue() { Name = reference.Name };
                context.ReferenceValues.Add(newReference);
            }
            
            newReference.GroupName = reference.GroupName;
            newReference.Type = reference.Type;
            newReference.Value = reference.Value;
            newReference.ConnectionName = reference.ConnectionName;
            newReference.CacheSeconds = reference.CacheSeconds;
            newReference.Authorisation = reference.Authorisation;
            newReference.InputDetails = reference.InputDetails;
            
            context.SaveChanges();

            return newReference;
        }
    }
}
