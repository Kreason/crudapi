using Assessment.DTO.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DTO.Response
{
    public class AuthenticationResponse : BaseResponse
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}
