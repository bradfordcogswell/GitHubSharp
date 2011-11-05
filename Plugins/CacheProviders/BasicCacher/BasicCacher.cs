﻿using System;
using System.Collections.Generic;
using GithubSharp.Core.Services;

namespace BasicCacher
{
    public class BasicCacher : ICacheProvider
    {
        [ThreadStatic]
        private static Dictionary<string, object> _cache;

        private const string CachePrefix = "GithubSharp.Plugins.CacheProviders.BasicCacher";

        public BasicCacher()
        {
            _cache = new Dictionary<string, object>();
        }

        public T Get<T>(string Name) where T : class
        {
            return Get<T>(Name, DefaultDuractionInMinutes);
        }

        public T Get<T>(string Name, int CacheDurationInMinutes) where T : class
        {
            if (!_cache.ContainsKey(CachePrefix + Name)) return null;
            var cached = _cache[CachePrefix + Name] as CachedObject<T>;
            if (cached == null) return null;

            if (cached.When.AddMinutes(CacheDurationInMinutes) < DateTime.Now)
                return null;

            return cached.Cached;
        }

        public bool IsCached<T>(string Name) where T : class
        {
            return _cache.ContainsKey(CachePrefix + Name);
        }

        public void Set<T>(T ObjectToCache, string Name) where T : class
        {
            var cacheObj = new CachedObject<T>();
            cacheObj.Cached = ObjectToCache;
            cacheObj.When = DateTime.Now;

            _cache[CachePrefix + Name] = cacheObj;
        }

        public void Delete(string Name)
        {
            _cache.Remove(CachePrefix + Name);
        }

        public void DeleteWhereStartingWith(string Name)
        {
            var enumerator = _cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Key.StartsWith(CachePrefix + Name))
                    _cache.Remove(enumerator.Current.Key);
            }
        }

        public void DeleteAll<T>() where T : class
        {
            _cache.Clear();
        }

        public int DefaultDuractionInMinutes
        {
            get { return 20; }
        }
    }
}
