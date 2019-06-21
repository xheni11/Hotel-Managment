using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mapping
{
    public class DriverServiceViewModelMapping
    {
        public static DriverServiceViewModel ToViewModel(DriverServiceModel driver)
        {
            return new DriverServiceViewModel
            {
                Id = driver.Id,
                Completed = driver.Completed,
                Destination = driver.Destination,
                Location = driver.Location,
                TotalTime = driver.TotalTime

            };
        }

        public static DriverServiceModel ToModel(DriverServiceViewModel driver, int driverId)
        {
            return new DriverServiceModel
            {

                Completed = driver.Completed,
                Destination = driver.Destination,
                DriverId = driverId,
                Location = driver.Location,
                TotalTime = driver.TotalTime

            };
        }

        public static List<DriverServiceViewModel> ToViewModel(IEnumerable<DriverServiceModel> drivers)
        {
            return drivers.Select(ToViewModel).ToList();
        }

    }
}