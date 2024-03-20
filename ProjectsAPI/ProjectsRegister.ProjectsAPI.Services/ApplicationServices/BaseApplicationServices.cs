using System.ComponentModel.DataAnnotations;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public class BaseApplicationServices
{
    protected static void ValidateModel<TEntity>(TEntity _Entity) where TEntity : class
    {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(_Entity);

            if (!Validator.TryValidateObject(_Entity, context, validationResults, validateAllProperties: true))
            {
                throw new Exception(validationResults.Count > 0 ? validationResults.First().ErrorMessage : string.Empty);
            }
    }

    protected static bool ValidateGuid(Guid guid)
        => guid != Guid.Empty;
}
