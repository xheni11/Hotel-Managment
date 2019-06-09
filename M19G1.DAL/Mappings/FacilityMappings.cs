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
            return new FacilityModel
            {
                FacilityId = facility.Id,
                Available = facility.Available,
                Description = facility.Description,
                FacilityName = facility.Name,
                ExtraFacilites = facility.ExtraFacilities.Select(ef => MapExtraFacilityToEFModel(ef)).ToList(),
                RoomFacilties = facility.RoomFacilities.Select(rf => MapRoomFacilityToRFModel(rf)).ToList()
            };            
        }
        public static RoomFacilityModel MapRoomFacilityToRFModel(RoomFacility roomFacility)
        {
            return new RoomFacilityModel
            {
                Id = roomFacility.Id,
                FQuantity = roomFacility.Quantity,
                FacilityId = roomFacility.FacilityId,
                Facility = MapFacilityToFacilityModel(roomFacility.Facility),
                RoomId = roomFacility.RoomId,
                Room = RoomMappings.MapRoomToRoomModel(roomFacility.Room)
            };

        }
        public static ExtraFacilityModel MapExtraFacilityToEFModel(ExtraFacility extraFacility)
        {
            return new ExtraFacilityModel
            {
                Id = extraFacility.Id,
                Price = extraFacility.Price,
                EFQuantity = extraFacility.Quantity,
                FacilityId = extraFacility.FacilityId,
                Facility = MapFacilityToFacilityModel(extraFacility.Facility),
                BookingRoomId = extraFacility.BookingRoomId,
                BookingRoom = BookingMappings.MapBookingRoomToBookingRoomModel(extraFacility.BookingRoom)
                
            };

        }
    }
}
