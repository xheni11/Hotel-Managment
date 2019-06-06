using M19G1.DAL;
using M19G1.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M19G1.Models;
using M19G1.DAL.Mappings;

namespace M19G1.BLL
{
    public class FacilityService : IFacilityService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        
        public FacilityService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }

        public List<FacilityModel> GetFacilites()
        {
            return _internalUnitOfWork.FacilityRepository.Get()
                .Select(m => FacilityMappings.MapFacilityToFacilityModel(m)).ToList();
        }
    }
}
