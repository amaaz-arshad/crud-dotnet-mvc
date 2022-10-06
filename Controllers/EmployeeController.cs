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
    public class EmployeeController : Controller
    {
        private crudDBEntities db = new crudDBEntities();

        // This function fetches list of all employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Bank).Include(e => e.Role);
            return View(employees.ToList());
        }

        // This function fetches the details of a specific employee
        public ActionResult EmployeeDetails(int id)
        {
            Employee employee = db.Employees.Find(id);
            return View(employee);
        }

        // Navigate to Employee/Create
        public ActionResult AddEmployee()
        {
            // selectlist used to retrieve list of banks and roles to choose from
            ViewBag.bank_id = new SelectList(db.Banks, "bank_id", "bank_name");
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "role_name");
            return View();
        }

        // This function adds a new employee
        [HttpPost]
        public ActionResult AddEmployee([Bind(Include = "employee_id,employee_name,role_id,salary,address,joining_date,account_no,bank_id")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.bank_id = new SelectList(db.Banks, "bank_id", "bank_name", employee.bank_id);
                ViewBag.role_id = new SelectList(db.Roles, "role_id", "role_name", employee.role_id);
                return View(employee);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // This function is used to fetch the employee which is to be edited
        public ActionResult EditEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            ViewBag.bank_id = new SelectList(db.Banks, "bank_id", "bank_name", employee.bank_id);
            ViewBag.role_id = new SelectList(db.Roles, "role_id", "role_name", employee.role_id);
            return View(employee);
        }

        // This function edits the selected employee
        [HttpPost]
        public ActionResult EditEmployee([Bind(Include = "employee_id,employee_name,role_id,salary,address,joining_date,account_no,bank_id")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.bank_id = new SelectList(db.Banks, "bank_id", "bank_name", employee.bank_id);
                ViewBag.role_id = new SelectList(db.Roles, "role_id", "role_name", employee.role_id);
                return View(employee);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // This function fetches the employee which is to be deleted
        public ActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            return View(employee);
        }

        // This function deletes the selected employee
        [HttpPost, ActionName("DeleteEmployee")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
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
