using System.Threading.Tasks;

namespace OnShop.Framework.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        TResult Handle(TQuery query);
       Task<TResult> HandleAsync(TQuery query);
    }
}