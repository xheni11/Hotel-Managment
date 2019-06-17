using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.IBLL
{
    public interface IRoomService
    {
        List<RoomModel> FilterRooms(FilterRoomModel model);
        List<RoomModel> GetFreeRoomsForBooking(int bookingId);
        
    }
}
