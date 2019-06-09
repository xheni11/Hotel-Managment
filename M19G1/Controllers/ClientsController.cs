using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class ClientsController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IFacilityService _facilityService;
        private readonly IRoomCategoryService _roomCategoryService;
        private readonly IRoomService _roomService;
        public ClientsController(IBookingService bookingService,IFacilityService facilityService, 
            IRoomCategoryService roomCategoryService, IRoomService roomService)
        {
            _bookingService = bookingService;
            _facilityService = facilityService;
            _roomCategoryService = roomCategoryService;
            _roomService = roomService;

        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OldBookings()
        {
            List<BookingModel> oldBookings = _bookingService.GetOldBookings(CurrentUser.Id);
            return View(oldBookings.Select(b => MapBookingModelToViewModel(b)).ToList());
        }


        [HttpGet]
        public ActionResult FilterRooms()
        {
            List<FacilityModel> Facilities = _facilityService.GetFacilites();
            List<RoomCategoryModel> RoomCategories = _roomCategoryService.GetRoomCategories();
            FilterRoomViewModel model = new FilterRoomViewModel();
            model.Facilities = Facilities;
            model.Categories = RoomCategories;

            return View(model);
        }
        [HttpPost]
        public ActionResult FilterRooms(FilterRoomViewModel filterModel)
        {
            if(ModelState.IsValid)
            {
                FilterRoomModel roomsFilter = MapFilterRoomViewModelToFRModel(filterModel);
                List<RoomModel> filteredRooms = _roomService.FilterRooms(roomsFilter);
                filterModel.Rooms = filteredRooms;
            }
            List<FacilityModel> Facilities = _facilityService.GetFacilites();
            List<RoomCategoryModel> RoomCategories = _roomCategoryService.GetRoomCategories();
            filterModel.Categories = RoomCategories;
            filterModel.Facilities = Facilities;
            return View(filterModel);
        }

        public FilterRoomModel MapFilterRoomViewModelToFRModel(FilterRoomViewModel model)
        {
            return new FilterRoomModel
            {
                CategoryId = model.CategoryId ?? 0,
                RoomName = model.Name,
                LessThanPrice = model.Price ?? 0,
                Occupied = model.Occupied,
                SelectedFacilities = model.SelectedFacilities
            };
        }

        public BookingElementViewModel MapBookingModelToViewModel(BookingModel booking)
        {
            BookingElementViewModel BookingViewModel = new BookingElementViewModel
            {
                Id = booking.Id,
                StartDate = booking.Start,
                EndDate = booking.End,
                rate = booking.Rating == null ? -1.0 : booking.Rating.RateValue
            };

            BookingViewModel.RoomList = "";
            foreach(var BookingRoom in booking.BookingRooms)
            {
                BookingViewModel.RoomList += BookingRoom.Room.RoomName + " ; ";
            }

            return BookingViewModel;
        }
    }
}