
namespace M19G1.DAL.Entities
{
    public partial class RoomFacility
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Room Room { get; set; }
        public Facility Facility { get; set; }
        public int RoomId { get; set; }
        public int FacilityId { get; set; }
    }
}
