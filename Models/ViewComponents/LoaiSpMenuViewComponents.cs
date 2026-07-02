using hahaha.Models;
using hahaha.Repository;
using Microsoft.AspNetCore.Mvc;
namespace hahaha.Models.ViewComponents
{
    public class LoaiSpMenuViewComponent: ViewComponent
    {
        private readonly  ILoaiSPRepository _loaiSPRepository;
        
        public LoaiSpMenuViewComponent(ILoaiSPRepository loaiSPRepository)
        {
            _loaiSPRepository = loaiSPRepository; 

        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSPRepository.GetAllLoaiSp().OrderBy(X => X.Loai);
            return View(loaisp);
        }
    }
}
