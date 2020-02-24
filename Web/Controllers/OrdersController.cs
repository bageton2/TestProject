using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Web.Models;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        private IMapper mapper;
        public OrdersController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            HttpResponseMessage respone = GlobalVariables.WebApiClient.GetAsync("Orders").Result;

            IEnumerable<Order> orders = respone.Content.ReadAsAsync<IEnumerable<OrderDTO>>().Result.Select(o => mapper.Map<Order>(o));

            return View(orders);
        }

        public IActionResult Create()
        {
            return View(new Order());
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreateAsync(Order model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            var order = mapper.Map<OrderDTO>(model);

            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Orders", order).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return BadRequest(errorMessage);
            }
        }

        public IActionResult Edit(int id)
        {
            HttpResponseMessage respone = GlobalVariables.WebApiClient.GetAsync("Orders/" + id.ToString()).Result;

            Order order = mapper.Map<Order>(respone.Content.ReadAsAsync<OrderDTO>().Result);

            return View(order);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditAsync(Order model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            var order = mapper.Map<OrderDTO>(model);

            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Orders/" + order.Id, order).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return BadRequest(errorMessage);
            }
        }

        public IActionResult Delete(int id)
        {
            HttpResponseMessage respone = GlobalVariables.WebApiClient.GetAsync("Orders/" + id.ToString()).Result;

            Order order = mapper.Map<Order>(respone.Content.ReadAsAsync<OrderDTO>().Result);

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Order order)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Orders/" + order.Id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return BadRequest(errorMessage);
            }
        }
    }
}
