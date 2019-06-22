using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mappings
{
    public static class DriverServiceMappings
    {
        public static TaxiServiceModel MapDriverServiceToDriverServiceModel(PersonalDriverService driverService)
        {
            return new TaxiServiceModel
            {
                Id = driverService.Id,
                Completed = driverService.Completed,
                Destination = driverService.Destination,
                Location = driverService.Location,
                StartTime = driverService.PickUpTime,
                TotalTime = driverService.VoyageTime,
                BookingId = driverService.BookingId,
                //Booking = BookingMappings.MapBookingToBookingModel(driverService.Booking),
                DriverId = driverService.DriverId,
                //Driver = UserMappings.MapAspNetUserToUserModel(driverService.Driver)

            };
        }
    }
}
