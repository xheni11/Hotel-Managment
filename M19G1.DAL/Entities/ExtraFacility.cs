
namespace M19G1.DAL.Entities
{
    public partial class ExtraFacility
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BookingRoom BookingRoom { get; set; }
        public Facility Facility { get; set; }
        public int BookingRoomId { get; set; }
        public int FacilityId { get; set; }
    }
}
