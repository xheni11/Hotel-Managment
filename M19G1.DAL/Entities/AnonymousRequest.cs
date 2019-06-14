using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public partial class AnonymousRequest:BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Confirmed { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
