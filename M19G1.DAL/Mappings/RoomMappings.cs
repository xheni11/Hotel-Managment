using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mappings
{
    public static class RoomMappings
    {
        public static RoomModel MapRoomToRoomModel(Room room,RoomCategoryModel CatModel)
        {
            var roomModel = new RoomModel
            {
                Id = room.Id,
                Active = room.Enabled,
                Occupied = room.Occupied,
                GuestsNr = room.NrOfGuests,
                Price = room.Price,
                RoomName = room.Name,
                CategoryId = room.CategoryId,
                RoomCategory = CatModel,
                RoomDescription = room.Description
            };
            roomModel.BookingRooms = room.BookingRooms.Select(b => BookingMappings.MapBookingRoomToBookingRoomModel(b, null, null)).ToList();
            roomModel.RoomFacilities = room.RoomFacilities.Select(rf => FacilityMappings.
            MapRoomFacilityToRFModel(rf, roomModel, FacilityMappings.MapFacilityToFacilityModel(rf.Facility))).ToList();
            return roomModel;
        }

        public static RoomCategoryModel MapRoomCategoryToRCModel(RoomCategory RoomCat)
        {
            var Cat = new RoomCategoryModel
            {
                Id = RoomCat.Id,
                CatName = RoomCat.Name,
                Description = RoomCat.Description
               
            };
            Cat.Rooms = RoomCat.Rooms.Select(r => MapRoomToRoomModel(r, Cat)).ToList();
            return Cat;
        }
    }
}
