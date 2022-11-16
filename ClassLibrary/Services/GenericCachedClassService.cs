using Product.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Product.Context;

namespace Product.Services
{
    public class GenericCachedClassService<T> : IGenericCachedServices<T> where T : class
    {
        private ProductContext _db;
        private int _rowsCount = 20;
        private IMemoryCache _cache;
        private DbSet<T> _table;
        private int _seconds = 262;

        public GenericCachedClassService(ProductContext contextDb, IMemoryCache memoryCache)
        {
            this._db = contextDb;
            this._cache = memoryCache;
            this._table = _db.Set<T>();
        }
        public IEnumerable<T> GetAll(string cacheKey)
        {
            IEnumerable<T>? elements = null;
            Type type = typeof(T);
            if (!_cache.TryGetValue(cacheKey, out elements))
            {
                elements = _table.Take(_rowsCount).ToList();
                if (elements != null)
                {
                    _cache.Set(cacheKey, elements, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(_seconds)));
                }
                else
                {
                    throw new Exception($"Any problems with cache _rowCount element in table {type.Name}");
                }
            }
            return elements;

        }
    }
}
