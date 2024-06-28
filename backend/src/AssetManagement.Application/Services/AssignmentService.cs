﻿using AssetManagement.Application.Common;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.Assignments.Reques;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.Assignments.Requests;
using AssetManagement.Application.Models.DTOs.Assignments.Response;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Application.Services
{
    public class AssignmentServiceAsync : IAssignmentServicesAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepositoriesAsync _assignmentRepositoriesAsync;
        private readonly IUriService _uriService;
        private readonly IValidator<AddAssignmentRequestDto> _addAssignmentValidator;
        private readonly IAssignmentRepositoriesAsync _assignmentRepository;
        private readonly IUserRepositoriesAsync _userRepository;
        private readonly IAssetRepositoriesAsync _assetRepository;

        public AssignmentServiceAsync(IAssignmentRepositoriesAsync assignmentRepositoriesAsync,
             IMapper mapper,
             IValidator<AddAssignmentRequestDto> addAssignmentValidator,
             IAssetRepositoriesAsync assetRepository,
             IUserRepositoriesAsync userRepository,
             IUriService uriService
            )
        {
            _mapper = mapper;
            _assignmentRepositoriesAsync = assignmentRepositoriesAsync;
            _uriService = uriService;
            _assignmentRepository = assignmentRepositoriesAsync;
            _addAssignmentValidator = addAssignmentValidator;
            _assetRepository = assetRepository;
            _userRepository = userRepository;
            _uriService = uriService;
        }

        public async Task<Response<AssignmentDto>> AddAssignmentAsync(AddAssignmentRequestDto request)
        {
            //validate data
            var validationResult = await _addAssignmentValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Errors = errors
                };
            }

            //check null
            var existingAsset = await _assetRepository.GetByIdAsync(request.AssetId);
            if (existingAsset == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Asset not found." };
            }

            var existingAssignedIdBy = await _userRepository.GetByIdAsync(request.AssignedIdBy);
            if (existingAssignedIdBy == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "User assigned by not found." };
            }
            if (existingAssignedIdBy.JoinedDate > request.AssignedDate)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Assigned Date must be greater than Joined Date." };
            }

            try
            {
                var newAssignment = _mapper.Map<Assignment>(request);
                newAssignment.CreatedOn = DateTime.Now;
                newAssignment.CreatedBy = request.AssignedIdBy.ToString();
                newAssignment.Note = request.Note.Trim();
                var asignment = await _assignmentRepository.AddAsync(newAssignment);
                existingAsset.State = AssetStateType.Assigned;
                await _assetRepository.UpdateAsync(existingAsset);
                var assetDto = _mapper.Map<AssignmentDto>(asignment);

                return new Response<AssignmentDto> { Succeeded = true, Message = " Create Assignment Successfully!" };
            }
            catch (Exception ex)
            {
                return new Response<AssignmentDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public Task<Response<AssignmentDto>> DeleteAssignmentAsync(Guid assignmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<AssignmentDto>> EditAssignmentAsync(EditAssignmentRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<AssignmentResponseDto>>> GetAllAssignmentsAsync(PaginationFilter paginationFilter, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate, EnumLocation location, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (paginationFilter == null)
                {
                    paginationFilter = new PaginationFilter();
                }

                var filterAsset = await _assignmentRepositoriesAsync.FilterAssignmentAsync(location, search, assignmentState, assignedDate);

                var totalRecords = filterAsset.Count();

                var specAssignment = AssignmentSpecificationHelper.AssignmentSpecificationWithAsset(paginationFilter, orderBy, isDescending);

                var assignments = await SpecificationEvaluator<Assignment>.GetQuery(filterAsset, specAssignment).ToListAsync();

                var responseAssignmentDtos = _mapper.Map<List<AssignmentResponseDto>>(assignments);

                var pagedResponse = PaginationHelper.CreatePagedReponse(responseAssignmentDtos, paginationFilter, totalRecords, _uriService, route);

                return pagedResponse;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<AssignmentResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<AssignmentResponseDto>> GetAssignmentByIdAsync(Guid assignmentId)
        {
            var assignment = await _assignmentRepositoriesAsync.GetAssignemntByIdAsync(assignmentId);
            if (assignment == null)
            {
                return new Response<AssignmentResponseDto> { Succeeded = false, Message = "Assignment not found" };
            }
            var assignmentDto = _mapper.Map<AssignmentResponseDto>(assignment);
            return new Response<AssignmentResponseDto> { Succeeded = true, Data = assignmentDto };
        }

        public async Task<PagedResponse<List<AssignmentResponseDto>>> GetAssignmentsOfUser(PaginationFilter paginationFilter, Guid userId, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (paginationFilter == null)
                {
                    paginationFilter = new PaginationFilter();
                }

                var filterAsset = await _assignmentRepositoriesAsync.FilterAssignmentOfUserAsync(userId, search, assignmentState, assignedDate);

                var totalRecords = filterAsset.Count();

                var specAssignment = AssignmentSpecificationHelper.AssignmentSpecificationWithAsset(paginationFilter, orderBy, isDescending);

                var assignments = await SpecificationEvaluator<Assignment>.GetQuery(filterAsset, specAssignment).ToListAsync();

                var responseAssignmentDtos = _mapper.Map<List<AssignmentResponseDto>>(assignments);

                var pagedResponse = PaginationHelper.CreatePagedReponse(responseAssignmentDtos, paginationFilter, totalRecords, _uriService, route);

                return pagedResponse;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<AssignmentResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<AssignmentDto>> ChangeAssignmentStateAsync(ChangeStateAssignmentDto request)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(request.AssignmentId);

            if (assignment == null)
            {
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Message = "Assignment not found."
                };
            }

            if (assignment.State == EnumAssignmentState.Accepted || assignment.State == EnumAssignmentState.Declined)
            {
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Message = "Assignment state cannot be changed."
                };
            }

            assignment.State = request.NewState;

            if (request.NewState == EnumAssignmentState.Accepted)
            {
                var assetResponse = await _assetRepository.GetByIdAsync(assignment.AssetId);

                if (assetResponse == null)
                {
                    return new Response<AssignmentDto>
                    {
                        Succeeded = false,
                        Message = "Asset not found."
                    };
                }

                assetResponse.State = AssetStateType.Available;

                try
                {
                    await _assetRepository.UpdateAsync(assetResponse);
                }
                catch (Exception ex)
                {
                    return new Response<AssignmentDto>
                    {
                        Succeeded = false,
                        Message = "An error occurred while updating the asset state.",
                        Errors = new List<string> { ex.Message }
                    };
                }
            }

            try
            {
                await _assignmentRepository.UpdateAsync(assignment);
                var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
                return new Response<AssignmentDto>
                {
                    Succeeded = true,
                    Message = "Assignment state changed successfully.",
                    Data = assignmentDto
                };
            }
            catch (Exception ex)
            {
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Message = "An error occurred while changing the assignment state.",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

    }
}