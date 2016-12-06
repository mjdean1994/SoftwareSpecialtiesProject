using GATM.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GATM.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            AccountClientService accountClientService = new AccountClientService();
            bool isAuthenticated = accountClientService.Authenticate("NOT_IMPLEMENTED_IN_SIMULATION", "0000");
            if (!isAuthenticated)
            {
                //This should never happen.
                return View(-1);
            }

            float balance = accountClientService.GetAccountBalance();
            accountClientService.Detach();

            return View(balance);
        }

        public ActionResult History()
        {
            AccountClientService accountClientService = new AccountClientService();
            bool isAuthenticated = accountClientService.Authenticate("NOT_IMPLEMENTED_IN_SIMULATION", "0000");
            if (!isAuthenticated)
            {
                //This should never happen.
                return View(-1);
            }

            float balance = accountClientService.GetAccountBalance();
            accountClientService.Detach();

            return View(balance);
        }

        [HttpPost]
        public JsonResult Withdraw(float amount)
        {
            TransactionClientService transactionClientService = new TransactionClientService();
            bool isAuthenticated = transactionClientService.Authenticate("NOT_IMPLEMENTED_IN_SIMULATION", "0000");
            if(!isAuthenticated)
            {
                //Returning null will force the JQuery fail method
                return null;
            }

            bool result = transactionClientService.Withdraw(amount);
            transactionClientService.Detach();

            return Json(new { result });
        }

        [HttpPost]
        public JsonResult Deposit(float amount)
        {
            TransactionClientService transactionClientService = new TransactionClientService();
            bool isAuthenticated = transactionClientService.Authenticate("NOT_IMPLEMENTED_IN_SIMULATION", "0000");
            if (!isAuthenticated)
            {
                //Returning null will force the JQuery fail method
                return null;
            }

            bool result = transactionClientService.Deposit(amount);
            transactionClientService.Detach();

            return Json(new { result });
        }
    }
}