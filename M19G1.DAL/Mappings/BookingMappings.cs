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
            var rooms = new List<BookingRoomModel>();
            if (booking.BookingRooms != null && booking.BookingRooms.Count > 0) {
                foreach (var bookingroom in booking.BookingRooms) {
                    var item = RoomMappings.MapRoomToRoomModel(bookingroom.Room, null);
                    if (item != null) {
                        var room2 = MapBookingRoomToBookingRoomModel(bookingroom, @return, item);
                        if (room2 != null) {
                            rooms.Add(room2);
                        }
                    }
                }

                @return.BookingRooms = rooms;
            }
            //@return.BookingRooms = booking.BookingRooms?.Select(br => MapBookingRoomToBookingRoomModel(br, @return,
            //    RoomMappings.MapRoomToRoomModel(br.Room, null))).ToList();
            var driverServices = new List<DriverServiceModel>();
            if(booking.DriverServices != null)
            {
                if(booking.DriverServices.Count > 0)
                {
                    foreach(var ds in booking.DriverServices)
                    {
                        DriverServiceModel dsModel = DriverServiceMappings.MapDriverServiceToDriverServiceModel(ds, @return, null);
                        if (dsModel != null)
                            driverServices.Add(dsModel);
                    }
                    
                }
            }
            @return.DriverServices = driverServices;
            //@return.DriverServices = booking.DriverServices?.Select(ds => DriverServiceMappings.MapDriverServiceToDriverServiceModel(ds, @return, null)).ToList();
            return @return;
        }

        public static Booking MapBookingModelToBooking(BookingModel model)
        {
            var @return = new Booking
            {
                BookingTime = model.BookTime,
                StartDate = model.Start,
                EndDate = model.End,
                Valid = model.Valid,
                CheckInTime = model.CheckIn,
                CheckOutTime = model.CheckOut,
                UserId = model.ClientId
   
                
            };
            @return.Rating = model.Rating != null ? MapRatingModelToRating(model.Rating, @return) : null;
            if(model.DriverServices != null)
            {
                if(model.DriverServices.Count > 0)
                {
                     @return.DriverServices = model.DriverServices.Select(ds =>
                         DriverServiceMappings.MapDriverServiceModelToPersonalDriverService(ds)).ToList();
                }
            }
            
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

            List<ExtraFacilityModel> extraFacilities = new List<ExtraFacilityModel>();
            if(bookingRoom.ExtraFacilities != null)
            {
                if(bookingRoom.ExtraFacilities.Count != 0)
                {
                    foreach(var ef in bookingRoom.ExtraFacilities)
                    {
                        FacilityModel fmodel;
                        if (ef.Facility != null)
                            fmodel = FacilityMappings.MapFacilityToFacilityModel(ef.Facility);
                        else
                            fmodel = null;
                        ExtraFacilityModel efm = FacilityMappings.MapExtraFacilityToEFModel(ef, BookingRoom,fmodel);
                        extraFacilities.Add(efm);
                    }
                }
            }
            BookingRoom.ExtraFacilities = extraFacilities;
                
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
                DateCreated = model.DateCreated,
                Description = model.Description,
                Rate = model.RateValue,
                Booking = @ref
            };

        }
        public static BookingRoom MapChooseRoomModelToBookingRoom(ChooseRoomModel model)
        {
            return new BookingRoom
            {
                BookingId = model.bookingId,
                RoomId = model.roomId
            };
        }

        public static BookAgainBookingModel MapBookingToBookAgainBookingModel(Booking model)
        {
            return new BookAgainBookingModel
            {
                BookingId = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

        }

        public static BookingRoom MapBookingRoomModelToBookingRoom(BookingRoomModel model)
        {
            return new BookingRoom
            {
                BookingId = model.BookingId,
                RoomId = model.RoomId
            };
        }
    }
}
