using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.DataAccess
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
           : base((DbContextOptions)options)
        {
        }

        public DbSet<user_role> user_Roles { get; set; }

        public DbSet<module> modules { get; set; }
        public DbSet<sub_module> Sub_Modules { get; set; }
        public DbSet<accessRight> accessRights { get; set; }
        public DbSet<userregistration> Userregistrations { get; set; }

    }
}
