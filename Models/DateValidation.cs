using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
public class DateValidation : ValidationAttribute
{

    protected override ValidationResult? IsValid(object dob, ValidationContext validationContext)
    {
        if((DateTime)dob > DateTime.Now)
        {
            return new ValidationResult("Must be a past date!");
        }
        return ValidationResult.Success;
    }
}