using HotelManagement.Models;
using HotelManagement.Models.Consts;
using HotelManagement.Models.Dtos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class MomoPaymentController : Controller
    {
        // GET: MomoPayment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Payment(int icOfGuest, string nameOfGuest, string addressOfGuest, bool genderOfGuest, int CatOfGuest, float totalCost, string invoiceIDInput)
        {
            //request params need to request to MoMo system
            string endpoint = @"https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = MomoConfig.ParnerCode;
            string accessKey = MomoConfig.AccessKey;
            string serectkey = MomoConfig.SecretKey;

            string orderInfo = "orderInfo";
            //string returnUrl = MomoConfig.ReturnUrl + "?icOfGuest=" + icOfGuest + "&nameOfGuest=" + nameOfGuest + "&addressOfGuest=" + addressOfGuest + "&genderOfGuest=" + genderOfGuest + "&CatOfGuest=" + CatOfGuest + "&totalCost=" + totalCost;
            string returnUrl = MomoConfig.ReturnUrl;
            string notifyUrl = MomoConfig.NotifyUrl;

            string myInvoiceID = Guid.NewGuid().ToString();

            string amount = totalCost.ToString();
            string orderid = myInvoiceID;
            string requestId = Guid.NewGuid().ToString();
            string extraData = icOfGuest + nameOfGuest + addressOfGuest + genderOfGuest + CatOfGuest + totalCost; ;

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyUrl + "&extraData=" +
                extraData;


            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
          
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyUrl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature },
                

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            //System.Diagnostics.Process.Start(jmessage.GetValue("payUrl").ToString());

            var controller = new ManagerController();
            controller.InvoiceOfGuest(icOfGuest, nameOfGuest, addressOfGuest, genderOfGuest, CatOfGuest, totalCost, myInvoiceID);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);

            
            MoMoSecurity crypto = new MoMoSecurity();

            string secretKey = MomoConfig.SecretKey;

            string signature = crypto.signSHA256(param, secretKey);

            if(signature != Request["Signature"].ToString())
            {
                ViewBag.message = "Invalid Information";
                return View();
            }

            if(!Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Payment failed";
            }
            else
            {
                ViewBag.message = "Payment Success";
            }

            //Add Invoice

            //
            return View();
        }

        public JsonResult NotifyUrl()
        {
            string param = "partner_code=" + Request["partner_code"] +
                           "&access_keys=" + Request["access_keys"] +
                           "&amount=" + Request["amount"] +
                           "&order_id=" + Request["order_id"] +
                           "&order_info=" + Request["order_info"] +
                           "&order_type=" + Request["order_type"] +
                           "&transaction_id=" + Request["transaction_id"] +
                           "&message=" + Request["message"] +
                           "&response_time=" + Request["response_time"] +
                           "&status_code=" + Request["status_code"];

            param = Server.UrlDecode(param);
            MoMoSecurity scryto = new MoMoSecurity();
            string secretKey = MomoConfig.SecretKey;
            string signature = scryto.signSHA256(param, secretKey);
            string statusCode = Request["status_code"].ToString();

            /*
            if (signature != Request["Signature"].ToString())
            {
                ViewBag.message = "Invalid Information";
                return ;
            }
            */

            if (statusCode != "0")
            {
                //Failed
            }
            else
            {
                //Success
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}