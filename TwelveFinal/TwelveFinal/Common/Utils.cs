﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Common
{
    public static class Utils
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static async Task RegisterMail(User user)
        {
            if (string.IsNullOrEmpty(user.Email)) return;
            string SendEmail = "hsntladykillah@gmail.com";
            string SendEmailPassword = "hoilamgi1234";

            var loginInfo = new NetworkCredential(SendEmail, SendEmailPassword);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            string body = "Tài khoản của bạn đã được đăng ký!\n";
            body += "Id: " + user.Username + "\n";
            body += "Password: " + user.Password;
            try
            {
                msg.From = new MailAddress(SendEmail);
                msg.To.Add(new MailAddress(user.Email));
                msg.Subject = "Đăng ký tài khoản TwelveFinal!";
                msg.Body = body;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error!");
            }
        }

        public static async Task RecoveryPasswordMail(User user)
        {
            if (string.IsNullOrEmpty(user.Email)) return;
            string SendEmail = "hsntladykillah@gmail.com";
            string SendEmailPassword = "hoilamgi1234";

            var loginInfo = new NetworkCredential(SendEmail, SendEmailPassword);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            string body = "Mật khẩu của bạn đã được khôi phục!\n";
            body += "Password: " + user.Password;
            try
            {
                msg.From = new MailAddress(SendEmail);
                msg.To.Add(new MailAddress(user.Email));
                msg.Subject = "Khôi phục mật khẩu Twelve Final!";
                msg.Body = body;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error!");
            }
        }

        public static Guid CreateGuid(string name)
        {
            MD5 md5 = MD5.Create();
            Byte[] myStringBytes = ASCIIEncoding.Default.GetBytes(name);
            Byte[] hash = md5.ComputeHash(myStringBytes);
            return new Guid(hash);
        }
    }
}
