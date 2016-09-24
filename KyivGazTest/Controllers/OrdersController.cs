using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.EntityFramework;
using KyivGazTest.Models;

namespace KyivGazTest.Controllers
{
    public class OrdersController : Controller
    {
        private KyivGazTestContext db = new KyivGazTestContext();

        // GET: Orders
        //public async Task<ActionResult> Index()
        //{
        //    var orders = db.Orders.Include(o => o.Manager);
        //    return View(await orders.ToListAsync());
        //}

        // GET: Orders - sorted
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NumberSortParam = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewBag.CreateDateSortParam = sortOrder == "create_date" ? "create_date_desc" : "create_date";
            ViewBag.UpdateDateSortParam = sortOrder == "update_date" ? "update_date_desc" : "update_date";

            // Paging
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            // Get Data
            var orders = from o in db.Orders
                         select o;

            // Search
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.Number.Contains(searchString)
                                       || o.Manager.LastName.Contains(searchString));
            }

            // Ordering
            switch (sortOrder)
            {
                case "number_desc":
                    orders = orders.OrderByDescending(o => o.Number);
                    break;
                case "create_date":
                    orders = orders.OrderBy(o => o.CreateDate);
                    break;
                case "create_date_desc":
                    orders = orders.OrderByDescending(o => o.CreateDate);
                    break;
                case "update_date":
                    orders = orders.OrderBy(o => o.UpdateDate);
                    break;
                case "update_date_desc":
                    orders = orders.OrderByDescending(o => o.UpdateDate);
                    break;
                default:
                    orders = orders.OrderBy(o => o.Number);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(await orders.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "LastName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,Number,ManagerId,CreateDate,UpdateDate,Comment")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "LastName", order.ManagerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "LastName", order.ManagerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,Number,ManagerId,CreateDate,UpdateDate,Comment")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "LastName", order.ManagerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
