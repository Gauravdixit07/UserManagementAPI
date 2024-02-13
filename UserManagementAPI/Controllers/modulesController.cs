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
    public class modulesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public modulesController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public ActionResult Get_all_module_list()
        {
            try
            {
                IEnumerable<module> allcenter_lists = this._dataAccessProvider.get_module_list();
                return allcenter_lists == null ? (ActionResult)this.NotFound((object)"Data Not found") : (ActionResult)this.Ok((object)allcenter_lists);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }

        [HttpPost]
        public IActionResult Create_module([FromBody] module mod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string receipt_no = null;
                    int i = this._dataAccessProvider.Get_srno_max_module();
                    if (i != null)
                    {
                        i = i + 1;
                        int no = i;

                        string abcc = "m_" + i;
                        mod.srno = i;
                        mod.module_id = abcc;
                        mod.is_active = true;
                        mod.created_date = DateTime.Now;
                        mod.updated_date = DateTime.Now;
                        mod.updated_by = "NA".ToString();
                        _dataAccessProvider.add_module(mod);
                        string str = "{\"Status\":\"Data Save\",\"Receipt No\":\"" + abcc + "\"}";
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
        public IActionResult module_update([FromBody] module mod)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            var single_center = _dataAccessProvider.GetSingle_module(mod.module_id);
            if (single_center != null)
            {
                if (ModelState.IsValid)
                {
                    {
                        mod.is_active = true;
                        mod.updated_date = System.DateTime.Now;

                        single_center.module_name = mod.module_name;
                        _dataAccessProvider.update_module_new(mod);

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
        public ActionResult module_get_by_id(string module_id)
        {
            try
            {
                IEnumerable<module> center_ = _dataAccessProvider.Single_module_by_id(module_id);
                string abc = "{\"Status\":\"No Data Found...!!!\"}";
                return center_ == null ? (ActionResult)this.StatusCode(200, (object)abc) : (ActionResult)this.Ok((object)center_);
            }
            catch (Exception ex)
            {
                string error = "{\"Status\":\" " + ex.InnerException.Message.Replace('"', ' ').Trim() + " \"}";
                return (ActionResult)this.StatusCode(200, error);
            }
        }
    }
}
