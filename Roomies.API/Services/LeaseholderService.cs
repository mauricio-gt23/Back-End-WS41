﻿using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Repositories;
using Roomies.API.Domain.Repositories;
using Roomies.API.Domain.Services;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Services
{
    public class LeaseholderService : ILeaseholderService
    {
        private readonly ILeaseholderRepository _leaseholderRepository;

        private readonly IPlanRepository _planRepository;
        private readonly IFavouritePostRepository _favouritePostRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public LeaseholderService(ILeaseholderRepository leaseholderRepository, IFavouritePostRepository favouritePostRepository, IUnitOfWork unitOfWork, IPlanRepository planRepository = null, IUserRepository userRepository = null)
        {
            _leaseholderRepository = leaseholderRepository;
            _favouritePostRepository = favouritePostRepository;
            _unitOfWork = unitOfWork;
            _planRepository = planRepository;
            _userRepository = userRepository;
        }

        public async Task<LeaseholderResponse> DeleteAsync(int id)
        {
            var existingLeaseholder = await _leaseholderRepository.FindById(id);

            if (existingLeaseholder == null)
                return new LeaseholderResponse("Arrendatario inexistente");

            try
            {
                if (existingLeaseholder.FavouritePosts != null)
                {
                    existingLeaseholder.FavouritePosts.ForEach(favouritePost=>
                {
                    _favouritePostRepository.Remove(favouritePost);
                });
                }

                _leaseholderRepository.Remove(existingLeaseholder);
                await _unitOfWork.CompleteAsync();

                return new LeaseholderResponse(existingLeaseholder);
            }
            catch (Exception ex)
            {
                return new LeaseholderResponse($"Un error ocurrió al eliminar el Arrendatario: {ex.Message}");
            }
        }

        public async Task<LeaseholderResponse> GetByIdAsync(int id)
        {
            var existingLeaseholder = await _leaseholderRepository.FindById(id);

            if (existingLeaseholder == null)
                return new LeaseholderResponse("Arrendatario inexistente");

            return new LeaseholderResponse(existingLeaseholder);
        }

        public async Task<IEnumerable<Leaseholder>> ListAsync()
        {
            return await _leaseholderRepository.ListAsync();
        }

        public async Task<IEnumerable<Leaseholder>> ListByPostIdAsync(int postId)
        {
            var favouritePost = await _favouritePostRepository.ListByPostIdAsync(postId);
            var leaseholders= favouritePost.Select(pt => pt.Leaseholder).ToList();
            return leaseholders;
        }

        public async Task<LeaseholderResponse> SaveAsync(Leaseholder leaseholder,int planId, string username)
        {
            var existingPlan = await _planRepository.FindById(planId);
            var ExistingUsername = await _userRepository.FindByUsername(username);

            if (existingPlan == null)
                return new LeaseholderResponse("Plan inexistente");
            if (ExistingUsername == null)
                return new LeaseholderResponse("Username no encontrado o invalido");


            DateTime fechaActual = DateTime.Today;
            if (fechaActual.Year - leaseholder.Birthday.Year < 18)
            {
                return new LeaseholderResponse("El Leaseholder debe ser mayor de 18 años");
            }
                

            try
            {

                //IEnumerable<Profile> users = await _userRepository.ListAsync();

                leaseholder.Plan = existingPlan;
                leaseholder.PlanId = planId;

                leaseholder.UserId = ExistingUsername.Id;
                leaseholder.User = ExistingUsername;

                await _leaseholderRepository.AddAsync(leaseholder);
                await _unitOfWork.CompleteAsync();

                return new LeaseholderResponse(leaseholder);
                
            }

            catch (Exception ex)
            {
                return new LeaseholderResponse($"Un error ocurrió al guardar el arrendatario: {ex.Message}");
            }
        }

        public async Task<LeaseholderResponse> UpdateAsync(int id, Leaseholder leaseholder)
        {
            var existingLeaseholder= await _leaseholderRepository.FindById(id);

            if (existingLeaseholder == null)
                return new LeaseholderResponse("Arrendatario inexistente");

            existingLeaseholder.Name = leaseholder.Name;
            existingLeaseholder.Address = leaseholder.Address;
            existingLeaseholder.Birthday = leaseholder.Birthday;
            existingLeaseholder.Department = leaseholder.Department;
            existingLeaseholder.CellPhone = leaseholder.CellPhone;
            existingLeaseholder.District = leaseholder.District;
            existingLeaseholder.LastName = leaseholder.LastName;
            existingLeaseholder.Province = leaseholder.Province;
            existingLeaseholder.IdCard = leaseholder.IdCard;
            existingLeaseholder.Description = leaseholder.Description;

            try
            {
                _leaseholderRepository.Update(existingLeaseholder);
                await _unitOfWork.CompleteAsync();

                return new LeaseholderResponse(existingLeaseholder);
            }
            catch (Exception ex)
            {
                return new LeaseholderResponse($"Un error ocurrió al actualizar el arrendador: {ex.Message}");
            }
        }
    }
}
