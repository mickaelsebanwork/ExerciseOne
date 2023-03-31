using System.Threading;
using System.Threading.Tasks;

namespace Exercise_1.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task Save(CancellationToken cancellationToken = default);
    }
}