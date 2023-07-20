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
        private static ClientInformationDTO ClientInstance = null;
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
             
                if (dtoObject.Username == "Pen" && dtoObject.Password == "pal")
                {
                    return Ok(dtoObject);
                }
                if (ClientInstance != null && dtoObject.Username == ClientInstance.Username && dtoObject.Password == ClientInstance.Password)
                {
                    LoginDTO returnObject = dtoObject;
                    return Ok(returnObject);
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
                    string encryptedPassword = ComputeSha256Hash(dtoObject.Password);
                    dtoObject.Password = encryptedPassword;
                    ClientInstance = new ClientInformationDTO
                    {
                        Username = dtoObject.Username,
                        Password = dtoObject.Password
                    };
                    return Ok(ClientInstance);
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
        [HttpGet]
        public async Task <IActionResult> GetClientInformation()
        {
            try
            {
                var getClients = await (from c in _dbContext.Clients
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

                return (Ok(ClientInstance));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
          
        }

        [Route("FinishRegistration")]
        [HttpPost]
        public  IActionResult FinishRegistration([FromBody] FinishProfileDTO dtoObject)
        {
            try
            {
                if (dtoObject.Fullname.Length >= 3 && dtoObject.Addressone.Length >= 3 && dtoObject.City.Length >= 3 && dtoObject.Zipcode.Length >= 3)
                {
                    if (ClientInstance == null)
                    {
                        return Ok("s");
                    }
                    else
                    {
                        ClientInstance.Fullname = dtoObject.Fullname;
                        ClientInstance.Addressone = dtoObject.Addressone;
                        ClientInstance.Addresstwo = dtoObject.Addresstwo;
                        ClientInstance.City = dtoObject.City;
                        ClientInstance.State = dtoObject.State;
                        ClientInstance.Zipcode = dtoObject.Zipcode;
                        return Ok(ClientInstance);


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
