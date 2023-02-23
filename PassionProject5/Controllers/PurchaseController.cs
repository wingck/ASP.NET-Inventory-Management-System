using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PassionProject5.Migrations;
using PassionProject5.Models;
using PassionProject5.Models.ViewModels;

namespace PassionProject5.Controllers
{
    public class PurchaseController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static PurchaseController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44399/api/");
        }
        // GET: Purchase/List
        public ActionResult List()
        {
            
            string url = "purchasedata/listpurchases";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PurchaseDto> purchases = response.Content.ReadAsAsync<IEnumerable<PurchaseDto>>().Result;

            return View(purchases);
        }

        // GET: Purchase/Details/5
        public ActionResult Details(int id)
        {
            DetailsPurchase ViewModel = new DetailsPurchase();
            string url = "purchasedata/findpurchase/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PurchaseDto SelectedPurchase = response.Content.ReadAsAsync<PurchaseDto>().Result;

            ViewModel.SelectedPurchase = SelectedPurchase;

            return View(ViewModel);
        }
        public ActionResult Error()
        {
            return View();
        }

        // GET: Purchase/New
        public ActionResult New()
        {
            string url = "itemdata/listitems";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<ItemDto> ItemOptions = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;
            return View(ItemOptions);
        }

        // POST: Purchase/Create
        [HttpPost]
        public ActionResult Create(Purchase purchase)
        {
            string url = "purchasedata/addpurchase";
            
            string jsonpayload = jss.Serialize(purchase);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else 
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Purchase/Edit/5
        public ActionResult Edit(int id)
        {
            UpdatePurchase ViewModel = new UpdatePurchase();
            string url = "purchasedata/findpurchase/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PurchaseDto SelectedPurchase = response.Content.ReadAsAsync<PurchaseDto>().Result;
            ViewModel.SelectedPurchase = SelectedPurchase;

            url = "itemdata/listitems/";
            response = client.GetAsync(url).Result;
            IEnumerable<ItemDto> ItemOptions = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;
            ViewModel.ItemOptions = ItemOptions;

            return View(ViewModel);
        }

        // POST: Purchase/Update/5
        [HttpPost]
        public ActionResult Update(int id, Purchase purchase)
        {
            string url = "purchasedata/updatepurchase/" + id;

            string jsonpayload = jss.Serialize(purchase);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Purchase/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "purchasedata/findpurchase/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PurchaseDto selectedpurchase = response.Content.ReadAsAsync<PurchaseDto>().Result;

            return View(selectedpurchase);
        }

        // POST: Purchase/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "purchasedata/deletepurchase/" + id;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
