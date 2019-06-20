using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.ViewModels
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}