using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.DataAccess
{
    public interface IDataAccessProvider
    {

        IEnumerable<user_role> GetAllRoles();

        IEnumerable<user_role> GetAllRoles_except_admin();
        void add_roles(user_role ur);
        void update_roles(user_role ur);
        user_role GetSingleRole(int role_id);
        void update_roles(int _role_id, user_role ur);
        IEnumerable<user_role> user_role_by_id(int role_id);
        void Delete_roles(user_role ur);
        IEnumerable<module> get_module_list();
        int Get_srno_max_module();
        void add_module(module mod);
        module GetSingle_module(string module_id);
        void update_module_new(module mod);
        IEnumerable<module> Single_module_by_id(string module_id);

        IQueryable<Object> get_sub_module_list(string module_id);
        IEnumerable<sub_module> GetAll_sub_module_id(string module_id);
        int Get_srno_max_sub_module();
        void add_sub_module(sub_module sub_mod);
        void update_sub_module_new(sub_module sub_mod);
        sub_module GetSingle_sub_module(string sub_module_id);
        IEnumerable<sub_module> Single_sub_module_by_id(string sub_module_id);

        IEnumerable<accessRight> Get_access_right_for_user(string username);
        int Get_srno_max_users();
        void add_access_right_for_user(accessRight acc_right);
        void Delete_access_right(string username, string module_id);
        accessRight Get_Delete_access_right(string username, string module_id);
        IQueryable<Object> Get_access_right_grid_for_user(string username);
        IQueryable<Object> user_access_right_sm(string username, string m_id);
        IQueryable<Object> user_not_access_right_sm(string username, string m_id);
        IQueryable<Object> access_right_sub_module_bind(string username, string m_id);

        IEnumerable<userregistration> GetAllUser();
        userregistration GetSingleUser(string username);
        void Add_users(userregistration usr);

        void update_userDetails(userregistration usr);
        userregistration GetSingle_user(string username);

        IQueryable<object> Get_sites_under_contractor(string ref_id);
        IQueryable<object> profile_details_user(string user_id);
        IQueryable<object> Get_role_list_by_name(int role_id);
    }
}
