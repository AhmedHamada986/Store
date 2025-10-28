using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstraction
{
    public interface ICashService
    {
        Task SetCasheValueAsync(string key , object value , TimeSpan duration);
        Task<string?> GetCashVlueAsync(string key );
        
    }
}
