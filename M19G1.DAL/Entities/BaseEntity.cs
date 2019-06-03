using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Entities
{
    public class BaseEntity
    {
        public bool Deleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
      

    }
}
