using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CUSTOMERWEBSITE.ViewModels
{
    public class ContactViewModels : IValidatableObject
    {
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名為必填欄位")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "電話為必填欄位")]
        public string? Phone { get; set; }

        [Display(Name = "留言內容")]
        public string? Message { get; set; }

        // 這就是自訂驗證方法
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 檢查 Email 和 Message 是否兩者皆為空
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Message))
            {
                // 兩個都沒填的話，回傳驗證失敗
                yield return new ValidationResult(
                    "Email 和留言內容請至少填寫一項",
                    new[] { nameof(Email), nameof(Message) }
                );
            }

            // Email 有填但格式錯誤
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var emailAttr = new EmailAddressAttribute();
                if (!emailAttr.IsValid(Email))
                {
                    yield return new ValidationResult(
                        "請輸入正確的Email格式",
                        new[] { nameof(Email) }
                    );
                }
            }
        }
    }
}
