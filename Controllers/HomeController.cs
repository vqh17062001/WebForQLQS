﻿using System;

using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebForQLQS.Models;
using System.Data.Common;

namespace WebForQLQS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

       



        // hàm   LoginButtonClick()
        // nhận  exampleInputEmail1 , exampleInputPassword1 từ Login.cshtml
        // truy vấn từ csdl để đăng nhập 

        [HttpPost]
        public IActionResult LoginButtonClick(string exampleInputEmail1 , string exampleInputPassword1 )
        {

            /*
            string connectionString = "Data Source=VO-QUOC-HUY;Initial Catalog=HTQLQS;Persist Security Info=True;User ID=sa;Password=Qnvn16062001@;Encrypt=false;Trusted_Connection=True;TrustServerCertificate=True";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            using ( DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 3. Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                using (DbCommand command = connection.CreateCommand())
                {


                    command.CommandText = "SELECT * from QUAN_NHAN";
                    var reader = command.ExecuteReader();
                }
            }

            */




            if (exampleInputEmail1 == "123" && exampleInputPassword1 == "123")
            {


                return View("Views/Home/Privacy.cshtml");
            }
            else
            {


                return View("Views/Home/Login.cshtml");
            }
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