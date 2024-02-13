using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.DataAccess;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{

    [Route("api/{controller}/{action}")]
    [ApiController]
    public class sub_moduleController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public sub_moduleController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }





        [HttpGet]
        public ActionResult Get_all_sub_module_list(string module_id)
        {
            try
            {
                IQueryable<Object> sub_module_list = this._dataAccessProvider.get_sub_module_list(module_id);
                return sub_module_list == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)sub_module_list);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }


        [HttpGet("{module_id}")]
        public ActionResult Getsub_module_list_by_ID(string module_id)
        {
            try
            {
                IEnumerable<sub_module> s_module = _dataAccessProvider.GetAll_sub_module_id(module_id);
                return s_module == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)s_module);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }

        [HttpPost]
        public IActionResult Create_sub_module([FromBody] sub_module sub_mod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string receipt_no = null;
                    int i = this._dataAccessProvider.Get_srno_max_sub_module();
                    if (i != null)
                    {
                        i = i + 1;
                        int no = i;
                        //DateTime dtm = System.DateTime.Now;
                        //string tax_rec = dtm.ToString("dd-MM-yyyy");
                        string abcc = "sm_" + i;



                        sub_mod.srno = i;
                        sub_mod.sub_module_id = abcc;
                        sub_mod.is_active = true;
                        sub_mod.created_date = DateTime.Now;
                        sub_mod.updated_date = DateTime.Now;
                        sub_mod.updated_by = "NA".ToString();
                        _dataAccessProvider.add_sub_module(sub_mod);
                        string str = "Data Save";
                        return Ok(str);
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
        public IActionResult sub_module_update([FromBody] sub_module sub_mod)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            var single_sub_module = _dataAccessProvider.GetSingle_sub_module(sub_mod.sub_module_id);
            if (single_sub_module != null)
            {
                if (ModelState.IsValid)
                {
                    {
                        sub_mod.is_active = true;
                        sub_mod.updated_date = System.DateTime.Now;

                        single_sub_module.sub_module_name = sub_mod.sub_module_name;
                        _dataAccessProvider.update_sub_module_new(sub_mod);

                        return Ok("Sub Module Update Successfully");
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
        public ActionResult sub_module_get_by_id(string sub_module_id)
        {
            try
            {
                IEnumerable<sub_module> sub_m = _dataAccessProvider.Single_sub_module_by_id(sub_module_id);
                string abc = "{\"Status\":\"No Data Found...!!!\"}";
                return sub_m == null ? (ActionResult)this.StatusCode(200, (object)abc) : (ActionResult)this.Ok((object)sub_m);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }
    }
}
