using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mappings
{
    public static class RoomMappings
    {
        public static RoomViewModel MapRoomModelToRVModel(RoomModel model)
        {
            return new RoomViewModel
            {
                RoomId = model.Id,
                Name = model.RoomName,
                GuestsNr = model.GuestsNr
            };
        }

        public static FilterRoomModel MapFilterRoomViewModelToFRModel(FilterRoomViewModel model)
        {
            return new FilterRoomModel
            {
                CategoryId = model.CategoryId ?? 0,
                RoomName = model.Name,
                LessThanPrice = model.Price ?? 0,
                Occupied = model.Occupied,
                SelectedFacilities = model.SelectedFacilities
            };
        }

    }
}