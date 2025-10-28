using Store.Domain.Contracts;
using Store.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public class CashService (ICashRepository cashRepository): ICashService
    {
        public async Task<string?> GetCashVlueAsync(string key)
        {
          var value =await  cashRepository.GetAsync(key);
            return value == null ? null : value;    
        }

        public async Task SetCasheValueAsync(string key, object value, TimeSpan duration)
        {
            await cashRepository.SetAsync(key,value,duration);
        }
    }
}
