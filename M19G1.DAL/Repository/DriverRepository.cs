using M19G1.DAL.Entities;
using M19G1.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Repository
{
    public class DriverRepository:BaseRepository<PersonalDriverService>
    {

        public DriverRepository(M19G1Context context):base(context)
        {

        }
        public void ConfirmRoutesTaken(int driverId)
        {
            PersonalDriverService driverServiceToUpdate = GetByID(driverId);
            driverServiceToUpdate.Taken = true;
            Update(driverServiceToUpdate);
        }
        public void ConfirmRoutesCompleted(int driverId)
        {
            PersonalDriverService driverServiceToUpdate = GetByID(driverId);
            driverServiceToUpdate.Completed = true;
            driverServiceToUpdate.PickUpTime = DateTime.Now;
            Update(driverServiceToUpdate);
        }
    }
}
