using StackExchange.Redis;
using System;

namespace eTamir.Services.Favorites.Services
{
    public class RedisService : IRedisService
    {
        private readonly string connectionString;
        private ConnectionMultiplexer connectionMultiplexer;
        private bool disposed = false;

        public RedisService(string host, int port)
        {
            connectionString = $"{host}:{port}";
        }

        public void Connect()
        {
            if (connectionMultiplexer == null || !connectionMultiplexer.IsConnected)
            {
                connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            }
        }

        public IDatabase GetDatabase(int db = 1)
        {
            if (connectionMultiplexer == null || !connectionMultiplexer.IsConnected)
            {
                throw new InvalidOperationException("Redis connection is not established.");
            }
            return connectionMultiplexer.GetDatabase(db);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connectionMultiplexer?.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
