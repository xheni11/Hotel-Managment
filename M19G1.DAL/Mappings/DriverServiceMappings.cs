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
        public static DriverServiceModel MapDriverServiceToDriverServiceModel(PersonalDriverService driverService,BookingModel @booking,UserModel @user)
        {
            return new DriverServiceModel
            {
                Id = driverService.Id,
                Completed = driverService.Completed,
                Destination = driverService.Destination,
                Location = driverService.Location,
                StartTime = driverService.PickUpTime,
                TotalTime = driverService.VoyageTime,
                BookingId = driverService.BookingId,
                Booking = @booking,
                DriverId = driverService.DriverId,
                Driver = @user

            };
        }
    }
}
