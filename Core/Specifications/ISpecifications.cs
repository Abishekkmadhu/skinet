using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecifications<T>  //Creating Generic methods
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }
    }
}
