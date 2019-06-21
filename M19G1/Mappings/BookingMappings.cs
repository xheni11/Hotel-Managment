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
            var BookingViewModel = new BookingViewModel
            {
                Id = model.Id,
                StartDate = model.Start,
                EndDate = model.End,
                CheckInTime = model.CheckIn,
                CheckOutTime = model.CheckOut
            };
            if(model.BookingRooms != null)
            {
                List<RoomViewModel> rooms = new List<RoomViewModel>();
                if(model.BookingRooms.Count > 0)
                {
                    foreach(var bookingRoom in model.BookingRooms)
                    {
                        if(bookingRoom.Room != null)
                        {
                            RoomViewModel roomModel = RoomMappings.MapRoomModelToRVModel(bookingRoom.Room);
                            rooms.Add(roomModel);
                        }
                    }
                    BookingViewModel.Rooms = rooms;
                }
            }
            if (model.DriverServices != null)
            {
                List<DriverServiceViewModel> driverServices = new List<DriverServiceViewModel>();
                if (model.DriverServices.Count > 0)
                {
                    foreach (var ds in model.DriverServices)
                    {
                        DriverServiceViewModel dsvModel = DriverServiceMappings.MapDriverServiceModelToDSViewModel(ds);
                        driverServices.Add(dsvModel);
                    }
                }
                BookingViewModel.DriverServices = driverServices;
            }
            return BookingViewModel;
        }

        public static BookingModel MapCreateBookingVModelToBookingModel(CreateBookingViewModel model)
        {
            var BookingModel = new BookingModel
            {
                BookTime = DateTime.Now,
                Start = model.StartDate,
                End = model.EndDate
            };
            if(model.InitialLocation != null || model.FinalLocation != null)
            {
                List<DriverServiceModel> dsModels = new List<DriverServiceModel>();
                if(model.InitialLocation != null)
                {
                    dsModels.Add(
                        new DriverServiceModel
                        {
                            Location = model.InitialLocation,
                            Destination = "Hotel",
                            StartTime = model.StartDate                            
                        }
                        );
                }
                if(model.FinalLocation != null)
                {
                    dsModels.Add(
                        new DriverServiceModel
                        {
                            Location = "Hotel",
                            Destination = model.FinalLocation,
                            StartTime = model.EndDate
                        }
                       );
                }
                BookingModel.DriverServices = dsModels;
            }
            return BookingModel;
        }

        public static ChooseRoomModel MapChooseRoomViewModelToChooseRoomModel(ChooseRoomViewModel model)
        {
            return new ChooseRoomModel
            {
                bookingId = model.booking.Id,
                roomId = model.RoomId,
                facilityIds = model.facilityIds
            };
        }

        public static BookAgainModel MapBookAgainViewModelToBookAgainModel(BookAgainViewModel model)
        {
            return new BookAgainModel
            {
                bookingId = model.bookingId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };
        }
    }
}