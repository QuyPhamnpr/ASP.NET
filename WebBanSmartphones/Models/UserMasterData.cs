using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSmartphones.Models
{
    public class UserMasterData
    {
        public int Id { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public Nullable<int> Email { get; set; }
        [Required]

        public string Password { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> Sex { get; set; }
       
    }
}