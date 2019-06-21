using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mapping
{
    public class DriverModelMapping
    {
        public static DriverServiceModel ToModel(DAL.Entities.PersonalDriverService driver)
        {
            return new DriverServiceModel
            {
                Id = driver.Id,                
                BookingId=driver.BookingId,
                Completed=driver.Completed,
                Destination=driver.Destination,
                DriverId=driver.DriverId,
                Location=driver.Location,
                TotalTime=driver.VoyageTime

            };
        }

        public static PersonalDriverService ToEntity(DriverServiceModel driver)
        {
            return new PersonalDriverService
            {
                Id = driver.Id,
                BookingId = driver.BookingId,
                Completed = driver.Completed,
                Destination = driver.Destination,
                DriverId = driver.DriverId,
                Location = driver.Location,
                VoyageTime = driver.TotalTime

            };
        }

        public static List<DriverServiceModel> ToModel(IEnumerable<DAL.Entities.PersonalDriverService> drivers)
        {
            return drivers.Select(ToModel).ToList();
        }

    }
}
