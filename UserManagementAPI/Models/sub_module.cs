using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    [Table("tbl_sub_modules")]
    public class sub_module
    {
        public int srno { get; set; }
        //[Key]
        public string? sub_module_id { get; set; }
        public string? sub_module_name { get; set; }
        public string? module_id { get; set; }
        public string? created_by { get; set; }
        public DateTime created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public Boolean is_active { get; set; }
    }
}
