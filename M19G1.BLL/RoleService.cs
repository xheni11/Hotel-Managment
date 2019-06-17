using M19G1.DAL;
using M19G1.DAL.Mapping.Role;
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
   public class RoleService:IRoleService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        private readonly RoleRepository _roleRepository;

        public RoleService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
            _roleRepository = _internalUnitOfWork.AspNetRolesRepository;
        }
        public List<RoleModel> GetAllRoles()
        {
            return RoleModelMapping.ToModel(_roleRepository.GetAll());
        }
        public List<RoleModel> GetRoleByName(List<string> roleName)
        {
            return RoleModelMapping.ToModel(_roleRepository.GetRoleByName(roleName));
        }
    }
}
