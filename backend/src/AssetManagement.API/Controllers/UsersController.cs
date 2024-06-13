using AssetManagement.API.CustomActionFilters;
using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceAsync _userService;
        private readonly IValidator<AddUserRequestDto> _validator;

        public UsersController(IUserServiceAsync userService, IValidator<AddUserRequestDto> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        [HttpPost]
        [Route("users")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto request)
        {
            var response = await _userService.AddUserAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? search, [FromQuery] string? orderBy, [FromQuery] bool isDescending = false, [FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] EnumLocation? adminLocation = EnumLocation.HaNoi)
        {
            var response = await _userService.GetAllUsersAsync(search, orderBy, isDescending, skip, take, adminLocation);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var response = await _userService.GetUserByIdAsync(userId);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new Response<UserDto> { Succeeded = false, Errors = { "Invalid request data" } });
            }

            var result = await _userService.UpdateUserAsync(request);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}