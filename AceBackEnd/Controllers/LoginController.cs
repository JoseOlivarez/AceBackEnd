using AceBackEnd.Data_Transfer_Objects;
using AceBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Runtime.Serialization;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public  ClientInformationDTO  ClientInstance = null;
        private readonly AceDbContext _dbContext;
        public LoginController(AceDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [Route("Logins")]
       [HttpPost]
       public IActionResult LoginEndpoint([FromBody] LoginDTO dtoObject)
        {
            try
            {
               
                string encryptedPassword = ComputeSha256Hash(dtoObject.Password);
                dtoObject.Password = encryptedPassword;

                // TODO: Access your database context to check if the user exists.
                    var user = _dbContext.Clients.FirstOrDefault(c => c.Username == dtoObject.Username);

                    if (user != null && user.Password == encryptedPassword)
                    {
                    // Passwords match, return the user or any relevant data.
                    Client clientTemp = new Client();
                    clientTemp.ClientId = user.ClientId;
                    clientTemp.Username = user.Username;
                    clientTemp.Password = user.Password;
                    
                    return Ok(user);
               
                }

                //if (dtoObject.Username.Length > 2 && dtoObject.Password.Length > 2) { 
                //    returnObject = dtoObject;
                //    return await Task.FromResult( Ok(returnObject));
                //}
                return BadRequest(new { error = "Invalid Username or Password" });
            }
            catch (Exception ex)
            {
                return  StatusCode(500, ex);
            }
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterEndpoint([FromBody] RegisterDTO dtoObject)
        {
            try
            {
                if (dtoObject.Username.Length > 2 && dtoObject.Password.Length > 2)
                {
                    // Retrieve the latest ClientId from the Clients table (assumes ClientId is an auto-incrementing primary key)
                    int? latestClientId = _dbContext.Clients.OrderByDescending(c => c.ClientId).Select(c => c.ClientId).FirstOrDefault();

                    // Increment the latestClientId by one to get the new ClientId for the new client
                    int nextClientId = (latestClientId.HasValue ? latestClientId.Value : 0) + 1;

                    string encryptedPassword = ComputeSha256Hash(dtoObject.Password);
                    dtoObject.Password = encryptedPassword;
                    Client ClientInstanceNew = new Client
                    {
                        ClientId = nextClientId,
                        Username = dtoObject.Username,
                        Password = dtoObject.Password
                    };
                    _dbContext.Clients.Add(ClientInstanceNew);
                    _dbContext.SaveChanges();

                    return Ok(ClientInstanceNew);
                }
                else
                {
                    return BadRequest("Not sufficient Username or Password requirements");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("GetClientInformation")]
        [HttpPost]
        public async Task <IActionResult> GetClientInformation([FromBody] ClientDTO objectDTO)
        {
            try
            {
                var getClients = await (
                    from Client c in _dbContext.Clients
                    where c.ClientId == objectDTO.ClientId
                    select new
                                        {
                                            c.ClientId,
                                            c.Fullname,
                                            c.Zipcode,
                                            c.State,
                                            c.City,
                                            c.Addressone,
                                           c.Addresstwo,
                                           c.Username
                                        }).ToListAsync();

                return (Ok(getClients));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
          
        }

        [Route("FinishRegistration")]
        [HttpPost]
        public async Task<IActionResult> FinishRegistration([FromBody] FinishProfileDTO dtoObject)
        {
            try
            {
                if (dtoObject.Fullname.Length >= 3 && dtoObject.Addressone.Length >= 3 && dtoObject.City.Length >= 3 && dtoObject.Zipcode.Length >= 3)
                {
                   
                        var existingClient = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == dtoObject.ClientId);

                        if (existingClient != null)
                        {
                            existingClient.Fullname = dtoObject.Fullname;
                            existingClient.Addressone = dtoObject.Addressone; 
                            existingClient.City = dtoObject.City;
                            existingClient.Zipcode = dtoObject.Zipcode;
                            existingClient.State = dtoObject.State;
                            existingClient.Addresstwo = dtoObject.Addresstwo;
                            existingClient.ClientId = dtoObject.ClientId;
                            _ = _dbContext.SaveChangesAsync();
                        
                            return Ok();
                        }
                        else
                        {
                            return Ok("s");
                        }
                }
                else
                {
                    return BadRequest(new { error = "Failed contact admin and give them an A" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        //// POST api/<LoginController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<LoginController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<LoginController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
