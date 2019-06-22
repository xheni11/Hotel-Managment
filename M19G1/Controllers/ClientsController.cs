
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
        private readonly IDriverService _driverService;
        private readonly IRatingService _ratingService;
        public ClientsController(IBookingService bookingService,IFacilityService facilityService, 
            IRoomCategoryService roomCategoryService, IRoomService roomService, IDriverService driverService,
            IRatingService ratingService)
        {
            _bookingService = bookingService;
            _facilityService = facilityService;
            _roomCategoryService = roomCategoryService;
            _roomService = roomService;
            _driverService = driverService;
            _ratingService = ratingService;

        }
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("ActiveBookings");
        }

        [HttpGet]
        public ActionResult OldBookings()
        {
            List<BookingModel> oldBookings = _bookingService.GetOldBookings(6);//CurrentUser.Id
            List<BookingElementViewModel> oldBookingsElements = oldBookings.Select(b => BookingMappings.MapBookingModelToViewModel(b)).ToList();
            return View(oldBookingsElements);
        }

        [HttpGet]
        public ActionResult ActiveBookings()
        {
            List<BookingModel> activeBookings = _bookingService.GetActiveBookings(6);//CurrentUser.Id
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
            if (bookingModel == null)
                return RedirectToAction("ActiveBookings");
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
            if (bookingModel == null)
                return RedirectToAction("ActiveBookings");
            BookingViewModel bookingViewModel = BookingMappings.MapBookingModelToBookingViewModel(bookingModel);
            AddDriverServiceViewModel model = new AddDriverServiceViewModel();
            model.Booking = bookingViewModel;

            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddService(AddDriverServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool success = _driverService.AddDriverService(DriverServiceMappings.MapAddDSViewModelToDriverServiceModel(model));
                if (success)
                {
                    ViewBag.Result = "Driver service successfully added !";
                }
                else
                {
                    ViewBag.Result = "Driver service could not be added !";
                }
            }
            BookingModel bookingModel = _bookingService.GetBookingById(model.Booking.Id);
            BookingViewModel bookingViewModel = BookingMappings.MapBookingModelToBookingViewModel(bookingModel);
            AddDriverServiceViewModel returnModel = new AddDriverServiceViewModel();
            returnModel.Booking = bookingViewModel;
            return View(returnModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            BookingModel bookingModel = _bookingService.GetBookingById(id);
            if (bookingModel == null)
                return RedirectToAction("OldBookings");
            BookingViewModel bookingViewModel = BookingMappings.MapBookingModelToBookingViewModel(bookingModel);
            BookingDetailsViewModel model = new BookingDetailsViewModel();
            model.bookingModel = bookingViewModel;
            model.ratingModel = bookingModel.Rating != null ? RatingMappings.MapRatingToRatingViewModel(bookingModel.Rating) : null;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddRating(BookingDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                RatingViewModel ratingModel = model.ratingModel;
                int bookingId = model.bookingModel.Id;
                ratingModel.BookingId = bookingId;
                RatingModel RatingModel = RatingMappings.MapRatingViewModelToRatingModel(ratingModel);
                _ratingService.AddBookingRating(RatingModel);
            }

            return RedirectToAction("Details", new { id = model.bookingModel.Id });
        }

        [HttpGet]
        public ActionResult CreateBooking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBooking(CreateBookingViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.EndDate < model.StartDate)
                    ViewBag.Result = "End date should be equal or greater than start date !";
                else
                {
                    BookingModel bookingModel = BookingMappings.MapCreateBookingVModelToBookingModel(model);
                    bookingModel.ClientId = 6; //CurrentUser.Id
                    int id = _bookingService.CreateNewBooking(bookingModel);
                    return RedirectToAction("ChooseRooms", new { bookingId = id });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ChooseRooms(int bookingId)
        {
            BookingModel booking = _bookingService.GetBookingById(bookingId);
            List<RoomModel> freeRooms = _roomService.GetFreeRoomsForBooking(bookingId);
            List<FacilityModel> roomFacilites = _facilityService.GetFacilites();
            ChooseRoomViewModel model = new ChooseRoomViewModel();
            model.rooms = freeRooms;
            model.booking = booking;
            model.facilities = roomFacilites;
            return View(model);

        }

        [HttpPost]
        public ActionResult ChooseRooms(ChooseRoomViewModel model)
        {
            
            if(ModelState.IsValid)
            {
                ChooseRoomModel ChooseModel = BookingMappings.MapChooseRoomViewModelToChooseRoomModel(model);
                if(_bookingService.AddRoomForBooking(ChooseModel))
                {
                    ViewBag.Result = "Room successfully added !";
                }
                else
                {
                    ViewBag.Result = "Room could not be added !";
                }
            }
            var returnModel = new ChooseRoomViewModel();
            BookingModel booking = _bookingService.GetBookingById(model.booking.Id);
            List<RoomModel> freeRooms = _roomService.GetFreeRoomsForBooking(model.booking.Id);
            List<FacilityModel> roomFacilites = _facilityService.GetFacilites();
            returnModel.rooms = freeRooms;
            returnModel.booking = booking;
            returnModel.facilities = roomFacilites;
            return View(returnModel);
        }

        [HttpPost]
        public ActionResult FinishBooking(int id)
        {
            _bookingService.FinishBooking(id);
            return RedirectToAction("ActiveBookings");
        }

        [HttpPost]
        public ActionResult BookAgain(BookAgainViewModel model)
        {
            if(model.StartDate <= DateTime.Now || model.StartDate >= model.EndDate)
            {
                return RedirectToAction("OldBookings");
               // return Json("OldBookings",JsonRequestBehavior.AllowGet);
            }
            else
            {
                BookAgainModel bookAgainModel = BookingMappings.MapBookAgainViewModelToBookAgainModel(model);
                NotifyMessage message =_bookingService.TryToBookAgain(bookAgainModel);
                //How to pass message ??
                return RedirectToAction("OldBookings");
            }
            
        }

        [HttpGet]
        public ActionResult RoomDetails(int id)
        {
            RoomModel roomModel = _roomService.GetRoomById(id);
            if (roomModel == null)
                return RedirectToAction("FilterRooms");
            RoomDetailsViewModel model = RoomMappings.MapRoomModelToRoomDetailsViewModel(roomModel);
            return View(model);
        } 

       
    }
}