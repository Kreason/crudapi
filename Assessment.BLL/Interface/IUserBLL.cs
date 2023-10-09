using Assessment.DTO.DataObjects;
using Assessment.DTO.Request;
using Assessment.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.BLL.Interface
{
    public interface IUserBLL
    {
        // these are interfaces that are used to access the methods that perform transformations in the data and process them further 
        Task<AuthenticationResponse> Authenticate(string username, string password);
        Task<BaseResponse> Create(CreateUserRequest createUser);
        Task<UserResponse> GetAll();
        Task<BaseResponse> Update(UpdateUserRequest updateUser);
        Task<BaseResponse> Delete(int userId);
    }
}
