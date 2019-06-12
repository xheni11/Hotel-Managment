using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mappings
{
    public static class FacilityMappings
    {
        public static FacilityModel MapFacilityToFacilityModel(Facility facility)
        {
            var facModel = new FacilityModel
            {
                FacilityId = facility.Id,
                Available = facility.Available,
                Description = facility.Description,
                FacilityName = facility.Name,
            };
            facModel.RoomFacilties = facility.RoomFacilities.Select(rf => MapRoomFacilityToRFModel(rf, null, facModel)).ToList();
            facModel.ExtraFacilites = facility.ExtraFacilities.Select(ef => MapExtraFacilityToEFModel(ef, null, facModel)).ToList();
            return facModel;     
        }
        public static RoomFacilityModel MapRoomFacilityToRFModel(RoomFacility roomFacility,RoomModel Rmodel, FacilityModel Fmodel)
        {
            return new RoomFacilityModel
            {
                Id = roomFacility.Id,
                FQuantity = roomFacility.Quantity,
                FacilityId = roomFacility.FacilityId,
                Facility = Fmodel,
                RoomId = roomFacility.RoomId,
                Room = Rmodel
            };

        }
        public static ExtraFacilityModel MapExtraFacilityToEFModel(ExtraFacility extraFacility,BookingRoomModel BRmodel,FacilityModel Fmodel )
        {
            return new ExtraFacilityModel
            {
                Id = extraFacility.Id,
                Price = extraFacility.Price,
                EFQuantity = extraFacility.Quantity,
                FacilityId = extraFacility.FacilityId,
                Facility = Fmodel,
                BookingRoomId = extraFacility.BookingRoomId,
                BookingRoom = BRmodel
                
            };

        }
    }
}
