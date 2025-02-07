﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.DataAccess.Implementation;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
using myshop.Utilities;
using Stripe;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles = SD.AdminRole) ]

    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            IEnumerable<OrderHeader> orderHeaders;
            orderHeaders = _unitOfWork.OrderHeader.GetAll(Includwoed: "ApplicationUser");
            return Json(new { data = orderHeaders });
        }

        public IActionResult Details(int orderid)
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.GitFirstorDefult(u => u.Id == orderid, Includwoed: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(x => x.OrderHeaderId == orderid, Includwoed: "Product")
            };

            return View(orderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetails()
        {
            var orderfromdb = _unitOfWork.OrderHeader.GitFirstorDefult(u => u.Id == OrderVM.OrderHeader.Id);
            orderfromdb.Name = OrderVM.OrderHeader.Name;
            orderfromdb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderfromdb.Address = OrderVM.OrderHeader.Address;
            orderfromdb.City = OrderVM.OrderHeader.City;

            if (OrderVM.OrderHeader.Carrier != null)
            {
                orderfromdb.Carrier = OrderVM.OrderHeader.Carrier;
            }

            if (OrderVM.OrderHeader.TrakcingNumber != null)
            {
                orderfromdb.TrakcingNumber = OrderVM.OrderHeader.TrakcingNumber;
            }

            _unitOfWork.OrderHeader.Update(orderfromdb);
            _unitOfWork.Complete();
            TempData["Update"] = "Item has Updated Successfully";
            return RedirectToAction("Details", "Order", new { orderid = orderfromdb.Id });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartProccess()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.Proccessing, null);
            _unitOfWork.Complete();

            TempData["Update"] = "Order Status has Updated Successfully";
            return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartShip()
        {
            var orderfromdb = _unitOfWork.OrderHeader.GitFirstorDefult(u => u.Id == OrderVM.OrderHeader.Id);
            orderfromdb.TrakcingNumber = OrderVM.OrderHeader.TrakcingNumber;
            orderfromdb.Carrier = OrderVM.OrderHeader.Carrier;
            orderfromdb.OrderStatus = SD.Shipped;
            orderfromdb.ShippingDate = DateTime.Now;

            _unitOfWork.OrderHeader.Update(orderfromdb);
            _unitOfWork.Complete();

            TempData["Update"] = "Order has Shipped Successfully";
            return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder()
        {
            var orderfromdb = _unitOfWork.OrderHeader.GitFirstorDefult(u => u.Id == OrderVM.OrderHeader.Id);
            if (orderfromdb.PaymentStatus == SD.Approve)
            {
                var option = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderfromdb.PaymentIntentId
                };

                var service = new RefundService();
               // Refund refund = service.Create(option);

                _unitOfWork.OrderHeader.UpdateStatus(orderfromdb.Id, SD.Cancelled, SD.Refund);
            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderfromdb.Id, SD.Cancelled, SD.Cancelled);
            }
            _unitOfWork.Complete();

            TempData["Update"] = "Order has Cancelled Successfully";
            return RedirectToAction("Details", "Order", new { orderid = OrderVM.OrderHeader.Id });
        }




    }
}
