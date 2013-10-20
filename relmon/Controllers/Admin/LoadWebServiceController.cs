using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;

namespace relmon.Controllers.Admin
{
    public class LoadWebServiceController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /LoadWebService/

        public ActionResult Index()
        {
            return PartialView();
        }

        public void Import()
        {
            //List<entahApaNamanya> listFromWebService = ;
            //foreach(var result in listFromWebService){
            //    var userList = (from i in db.users
            //                   join j in db.users_jabatan on i.rm_role equals j.id
            //                   where (i.no_pekerja == result.EmployeeID
            //                   select i).ToList();
            //    user getUser;
            //    if(userList.Count != 0){
            //        getUser = userList.First();
            //        //update-update
            //    }else{
            //      getUser = new user();    
            //        //insert-insert
            //    }
            //    getUser.directoratecode = result.Directoratecode;
            //    getUser.directoratedesc = result.Directoratedesc;
            //    getUser.departementcode = result.Departementcode;
            //    getUser.depart = result.Departementdesc;
            //    getUser.divisioncode = result.Divisioncode;
            //    getUser.divisiondesc = result.Divisiondesc;
            //    getUser.functioncode = result.Functioncode;
            //    getUser.functiondesc = result.Functiondesc;
            //    getUser.sectionnode = result.Sectioncode;
            //    getUser.sectiondesc = result.Sectiondesc;
            //    getUser.groupcode = result.Groupcode;
            //    getUser.groupdesc = result.Groupdesc;
            //    getUser.no_pekerja = result.EmployeeID;
            //    getUser.fullname = result.FirstName +" "+result.LastName;
            //    getUser.employeestatus = result.EmployeeStatus;
            //    getUser.phonenumber = result.PhoneNumber;
            //    getUser.tgl_lahir = result.BirthDate;
            //    getUser.tgl_aktif = result.StartWorkDate;
            //    getUser.layering = result.Layer;
            //    getUser.gol_upah_persero = result.GolJabatan;
            //    getUser.jenis_kelamin = result.Gender;
            //    getUser.alamat_rumah = result.Address

            //        users_jabatan getJabatan = 
            //            parentposition
            //            employeeTitle
            //            positionID

            //    db.SaveChanges();
            //}
        }
    }
}
