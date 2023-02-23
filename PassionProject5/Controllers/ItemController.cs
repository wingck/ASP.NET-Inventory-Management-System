using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PassionProject5.Models;
using System.Web.Script.Serialization;
using PassionProject5.Migrations;
using PassionProject5.Models.ViewModels;

namespace PassionProject5.Controllers
{

    public class ItemController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ItemController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44399/api/");
        }

        // GET: Item/List
        public ActionResult List()
        {
            
            string url = "itemdata/listitems";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ItemDto> Item = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;

            return View(Item);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            DetailsItem ViewModel = new DetailsItem();
            string url = "itemdata/finditem/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ItemDto SelectedItem = response.Content.ReadAsAsync<ItemDto>().Result;

            ViewModel.SelectedItem = SelectedItem;

            url = "purchasedata/listpurchasesforitem/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PurchaseDto> RelatedPurchases = response.Content.ReadAsAsync<IEnumerable<PurchaseDto>>().Result;
            ViewModel.RelatedPurchases = RelatedPurchases;
            return View(ViewModel);
        }
        public ActionResult Error() {
            return View();
        }

        // GET: Item/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(Item item)
        {
            string url = "itemdata/additem";
            string jsonpayload = jss.Serialize(item);


            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("List");
            }
            else {
                return RedirectToAction("Error");
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateItem ViewModel = new UpdateItem();
            string url = "itemdata/finditem/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ItemDto SelectedItem = response.Content.ReadAsAsync<ItemDto>().Result;
            ViewModel.SelectedItem = SelectedItem;

            return View(ViewModel);
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Item item)
        {
            string url = "itemdata/updateitem/" + id;
            string jsonpayload = jss.Serialize(item);


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

        // GET: Item/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "itemdata/finditem/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ItemDto SelectedItem = response.Content.ReadAsAsync<ItemDto>().Result;

            return View(SelectedItem);
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "itemdata/deleteitem/" + id;


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
