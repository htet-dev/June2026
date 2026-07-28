using June2026.Database.AppDbContextModels;
using June2026.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace June2026.WebApi.Controllers
{
    // api/user
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UserController()
        {
            _db = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var lst = _db.TblUsers.ToList();      //This line shoule be inside try catch as it might throw an error.  // most likely Status Code 500
            return Ok(lst);    // Ok() is HTTP Status Code

            //return StatusCode(500, lst); //  we can add Status Code together with the result we want to return to the frontend.
        }

        // api/user/edit/1
        // api/user/1
        [HttpGet("Edit/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == id);
            if(item is null)
            {
                return NotFound("User does not exist.");
            }
            return Ok(item);    // Ok() is HTTP Status Code
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequestModel requestModel)
        {
            TblUser user = new TblUser
            {
                Username = requestModel.Username,
                Password = requestModel.Password
            };
            _db.TblUsers.Add(user);
            int result = _db.SaveChanges();

            UserCreateResponseModel model = new UserCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful." : "Saving Failed.",
                UserId = user.UserId
            };

            return Ok(model);    // Ok() is HTTP Status Code
        }



        /**
         * UpsertUser: Update the record if it already exists; otherwise Insert (create) a new one.          
         * */

        [HttpPut("{id}")]
        public IActionResult UpsertUser(int id, UserUpsertRequestModel requestModel)
        {
            try
            {
                var item = _db.TblUsers.FirstOrDefault(x => x.UserId == id);

                bool isNewUser = item is null;

                if (isNewUser)
                {
                    item = new TblUser
                    {
                        Username = requestModel.Username,
                        Password = requestModel.Password
                    };

                    _db.TblUsers.Add(item);
                }
                else
                {
                    if (item.Username == requestModel.Username &&
                        item.Password == requestModel.Password)
                    {
                        return Ok(new UserUpsertResponseModel
                        {
                            IsSuccess = true,
                            Message = "There is no update required.",
                            UserId = item.UserId
                        });
                    }

                    item.Username = requestModel.Username;
                    item.Password = requestModel.Password;                    
                }

                _db.SaveChanges();

                return Ok(new UserUpsertResponseModel
                {
                    IsSuccess = true,
                    Message = isNewUser ? "Upsert Created User." : "Upsert Updated User.",
                    UserId = item.UserId
                });
            }
            catch(Exception)
            {
                return StatusCode(500, new UserUpsertResponseModel
                {
                    Message = "Upsert Failed."
                });
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, UserPatchRequestModel requestModel)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == id);
            if(item is null)
            {
                return NotFound(new UserPatchResponseModel
                {
                    Message = "User does not exist."
                });                
            }

            if (!string.IsNullOrEmpty(requestModel.Username))
            {
                item.Username = requestModel.Username;
            }

            if (!string.IsNullOrEmpty(requestModel.Password))
            {
                item.Password = requestModel.Password;
            }

            int result = _db.SaveChanges();

            UserPatchResponseModel model = new UserPatchResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful." : "Updating Failed."                
            };


            return Ok(model);    // Ok() is HTTP Status Code
        }

        [HttpDelete("{UserId}")]
        public IActionResult DeleteUser([FromRoute] UserDeleteRequestModel requestModel)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == requestModel.UserId);
            if (item is null)
            {
                return NotFound(new UserDeleteResponseModel
                {
                    Message = "User does not exist."
                });
            }

            _db.Remove(item);
            int result = _db.SaveChanges();

            UserDeleteResponseModel model = new UserDeleteResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful." : "Deleting Failed."
            };

            return Ok("Delete User");    // Ok() is HTTP Status Code
        }
    }
}
