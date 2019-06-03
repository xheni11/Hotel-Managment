using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mappings
{
    public static class BookingMappings
    {
        public static BookingModel MapBookingToBookingModel(Booking booking )
        {
            return new BookingModel
            {
                Id = booking.Id,
                Start = booking.StartDate,
                End = booking.EndDate,
                Valid = booking.Valid,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                Client = UserMappings.MapAspNetUserToUserModel(booking.Client),
                ClientId = booking.UserId,
                BookingRooms = booking.BookingRooms.Select(br => MapBookingRoomToBookingRoomModel(br)).ToList(),
                DriverServices = booking.DriverServices.Select(ds => DriverServiceMappings.MapDriverServiceToDriverServiceModel(ds)).ToList()
            };

        }
        public static BookingRoomModel MapBookingRoomToBookingRoomModel(BookingRoom bookingRoom)
        {
            return new BookingRoomModel
            {
                Id = bookingRoom.Id,
                BookingId = bookingRoom.BookingId,
                BookingModel = MapBookingToBookingModel(bookingRoom.Booking),
                RoomId = bookingRoom.RoomId,
                Room = RoomMappings.MapRoomToRoomModel(bookingRoom.Room),
                ExtraFacilities = bookingRoom.ExtraFacilities.Select(ef => FacilityMappings.MapExtraFacilityToEFModel(ef)).ToList()
            };

        }
        public static RatingModel MapRatingToRatingModel(Rating rating)
        {
            return new RatingModel
            {
                Id = rating.Id,
                BookingId = rating.Booking.Id,
                Booking = MapBookingToBookingModel(rating.Booking),
                DateCreated = rating.DateCreated ?? DateTime.Now,
                RateValue = rating.Rate

            };

        }
    }
}
