using HotelManagement.Models;
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

        public ActionResult Payment()
        {
            //request params need to request to MoMo system
            string endpoint = @"https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOIQA420180417";
            string accessKey = "SvDmj2cOTYZmQQ3H";
            string serectkey = "PPuDXq1KowPT1ftR8DvlQTHhC03aul17";

            string orderInfo = "orderInfo";
            string returnUrl = @"https://localhost:44333/Manager/RoomRentalSlip";
            string notifyurl = @"https://localhost:44333/Manager/RoomRentalSlip";

            string amount = "100000";
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
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
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            //System.Diagnostics.Process.Start(jmessage.GetValue("payUrl").ToString());

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
    }
}