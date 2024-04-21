using StackExchange.Redis;
using System;

namespace eTamir.Services.Favorites.Services
{
    public interface IRedisService : IDisposable
    {
        void Connect();
        IDatabase GetDatabase(int db = 1);
    }
}
