using Azure;
using hahaha.Models;
using hahaha.Models.Authention;
using hahaha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace hahaha.Controllers
{
    public class HomeController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[Authentication]

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.TDanhMucSps.OrderBy(x => x.TenSp).AsNoTracking().OrderBy(x=>x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanPham, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult SanPhamTheoLoai(String maloai, int? page)
        {
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstSanPham = db.TDanhMucSps.OrderBy(x => x.TenSp).AsNoTracking().Where(x=>x.MaLoai==maloai).OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanPham, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(lst);
        }
        public IActionResult ChiTietSanPham(string maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham=db.TAnhSps.Where(x=>x.MaSp==maSp).ToList();
            ViewBag.anhSanPham=anhSanPham;
            return View(sanPham);
        }
        public IActionResult ProductDetail(string maSp)
        {
			var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
			var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            var homeProductDetailViewModel = new HomeProductDetailViewModel { danhMucSp = sanPham, anhSps = anhSanPham };
            return View(homeProductDetailViewModel);
        }
		public IActionResult SanPhamTheoQuocGia(string maNuoc)
		{
			maNuoc = maNuoc?.Trim();

			var lstSanPham = db.TDanhMucSps
				.AsNoTracking()
				.Where(x => x.MaNuocSx != null && x.MaNuocSx.Trim() == maNuoc)
				.OrderBy(x => x.TenSp)
				.ToList();

			return PartialView("PartialSanPham", lstSanPham);
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
