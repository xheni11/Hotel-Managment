using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.Common.Exceptions.Models
{
    public class NotifficationModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string ClassType { get { return "alert-"+this.Type.ToLower(); } set { } }
    }
}
