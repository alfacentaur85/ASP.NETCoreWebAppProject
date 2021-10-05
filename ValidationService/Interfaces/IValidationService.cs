using System.Collections.Generic;

namespace ValidationService.Interfaces
{
    public interface IValidationService<TEntity>
            where TEntity : class
    {
        IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item);
    }

}
