using M19G1.DAL;
using M19G1.DAL.Entities;
using M19G1.IBLL;
using System;

namespace M19G1.BLL
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _internalUnitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }

        public void CreateDummyUser()
        {
            AspNetUser dummy = new AspNetUser
            {
                FirstName = "d",
                LastName = "d",
                Gender = "M",
                DateCreated = DateTime.Now,
                PasswordHash = "ddd",
                Birthday = DateTime.Now,
                Email = "ddd",
                EmailConfirmed = false,
                PhoneNumber = "x",
                UserName = "xx"
            };
            _internalUnitOfWork.AspNetUsersRepository.Insert(dummy);
            _internalUnitOfWork.Save();
        }

        public void CreateDummyUserWithError()
        {
            AspNetUser dummy = new AspNetUser
            {
                FirstName = "ds",
                LastName = "ds",
                Gender = "Ms",
                DateCreated = DateTime.Now,
                PasswordHash = "ddds",
                Birthday = DateTime.Now,
                Email = "ddds",
                EmailConfirmed = false,
                PhoneNumber = "xss",
                UserName = "xsssx"
            };
            _internalUnitOfWork.AspNetUsersRepository.Insert(dummy);
            _internalUnitOfWork.Save();
        }
    }
}
