using Assessment.DAL.Interface;
using Assessment.DTO.Response;
using Assessment.Helpers.Interface;
using Assessment.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Assessment.DTO.Enums;
using Dapper;
using System.Data;
using Assessment.DTO.DataObjects;
using Assessment.DTO.Request;
using System.Reflection;
using System.Xml.Linq;

namespace Assessment.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly IDapperHelper _dapperHelper;
        private readonly RepositoryFactory _repositoryFactory;
        public UserDAL(IDapperHelper dapperHelper, RepositoryFactory repositoryFactory)
        {
            _dapperHelper = dapperHelper;
            _repositoryFactory = repositoryFactory;
        }

        //The dal is used to build a connection to the db and pass through parameters to return data, a using statement is used
        // so that a connection is not kept open after its use
        public async Task<AuthenticationResponse> Authenticate(string username, string password)
        {
            var response = new AuthenticationResponse();
            try
            {
                using (SqlConnection con = _repositoryFactory.GetMyConnection(Databases.Assessment, ConnectionType.Dapper))
                {
                    response = await _dapperHelper.QuerySingleAsync<AuthenticationResponse>(con, Queries.Authenticate, CommandType.StoredProcedure,
                        new {@Username = username,@Password = password});
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
                using (SqlConnection con = _repositoryFactory.GetMyConnection(Databases.Assessment, ConnectionType.Dapper))
                {
                    response = await _dapperHelper.QuerySingleAsync<BaseResponse>(con, Queries.CreateUser, CommandType.StoredProcedure,
                    new
                        {
                            @Name = createUser.Name,
                            @Surname = createUser.Surname,
                            @Username = createUser.Username,
                            @Email = createUser.Email,
                            @Mobile = createUser.Mobile,
                            @RoleID = createUser.RoleID,
                            @Password = createUser.Password
                        });
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 0;
            }
            return response;
        }

        public async Task<BaseResponse> Delete(int userID)
        {
            var response = new BaseResponse();
            try
            {
                using (SqlConnection con = _repositoryFactory.GetMyConnection(Databases.Assessment, ConnectionType.Dapper))
                {
                    response = await _dapperHelper.QuerySingleAsync<BaseResponse>(con, Queries.DeleteReadOnly, CommandType.StoredProcedure, new
                    {
                        @UserID = userID
                    });
                }
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
                using (SqlConnection con = _repositoryFactory.GetMyConnection(Databases.Assessment, ConnectionType.Dapper))
                {
                    var result = await _dapperHelper.QueryAsync<Users>(con, Queries.GetUsers, CommandType.StoredProcedure);
                    response.Users = result.ToList();
                }
            }
            catch(Exception ex)
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
                using (SqlConnection con = _repositoryFactory.GetMyConnection(Databases.Assessment, ConnectionType.Dapper))
                {
                    response = await _dapperHelper.QuerySingleAsync<BaseResponse>(con, Queries.UpdateUsers, CommandType.StoredProcedure,
                        new
                        {
                            @Name = updateUser.Name,
                            @Surname = updateUser.Surname,
                            @Email = updateUser.Email,
                            @Mobile = updateUser.Mobile,
                            @IsActive = updateUser.IsActive,
                            @UserID = updateUser.UserID
                        });
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = 0;
            }
            return response;
        }
    }
}
