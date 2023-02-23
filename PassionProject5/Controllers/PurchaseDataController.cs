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
using PassionProject5.Models;

namespace PassionProject5.Controllers
{
    public class PurchaseDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PurchaseData/ListPurchase
        [HttpGet]
        [ResponseType(typeof(PurchaseDto))]
        public IHttpActionResult ListPurchases()
        {
            List<Purchase> Purchases = db.Purchases.ToList();
            List<PurchaseDto> PurchaseDtos = new List<PurchaseDto>();

            Purchases.ForEach(a => PurchaseDtos.Add(new PurchaseDto()
            {
                PurchaseID = a.PurchaseID,
                PurchaseNum = a.PurchaseNum,
                ItemName = a.Item.ItemName
            }));
            return Ok(PurchaseDtos);
        }

        [HttpGet]
        [ResponseType(typeof(PurchaseDto))]
        public IHttpActionResult ListPurchasesForItem(int id)
        {
            List<Purchase> Purchases = db.Purchases.Where(a => a.ItemID == id).ToList();
            List<PurchaseDto> PurchaseDtos = new List<PurchaseDto>();

            Purchases.ForEach(a => PurchaseDtos.Add(new PurchaseDto()
            {
                PurchaseID = a.PurchaseID,
                PurchaseNum = a.PurchaseNum,
                ItemName = a.Item.ItemName
            }));
            return Ok(PurchaseDtos);
        }

        // GET: api/PurchaseData/FindPurchase/5
        [ResponseType(typeof(Purchase))]
        [HttpGet]
        public IHttpActionResult FindPurchase(int id)
        {
            Purchase Purchase = db.Purchases.Find(id);
            PurchaseDto PurchaseDto = new PurchaseDto()
            {
                PurchaseID = Purchase.PurchaseID,
                PurchaseNum = Purchase.PurchaseNum,
                ItemName = Purchase.Item.ItemName
            };
            if (Purchase == null)
            {
                return NotFound();
            }

            return Ok(PurchaseDto);
        }

        // PUT: api/PurchaseData/UpdatePurchase/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.PurchaseID)
            {
                return BadRequest();
            }

            db.Entry(purchase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/PurchaseData/AddPurchase
        [ResponseType(typeof(Purchase))]
        [HttpPost]
        public IHttpActionResult AddPurchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Purchases.Add(purchase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase.PurchaseID }, purchase);
        }

        // DELETE: api/PurchaseData/DeletePurchase/5
        [ResponseType(typeof(Purchase))]
        [HttpPost]
        public IHttpActionResult DeletePurchase(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }

            db.Purchases.Remove(purchase);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return db.Purchases.Count(e => e.PurchaseID == id) > 0;
        }
    }
}