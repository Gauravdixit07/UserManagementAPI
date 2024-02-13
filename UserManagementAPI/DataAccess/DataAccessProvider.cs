using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using UserManagementAPI.Models;

namespace UserManagementAPI.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }
        public IEnumerable<user_role> GetAllRoles()
        {
            try
            {
                return (IEnumerable<user_role>)_context.user_Roles.OrderByDescending(x => x.created_date).Where(x => x.is_active == true).ToList<user_role>().OrderByDescending(x => x.role_id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<user_role> GetAllRoles_except_admin()
        {
            try
            {
                return (IEnumerable<user_role>)_context.user_Roles.OrderByDescending(x => x.created_date).Where(x => x.is_active == true && x.role_id != 1).ToList<user_role>().OrderByDescending(x => x.role_id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void add_roles(user_role ur)
        {
            try
            {
                this._context.user_Roles.Add(ur);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void update_roles(int _role_id, user_role ur)
        {
            {
                try
                {
                    user_role user1 = new user_role()
                    {
                        role_id = _role_id,
                        role_name = ur.role_name
                    };
                    this._context.user_Roles.Update(ur);
                    this._context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public user_role GetSingleRole(int role_id)
        {
            try
            {
                return _context.user_Roles.FirstOrDefault<user_role>((Expression<Func<user_role, bool>>)(x => x.role_id == role_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void update_roles(user_role ur)
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<user_role> user_role_by_id(int role_id)
        {
            try
            {
                return this._context.user_Roles.Where<user_role>((Expression<Func<user_role, bool>>)(t => t.role_id == role_id)).ToList<user_role>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_roles(user_role ur)
        {
            try
            {
                user_role user1 = new user_role()
                {
                    role_id = ur.role_id,
                    is_active = ur.is_active
                };
                this._context.user_Roles.Update(ur);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<module> get_module_list()
        {
            try
            {
                return (IEnumerable<module>)this._context.modules.Where(x => x.is_active == true).ToList<module>().OrderByDescending(x => x.created_date).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Get_srno_max_module()
        {
            try
            {
                var max_date = _context.modules.Select(t => t.srno);
                if (max_date.Any())
                {
                    return max_date.Max();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //try
            //{
            //    var max = _context.modules.DefaultIfEmpty().Max(r => r == null ? 0 : r.srno);
            //    return max;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }



        public void add_module(module mod)
        {
            try
            {
                this._context.modules.Add(mod);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public module GetSingle_module(string module_id)
        {
            try
            {
                return _context.modules.FirstOrDefault<module>((Expression<Func<module, bool>>)(x => x.module_id == module_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void update_module_new(module mod)
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<sub_module> Single_sub_module_by_id(string sub_module_id)
        {
            try
            {
                return this._context.Sub_Modules.Where<sub_module>((Expression<Func<sub_module, bool>>)(t => t.sub_module_id == sub_module_id)).ToList<sub_module>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<accessRight> Get_access_right_for_user(string username)
        {
            try
            {
                return this._context.accessRights.Where<accessRight>((Expression<Func<accessRight, bool>>)(t => t.username == username)).ToList<accessRight>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void add_access_right_for_user(accessRight acc_right)
        {
            try
            {
                this._context.accessRights.Add(acc_right);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete_access_right(string username, string module_id)
        {
            try
            {
                //this._context.accessRights.Remove(this._context.accessRights.FirstOrDefault<accessRight>((Expression<Func<accessRight, bool>>)(t => t.username == username && t.module_id == module_id)));
                var recordsToDelete = _context.accessRights.Where(t => t.username == username && t.module_id == module_id);
                _context.accessRights.RemoveRange(recordsToDelete);

                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public accessRight Get_Delete_access_right(string username, string module_id)
        {
            try
            {
                return this._context.accessRights.FirstOrDefault<accessRight>((Expression<Func<accessRight, bool>>)(t => t.username == username && t.module_id == module_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<object> Get_access_right_grid_for_user(string username)
        {
            try
            {
                return from a in _context.accessRights.Where(x => x.username == username)
                       join p in _context.Sub_Modules on a.sub_module_id equals p.sub_module_id
                       join c in _context.modules on a.module_id equals c.module_id
                       select new
                       {
                           username = a.username,
                           module_name = c.module_name,
                           sub_module_name = p.sub_module_name,
                           edit_access = a.edit_access,
                           add_access = a.add_access,
                           view_access = a.view_access,
                           delete_access = a.delete_access
                       };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<object> user_access_right_sm(string username, string m_id)
        {
            try
            {
                return from a in _context.accessRights.Where(x => x.username == username && x.module_id == m_id)
                       join c in _context.modules on a.module_id equals c.module_id
                       join p in _context.Sub_Modules on a.sub_module_id equals p.sub_module_id

                       select new
                       {
                           username = a.username,
                           module_name = c.module_name,
                           sub_module_name = p.sub_module_name,
                           sub_module_id = a.sub_module_id,
                           edit_access = a.edit_access,
                           add_access = a.add_access,
                           view_access = a.view_access,
                           delete_access = a.delete_access
                       };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<object> user_not_access_right_sm(string username, string m_id)
        {
            try
            {
                return from a in _context.accessRights.Where(x => x.username == username && x.module_id == m_id)
                       join p in _context.Sub_Modules on a.sub_module_id equals p.sub_module_id
                       select new
                       {
                           sub_module_id = a.sub_module_id,
                           sub_module_name = p.sub_module_name,
                       };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<object> access_right_sub_module_bind(string username, string m_id)
        {
            try
            {
                return ((from a in _context.Sub_Modules.Where(x => x.module_id == m_id)

                         select new
                         {
                             sub_module_id = a.sub_module_id,
                             sub_module_name = a.sub_module_name,
                         }).
                         Except(from a in _context.accessRights.Where(x => x.module_id == m_id && x.username == username)
                                join p in _context.Sub_Modules on a.sub_module_id equals p.sub_module_id
                                select new
                                {
                                    sub_module_id = a.sub_module_id,
                                    sub_module_name = p.sub_module_name,
                                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<userregistration> GetAllUser()
        {
            try
            {
                return _context.Userregistrations.ToList<userregistration>().OrderByDescending(x => x.created_date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<object> Get_sites_under_contractor(string ref_id)
        {
            try
            {
                if (ref_id == "0")
                {
                    return from a in _context.Userregistrations.Where(x => x.is_active == true).OrderByDescending(x => x.created_date)
                           join m in _context.user_Roles.Where(x => x.is_active == true) on a.role_id equals m.role_id
                           select new
                           {
                               user_id = a.user_id,
                               username = a.username,
                               name = a.name,
                               mobile_no = a.mobile_no,
                               email = a.email,
                               address = a.address,
                               last_loging = a.last_loging,
                               created_by = a.created_by,
                               role_id = m.role_id,
                               role_name = m.role_name,
                               ref_id = a.ref_id,
                           };
                }
                else
                {
                    return from a in _context.Userregistrations.Where(x => x.is_active == true).Where(x => x.ref_id == ref_id)
                           join m in _context.user_Roles.Where(x => x.is_active == true) on a.role_id equals m.role_id
                           select new
                           {
                               user_id = a.user_id,
                               username = a.username,
                               name = a.name,
                               mobile_no = a.mobile_no,
                               email = a.email,
                               address = a.address,
                               last_loging = a.last_loging,
                               created_by = a.created_by,
                               role_id = m.role_id,
                               role_name = m.role_name,
                               ref_id = a.ref_id,
                           };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IQueryable<object> profile_details_user(string user_id)
        {
            try
            {
                return from a in _context.Userregistrations.Where(x => x.user_id == user_id).OrderByDescending(x => x.created_date)
                       select new
                       {
                           sno = a.sno,
                           username = a.username,
                           password = a.password,
                           name = a.name,
                           mobile_no = a.mobile_no,
                           email = a.email,
                           address = a.address,
                           last_loging = a.last_loging,
                           is_active = a.is_active,
                           created_by = a.created_by,
                           created_date = a.created_date,
                           updated_by = a.updated_by,
                           updated_date = a.updated_date,
                           role_id = a.role_id,
                           token = a.token,
                           role_name = a.role_name,
                           user_id = a.user_id,
                           ref_id = a.ref_id,
                       };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<object> Get_role_list_by_name(Int32 role_id)
        {
            try
            {
                if (role_id == 0)
                {
                    return from a in _context.Userregistrations.Where(x => x.is_active == true).OrderByDescending(x => x.created_date)
                           join m in _context.user_Roles.Where(x => x.is_active == true) on a.role_id equals m.role_id
                           select new
                           {
                               user_id = a.user_id,
                               username = a.username,
                               name = a.name,
                               mobile_no = a.mobile_no,
                               email = a.email,
                               address = a.address,
                               last_loging = a.last_loging,
                               created_by = a.created_by,
                               role_id = m.role_id,
                               role_name = m.role_name,
                           };
                }
                else
                {
                    return from a in _context.Userregistrations.Where(x => x.is_active == true).Where(x => x.role_id == role_id)
                           join m in _context.user_Roles.Where(x => x.is_active == true) on a.role_id equals m.role_id
                           select new
                           {
                               user_id = a.user_id,
                               username = a.username,
                               name = a.name,
                               mobile_no = a.mobile_no,
                               email = a.email,
                               address = a.address,
                               last_loging = a.last_loging,
                               created_by = a.created_by,
                               role_id = m.role_id,
                               role_name = m.role_name,
                           };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public userregistration GetSingle_user(string username)
        {
            try
            {
                return _context.Userregistrations.FirstOrDefault<userregistration>((Expression<Func<userregistration, bool>>)(x => x.username == username));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void update_userDetails(userregistration usr)
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Add_users(userregistration ur)
        {
            try
            {
                this._context.Userregistrations.Add(ur);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public userregistration GetSingleUser(string username)
        {
            try
            {
                return this._context.Userregistrations.FirstOrDefault<userregistration>((Expression<Func<userregistration, bool>>)(t => t.username == username));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<object> get_sub_module_list(string module_id)
        {
            try
            {
                if (module_id == "0")
                {
                    return from a in _context.Sub_Modules.Where(x => x.is_active == true).OrderByDescending(x => x.created_date)
                           join p in _context.modules.Where(x => x.is_active == true) on a.module_id equals p.module_id
                           select new
                           {
                               sub_module_id = a.sub_module_id,
                               module_name = p.module_name,
                               sub_module_name = a.sub_module_name,
                               module_id = a.module_id,
                           };
                }
                else
                {
                    return from a in _context.Sub_Modules.Where(x => x.module_id == module_id && x.is_active == true)
                           join p in _context.modules.Where(x => x.is_active == true) on a.module_id equals p.module_id
                           select new
                           {
                               sub_module_id = a.sub_module_id,
                               module_name = p.module_name,
                               sub_module_name = a.sub_module_name,
                               module_id = a.module_id,
                           };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<sub_module> GetAll_sub_module_id(string module_id)
        {
            try
            {
                return this._context.Sub_Modules.Where<sub_module>((Expression<Func<sub_module, bool>>)(t => t.module_id == module_id)).ToList<sub_module>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Get_srno_max_sub_module()
        {
            try
            {
                var max = _context.Sub_Modules.DefaultIfEmpty().Max(r => r == null ? 0 : r.srno);
                return max;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void add_sub_module(sub_module sub_mod)
        {
            try
            {
                this._context.Sub_Modules.Add(sub_mod);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void update_sub_module_new(sub_module mod)
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public sub_module GetSingle_sub_module(string sub_module_id)
        {
            try
            {
                return _context.Sub_Modules.FirstOrDefault<sub_module>((Expression<Func<sub_module, bool>>)(x => x.sub_module_id == sub_module_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Get_srno_max_users()
        {
            try
            {
                var max = _context.Userregistrations.DefaultIfEmpty().Max(r => r == null ? 0 : r.sno);
                return max;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<module> Single_module_by_id(string module_id)
        {
            try
            {
                return this._context.modules.Where<module>((Expression<Func<module, bool>>)(t => t.module_id == module_id)).ToList<module>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
