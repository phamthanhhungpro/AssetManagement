using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Common.Specifications;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace AssetManagement.Application.Services
{
    public class UserServiceAsync : IUserServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IUserRepositoriesAsync _userRepositoriesAsync;
        private readonly IValidator<AddUserRequestDto> _validator;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserServiceAsync(IUserRepositoriesAsync userRepositoriesAsync,
            IMapper mapper,
            IValidator<AddUserRequestDto> validator)
        {
            _mapper = mapper;
            _userRepositoriesAsync = userRepositoriesAsync;
            _validator = validator;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<Response<UserDto>> AddUserAsync(AddUserRequestDto request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<UserDto> { Succeeded = false, Errors = errors };
            }

            try
            {
                var userDomain = _mapper.Map<User>(request);

                userDomain.Username = await _userRepositoriesAsync.GenerateUsername(userDomain.FirstName, userDomain.LastName);
                userDomain.PasswordHash = _passwordHasher.HashPassword(userDomain, _userRepositoriesAsync.GeneratePassword(userDomain.Username, userDomain.DateOfBirth));
                userDomain.IsDeleted = false;
                userDomain.CreatedOn = DateTime.Now;

                var user = await _userRepositoriesAsync.AddAsync(userDomain);

                var userDto = _mapper.Map<UserDto>(user);

                return new Response<UserDto>();
            }
            catch (Exception ex)
            {
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<UserResponseDto>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _userRepositoriesAsync.GetByIdAsync(userId);
                if (user == null)
                {
                    return new Response<UserResponseDto>("User not found");
                }

                var userDto = _mapper.Map<UserResponseDto>(user);
                return new Response<UserResponseDto> { Data = userDto, Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response<UserResponseDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<List<UserResponseDto>>> GetAllUsersAsync(string? search, string? orderBy, bool isDescending, int skip, int take, EnumLocation? adminLocation)
        {
            try
            {
                var users = await _userRepositoriesAsync.ListAsync(CreateSpecification(search, orderBy, isDescending, skip, take, adminLocation));
                var userDtos = _mapper.Map<List<UserResponseDto>>(users);

                return new Response<List<UserResponseDto>> { Data = userDtos, Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response<List<UserResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        private ISpecification<User> CreateSpecification(string? search, string? orderBy, bool isDescending, int skip, int take, EnumLocation? adminLocation)
        {
            return new DynamicSpecification<User>(
                criteria: u => (string.IsNullOrEmpty(search) || u.Username.Contains(search) || u.StaffCode.Contains(search)) && u.Location == adminLocation,
                orderBy: !string.IsNullOrEmpty(orderBy) && !isDescending ? GetOrderByExpression(orderBy) : null,
                orderByDescending: !string.IsNullOrEmpty(orderBy) && isDescending ? GetOrderByExpression(orderBy) : null,
                skip: skip,
                take: take
            );
        }

        private Expression<Func<User, object>> GetOrderByExpression(string orderBy)
        {
            return orderBy.ToLower() switch
            {
                "firstname" => u => u.FirstName,
                "lastname" => u => u.LastName,
                "username" => u => u.Username,
                "dateofbirth" => u => u.DateOfBirth,
                "joineddate" => u => u.JoinedDate,
                "gender" => u => u.Gender,

                _ => u => u.FirstName,
            }; ;
        }

        public async Task<Response<UserDto>> UpdateUserAsync(UpdateUserRequestDto request)
        {
            try
            {
                // Fetch the existing user
                var existingUser = await _userRepositoriesAsync.GetByIdAsync(request.UserId);
                if (existingUser == null)
                {
                    return new Response<UserDto>("User not found");
                }

                // Update the user fields
                existingUser.DateOfBirth = request.DateOfBirth;
                existingUser.Gender = request.Gender;
                existingUser.JoinedDate = request.JoinedDate;

                // Update the user's roles
                var existingRoles = existingUser.UserRoles.ToList();
                var newRoles = request.RoleIds.Except(existingRoles.Select(ur => ur.RoleId)).ToList();
                var removedRoles = existingRoles.Where(ur => !request.RoleIds.Contains(ur.RoleId)).ToList();

                foreach (var role in removedRoles)
                {
                    existingUser.UserRoles.Remove(role);
                    await _userRepositoriesAsync.RemoveUserRolesAsync(role);
                }

                foreach (var roleId in newRoles)
                {
                    var userRole = new UserRoles { UserId = existingUser.Id, RoleId = roleId };
                    existingUser.UserRoles.Add(userRole);
                    await _userRepositoriesAsync.AddUserRolesAysnc(userRole);
                }

                // Save the updated user
                await _userRepositoriesAsync.UpdateAsync(existingUser);

                // Map the updated user to a UserDto and return a response
                var updatedUserDto = _mapper.Map<UserDto>(existingUser);
                return new Response<UserDto> { Data = updatedUserDto, Succeeded = true };
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                //_logger.LogError(ex, "An error occurred while updating the user.");
                return new Response<UserDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        //    public async Task<Response<UserDto>> UpdateUserAsync(UpdateUserRequestDto request)
        //    {
        //        try
        //        {
        //            // Fetch the existing user
        //            var existingUser = await _userRepositoriesAsync.GetByIdAsync(request.UserId);
        //            if (existingUser == null)
        //            {
        //                return new Response<UserDto> { Succeeded = false, Errors = { "User not found" } };
        //            }

        //            // Update the user fields
        //            existingUser.DateOfBirth = request.DateOfBirth;
        //            existingUser.Gender = request.Gender;
        //            existingUser.JoinedDate = request.JoinedDate;

        //            // Update the user's roles
        //            var existingRoles = existingUser.UserRoles.ToList();
        //            var newRoles = request.RoleIds.Except(existingRoles.Select(ur => ur.RoleId)).ToList();
        //            var removedRoles = existingRoles.Where(ur => !request.RoleIds.Contains(ur.RoleId)).ToList();

        //            foreach (var role in removedRoles)
        //            {
        //                existingUser.UserRoles.Remove(role);
        //                await _userRepositoriesAsync.RemoveUserRolesAsync(role);
        //            }

        //            foreach (var roleId in newRoles)
        //            {
        //                if (!existingRoles.Any(ur => ur.RoleId == roleId))
        //                {
        //                    var userRole = new UserRoles { UserId = existingUser.Id, RoleId = roleId };
        //                    existingUser.UserRoles.Add(userRole);
        //                    await _userRepositoriesAsync.AddUserRolesAysnc(userRole);
        //                }
        //            }

        //            // Save the updated user
        //            await _userRepositoriesAsync.UpdateAsync(existingUser);

        //            // Map the updated user to a UserDto and return a response
        //            var updatedUserDto = _mapper.Map<UserDto>(existingUser);
        //            return new Response<UserDto> { Data = updatedUserDto, Succeeded = true };
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception (optional)
        //            //_logger.LogError(ex, "An error occurred while updating the user.");
        //            return new Response<UserDto> ( ex.Message.ToString() ) ;
        //        }
        //    //}






        //}
    }

    public class DynamicSpecification<T> : ISpecification<T>
    {
        public DynamicSpecification(
            Expression<Func<T, bool>> criteria = null,
            Expression<Func<T, object>> orderBy = null,
            Expression<Func<T, object>> orderByDescending = null,
            int skip = 0,
            int take = 0)
        {
            Criteria = criteria;
            OrderBy = orderBy;
            OrderByDescending = orderByDescending;
            Skip = skip;
            Take = take;
            IsPagingEnabled = take > 0;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; }
        public Expression<Func<T, object>> OrderByDescending { get; }
        public Expression<Func<T, object>> GroupBy { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagingEnabled { get; }
    }
}




