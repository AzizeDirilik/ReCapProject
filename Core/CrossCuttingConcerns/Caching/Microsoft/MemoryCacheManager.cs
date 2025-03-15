using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private static readonly ConcurrentDictionary<string, byte> _keys = new ConcurrentDictionary<string, byte>();

        public MemoryCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
            _keys.TryAdd(key, 0);
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _keys.TryRemove(key, out _);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = _keys.Keys.Where(k => regex.IsMatch(k)).ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
                _keys.TryRemove(key, out _);
            }
        }
    }
}