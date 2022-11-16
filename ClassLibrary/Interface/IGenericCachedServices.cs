using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Interface
{
    public interface IGenericCachedServices<T> where T : class
    {
        IEnumerable<T> GetAll(string cacheKey);
    }
}
