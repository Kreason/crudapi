using Assessment.DTO.DataObjects;
using Assessment.DTO.Request;
using Assessment.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DAL.Interface
{
    public interface IUserDAL
    {
        Task<AuthenticationResponse> Authenticate(string username, string password);
        Task<BaseResponse> Create(CreateUserRequest createUser);
        Task<UserResponse> GetAll();
        Task<BaseResponse> Update(UpdateUserRequest updateUser);
        Task<BaseResponse> Delete(int userID);
    }
}
