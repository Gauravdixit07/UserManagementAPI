using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    [Table("tbl_access_right")]
    public class accessRight
    {
        //[Key]
        public int id { get; set; }

        //public string user_id { get; set; }
        public string username { get; set; }
        public string module_id { get; set; }
        public string sub_module_id { get; set; }
        public Boolean add_access { get; set; }
        public Boolean edit_access { get; set; }
        public Boolean view_access { get; set; }
        public Boolean delete_access { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
        public Boolean is_active { get; set; }
    }
}
