using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ReasnAPI.Tests.Services
{
    public class FakeDbSet<T> : DbSet<T>, IAsyncEnumerable<T>
        where T : class
    {
        private readonly List<T> _data;

        public FakeDbSet()
        {
            _data = [];
        }

        public override T? Find(params object[] keyValues)
        {
            return _data.FirstOrDefault();
        }

        public override EntityEntry<T> Add(T entity)
        {
            _data.Add(entity);
            return null; // Return null for simplicity, adjust as needed
        }

        public override IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<T>(_data.GetEnumerator());
        }

        public override Microsoft.EntityFrameworkCore.Metadata.IEntityType EntityType => throw new NotImplementedException();

        // Implement other methods...

        private class AsyncEnumerator<T>(IEnumerator<T> enumerator) : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator = enumerator;

            public ValueTask DisposeAsync()
            {
                _enumerator.Dispose();
                return ValueTask.CompletedTask;
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return new ValueTask<bool>(_enumerator.MoveNext());
            }

            public T Current => _enumerator.Current;
        }
    }
}