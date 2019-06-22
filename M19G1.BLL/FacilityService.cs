using M19G1.DAL;
using M19G1.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M19G1.Models;
using M19G1.DAL.Mappings;
using M19G1.DAL.Entities;

namespace M19G1.BLL
{
    public class FacilityService : IFacilityService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        
        public FacilityService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }

        public bool AddExtraFacility(ExtraFacilityModel model)
        {
           BookingRoomModel bookingRoom = _internalUnitOfWork.BookingRoomRepository.Get(br => br.BookingId == model.BookingRoom.BookingId
                                           && br.RoomId == model.BookingRoom.RoomId).
                                           Select(b => BookingMappings.MapBookingRoomToBookingRoomModel(b,null,null)).First();
            model.BookingRoomId = bookingRoom.Id;
            ExtraFacilityModel extraFacility = bookingRoom.ExtraFacilities.Where(ef => ef.FacilityId == model.FacilityId)?.FirstOrDefault();
            if (extraFacility != null)
            {
                //booking room e ka ate facility, do shtojme quantity
                int ExtraFacilityId = extraFacility.Id;
                ExtraFacility ef = _internalUnitOfWork.ExtraFacilityRepository.GetByID(ExtraFacilityId);
                ef.Quantity += model.EFQuantity;
                _internalUnitOfWork.ExtraFacilityRepository.Update(ef);
                _internalUnitOfWork.Save();
                return true;
            }
            else
            {
                //nqs eshte available facility, e shtojme
                if(_internalUnitOfWork.FacilityRepository.GetByID(model.FacilityId).Available)
                {
                    ExtraFacility EFacilityToAdd = FacilityMappings.MapExtraFacilityModelToExtraFacility(model, null, null);
                    _internalUnitOfWork.ExtraFacilityRepository.Insert(EFacilityToAdd);
                    _internalUnitOfWork.Save();
                    return true;
                }
                return false;

            }
        }

        public List<FacilityModel> GetFacilites()
        {
            return _internalUnitOfWork.FacilityRepository.Get()
                .Select(m => FacilityMappings.MapFacilityToFacilityModel(m)).ToList();
        }
    }
}
