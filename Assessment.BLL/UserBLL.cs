using Assessment.BLL.Interface;
using Assessment.DAL.Interface;
using Assessment.DTO.DataObjects;
using Assessment.DTO.Request;
using Assessment.DTO.Response;
using Assessment.Helpers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.BLL
{
    // this is the class that inherits the bll interface and generates the required methods that process data
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _userDAL;
        private readonly IJWTHelper _jWTHelper;

        public UserBLL(IUserDAL userDAL, IJWTHelper jWTHelper)
        {
            _userDAL = userDAL;
            _jWTHelper = jWTHelper;
        }

        // the blls are used to then process data by sending them to data access layers which interact with the required databases
        public async Task<AuthenticationResponse> Authenticate(string username, string password)
        {
            var response = new AuthenticationResponse();
            try
            {
                password = await GetSha256(password);
                response = await _userDAL.Authenticate(username, password);

                if (response.Code == 1)
                {
                    response.Token = await _jWTHelper.GenerateJwt(response);
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 0;
            }
            return response;
        }

        public async Task<BaseResponse> Create(CreateUserRequest createUser)
        {
            var response = new BaseResponse();
            try
            {
                createUser.Password = await GetSha256(createUser.Password);
                response = await _userDAL.Create(createUser);
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 0;
            }
            return response;
        }

        public async Task<BaseResponse> Delete(int userId)
        {
            var response = new BaseResponse();
            try
            {
                response = await _userDAL.Delete(userId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 0;
            }
            return response;
        }

        public async Task<UserResponse> GetAll()
        {
            var response = new UserResponse();
            try
            {
                response = await _userDAL.GetAll();
                if (response.Users.Count > 0)
                {
                    response.Code = response.Users.Count;
                    response.Message = "Data Available";
                }
            }
            catch( Exception ex )
            {
                response.Message = ex.Message;
                response.Code = 0;
            }
            return response;
        }

        public async Task<BaseResponse> Update(UpdateUserRequest updateUser)
        {
            var response = new BaseResponse();
            try
            {
                response = await _userDAL.Update(updateUser);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 0;
            }
            return response;
        }

        #region Helpers
        private async Task<string> GetSha256(string str)
        {
            byte[] hash;
            byte[] inputHash = Encoding.UTF8.GetBytes(str);

            using (MemoryStream ms = new MemoryStream(inputHash))
                hash = await MD5.Create().ComputeHashAsync(ms);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString());
            }

            return sb.ToString();
        }
        #endregion
    }
}
