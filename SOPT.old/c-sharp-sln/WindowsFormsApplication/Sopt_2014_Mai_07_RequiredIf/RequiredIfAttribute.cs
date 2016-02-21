using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WindowsFormsApplication.Sopt_2014_Mai_07_RequiredIf
{
    public class RequiredIfAttribute : RequiredAttribute, IClientValidatable
    {
        private String PropertyName { get; set; }
        private Object Comparand { get; set; }

        public RequiredIfAttribute(String propertyName, Object comparand)
        {
            PropertyName = propertyName;
            Comparand = comparand;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();
            var proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);

            if (proprtyvalue.ToString() != Comparand.ToString())
                return ValidationResult.Success;

            var result = base.IsValid(value, context);
            return result;
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var requiredIfRule = new ModelClientValidationRule();
            requiredIfRule.ErrorMessage = this.ErrorMessageString;
            requiredIfRule.ValidationType = "requiredif";
            requiredIfRule.ValidationParameters.Add("propertyname", this.PropertyName);
            requiredIfRule.ValidationParameters.Add("comparand", this.Comparand);
            yield return requiredIfRule;
        }
    }
}