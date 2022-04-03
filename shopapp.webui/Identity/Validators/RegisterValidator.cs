using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using shopapp.webui.Models;

namespace shopapp.webui.Identity.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(i => i.UserName).NotEqual("admin").WithMessage("'admin' adında kullanıcı oluşturulamaz !");
            RuleFor(i => i.Email).EmailAddress().WithMessage("Lütfen geçerli bir e-mail adresi giriniz !");
        }
    }
}