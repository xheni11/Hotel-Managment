using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mappings
{
    public static class FacilityMappings
    {
       public static ExtraFacilityModel MapFacilityViewModelToEFacilityModel(AddFacilityViewModel model)
        {
            return new ExtraFacilityModel
            {
                 BookingRoom = new BookingRoomModel
                 {
                     BookingId = model.Booking.Id,
                     RoomId = model.RoomId
                 },
                 FacilityId = model.FacilityId,
                 EFQuantity = model.FacilityQuantity

            };

        }
    }
}