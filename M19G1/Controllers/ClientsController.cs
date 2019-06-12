
using M19G1.IBLL;
using M19G1.Mappings;
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
            List<BookingElementViewModel> oldBookingsElements = oldBookings.Select(b => BookingMappings.MapBookingModelToViewModel(b)).ToList();
            return View(oldBookingsElements);
        }

        [HttpGet]
        public ActionResult ActiveBookings()
        {
            List<BookingModel> activeBookings = _bookingService.GetActiveBookings(6);
            List<BookingElementViewModel> activeBookingsElements = activeBookings?.Select(b => BookingMappings.MapBookingModelToViewModel(b)).ToList();
            return View(activeBookingsElements);
        }

        [HttpGet]
        public ActionResult CancelBooking(int id)
        {
            bool canceled = _bookingService.CancelBooking(id);
            if (canceled)
                TempData["Result"] = "Booking successfully canceled !";
            else
                TempData["Result"] = "Booking could not be canceled !";
            return RedirectToAction("ActiveBookings");
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
                FilterRoomModel roomsFilter = RoomMappings.MapFilterRoomViewModelToFRModel(filterModel);
                List<RoomModel> filteredRooms = _roomService.FilterRooms(roomsFilter);
                filterModel.Rooms = filteredRooms;
            }
            List<FacilityModel> Facilities = _facilityService.GetFacilites();
            List<RoomCategoryModel> RoomCategories = _roomCategoryService.GetRoomCategories();
            filterModel.Categories = RoomCategories;
            filterModel.Facilities = Facilities;
            return View(filterModel);
        }

        [HttpGet]
        public ActionResult AddFacility(int id)
        {
            BookingModel bookingModel = _bookingService.GetBookingById(id);
            List<FacilityModel> Facilities = _facilityService.GetFacilites();

            BookingViewModel bookingViewModel = BookingMappings.MapBookingModelToBookingViewModel(bookingModel);
            AddFacilityViewModel model = new AddFacilityViewModel();
            model.Booking = bookingViewModel;
            model.Facilities = Facilities;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddFacility(AddFacilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                ExtraFacilityModel efModel = FacilityMappings.MapFacilityViewModelToEFacilityModel(model);
                if (_facilityService.AddExtraFacility(efModel))
                    ViewBag.Result = "Facility successfully added !";
                else
                    ViewBag.Result = "Facility could not be added !";
            }
            BookingModel bookingModel = _bookingService.GetBookingById(model.Booking.Id);
            List<FacilityModel> Facilities = _facilityService.GetFacilites();
            BookingViewModel bookingViewModel = BookingMappings.MapBookingModelToBookingViewModel(bookingModel);
            model.Booking = bookingViewModel;
            model.Facilities = Facilities;
            return View(model);
            
        }

        [HttpGet]
        public ActionResult AddService(int id)
        {
            BookingModel bookingModel = _bookingService.GetBookingById(id);
            BookingViewModel bookingViewModel = BookingMappings.MapBookingModelToBookingViewModel(bookingModel);
            AddDriverServiceViewModel model = new AddDriverServiceViewModel();
            model.Booking = bookingViewModel;

            return View(model);
        }
        

        

        
    }
}