using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserManagementAPI.DataAccess;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{

    [Route("api/{controller}/{action}")]
    [ApiController]
    public class user_roleController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        public user_roleController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public ActionResult Get_all_roles()
        {
            try
            {
                IEnumerable<user_role> allUser_roles = this._dataAccessProvider.GetAllRoles();
                return allUser_roles == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)allUser_roles);
            }
            catch (Exception ex)
            {
                return (ActionResult)this.StatusCode(500, (object)"Internal server error");
            }
        }

        [HttpGet]
        public ActionResult Get_all_roles_except_admin()
        {
            try
            {
                IEnumerable<user_role> allUser_roles = this._dataAccessProvider.GetAllRoles_except_admin();
                return allUser_roles == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)allUser_roles);
            }
            catch (Exception ex)
            {
                return (ActionResult)this.StatusCode(500, (object)"Internal server error");
            }
        }
        
        [HttpPost]
        public IActionResult Create_role([FromBody] user_role ur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        ur.is_active = true;
                        ur.created_date = DateTime.Now;
                        ur.updated_date = DateTime.Now;
                        ur.updated_by = "NA".ToString();
                        _dataAccessProvider.add_roles(ur);
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

        [HttpPost]
        public IActionResult role_update([FromBody] user_role ur)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            var singleUser = _dataAccessProvider.GetSingleRole(ur.role_id);
            if (singleUser != null)
            {
                if (ModelState.IsValid)
                {
                    {
                        ur.is_active = true;
                        ur.updated_date = System.DateTime.Now;

                        singleUser.role_name = ur.role_name;
                        _dataAccessProvider.update_roles(ur);

                        return Ok("Data Update Successfully");
                    }
                }

            }
            else
            {
                return NotFound();
            }
            return Ok("Data Update Successfully");
        }

        [HttpGet]
        public ActionResult Get_role_by_ID(int role_id)
        {
            try
            {
                IEnumerable<user_role> roles = _dataAccessProvider.user_role_by_id(role_id);


                string abc = "{\"Status\":\"No Data Found...!!!\"}";


                return roles == null ? (ActionResult)this.StatusCode(200, (object)abc) : (ActionResult)this.Ok((object)roles);
            }
            catch (Exception ex)
            {
                return (ActionResult)this.StatusCode(500, (object)"Internal server error");
            }

        }

        [HttpGet]
        public IActionResult DeleteConfirmed_roles(int role_id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            var singleUser = _dataAccessProvider.GetSingleRole(role_id);
            if (singleUser != null)
            {
                if (ModelState.IsValid)
                {
                    {
                        singleUser.is_active = false;
                        singleUser.updated_date = System.DateTime.Now;

                        //singleUser.is_active = url.is_active;
                        _dataAccessProvider.Delete_roles(singleUser);

                        return Ok("Role Deleted Successfully");
                    }
                }

            }
            else
            {
                return NotFound();
            }
            return Ok("Data Update Successfully");
        }

    }
}
