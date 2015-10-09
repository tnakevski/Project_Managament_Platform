using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementPlatform.Persistence
{
    public class UserConnectionMapping<T>
    {
        private static UserConnectionMapping<T> _instance;
        private readonly ConcurrentDictionary<T, HashSet<string>> _connections = new ConcurrentDictionary<T, HashSet<string>>();

        public static UserConnectionMapping<T> GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserConnectionMapping<T>();
            }

            return _instance;
        }

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                connections = new HashSet<string>();
                _connections.TryAdd(key, connections);
            }

            lock (connections)
            {
                connections.Add(connectionId);
            }
        }

        public IEnumerable<T> GetKeys()
        {
            var keys = _connections.Keys.ToList<T>();
            if (keys.Count == 0)
                return Enumerable.Empty<T>();
            return keys;
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                return;
            }

            lock (connections)
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    HashSet<string> tempConnections;
                    _connections.TryRemove(key, out tempConnections);
                }
            }
        }

    }
}