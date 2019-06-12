using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mappings
{
    public static class DriverServiceMappings
    {
        public static DriverServiceViewModel MapDriverServiceModelToDSViewModel(DriverServiceModel model)
        {
            return new DriverServiceViewModel
            {
                Id = model.Id,
                PickUpTime = model.StartTime,
                Destination = model.Destination,
                Location = model.Location
            };
        }
    }
}