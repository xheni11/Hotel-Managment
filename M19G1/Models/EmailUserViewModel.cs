using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace M19G1.Models
{
    public class EmailUserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }


    }
   
}