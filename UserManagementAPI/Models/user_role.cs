using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    [Table("tbl_user_role")]
    public class user_role
    {
        //[Key]
        public int role_id { get; set; }
        public string role_name { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public Boolean is_active { get; set; }
    }
}
