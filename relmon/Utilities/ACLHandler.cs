using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;

namespace relmon.Utilities
{
    public class ACLHandler
    {
        //
        // GET: /ACLHandler/

        public static bool isUserAllowedTo(string pageName, int userId, string action)
        {
            ACLContainer aclContainer = new ACLContainer();
            if (aclContainer.getPageDefaultAction(pageName, action)) //kalo defaultnya true, alias bisa diubah2, alias ga semua user boleh lakuin action di page ini
            {
                RelmonStoreEntities db = new RelmonStoreEntities();
                var currentACL = db.acls.Where(a => (a.user_id == userId) && (a.page_name == pageName));
                if (currentACL.Count() == 1)
                {
                    switch (action)
                    {
                        case "view": return (currentACL.First().allow_view == 1);
                        case "create": return (currentACL.First().allow_create == 1);
                        case "update": return (currentACL.First().allow_update == 1);
                        case "delete": return (currentACL.First().allow_delete == 1);
                        case "print": return (currentACL.First().allow_print == 1);
                        default: return false;
                    }
                }
                return false;
            }
            else //kalo defaultnya false, alias ga bisa diubah2, alias semua user boleh lakuin action di page itu
            {
                return true;
            }
        }

        public static bool isMenuShown(string pageName, int userId)
        {
            ACLContainer aclContainer = new ACLContainer();
            var pageFound = aclContainer.pageList.Find(pageName);
            if (pageFound.isValid())
            {
                List<Tuple<string, string>> arrayChild = pageFound.toArray();
                foreach (Tuple<string, string> childTuple in arrayChild)
                {
                    string childPageName = childTuple.Item2;
                    if (isUserAllowedTo(childPageName, userId, "view"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}
