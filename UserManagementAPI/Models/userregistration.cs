using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    [Table("tbl_userregistration")]
    public class userregistration
    {
        public int sno { get; set; }
        //[Key] Primary Key
        public string user_id { get; set; }
        public string username { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? mobile_no { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public DateTime? last_loging { get; set; }
        public Boolean? is_active { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public int role_id { get; set; }
        public string? token { get; set; }
        public string? role_name { get; set; }
        public string ref_id { get; set; }

    }
}
