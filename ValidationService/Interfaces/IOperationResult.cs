using System.Collections.Generic;

namespace ValidationService.Interfaces
{
    public interface IOperationResult<TResult>
    {
        TResult Result { get; }

        IReadOnlyList<IOperationFailure> Failures { get; }

        bool Succeed { get; }
    }

}
