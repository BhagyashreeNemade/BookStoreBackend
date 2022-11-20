using Common_Layer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Service
{
    public class UserRL : IUserRL
    {
        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        SqlConnection sqlConnection;

        public bool Registration(RegisterModel model)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    
                    SqlCommand command = new SqlCommand("dbo.Add_User", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@FullName", model.FullName);
                    command.Parameters.AddWithValue("@EmailId", model.Email);
                    command.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    command.Parameters.AddWithValue("@Password",model.Password);


                    var result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }
        public string UserLogin(LoginModel loginModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("Login_User", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    command.Parameters.AddWithValue("@Password", loginModel.Password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT ID FROM Users WHERE EmailId = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        var Id = cmd.ExecuteScalar();
                        var token = GenerateSecurityToken(loginModel.EmailId, Id.ToString());
                        return token;

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public string GenerateSecurityToken(string email, string userID)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Configuration[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                       new Claim(ClaimTypes.Email, email),
                        new Claim("ID", userID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public string ForgetPassword(string Email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT EmailId FROM Users WHERE EmailId = '" + Email + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    var email = cmd.ExecuteScalar();
                    string query1 = "SELECT ID FROM Users WHERE EmailId = '" + Email + "'";
                    SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                    var id = sqlCommand.ExecuteScalar();
                    if (email != null)
                    {
                        var token = GenerateSecurityToken(email.ToString(), id.ToString());
                        MSMQ msmqModel = new MSMQ();
                        msmqModel.sendData2Queue(token);
                        return token;
                    }
                    else
                        return null;

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }


    }
}
