using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace relmon.Controllers
{
    public class AccountController : Controller
    {

        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        int role = int.Parse(Session["role"].ToString());
                        if(role > 0){
                            return RedirectToAction("Index", "");
                        }else{
                            return RedirectToAction("Index", "Admin");
                        }
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            Session["role"] = null;
            Session["name"] = null;
            Session["id"] = null;
            FormsAuthentication.SignOut();
            
            return RedirectToAction("LogOn", "Account");
        }

        private bool ValidateUser(string username, string password)
        {
            password = EncodePassword(password);
            user a = db.users.Where(b => b.username == username).SingleOrDefault();
            if (a != null) {
                if(a.password == password){
                    Session["role"] = a.rm_role;
                    Session["name"] = a.fullname;
                    Session["id"] = a.id;
                    return true;
                }
                return false;
            }
            return false;
        }

        private string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-","").ToLower();
        }

    }
}
