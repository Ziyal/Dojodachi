using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("")]
        
        public IActionResult Index(){

            if(HttpContext.Session.GetObjectFromJson<Dojodachi>("DojodachiStats") == null) {
                HttpContext.Session.SetObjectAsJson("DojodachiStats", new Dojodachi());
            }
            ViewBag.Dojodachi = HttpContext.Session.GetObjectFromJson<Dojodachi>("DojodachiStats");
            
            if (ViewBag.Dojodachi.Happiness <= 0 || ViewBag.Dojodachi.Fullness <= 0) {
                ViewBag.Dojodachi.Alive = false;
                ViewBag.Dojodachi.Playing = false;
                ViewBag.Dojodachi.Image = "sad.jpeg";
                TempData["ReturnText"] = "You Dojodachi is dead. :(";
            }

            if (ViewBag.Dojodachi.Energy > 99 && ViewBag.Dojodachi.Happiness > 99 && ViewBag.Dojodachi.Fullness > 99) {
                ViewBag.Dojodachi.Win = true;
                ViewBag.Dojodachi.Playing = false;
                ViewBag.Dojodachi.Image = "rainbow.png";
                TempData["ReturnText"] = "Hooray, you just won Dojodachi!";
            }
            ViewBag.ReturnText = TempData["ReturnText"];

            return View("Index");
        }


        [HttpPost]
        [Route("/actions")]
        public IActionResult actions(string action) {


            var MyDojodachi = HttpContext.Session.GetObjectFromJson<Dojodachi>("DojodachiStats");
            string FormResponse = action;

            switch(FormResponse){
                case "feed":
                    TempData["ReturnText"] = MyDojodachi.feed();
                    HttpContext.Session.SetObjectAsJson("DojodachiStats", MyDojodachi);

                    break;
                case "play":
                    TempData["ReturnText"] = MyDojodachi.happiness();
                    HttpContext.Session.SetObjectAsJson("DojodachiStats", MyDojodachi);
                    break;
                case "work":
                    TempData["ReturnText"] = MyDojodachi.work();
                    HttpContext.Session.SetObjectAsJson("DojodachiStats", MyDojodachi);
                    break;
                case "sleep":
                    TempData["ReturnText"] = MyDojodachi.sleep();
                    HttpContext.Session.SetObjectAsJson("DojodachiStats", MyDojodachi);
                    break;

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("/reset")]
        public IActionResult reset() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }

    public static class SessionExtensions {
        public static void SetObjectAsJson(this ISession session, string key, object value) {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key) {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}