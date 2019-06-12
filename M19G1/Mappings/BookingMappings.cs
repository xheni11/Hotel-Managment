using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mappings
{
    public static class BookingMappings
    {
        public static BookingElementViewModel MapBookingModelToViewModel(BookingModel booking)
        {
            BookingElementViewModel BookingViewModel = new BookingElementViewModel
            {
                Id = booking.Id,
                StartDate = booking.Start,
                EndDate = booking.End,
                rate = booking.Rating == null ? -1.0 : booking.Rating.RateValue,
                Cancelable = booking.Cancelable

            };

            BookingViewModel.RoomList = "";
            foreach (var BookingRoom in booking.BookingRooms)
            {
                BookingViewModel.RoomList += BookingRoom.Room.RoomName + " ; ";
            }

            return BookingViewModel;
        }

        public static BookingViewModel MapBookingModelToBookingViewModel(BookingModel model)
        {
            return new BookingViewModel
            {
                Id = model.Id,
                StartDate = model.Start,
                EndDate = model.End,
                Rooms = model.BookingRooms.Select(br => RoomMappings.MapRoomModelToRVModel(br.Room)).ToList(),
                DriverServices = model.DriverServices.Select(ds =>
                                    DriverServiceMappings.MapDriverServiceModelToDSViewModel(ds)).ToList()

            };
        }
    }
}