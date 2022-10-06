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
    public class DepartmentController : Controller
    {
        private crudDBEntities db = new crudDBEntities();

        // This function fetches list of all departments
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.Employee);
            return View(departments.ToList());
        }

        // This function fetches the details of a specific department
        public ActionResult DepartmentDetails(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        // Navigate to Department/Create
        public ActionResult AddDepartment()
        {
            // selectlist used to retrieve list of employees to choose from
            ViewBag.manager_id = new SelectList(db.Employees, "employee_id", "employee_name");
            return View();
        }

        // This function adds a new department
        [HttpPost]
        public ActionResult AddDepartment([Bind(Include = "dept_id,dept_name,manager_id")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.err = "Multiple departments cannot have same manager";
            }
            ViewBag.manager_id = new SelectList(db.Employees, "employee_id", "employee_name", department.manager_id);
            return View(department);
            
            
        }

        // This function is used to fetch the department which is to be edited
        public ActionResult EditDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            ViewBag.manager_id = new SelectList(db.Employees, "employee_id", "employee_name", department.manager_id);
            return View(department);
        }

        // This function edits the selected department
        [HttpPost]
        public ActionResult EditDepartment([Bind(Include = "dept_id,dept_name,manager_id")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(department).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.err = "Multiple departments cannot have same manager";
            }
            ViewBag.manager_id = new SelectList(db.Employees, "employee_id", "employee_name", department.manager_id);
            return View(department);
            
        }

        // This function fetches the department which is to be deleted
        public ActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        // This function deletes the selected department
        [HttpPost, ActionName("DeleteDepartment")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Department department = db.Departments.Find(id);
                db.Departments.Remove(department);
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
