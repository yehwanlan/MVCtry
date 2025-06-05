using Microsoft.AspNetCore.Mvc;
using ProductCategory.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
 // 請用你的實際命名空間

public class MembersController : Controller
{
    private readonly MyMemberDbContext _context; // 用你 Scaffold 出來的 DbContext

    public MembersController(MyMemberDbContext context)
    {
        _context = context;
    }

    // 頭像上傳 API
    [HttpPost]
    public async Task<IActionResult> UploadAvatar(int memberId, IFormFile file)
    {
        // 1. 檔案驗證
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        if (file == null || file.Length == 0)
            return BadRequest("未選擇檔案");
        if (!allowedTypes.Contains(file.ContentType))
            return BadRequest("檔案格式不支援");
        if (file.Length > 5 * 1024 * 1024)
            return BadRequest("檔案太大");

        // 2. 產生檔名與存檔路徑
        var ext = Path.GetExtension(file.FileName);
        var fileName = $"avatar_{memberId}_{Guid.NewGuid():N}{ext}";
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "avatar");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        var filePath = Path.Combine(dir, fileName);

        // 3. 存檔
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // 4. 更新 DB 的 AvatarPath 欄位
        var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
        if (member == null)
            return NotFound("找不到會員");

        member.AvatarPath = "/uploads/avatar/" + fileName;
        await _context.SaveChangesAsync();

        // 5. 回傳新頭像網址
        return Ok(new { url = member.AvatarPath });
    }
    public IActionResult UploadImg()
    {
        return View();
    }

}
