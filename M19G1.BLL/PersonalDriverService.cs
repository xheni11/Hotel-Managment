using M19G1.DAL;
using M19G1.DAL.Mapping;
using M19G1.DAL.Repository;
using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.BLL
{
    public class PersonalDriverService:IPersonalDriverService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        private readonly DriverRepository _driverRepository;

        public PersonalDriverService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
            _driverRepository = _internalUnitOfWork.DiverRepository;
        }
        public List<TaxiServiceModel> GetAllDriverService()
        {
            return DriverModelMapping.ToModel(_driverRepository.GetAll().Where(d=>d.Taken==false && d.Booking.StartDate.Date==DateTime.Now.Date).ToList());
        }
        public void GetMyDriverServiceUnCompleted(int idUser)
        {
            _driverRepository.GetAll().Where(d => d.Taken == true && d.DriverID==idUser && d.Completed==false && d.Booking.StartDate.Date == DateTime.Now.Date);
        }

        public void RouteTaken(int id)
        {
            _driverRepository.ConfirmRoutesTaken(id);
            _internalUnitOfWork.Save();
        }
        public void RouteCompleted(int id)
        {
            _driverRepository.ConfirmRoutesCompleted(id);
            _internalUnitOfWork.Save();
        }
    }
}
