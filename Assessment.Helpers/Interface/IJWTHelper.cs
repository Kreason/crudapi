using Assessment.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Helpers.Interface
{
    public interface IJWTHelper
    {
        Task<string> GenerateJwt(AuthenticationResponse authentication);
    }
}
