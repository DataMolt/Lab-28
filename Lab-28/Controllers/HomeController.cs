using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab_28.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Lab_28.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public async Task<IActionResult> Index()
        {
            if (_session.GetString("deck_id") == null)
            {
                return RedirectToAction("CreateNewDeck");
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");
            var deckId = _session.GetString("deck_id");

            var response = await client.GetAsync($"api/deck/{deckId}/draw/?count=5");
            var content = await response.Content.ReadAsAsync<Draw>();
            return View(content);
        }

        public async Task<IActionResult> CreateNewDeck()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");

            var response = await client.GetAsync("api/deck/new/");
            var content = await response.Content.ReadAsAsync<Draw>();

            _session.SetString("deck_id", content.deck_id);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
