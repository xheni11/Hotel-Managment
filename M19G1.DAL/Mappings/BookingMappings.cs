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
        public static BookingModel MapBookingToBookingModel(Booking booking,UserModel @user)
        {
            var @return =  new BookingModel
            {
                Id = booking.Id,
                BookTime = booking.BookingTime,
                Start = booking.StartDate,
                End = booking.EndDate,
                Valid = booking.Valid,
                CheckIn = booking.CheckInTime,
                CheckOut = booking.CheckOutTime,
                ClientId = booking.UserId
            };
            @return.Client = @user;
            
            @return.Rating = booking.Rating != null ? MapRatingToRatingModel(booking.Rating, @return) : null;
            @return.BookingRooms = booking.BookingRooms.Select(br => MapBookingRoomToBookingRoomModel(br, @return,
                RoomMappings.MapRoomToRoomModel(br.Room, RoomMappings.MapRoomCategoryToRCModel(br.Room.Category)))).ToList();
            @return.DriverServices = booking.DriverServices.Select(ds => DriverServiceMappings.MapDriverServiceToDriverServiceModel(ds, @return, null)).ToList();
            return @return;
        }

        public static Booking MapBookingModelToBooking(BookingModel model)
        {
            var @return = new Booking
            {
                Id = model.Id,
                BookingTime = model.BookTime,
                StartDate = model.Start,
                EndDate = model.End,
                Valid = model.Valid,
                CheckInTime = model.CheckIn,
                CheckOutTime = model.CheckOut,
                UserId = model.ClientId
   
                
            };
            @return.Rating = model.Rating != null ? MapRatingModelToRating(model.Rating, @return) : null;
            return @return;

        }

        public static BookingRoomModel MapBookingRoomToBookingRoomModel(BookingRoom bookingRoom, BookingModel @ref, RoomModel room)
        {
            var BookingRoom =  new BookingRoomModel
            {
                Id = bookingRoom.Id,
                BookingId = bookingRoom.BookingId,
                BookingModel = @ref,
                RoomId = bookingRoom.RoomId,
                Room = room
            };
            BookingRoom.ExtraFacilities = bookingRoom.ExtraFacilities.Select(ef => FacilityMappings.MapExtraFacilityToEFModel(ef, 
                BookingRoom,FacilityMappings.MapFacilityToFacilityModel(ef.Facility))).ToList();
            return BookingRoom;
        }
        public static RatingModel MapRatingToRatingModel(Rating rating, BookingModel @ref)
        {
            return new RatingModel
            {
                Id = rating.Id,
                BookingId = rating.Booking.Id,
                Booking = @ref ,
                DateCreated = rating.DateCreated ?? DateTime.Now,
                RateValue = rating.Rate,
                Description = rating.Description

            };

        }

        public static Rating MapRatingModelToRating(RatingModel model, Booking @ref)
        {
            return new Rating
            {
                Id = model.Id,
                DateCreated = model.DateCreated,
                Description = model.Description,
                Rate = model.RateValue,
                Booking = @ref
            };

        }
    }
}
