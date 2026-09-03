using June2026.Database.AppDbContextModels;
using June2026.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace June2026.Domain.Features.User;

public class UserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserListResponseModel> GetUsersAsync(UserListRequestModel requestModel)
    {
        try
        {
            var lst = await _db.TblUsers
                .OrderByDescending(x => x.UserId)
                .ToListAsync();

            return new UserListResponseModel
            {
                IsSuccess = true,
                Message = "Users fetched successfully.",

                Users = lst.Select(x => new UserModel
                {
                    UserId = x.UserId,
                    Username = x.Username
                }).ToList(),

            };
        }
        catch (Exception ex)
        {
            return new UserListResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString(),
            };
        }
    }

    public async Task<UserEditResponseModel> GetUserAsync(UserEditRequestModel requestModel)
    {
        try
        {
            var item = await _db.TblUsers
                .FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);
            if (item is null)
            {
                return new UserEditResponseModel
                {
                    IsSuccess = false,
                    Message = "User does not exist."
                };
            }

            return new UserEditResponseModel
            {
                IsSuccess = true,
                Message = "User fetched successfully.",
                UserId = item.UserId,
                Username = item.Username
            };
        }
        catch (Exception ex)
        {
            return new UserEditResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public async Task<UserCreateResponseModel> CreateUserAsync(UserCreateRequestModel requestModel)
    {
        try
        {
            TblUser user = new TblUser
            {
                Username = requestModel.Username,
                Password = requestModel.Password
            };
            await _db.TblUsers.AddAsync(user);
            int result = await _db.SaveChangesAsync();

            UserCreateResponseModel model = new UserCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful." : "Saving Failed.",
                UserId = user.UserId
            };

            return model;
        }
        catch (Exception ex)
        {
            return new UserCreateResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }
    public async Task<UserPatchResponseModel> PatchUserAsync(UserPatchRequestModel requestModel)
    {
        try
        {
            var item = await _db.TblUsers.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);
            if (item is null)
            {
                return new UserPatchResponseModel
                {
                    Message = "User does not exist."
                };
            }

            if (!string.IsNullOrEmpty(requestModel.Username))
            {
                item.Username = requestModel.Username;
            }

            if (!string.IsNullOrEmpty(requestModel.Password))
            {
                item.Password = requestModel.Password;
            }

            int result = await _db.SaveChangesAsync();

            UserPatchResponseModel model = new UserPatchResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful." : "Updating Failed."
            };

            return model;
        }
        catch (Exception ex)
        {
            return new UserPatchResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public async Task<UserDeleteResponseModel> DeleteUserAsync(UserDeleteRequestModel requestModel)
    {
        try
        {
            var item = await _db.TblUsers.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);
            if (item is null)
            {
                return new UserDeleteResponseModel
                {
                    Message = "User does not exist."
                };
            }

            _db.Remove(item);
            int result = await _db.SaveChangesAsync();

            UserDeleteResponseModel model = new UserDeleteResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful." : "Deleting Failed."
            };

            return model;
        }
        catch (Exception ex)
        {
            return new UserDeleteResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }
}
