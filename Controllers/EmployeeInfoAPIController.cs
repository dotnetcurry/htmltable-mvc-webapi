using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MVC5_Editable_Table.Models;

namespace MVC5_Editable_Table.Controllers
{
    public class EmployeeInfoAPIController : ApiController
    {
        private ApplicationEntities db = new ApplicationEntities();

        // GET api/EmployeeInfoAPI
        public IQueryable<EmployeeInfo> GetEmployeeInfoes()
        {
            return db.EmployeeInfoes;
        }

        // GET api/EmployeeInfoAPI/5
        [ResponseType(typeof(EmployeeInfo))]
        public IHttpActionResult GetEmployeeInfo(int id)
        {
            EmployeeInfo employeeinfo = db.EmployeeInfoes.Find(id);
            if (employeeinfo == null)
            {
                return NotFound();
            }

            return Ok(employeeinfo);
        }

        // PUT api/EmployeeInfoAPI/5
        public IHttpActionResult PutEmployeeInfo(int id, EmployeeInfo employeeinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeinfo.EmpNo)
            {
                return BadRequest();
            }

            db.Entry(employeeinfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/EmployeeInfoAPI
        [ResponseType(typeof(EmployeeInfo))]
        public IHttpActionResult PostEmployeeInfo(EmployeeInfo employeeinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeInfoes.Add(employeeinfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeinfo.EmpNo }, employeeinfo);
        }

        // DELETE api/EmployeeInfoAPI/5
        [ResponseType(typeof(EmployeeInfo))]
        public IHttpActionResult DeleteEmployeeInfo(int id)
        {
            EmployeeInfo employeeinfo = db.EmployeeInfoes.Find(id);
            if (employeeinfo == null)
            {
                return NotFound();
            }

            db.EmployeeInfoes.Remove(employeeinfo);
            db.SaveChanges();

            return Ok(employeeinfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeInfoExists(int id)
        {
            return db.EmployeeInfoes.Count(e => e.EmpNo == id) > 0;
        }
    }
}