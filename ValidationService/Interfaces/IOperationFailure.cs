
namespace ValidationService.Interfaces
{
    public interface IOperationFailure
    {
        string PropertyName { get; }

        string Description { get; }

        string Code { get; }

    }
}
