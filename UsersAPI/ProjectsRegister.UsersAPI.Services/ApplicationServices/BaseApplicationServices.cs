using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProjectsRegister.UsersAPI.Services.ApplicationServices;
public class BaseApplicationServices
{
    protected static void ValidateModel<TEntity>(TEntity _Entity) where TEntity : class
    {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(_Entity);

            if (!Validator.TryValidateObject(_Entity, context, validationResults, validateAllProperties: true))
            {
                throw new Exception(validationResults.Count > 0 ? validationResults.First().ErrorMessage : string.Empty);
            }
    }

    protected static bool EmailValidate(string _Email)
    {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(_Email);
    }
}
