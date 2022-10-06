using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeCRUD.Models;

namespace EmployeeCRUD.Controllers
{
    public class RoleController : Controller
    {
        private crudDBEntities db = new crudDBEntities();

        // This function fetches list of all employee roles available
        public ActionResult Index()
        {
            var roles = db.Roles.Include(r => r.Department);
            return View(roles.ToList());
        }

        // This function fetches a specific role
        public ActionResult RoleDetails(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        // Navigate to Role/Create
        public ActionResult AddRole()
        {
            // selectlist used to retrieve list of departments to choose from
            ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name");
            return View();
        }

        // This function adds a new role
        [HttpPost]
        public ActionResult AddRole([Bind(Include = "role_id,role_name,dept_id")] Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name", role.dept_id);
                return View(role);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // This function is used to fetch the role which is to be edited
        public ActionResult EditRole(int id)
        {
            Role role = db.Roles.Find(id);
            ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name", role.dept_id);
            return View(role);
        }

        // This function edits the selected role
        [HttpPost]
        public ActionResult EditRole([Bind(Include = "role_id,role_name,dept_id")] Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name", role.dept_id);
                return View(role);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // This function fetches the role which is to be deleted
        public ActionResult DeleteRole(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        // This function deletes the selected role
        [HttpPost, ActionName("DeleteRole")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Role role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

    }
}
