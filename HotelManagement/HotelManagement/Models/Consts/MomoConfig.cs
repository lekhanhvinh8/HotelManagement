using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Consts
{
    public static class MomoConfig
    {
        public static string ParnerCode = "MOMOGQOC20201207";
        public static string AccessKey = "tb0AnQrtECJ3H0Zu";
        public static string SecretKey = "2l9NSfk8rWqc4CpYGZva7dBfCYo9xM25";

        public static string ReturnUrl = @"https://localhost:44333/MomoPayment/ReturnUrl";
        public static string NotifyUrl = @"https://localhost:44333/MomoPayment/NotifyUrl";
    }
}