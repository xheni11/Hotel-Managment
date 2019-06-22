using M19G1.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M19G1.Models;
using M19G1.DAL;
using M19G1.DAL.Mappings;
using M19G1.DAL.Entities;

namespace M19G1.BLL
{
    public class DriverService : IDriverService
    {
        private readonly UnitOfWork _internalUnitOfWork;

        public DriverService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }
        public bool AddDriverService(DriverServiceModel model)
        {
            if(model.StartTime >= DateTime.Now.AddMinutes(10) && model.StartTime <= DateTime.Now.AddDays(10))
            {
                PersonalDriverService driverService = DriverServiceMappings.MapDriverServiceModelToPersonalDriverService(model);
                _internalUnitOfWork.DriverServiceRepository.Insert(driverService);
                return true;

            }
            return false;
            
        }
    }
}
