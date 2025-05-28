using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class FUN149controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // 新增這個 Action 用來測試錯誤頁
        public IActionResult TestError()
        {
            throw new Exception("這是測試用的錯誤，請安心。");
        }
    }
}
