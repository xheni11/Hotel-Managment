using System;

namespace M19G1.DAL.Entities
{
    public partial class AnonymousRequest
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Confirmed { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
