using M19G1.DAL;
using M19G1.DAL.Mapping;
using M19G1.DAL.Mappings;
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
        private readonly RoleRepository _roleRepository;

        public UserRequestService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
            _usersRequestRepository = _internalUnitOfWork.UserRequestRepository;
            _roleRepository = _internalUnitOfWork.AspNetRolesRepository;
        }
        public List<UserRequestModel> GetAllRequests( bool desc, string columnName, string search, int pageNumber, int pageSize)
        {
            return UserRequestModelMapping.ToModel(_usersRequestRepository.GetAll(pageNumber, pageSize, columnName, search, desc));
        }
        public List<UserRequestModel> GetUsersOrderBy(string sortField, string search)
        {
            return UserRequestModelMapping.ToModel(_usersRequestRepository.OrderBy(_usersRequestRepository.GetAll(), sortField, search));
        }
        public void DeleteRequest(int id)
        {
            _usersRequestRepository.Delete(id);
            _internalUnitOfWork.Save();

        }
        public UserRequestModel GetRequestById(int id)
        {
            return UserRequestModelMapping.ToModel(_usersRequestRepository.GetByID(id));
        }

        public void CreateRequest(UserRequestModel userModel)
        {
            var user = UserRequestModelMapping.ToEntityToCreate(userModel);
            var role = _internalUnitOfWork.AspNetRolesRepository.GetByID(userModel.RoleId);
            user.RoleId = userModel.RoleId;
            user.Role = role;
            _internalUnitOfWork.UserRequestRepository.Insert(user);
            _internalUnitOfWork.Save();
        }
        public int CountAllRecords(int currentUser)
        {
           return  _usersRequestRepository.CountAllRecords(currentUser);
        }
    }
}
