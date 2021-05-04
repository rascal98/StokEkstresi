using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TestApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        StokContext _db;
        public HomeController()
        {
            _db = new StokContext();
        }
        /// <summary>
        /// Tüm stok verilerinin getirilmesi işlemi
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<STKObject> tumStoklar = _db.Test.FromSqlRaw("exec dbo.sp_stokekstresi").ToList();
            List<STKTable> stkTable = _db.STK.ToList();
            HomePageViewModel model = new HomePageViewModel { StkObjectList = tumStoklar,StkTableList= stkTable };
            return View(model);
        }
        /// <summary>
        /// Post edilen verilere göre filtreleme işlemlerinin gerçekleştirilme işlemi
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(StokViewModel data)
        {
            string malKodu = data.MalKodu;
            int? baslangicTarihi = Convert.ToInt32(data.BaslangicTarihi.ToOADate());
            int? bitisTarihi = Convert.ToInt32(data.BitisTarihi.ToOADate());
            baslangicTarihi = baslangicTarihi == 0 ? new Nullable<int>() : baslangicTarihi;
            bitisTarihi = bitisTarihi == 0 ? new Nullable<int>() : bitisTarihi;
            SqlParameter MalKodu = new SqlParameter("@MalKodu", (object)malKodu ?? DBNull.Value);
            SqlParameter BaslangicTarihi = new SqlParameter("@BaslangicTarihi", (object)baslangicTarihi ?? DBNull.Value);
            SqlParameter BitisTarihi = new SqlParameter("@BitisTarihi", (object)bitisTarihi ?? DBNull.Value);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(MalKodu);
            parameters.Add(BaslangicTarihi);
            parameters.Add(BitisTarihi);
            List<STKObject> stoklar = _db.Test.FromSqlRaw("exec dbo.sp_stokekstresi @MalKodu,@BaslangicTarihi,@BitisTarihi", parameters.ToArray()).ToList();
            List<STKTable> stkTable = _db.STK.ToList();
            HomePageViewModel model = new HomePageViewModel { StkObjectList = stoklar, StkTableList = stkTable };
            return View(model);
        }

        /// <summary>
        /// Ajax ile index.cshtml içerisindeki mal kodu listesini doldurma işlemi
        /// </summary>
        /// <param name="malkodu"></param>
        /// <param name="maladi"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult FindSTK(string malkodu,string maladi)
        {
            var stkTableResult=_db.STK.Where(x => x.MalAdi.Contains(maladi) || x.MalKodu.Contains(malkodu)).ToList();
            return Json(stkTableResult);
        }
    }
}
