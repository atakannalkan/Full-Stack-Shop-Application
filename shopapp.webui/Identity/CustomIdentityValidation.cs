using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace shopapp.webui.Identity
{
    public class CustomIdentityValidation : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError {Code="DuplicateEmail",Description=$"'{email}' adlı email zaten kullanımda !"};
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError {Code="DuplicateUserName",Description=$"'{userName}' adlı kulanıcı adı zaten kullanımda !"};
        }
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError{Code="InvalidUserName", Description=$"'{userName}' Kullanıcı adı geçersiz, sadece harf veya rakam içerebilir."};
        }
        public override IdentityError InvalidToken()
        {
            return new IdentityError {Code="InvalidToken", Description="Geçersiz token ! lütfen tekrar deneyin, hata devam ederse site yöneticisi ile iletişime geçiniz."};
        }


        // Password Settings
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError {Code="PasswordRequiresDigit", Description="Şifre en az bir rakamdan oluşmalıdır !"};
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError{Code="PasswordRequiresLower", Description="Şifrede en az bir küçük harf ('a'-'z') bulunmalıdır."};
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError{Code="PasswordRequiresUpper",Description=$"Parolalarda en az bir büyük harf ('A'-'Z') olmalıdır."};
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError{Code="PasswordRequiresNonAlphanumeric", Description="Şifrede @^[*-'._/]{,}$ karakterlerden birisi bulunmalıdır"};
        }
    }
}