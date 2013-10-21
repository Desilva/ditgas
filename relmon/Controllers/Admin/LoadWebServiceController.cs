using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using System.Data;

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

        public List<WebServiceContainer> Get_List()
        {
            return null;
        }

        public void Import()
        {
            List<WebServiceContainer> listFromWebService = this.Get_List();
            foreach(var result in listFromWebService){
                var userList = (from i in db.users
                               where (i.no_pekerja == result.EmployeeID)
                               select i).ToList();
                user getUser;
                if(userList.Count != 0){
                    getUser = userList.First(); //update
                }else{
                  getUser = new user(); //insert
                }
	            //pindahin isinya ke table users
                getUser.directoratecode = result.Directoratecode;
                getUser.directoratedesc = result.Directoratedesc;
                getUser.departementcode = result.Departementcode;
                getUser.departementdesc = result.Departementdesc;
                getUser.divisioncode = result.Divisioncode;
                getUser.divisiondesc = result.Divisiondesc;
                getUser.functioncode = result.Functioncode;
                getUser.functiondesc = result.Functiondesc;
                getUser.sectioncode = result.Sectioncode;
                getUser.sectiondesc = result.Sectiondesc;
                getUser.groupcode = result.Groupcode;
                getUser.groupdesc = result.Groupdesc;
                getUser.no_pekerja = result.EmployeeID;
                getUser.fullname = result.FirstName +" "+result.LastName;
                getUser.employeestatus = result.EmployeeStatus;
                getUser.phonenumber = result.PhoneNumber;
                getUser.tgl_lahir = new DateTime((Int32)Int64.Parse(result.BirthDate.Substring(0, 4)), (Int32)Int64.Parse(result.BirthDate.Substring(4, 2)), (Int32)Int64.Parse(result.BirthDate.Substring(6, 2)));
                getUser.tgl_aktif = new DateTime((Int32)Int64.Parse(result.StartWorkDate.Substring(0,4)) , (Int32)Int64.Parse(result.StartWorkDate.Substring(4,2)) , (Int32)Int64.Parse(result.StartWorkDate.Substring(6,2)));;
                getUser.layering = result.Layer;
                getUser.gol_upah_persero = result.GolJabatan;
                if (result.Gender.ToLower().CompareTo("male") == 0)
                {
                    getUser.jenis_kelamin = 1;
                }
                else
                {
                    getUser.jenis_kelamin = 0;
                }
                getUser.alamat_rumah = result.Address;
                
                
                
	            //isi smua positionId sm nama (parent belum)
                var userJabatanList = (from i in db.users_jabatan
                               where (i.positionID == result.PositionID && i.nama == result.EmployeeTitle )
                               select i).ToList();
               
                if(userJabatanList.Count == 0){
                     users_jabatan getJabatan = new users_jabatan();
                    getJabatan.positionID = result.PositionID;
                    getJabatan.nama = result.EmployeeTitle;
                    db.users_jabatan.Add(getJabatan);
                    db.SaveChanges();
                }


                //nyari id buat rm_role
                var getIdJabatanList = (from i in db.users_jabatan
                                        where (i.positionID == result.PositionID && i.nama == result.EmployeeTitle)
                                        select new {i.id}).ToList();
                if(getIdJabatanList.Count != 0){
                    //kalo ganti jabatan ganti rm_role nya
                    getUser.rm_role = getIdJabatanList.OrderBy(x=>x.id).Last().id;
                }

                if (userList.Count != 0)
                {
                    db.Entry(getUser).State = EntityState.Modified;
                }
                else
                {
                    db.users.Add(getUser);
                }
                db.SaveChanges();
                
            }


            //skrg br ngisi s parentnya dan update smua parent ny biar bener

            var getJabatanList = (from i in db.users_jabatan
                                select i).ToList();
            foreach(var result in getJabatanList)
            {
	            var getPosID = result.positionID;
	            var parentId = result.id;
                List<WebServiceContainer> temp = new List<WebServiceContainer>();
	            foreach(var search in listFromWebService)
                {
		            if(search.ParentPositionID == getPosID)
                    {
			            temp.Add(search);
                    }
                }

                foreach(var getRowFromTemp in temp){
	                var getJabatanList2 = (from i in db.users_jabatan
                                        where (i.positionID == getRowFromTemp.PositionID)
                                        select i).ToList();
	                foreach(var result2 in getJabatanList2){
                        if (result2.parent != parentId)
                        {
                            result2.parent = parentId;
	                        db.Entry(result2).State = EntityState.Modified;
                            db.SaveChanges();		
                        }

                    }
                }
            }
        }
    }
}
