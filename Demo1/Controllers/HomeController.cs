using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo1.Models;
using Microsoft.AspNetCore.Authorization;
using Demo.Data;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public HomeController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="admin")]
        public IActionResult Privacy()
        {
            customerRepository.Add(new Customer() { Name = "AMIR" });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    }

