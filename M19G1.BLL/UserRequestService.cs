﻿using M19G1.DAL;
using M19G1.DAL.Mapping;
using M19G1.DAL.Mapping.User;
using M19G1.DAL.Repository;
using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.BLL
{
    public class UserRequestService:IUserRequestService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        private readonly UserRequestRepository _usersRequestRepository;


        public UserRequestService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
            _usersRequestRepository = _internalUnitOfWork.UserRequestRepository;
            
        }
        public List<UserRequestModel> GetAllRequests()
        {
            return UserRequestModelMapping.ToModel(_usersRequestRepository.GetAll());
        }
        public List<UserRequestModel> GetUsersOrderBy(string sortField, string search)
        {
            return UserRequestModelMapping.ToModel(_usersRequestRepository.OrderBy(_usersRequestRepository.GetAll(), sortField, search));
        }
        public void DeleteRequest(int id)
        {
            _usersRequestRepository.Delete(id);

        }
        public UserRequestModel GetRequestById(int id)
        {
            return UserRequestModelMapping.ToModel(_usersRequestRepository.GetByID(id));
        }
    }
}