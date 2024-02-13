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
    public class accessRightController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public accessRightController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet("{username}")]
        public ActionResult Get_accessright_id(string username)
        {
            try
            {
                IEnumerable<accessRight> tax_receipt_no = _dataAccessProvider.Get_access_right_for_user(username);

                string abc = "{\"Status\":\"No Data Found...!!!\"}";

                return tax_receipt_no == null ? (ActionResult)this.StatusCode(200, (object)abc) : (ActionResult)this.Ok((object)tax_receipt_no);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }




        [HttpPost]
        public IActionResult Create_access_right_for_user([FromBody] accessRight acc_right)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    acc_right.is_active = true;
                    acc_right.created_on = DateTime.Now;
                    acc_right.updated_on = DateTime.Now;
                    acc_right.updated_by = "NA".ToString();
                    _dataAccessProvider.add_access_right_for_user(acc_right);
                    string str = "Data Save";
                    return Ok(str);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }



        [HttpDelete]
        public IActionResult DeleteConfirmed_accessRight(string username, string module_id)
        {
            try
            {
                string error_msg = "{\"Status\":\"User Name Not Found !!!\"}";
                string success = "{\"Status\":\"Record Deleted Succufully.\"}";

                if (this._dataAccessProvider.Get_Delete_access_right(username, module_id) == null)

                    return (IActionResult)this.StatusCode(200, (object)error_msg);
                this._dataAccessProvider.Delete_access_right(username, module_id);
                return (IActionResult)this.Ok((object)success);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }



        [HttpGet("{username}")]
        public ActionResult Get_all_accessRight_userwise(string username)
        {
            try
            {
                IQueryable<Object> access_right_list = this._dataAccessProvider.Get_access_right_grid_for_user(username);
                return access_right_list == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)access_right_list);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }



        [HttpGet("{username}/{m_id}")]
        public ActionResult Get_user_sub_module_access_right(string username, string m_id)
        {
            try
            {
                IQueryable<Object> access_right_list_sub_module = this._dataAccessProvider.user_access_right_sm(username, m_id);
                return access_right_list_sub_module == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)access_right_list_sub_module);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }

        [HttpGet("{username}/{m_id}")]
        public ActionResult Bind_sub_module_access_right(string username, string m_id)
        {
            try
            {
                IQueryable<Object> access_right_list_sub_module = this._dataAccessProvider.access_right_sub_module_bind(username, m_id);
                return access_right_list_sub_module == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)access_right_list_sub_module);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }


        [HttpGet("{username}/{m_id}")]
        public ActionResult Get_user_sub_module_not_access_right(string username, string m_id)
        {
            try
            {
                IQueryable<Object> not_access_right_list_sub_module = this._dataAccessProvider.user_not_access_right_sm(username, m_id);
                return not_access_right_list_sub_module == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)not_access_right_list_sub_module);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }




        [HttpGet]
        //public IActionResult DeleteConfirmed_access_right(string module_id, string username)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest("Not a valid model");
        //        var acc_rht = _dataAccessProvider.delete_access_right_id(module_id, username);
        //        if (acc_rht != null)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                {
        //                    _dataAccessProvider.delete_access_right(acc_rht);

        //                    return Ok("Role Deleted Successfully");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //        return Ok("Data Update Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
        //        return (ActionResult)this.StatusCode(200, error);
        //    }
        //}


        public IActionResult DeleteConfirmed_access_right(string module_id, string username)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid model");

                _dataAccessProvider.Delete_access_right(username, module_id);

                return Ok("Data Deleted Successfully");
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }

    }
}
