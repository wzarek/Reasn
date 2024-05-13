﻿using ReasnAPI.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ReasnAPI.Validators
{
    public class ObjectTypeValidator : IValidator<ObjectType>
    {
        public static IEnumerable<ValidationResult> Validate(ObjectType objectType)
        {
            if (string.IsNullOrWhiteSpace(objectType.Name))
            {
                yield return new ValidationResult("Name is required", [nameof(objectType.Name)]);
            }

            if (objectType.Name.Length > 32)
            {
                yield return new ValidationResult("Name is too long", [nameof(objectType.Name)]);
            }

            if (new Regex("^(Event|User)$").IsMatch(objectType.Name) is false)
            {
                yield return new ValidationResult("Name is invalid", [nameof(objectType.Name)]);
            }
        }
    }
}