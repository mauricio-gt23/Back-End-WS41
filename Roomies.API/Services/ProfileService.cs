﻿using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Repositories;
using Roomies.API.Domain.Repositories;
using Roomies.API.Domain.Services;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IProfileRepository profileRepository, IUnitOfWork unitOfWork, IPlanRepository planRepository = null)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _planRepository = planRepository;
        }

        public async Task<ProfileResponse> DeleteAsync(int id)
        {
            var existingUser = await _profileRepository.FindById(id);

            if (existingUser == null)
                return new ProfileResponse("Usuario inexistente");

            try
            {
                _profileRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new ProfileResponse($"Un error ocurrió al eliminar el usuario: {ex.Message}");
            }
        }

        public async Task<ProfileResponse> GetByIdAsync(int id)
        {
            var existingUser = await _profileRepository.FindById(id);

            if (existingUser == null)
                return new ProfileResponse("Usuario inexistente");

            return new ProfileResponse(existingUser);
        }

        public async Task<IEnumerable<Profile>> ListAsync()
        {
            return await _profileRepository.ListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByPlanIdAsync(int planId)
        {
            return await _profileRepository.ListByPlanId(planId);
        }

        public async Task<ProfileResponse> SaveAsync(Profile user,int planId)
        {

            var existingPlan = await _planRepository.FindById(planId);

            ////////////////////////

            if (existingPlan == null)
                return new ProfileResponse("Plan inexistente");

            try
            {
                user.PlanId = planId;
                user.Plan = existingPlan;
                await _profileRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(user);
            }
            catch (Exception ex)
            {
                return new ProfileResponse($"Un error ocurrió al guardar el usuario: {ex.Message}");
            }
        }

        public async Task<ProfileResponse> UpdateAsync(int id, Profile user)
        {
            var existingUser = await _profileRepository.FindById(id);

            if (existingUser == null)
                return new ProfileResponse("Usuario inexistente");

            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Province = user.Province;
            existingUser.District = user.District;
            existingUser.Address = user.Address;
            existingUser.Birthday = user.Birthday;
            existingUser.CellPhone = user.CellPhone;
            existingUser.Department = user.Department;
            existingUser.IdCard = user.IdCard;
            existingUser.Description = user.Description;

            try
            {
                _profileRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new ProfileResponse($"Un error ocurrió al actualizar el usuario: {ex.Message}");
            }
        }
    }
}
