using System;

namespace Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}