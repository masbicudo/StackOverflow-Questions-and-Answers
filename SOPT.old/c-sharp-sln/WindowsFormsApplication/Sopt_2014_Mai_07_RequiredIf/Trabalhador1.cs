using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WindowsFormsApplication.Sopt_2014_Mai_07_RequiredIf
{
    public class Trabalhador1 : IValidatableObject, IClientValidatable
    {
        [Required(ErrorMessage = "*")]
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public String Nome { get; set; }

        public String Aposentado { get; set; }

        public DateTime DataAposentadoria { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (this.Aposentado == "S")
                yield return new ValidationResult("DataAposentadoria é requerido quando Aposentado é 'S'.");
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var requiredIfRule = new ModelClientValidationRule();
            requiredIfRule.ErrorMessage = "DataAposentadoria é requerido quando Aposentado é 'S'.";
            requiredIfRule.ValidationType = "requiredif";
            requiredIfRule.ValidationParameters.Add("propertyname", "Aposentado");
            requiredIfRule.ValidationParameters.Add("comparand", "S");
            yield return requiredIfRule;
        }
    }
}