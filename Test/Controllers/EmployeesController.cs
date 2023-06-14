using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models.Database.master;
using Test.Models.Employees;

namespace Test.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly masterContext _masterContext;

        public EmployeesController(masterContext masterContext)
        {
            _masterContext = masterContext;
        }

        // GET: EmployeesController
        public ActionResult Index()
        {
            var rows = _masterContext.Employees
                .AsNoTracking()
                .Select(x => new Employees_IndexModel()
                {
                    EmployeeID = x.EmployeeId,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    TitleOfCourtesy = x.TitleOfCourtesy,
                    Extension = x.Extension
                }).ToList();
            return View(rows);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            var row = _masterContext.Employees
                .AsNoTracking()
                .Where(x => x.EmployeeId == id)
                .Select(x => new Employees_DetailModel()
                {
                    EmployeeID = x.EmployeeId,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    TitleOfCourtesy = x.TitleOfCourtesy,
                    BirthDate = x.BirthDate.ToString(),
                    HireDate = x.HireDate.ToString(),
                    Address = x.Address,
                    City = x.City,
                    Region = x.Region,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    HomePhone = x.HomePhone,
                    Extension = x.Extension,
                    Notes = x.Notes,
                    ReportsToIndex = x.ReportsTo
                }).FirstOrDefault();
            if (row != null)
            {
                var rows = _masterContext.Employees
                    .AsNoTracking()
                    .ToList();

                var leader = rows.Where(x => x.EmployeeId == row.ReportsToIndex).FirstOrDefault();
                if (leader != null)
                    row.ReportsTo = leader.TitleOfCourtesy + " " + leader.LastName;

                return View(row);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            var reportsToList = _masterContext.Employees
                   .AsNoTracking()
                   .Select(x => new SelectListItem()
                   {
                       Text = x.TitleOfCourtesy + " " + x.LastName,
                       Value = x.EmployeeId.ToString()
                   })
                   .ToList();
            reportsToList.Insert(0, new SelectListItem()
            {
                Text = "無",
                Value = null
            });

            var titleOfCourtesyList = new List<SelectListItem>() {
                new SelectListItem(){
                    Text = "Dr.",
                    Value = "Dr."
                },
                new SelectListItem(){
                    Text = "Ms.",
                    Value = "Ms."
                },
                new SelectListItem(){
                    Text = "Mr.",
                    Value = "Mr."
                },
                new SelectListItem(){
                    Text = "Mrs.",
                    Value = "Mrs."
                }
            };

            return View(new Employees_CreateModel()
            {
                ReportsToList = reportsToList,
                TitleOfCourtesyList = titleOfCourtesyList
            });
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees_CreateModel model)
        {
            try
            {
                int? reportsTo = null;
                if (model.ReportsTo != null)
                    reportsTo = int.Parse(model.ReportsTo);

                _masterContext.Employees.Add(new Employees()
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Title = model.Title,
                    TitleOfCourtesy = model.TitleOfCourtesy,
                    BirthDate = model.BirthDate,
                    HireDate = model.HireDate,
                    Address = model.Address,
                    City = model.City,
                    Region = model.Region,
                    PostalCode = model.PostalCode,
                    Country = model.Country,
                    HomePhone = model.HomePhone,
                    Extension = model.Extension,
                    Notes = model.Notes,
                    ReportsTo = reportsTo
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            var row = _masterContext.Employees
                .AsNoTracking()
                .Where(x => x.EmployeeId == id)
                .Select(x => new Employees_EditModel()
                {
                    EmployeeID = x.EmployeeId,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    TitleOfCourtesy = x.TitleOfCourtesy,
                    BirthDate = x.BirthDate ?? DateTime.MinValue,
                    HireDate = x.HireDate ?? DateTime.MinValue,
                    Address = x.Address,
                    City = x.City,
                    Region = x.Region,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    HomePhone = x.HomePhone,
                    Extension = x.Extension,
                    Notes = x.Notes,
                    ReportsTo = x.ReportsTo.ToString()
                }).FirstOrDefault();
            if (row != null)
            {
                var reportsToList = _masterContext.Employees
                     .AsNoTracking()
                     .Select(x => new SelectListItem()
                     {
                         Text = x.TitleOfCourtesy + " " + x.LastName,
                         Value = x.EmployeeId.ToString()
                     })
                     .ToList();
                reportsToList.Insert(0, new SelectListItem()
                {
                    Text = "無",
                    Value = null
                });

                var titleOfCourtesyList = new List<SelectListItem>() {
                        new SelectListItem(){
                            Text = "Dr.",
                            Value = "Dr."
                        },
                        new SelectListItem(){
                            Text = "Ms.",
                            Value = "Ms."
                        },
                        new SelectListItem(){
                            Text = "Mr.",
                            Value = "Mr."
                        },
                        new SelectListItem(){
                            Text = "Mrs.",
                            Value = "Mrs."
                        }
                };
                row.TitleOfCourtesyList = titleOfCourtesyList;
                row.ReportsToList = reportsToList;
                return View(row);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employees_EditModel model)
        {
            try
            {
                int? reportsTo = null;
                if (model.ReportsTo != null)
                    reportsTo = int.Parse(model.ReportsTo);
                var row = _masterContext.Employees
                    .Where(x => x.EmployeeId == model.EmployeeID)
                    .FirstOrDefault();
                if (row != null)
                {
                    row.LastName = model.LastName;
                    row.FirstName = model.FirstName;
                    row.Title = model.Title;
                    row.TitleOfCourtesy = model.TitleOfCourtesy;
                    row.BirthDate = model.BirthDate;
                    row.HireDate = model.HireDate;
                    row.Address = model.Address;
                    row.City = model.City;
                    row.Region = model.Region;
                    row.PostalCode = model.PostalCode;
                    row.Country = model.Country;
                    row.HomePhone = model.HomePhone;
                    row.Extension = model.Extension;
                    row.Notes = model.Notes;
                    row.ReportsTo = reportsTo;
                    _masterContext.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
