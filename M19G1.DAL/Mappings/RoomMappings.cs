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
        public static RoomModel MapRoomToRoomModel(Room room)
        {
            return new RoomModel
            {
                Id = room.Id,
                Active = room.Enabled,
                Occupied = room.Occupied,
                GuestsNr = room.NrOfGuests,
                Price = room.Price,
                RoomName = room.Name,
                CategoryId = room.CategoryId,
                RoomCategory = MapRoomCategoryToRCModel(room.Category),
                RoomDescription = room.Description,
                BookingRooms = room.BookingRooms.Select(b => BookingMappings.MapBookingRoomToBookingRoomModel(b)).ToList(),
                RoomFacilities = room.RoomFacilities.Select(rf => FacilityMappings.MapRoomFacilityToRFModel(rf)).ToList()
            };
        }
        public static RoomCategoryModel MapRoomCategoryToRCModel(RoomCategory RoomCat)
        {
            return new RoomCategoryModel
            {
                Id = RoomCat.Id,
                CatName = RoomCat.Name,
                Description = RoomCat.Description
                //Shkakton stack overflow sepse krijohet cikel cat-rooms-cat-..
                //Rooms = RoomCat.Rooms.Select(r => MapRoomToRoomModel(r)).ToList()
            };
        }
    }
}
