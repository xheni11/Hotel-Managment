using M19G1.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M19G1.Models;
using M19G1.DAL;
using M19G1.DAL.Mappings;

namespace M19G1.BLL
{
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly UnitOfWork _internalUnitOfWork;

        public RoomCategoryService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }
        public List<RoomCategoryModel> GetRoomCategories()
        {
            return _internalUnitOfWork.RoomCategoryRepository.Get()
                .Select(rc => RoomMappings.MapRoomCategoryToRCModel(rc)).ToList();
        }
    }
}
