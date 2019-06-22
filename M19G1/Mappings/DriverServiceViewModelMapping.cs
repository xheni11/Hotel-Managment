using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mapping
{
    public class DriverServiceViewModelMapping
    {
        public static TaxiServiceViewModel ToViewModel(TaxiServiceModel driver)
        {
            return new TaxiServiceViewModel
            {
                Id = driver.Id,
                Completed = driver.Completed,
                Destination = driver.Destination,
                Location = driver.Location,
                TotalTime = driver.TotalTime

            };
        }

        public static TaxiServiceModel ToModel(TaxiServiceViewModel driver, int driverId)
        {
            return new TaxiServiceModel
            {

                Completed = driver.Completed,
                Destination = driver.Destination,
                DriverId = driverId,
                Location = driver.Location,
                TotalTime = driver.TotalTime

            };
        }

        public static List<TaxiServiceViewModel> ToViewModel(IEnumerable<TaxiServiceModel> drivers)
        {
            return drivers.Select(ToViewModel).ToList();
        }

    }
}