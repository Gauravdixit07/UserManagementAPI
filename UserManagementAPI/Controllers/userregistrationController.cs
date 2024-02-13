
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.DataAccess;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{

    [Route("api/{controller}/{action}")]
    [ApiController]
    public class userregistrationController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

      

        public userregistrationController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                IEnumerable<userregistration> allUser = (IEnumerable<userregistration>)(IEnumerable<userregistration>)this._dataAccessProvider.GetAllUser();
                return allUser == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)allUser);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }

        [HttpPost]
        public IActionResult Create_user([FromBody] userregistration usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //string reciept_no = null;
                    int i = this._dataAccessProvider.Get_srno_max_users();
                    if (i != null)
                    {
                        i = i + 1;

                        int no = i;
                        string upass = Password.Encrypt(usr.password);

                        usr.sno = no;
                        usr.user_id = no.ToString();
                        usr.password = upass;
                        usr.last_loging = DateTime.Now;

                        usr.is_active = true;

                        usr.created_date = DateTime.Now;

                        usr.updated_date = DateTime.Now;

                        usr.updated_by = "NA".ToString();

                        _dataAccessProvider.Add_users(usr);
                        return Ok("Data Save");
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }


        [HttpGet("{username}")]
        public IActionResult GetUsername(string username)
        {
            try
            {
                userregistration singleUser = this._dataAccessProvider.GetSingleUser(username);
                return singleUser == null ? (IActionResult)this.StatusCode(200, (object)"User Name Not Found !!!") : (IActionResult)this.Ok((object)singleUser);
            }
            catch (Exception ex)
            {
                return (IActionResult)this.StatusCode(500, (object)"Internal server error");
            }
        }


        [HttpGet("{role_id}")]
        public ActionResult Get_data_by_name(Int32 role_id)
        {
            try
            {
                IQueryable<Object> role_list = this._dataAccessProvider.Get_role_list_by_name(role_id);
                return role_list == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)role_list);
            }
            catch (Exception ex)
            {
                return (ActionResult)this.StatusCode(500, (object)"Internal server error");
            }
        }


        [HttpGet("{ref_id}")]
        public ActionResult Get_site_data_by_contractor(string ref_id)
        {
            try
            {
                IQueryable<Object> site_list = this._dataAccessProvider.Get_sites_under_contractor(ref_id);
                return site_list == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)site_list);
            }
            catch (Exception ex)
            {
                return (ActionResult)this.StatusCode(500, (object)"Internal server error");
            }
        }

        [HttpGet("{user_id}")]
        public ActionResult Get_user_details_profile(string user_id)
        {
            try
            {
                IQueryable<Object> user_det = this._dataAccessProvider.profile_details_user(user_id);
                return user_det == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)user_det);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }

        [HttpPost]
        public IActionResult user_details_update([FromBody] userregistration usr)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");
                var usr_update = _dataAccessProvider.GetSingle_user(usr.username);
                if (usr_update != null)
                {
                    if (ModelState.IsValid)
                    {
                        {
                            usr.is_active = true;
                            usr.updated_date = System.DateTime.Now;

                            usr_update.name = usr.name;
                            usr_update.email = usr.email;
                            usr_update.mobile_no = usr.mobile_no;
                            usr_update.address = usr.address;
                            _dataAccessProvider.update_userDetails(usr);

                            return Ok("Data Update Successfully");
                        }
                    }
                }
                else
                {
                    return NotFound();
                }
                //return Ok("Data Update Successfully");
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
            return Ok("Data Update Successfully");
        }
    }
}
