using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Experimental.Models;

namespace Experimental.Controllers
{
    public class PaaMembersController : Controller
    {
        private ExperimentalContext db = new ExperimentalContext();

        // GET: PaaMembers
        public ActionResult Index()
        {
            return View(db.PaaMembers.ToList());
        }

        // GET: PaaMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaaMember paaMember = db.PaaMembers.Find(id);
            if (paaMember == null)
            {
                return HttpNotFound();
            }
            return View(paaMember);
        }

        // GET: PaaMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaaMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaaMemberId,Email,FirstName,LastName,Company,StreetAddress,StreetAddress2,City,State,ZipCode,Url,ArtWork,FamilyMemberEmail,NonPayingMember,HomePageApproval,Phone,Renewal")] PaaMember paaMember)
        {
            if (ModelState.IsValid)
            {
                db.PaaMembers.Add(paaMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paaMember);
        }

        // GET: PaaMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaaMember paaMember = db.PaaMembers.Find(id);
            if (paaMember == null)
            {
                return HttpNotFound();
            }
            return View(paaMember);
        }

        // POST: PaaMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaaMemberId,Email,FirstName,LastName,Company,StreetAddress,StreetAddress2,City,State,ZipCode,Url,ArtWork,FamilyMemberEmail,NonPayingMember,HomePageApproval,Phone,Renewal")] PaaMember paaMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paaMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paaMember);
        }

        // GET: PaaMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaaMember paaMember = db.PaaMembers.Find(id);
            if (paaMember == null)
            {
                return HttpNotFound();
            }
            return View(paaMember);
        }

        // POST: PaaMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaaMember paaMember = db.PaaMembers.Find(id);
            db.PaaMembers.Remove(paaMember);
            db.SaveChanges();
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
