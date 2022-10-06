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
    public class BankController : Controller
    {
        private crudDBEntities db = new crudDBEntities();

        // This function fetches list of all banks
        public ActionResult Index()
        {
            return View(db.Banks.ToList());
        }

        // This function fetches the details of a specific bank
        public ActionResult BankDetails(int id)
        {
            Bank bank = db.Banks.Find(id);
            return View(bank);
        }

        // Navigate to Bank/Create
        public ActionResult AddBank()
        {
            return View();
        }

        // This function adds a new bank
        [HttpPost]
        public ActionResult AddBank([Bind(Include = "bank_id,bank_name")] Bank bank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Banks.Add(bank);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(bank);
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }

        // This function is used to fetch the bank which is to be edited
        public ActionResult EditBank(int id)
        {
            Bank bank = db.Banks.Find(id);
            return View(bank);
        }

        // This function edits the selected bank
        [HttpPost]
        public ActionResult EditBank([Bind(Include = "bank_id,bank_name")] Bank bank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(bank).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(bank);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        // This function fetches the bank which is to be deleted
        public ActionResult DeleteBank(int id)
        {
            Bank bank = db.Banks.Find(id);
            return View(bank);
        }

        // This function deletes the selected bank
        [HttpPost, ActionName("DeleteBank")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Bank bank = db.Banks.Find(id);
                db.Banks.Remove(bank);
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
