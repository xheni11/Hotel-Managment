using M19G1.DAL;
using M19G1.DAL.Entities;
using M19G1.DAL.Mappings;
using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.BLL
{
    public class BookingService : IBookingService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        public BookingService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }

        public List<BookingModel> GetOldBookings(int UserId)
        { 
            DateTime now = DateTime.Now;
            List<BookingModel> oldBookings = _internalUnitOfWork.BookingsRepository.Get(b => b.Valid == true && b.UserId == UserId && b.CheckOutTime <= now)
                .Select(b => BookingMappings.MapBookingToBookingModel(b,null)).ToList();
            return oldBookings;
            
        }
        public List<BookingModel> GetActiveBookings(int UserId)
        {
            DateTime now = DateTime.Now;
            List<BookingModel> activeBookings = _internalUnitOfWork.BookingsRepository.Get(b => b.Valid == true && b.UserId == UserId && b.CheckOutTime == null && b.EndDate >= now)
                .Select(m => BookingMappings.MapBookingToBookingModel(m,null)).ToList();
            foreach(var booking in activeBookings)
            {
                booking.Cancelable = isCancelable(booking);
            }
            return activeBookings;
        }

        private bool isCancelable(BookingModel booking)
        {
            if (booking.BookTime <= booking.Start.AddHours(-24))
            {
                // booking i kryer te pakten 24 ore para fillimit
                if (DateTime.Now <= booking.BookTime.AddHours(4))
                {
                    //jemi brenda afatit 4 ore per ta bere cancel
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool CancelBooking(int bookingId)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(bookingId);
            if (booking == null)
                return false;
            BookingModel bookingModel = BookingMappings.MapBookingToBookingModel(booking,null);
            if (!isCancelable(bookingModel))
                return false;
            else
            {
                booking.Valid = false;
                _internalUnitOfWork.BookingsRepository.Update(booking);
                _internalUnitOfWork.Save();
                return true;
            }
        }

        public BookingModel GetBookingById(int bookingId)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(bookingId);
            if (booking == null)
                return null;
            BookingModel bookingModel = BookingMappings.MapBookingToBookingModel(booking, null);
            return bookingModel;
        }

        public int CreateNewBooking(BookingModel model)
        {
            model.Valid = false; // ende nuk kemi shtuar dhomat
            Booking booking = BookingMappings.MapBookingModelToBooking(model);
            _internalUnitOfWork.BookingsRepository.Insert(booking);
            _internalUnitOfWork.Save();
            int id = booking.Id;
            return id;
        }

        public bool AddRoomForBooking(ChooseRoomModel model)
        {
            if(_internalUnitOfWork.RoomRepository.GetByID(model.roomId).Occupied == false)
            {
                BookingRoom bookingRoom = BookingMappings.MapChooseRoomModelToBookingRoom(model);
                _internalUnitOfWork.BookingRoomRepository.Insert(bookingRoom);
                _internalUnitOfWork.Save();
                int bookingRoomId = bookingRoom.Id;
                foreach(int facId in model.facilityIds)
                {
                    _internalUnitOfWork.ExtraFacilityRepository.Insert(
                        new ExtraFacility()
                        {
                            BookingRoomId = bookingRoomId,
                            FacilityId = facId,
                            Price = 0.0M,
                            Quantity = 1
                        });
                    _internalUnitOfWork.Save();
                }
                return true;
            }
            return false;
        }

        public bool FinishBooking(int bookingId)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(bookingId);
            if(booking != null)
            {
                booking.Valid = true;
                _internalUnitOfWork.BookingsRepository.Update(booking);
                _internalUnitOfWork.Save();
                return true;
            }
            return false;
        }

        public NotifyMessage TryToBookAgain(BookAgainModel model)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(model.bookingId);
            if (booking != null)
            {
                BookingModel bookingModel = BookingMappings.MapBookingToBookingModel(booking, null);
                if(bookingModel.BookingRooms.Count > 0)
                {
                    List<int> extraFacilites = new List<int>();
                    foreach(var bookingRoom in bookingModel.BookingRooms)
                    {
                        if(!CanBeBooked(bookingRoom.RoomId,model.StartDate,model.EndDate))
                        {
                            return new NotifyMessage
                            {
                                success = false,
                                Notification = $"Room {bookingRoom.Room.RoomName} is booked on those dates !"
                            };

                        }
                        else
                        {
                            if(bookingRoom.ExtraFacilities != null)
                            {
                                if(bookingRoom.ExtraFacilities.Count > 0)
                                {
                                    extraFacilites.AddRange(bookingRoom.ExtraFacilities.Select(ef => ef.FacilityId).ToList());
                                }
                            }
                        }
                    }
                    if(extraFacilites.Count > 0)
                    {
                        extraFacilites = extraFacilites.Distinct().ToList();
                        foreach(var facilityId in extraFacilites)
                        {
                            if(_internalUnitOfWork.FacilityRepository.GetByID(facilityId).Available == false)
                            {
                                string facilityName = _internalUnitOfWork.FacilityRepository.GetByID(facilityId).Name;
                                return new NotifyMessage
                                {
                                    success = false,
                                    Notification = $"Facility {facilityName} is not available !"
                                };
                            }
                        }
                    }
                    return BookAgain(model);
                }
                return new NotifyMessage
                {
                    success = false,
                    Notification = "This booking does not have rooms !"
                };
                
            }
            else
            {
                return new NotifyMessage
                {
                    success = false,
                    Notification = "This booking does not exist"
                };
            }
        }

        private bool CanBeBooked(int roomId, DateTime startDate, DateTime endDate)
        {
            List<Booking> bookings = _internalUnitOfWork.BookingRoomRepository.Get(br => br.RoomId == roomId)
                .Select(br => br.Booking).ToList();
            if(bookings != null)
            {
                if (bookings.Count > 0)
                {
                    List<BookAgainBookingModel> bookingModels = bookings.Select(b => BookingMappings.MapBookingToBookAgainBookingModel(b)).ToList();
                    foreach (var booking in bookingModels)
                    {
                        if (!((booking.StartDate < startDate && booking.EndDate < startDate) || (booking.StartDate > endDate && booking.EndDate > endDate)))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return true;
                
            }
            return true;
        }

        private NotifyMessage BookAgain(BookAgainModel model)
        {
            Booking booking = _internalUnitOfWork.BookingsRepository.GetByID(model.bookingId);
            if (booking != null)
            {
                BookingModel bookingModel = BookingMappings.MapBookingToBookingModel(booking,null);
                BookingModel bookingToInsert = new BookingModel();
                bookingToInsert.ClientId = bookingModel.ClientId;
                bookingToInsert.BookTime = DateTime.Now;
                bookingToInsert.Start = model.StartDate;
                bookingToInsert.End = model.EndDate;
                bookingToInsert.Valid = true;

                Booking insertBooking = BookingMappings.MapBookingModelToBooking(bookingToInsert);
                _internalUnitOfWork.BookingsRepository.Insert(insertBooking);
                _internalUnitOfWork.Save();
                int BookingId = insertBooking.Id;

                foreach(var bookingRoom in bookingModel.BookingRooms)
                {
                    BookingRoomModel bookingRoomModel = new BookingRoomModel
                    {
                        RoomId = bookingRoom.RoomId,
                        BookingId = BookingId
                    };
                    BookingRoom BookingRoom = BookingMappings.MapBookingRoomModelToBookingRoom(bookingRoomModel);
                    _internalUnitOfWork.BookingRoomRepository.Insert(BookingRoom);
                    _internalUnitOfWork.Save();

                    int BookingRoomId = BookingRoom.Id;

                    if(bookingRoom.ExtraFacilities != null)
                    {
                        if(bookingRoom.ExtraFacilities.Count > 0)
                        {
                            foreach(var extraFacility in bookingRoom.ExtraFacilities)
                            {
                                ExtraFacilityModel efModel = new ExtraFacilityModel
                                {
                                    BookingRoomId = BookingRoomId,
                                    EFQuantity = extraFacility.EFQuantity,
                                    Price = extraFacility.Price,
                                    FacilityId = extraFacility.FacilityId

                                };
                                ExtraFacility ExtraFacility = FacilityMappings.MapExtraFacilityModelToExtraFacility(efModel, null, null);
                                _internalUnitOfWork.ExtraFacilityRepository.Insert(ExtraFacility);
                                _internalUnitOfWork.Save();

                            }
                        }
                    }
                }

                return new NotifyMessage
                {
                    success = true,
                    Notification = "Booking successfully added !"
                };
            }
            return new NotifyMessage
            {
                success = false,
                Notification = "Booking does not exist !"
            };

        }
    }
}
