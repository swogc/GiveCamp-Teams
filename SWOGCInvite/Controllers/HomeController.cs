using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SWOGCInvite.Models;
using SWOGCInvite.Services;

namespace SWOGCInvite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInvitationService _invitationService;

        public HomeController(
            ILogger<HomeController> logger,
            IInvitationService invitationService)
        {
            _logger = logger;
            _invitationService = invitationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewInvitationModel newInvitation, CancellationToken token)
        {
            var result = await _invitationService.CreateInvitation(newInvitation.EmailAddress, token);
            if (result.Success)
            {
                return View("Success");
            }

            return View("Error");
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
