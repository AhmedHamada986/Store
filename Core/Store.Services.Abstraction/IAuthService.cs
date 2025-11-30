using Store.Shard;
using Store.Shard.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstraction
{
    public interface IAuthService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync( RegisterDto registerDto  );

        // Check Email Exists or not 
       Task<bool> CheckEmailExistAsync(string email);

        // Ger Current User 
      Task<UserResultDto?>  GetCurrentUserAsync(string email );
        // Current User Address 
     Task<AddressDto>   GetCurrentUserAddress(string email );

        // Update Current user Address 
       Task<AddressDto> UpdateCurretnUserAddress(AddressDto request,string email );
    }
}
