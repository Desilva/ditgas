using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using Telerik.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using Telerik.Web.Mvc.UI;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using com.mxgraph;

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //public class EmployeeTree
        //{
        //    private RelmonStoreEntities db = new RelmonStoreEntities();

        //    public string current;
        //    public List<EmployeeTree> child;

        //    //public EmployeeTree()
        //    //{
        //    //    current = new user();
        //    //    child = new List<EmployeeTree>();
        //    //}

        //    public EmployeeTree(int id)
        //    {
        //        //current = new user();
        //        current = "";
        //        child = new List<EmployeeTree>();

        //        var tempUser = db.users.Find(id);
        //        current = tempUser.fullname;
        //        users_jabatan currentJabatan = db.users_jabatan.Find(tempUser.rm_role);
        //        foreach (user u in db.users.Where(x => x.users_jabatan.parent == currentJabatan.id))
        //        {
        //            child.Add(new EmployeeTree(u.id));
        //        }
        //    }

        //    //public EmployeeTree(user user)
        //    //{
        //    //    current = new user();
        //    //    child = new List<EmployeeTree>();

        //    //    current = user;
        //    //    users_jabatan currentJabatan = db.users_jabatan.Find(current.rm_role);
        //    //    foreach (user u in db.users.Where(x => x.users_jabatan.parent == currentJabatan.id))
        //    //    {
        //    //        child.Add(new EmployeeTree(u));
        //    //    }
        //    //}
        //}

        #region view

        public ActionResult Index()
        {
            IEnumerable<SelectListItem> items = db.users_jabatan
                .AsEnumerable()
                .Where(uj => uj.id > 0)
                .OrderBy(uj => uj.parent)
                .Select(uj => new SelectListItem
                {
                    Value = uj.id.ToString(),
                    Text = uj.nama
                });
            ViewBag.role_employee = items;
            ViewBag.rm_role = items;

            List<user> u = db.users.ToList();

            return PartialView(db.users.ToList());
        }

        public ActionResult AddView()
        {
            ViewBag.id_company = new SelectList(db.companies, "id", "nama");
            ViewBag.id_role = new SelectList(db.users_jabatan, "id", "nama");

            ViewBag.golUpahAp = getGolUpah("");
            ViewBag.golUpahPersero = getGolUpah("");
            ViewBag.pendidikanTerakhir = getPendidikanTerakhir("");

            return PartialView();
        }

        public ActionResult EditView(int userId, int rm_role, int company_id)
        {
            var companiesList = (from o in db.companies
                                select o)
                                .AsEnumerable()
                                .Select(o => new SelectListItem
                                {
                                    Value = o.id.ToString(),
                                    Text = o.nama
                                });
            foreach (SelectListItem item in companiesList)
            {
                if (item.Value == company_id.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.company_id = companiesList;

            var roleList = (from o in db.users_jabatan
                                 where o.company_id == company_id
                                 select o)
                                .AsEnumerable()
                                .Select(o => new SelectListItem
                                {
                                    Value = o.id.ToString(),
                                    Text = o.nama
                                });
            foreach (SelectListItem item in roleList)
            {
                if (item.Value == rm_role.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.rm_role = roleList;

            user userEdit = db.users.Find(userId);
            ViewBag.userEdit = userEdit;
            ViewBag.golUpahAp = getGolUpah(userEdit.gol_upah_ap);
            ViewBag.golUpahPersero = getGolUpah(userEdit.gol_upah_persero);
            ViewBag.pendidikanTerakhir = getPendidikanTerakhir(userEdit.pendidikan_terakhir);

            return PartialView();
        }

        private List<SelectListItem> getGolUpah(string golUpahSelected)
        {
            List<SelectListItem> golUpahListTemp = new List<SelectListItem>();
            string[] golUpahStrList = { "", "P3", "P2", "P1", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15" };

            foreach (string golUpahStr in golUpahStrList)
            {
                SelectListItem golUpah = new SelectListItem();
                golUpah.Value = golUpahStr;
                golUpah.Text = golUpahStr;
                if (golUpahStr == golUpahSelected)
                    golUpah.Selected = true;

                golUpahListTemp.Add(golUpah);

            }
            return golUpahListTemp;
        }

        private List<SelectListItem> getPendidikanTerakhir(string pendidikanSelected)
        {
            List<SelectListItem> pendidikanListTemp = new List<SelectListItem>();
            List<Tuple<string, string>> pendidikanStrList = new List<Tuple<string, string>>();

            pendidikanStrList.Add(new Tuple<string, string>("", ""));
            pendidikanStrList.Add(new Tuple<string, string>("SD", "SD"));
            pendidikanStrList.Add(new Tuple<string, string>("SLTP", "SLTP"));
            pendidikanStrList.Add(new Tuple<string, string>("SLTA", "SLTA"));
            pendidikanStrList.Add(new Tuple<string, string>("D1+D2", "Diploma I&II"));
            pendidikanStrList.Add(new Tuple<string, string>("D3", "Diploma III"));
            pendidikanStrList.Add(new Tuple<string, string>("D4", "Diploma IV"));
            pendidikanStrList.Add(new Tuple<string, string>("S1", "S1-Perguruan Tinggi"));
            pendidikanStrList.Add(new Tuple<string, string>("S2", "S2-Pasca Sarjana"));
            pendidikanStrList.Add(new Tuple<string, string>("S3", "S3-Pasca/Doktor"));

            foreach (Tuple<string, string> pendidikanStr in pendidikanStrList)
            {
                SelectListItem pendidikan = new SelectListItem();
                pendidikan.Value = pendidikanStr.Item1;
                pendidikan.Text = pendidikanStr.Item2;
                if (pendidikanStr.Item1 == pendidikanSelected)
                    pendidikan.Selected = true;

                pendidikanListTemp.Add(pendidikan);

            }
            return pendidikanListTemp;
        }

        #endregion
        //
        // GET: /Employee/

        // Ajax select binding
        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return binding();
        }

        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            delete(id);
            return binding();
        }

        //select data user
        private ViewResult binding()
        {
            var result = from o in db.users
                         join e in db.users_jabatan on o.rm_role equals e.id
                         join f in db.companies on e.company_id equals f.id
                         where o.rm_role > 0
                         orderby o.fullname
                         select new user_rm_role
                         {
                             id = o.id,
                             fullname = o.fullname,
                             rm_role = o.rm_role,
                             no_pekerja = o.no_pekerja,
                             lokasi = o.lokasi,
                             direktorat = o.direktorat,
                             tempat_lahir = o.tempat_lahir,
                             tgl_lahir = o.tgl_lahir,
                             jenis_kelamin = o.jenis_kelamin,
                             agama = o.agama,
                             susunan_keluarga = o.susunan_keluarga,
                             NPWP = o.NPWP,
                             alamat_pajak = o.alamat_pajak,
                             alamat_rumah = o.alamat_rumah,
                             alamat_ktp = o.alamat_ktp,
                             tgl_mppk = o.tgl_mppk,
                             tgl_pensiun = o.tgl_pensiun,
                             sub_area = o.sub_area,
                             tgl_aktif = o.tgl_aktif,
                             tmt_dapen_dplk = o.tmt_dapen_dplk,
                             tmt_jabatan = o.tmt_jabatan,
                             tmt_gol_jabatan = o.tmt_gol_jabatan,
                             gol_upah_persero = o.gol_upah_persero,
                             tmt_gol_upah_persero = o.tmt_gol_upah_persero,
                             gol_upah_ap = o.gol_upah_ap,
                             tmt_gol_upah_ap = o.tmt_gol_upah_ap,
                             layering = o.layering,
                             pendidikan_terakhir = o.pendidikan_terakhir,
                             status_pekerja = o.status_pekerja,

                             users_jabatan_parent = new users_jabatan_parent
                             {
                                 id = e.id,
                                 nama = e.nama,
                                 parent = e.parent,
                                 deskripsi = e.deskripsi,
                                 company2 = new company2
                                 {
                                     nama = f.nama
                                 }
                             }
                         };
            List<user_rm_role> temp = result.ToList();
            List<user> u = new List<user>();

            foreach (var item in temp)
            {
                user utemp = new user();
                utemp.id = item.id;
                utemp.fullname = item.fullname;
                utemp.rm_role = item.rm_role;
                utemp.no_pekerja = item.no_pekerja;
                utemp.lokasi = item.lokasi;
                utemp.direktorat = item.direktorat;
                utemp.tempat_lahir = item.tempat_lahir;
                utemp.tgl_lahir = item.tgl_lahir;
                utemp.jenis_kelamin = item.jenis_kelamin;
                utemp.agama = item.agama;
                utemp.susunan_keluarga = item.susunan_keluarga;
                utemp.NPWP = item.NPWP;
                utemp.alamat_pajak = item.alamat_pajak;
                utemp.alamat_rumah = item.alamat_rumah;
                utemp.alamat_ktp = item.alamat_ktp;
                utemp.tgl_mppk = item.tgl_mppk;
                utemp.tgl_pensiun = item.tgl_pensiun;
                utemp.sub_area = item.sub_area;
                utemp.tgl_aktif = item.tgl_aktif;
                utemp.tmt_dapen_dplk = item.tmt_dapen_dplk;
                utemp.tmt_jabatan = item.tmt_jabatan;
                utemp.tmt_gol_jabatan = item.tmt_gol_jabatan;
                utemp.gol_upah_persero = item.gol_upah_persero;
                utemp.tmt_gol_upah_persero = item.tmt_gol_upah_persero;
                utemp.gol_upah_ap = item.gol_upah_ap;
                utemp.tmt_gol_upah_ap = item.tmt_gol_upah_ap;
                utemp.layering = item.layering;
                utemp.pendidikan_terakhir = item.pendidikan_terakhir;
                utemp.status_pekerja = item.status_pekerja;
                utemp.users_jabatan_parent = item.users_jabatan_parent;
                utemp.users_jabatan_parent.company2 = item.users_jabatan_parent.company2;
                u.Add(utemp);
            }

            return View(new GridModel<user>
            {
                Data = u
            });
        }

        //insert data user
        [HttpPost]
        public void create(user user)
        {
            if (user.tgl_lahir != null)
            {
                user.tgl_mppk = user.tgl_lahir.Value.AddYears(55).AddMonths(6);
                user.tgl_pensiun = user.tgl_lahir.Value.AddYears(56);
            }
            db.users.Add(user);
            db.SaveChanges();
        }

        //update data user
        [HttpPost]
        public void update(user user)
        {
            //user u = db.users.Find(user.id);
            //u.fullname = user.fullname;
            //u.rm_role = user.rm_role;
            if (user.tgl_lahir != null)
            {
                user.tgl_mppk = user.tgl_lahir.Value.AddYears(55).AddMonths(6);
                user.tgl_pensiun = user.tgl_lahir.Value.AddYears(56);
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data user
        private void delete(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        #region JsonResult

        public JsonResult addUser()
        {
            return Json(true);
        }

        public JsonResult editUser(int id)
        {
            var u = (from o in db.users
                          join e in db.users_jabatan on o.rm_role equals e.id
                          where o.id == id
                          select new
                          {
                              id = o.id,
                              rm_role = o.rm_role,
                              company_id = e.company_id
                          }).FirstOrDefault();
            return Json(new
            {
                id = u.id,
                rm_role = u.rm_role,
                company_id = u.company_id
            });
        }

        public JsonResult GetDetail(int id)
        {
            user u = db.users.Find(id);
            return Json(new
            {
                fullname = u.fullname,
                rm_role = u.rm_role,
                edit_rm_role_employee = u.rm_role,
                no_pekerja = u.no_pekerja,
                lokasi = u.lokasi,
                direktorat = u.direktorat,
                tempat_lahir = u.tempat_lahir,
                tgl_lahir = u.tgl_lahir,
                jenis_kelamin = u.jenis_kelamin,
                agama = u.agama,
                susunan_keluarga = u.susunan_keluarga,
                NPWP = u.NPWP,
                alamat_pajak = u.alamat_pajak,
                alamat_rumah = u.alamat_rumah,
                alamat_ktp = u.alamat_ktp,
                tgl_mppk = u.tgl_mppk,
                tgl_pensiun = u.tgl_pensiun,
                sub_area = u.sub_area,
                tgl_aktif = u.tgl_aktif,
                tmt_dapen_dplk = u.tmt_dapen_dplk,
                tmt_jabatan = u.tmt_jabatan,
                tmt_gol_jabatan = u.tmt_gol_jabatan,
                gol_upah_persero = u.gol_upah_persero,
                tmt_gol_upah_persero = u.tmt_gol_upah_persero,
                gol_upah_ap = u.gol_upah_ap,
                tmt_gol_upah_ap = u.tmt_gol_upah_ap,
                layering = u.layering,
                pendidikan_terakhir = u.pendidikan_terakhir,
                status_pekerja = u.status_pekerja,
            });
        }

        public JsonResult GetRole(int id_company)
        {
            var model = from o in db.users_jabatan
                        where o.company_id == id_company
                        select new dropdown_jabatan
                        {
                            id = o.id,
                            nama = o.nama
                        };
            return Json(model.ToList());
        }

        #endregion

        private class dropdown_jabatan
        {
            public dropdown_jabatan()
            {
            }

            public int id { get; set; }
            public string nama { get; set; }
        }

        #region admin profil tab

        //public EmployeeTree getEmployeeTree(int id)
        //{
        //    return new EmployeeTree(id);
        //}

        //public string getEmployeeTree(int id)
        //{
            
        //    var serializer = new JavaScriptSerializer();
        //    var jsonString = serializer.Serialize(new EmployeeTree(id));
        //    return jsonString;
        //}
        public ActionResult Profil_Organisasi()
        {
            //ViewBag.struktur = new EmployeeTree(0);
            return PartialView();
        }

        public string getXml()
        {
            var physicalPath = Path.Combine(Server.MapPath("~/Upload"), "struktur.xml");
            if(System.IO.File.Exists(physicalPath)){
                 string getString = System.IO.File.ReadAllText(physicalPath);
                return getString;
            }
           return "";
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void ProcessRequest(string filename, string format, string bg, string w, string h, string xml,string xmlSave)
        {
            xml = HttpUtility.UrlDecode(xml);
            string width = w;
            string height = h;
            xml = xml.Replace(HttpUtility.HtmlEncode("<p style=\"margin:0px;width:115px\">&nbsp;</p><h4 style=\"margin:0px;color:#1d258f;text-align:center;\">"), "\n    ");
            xml = xml.Replace(HttpUtility.HtmlEncode("&nbsp;&nbsp;&nbsp;&nbsp;</h4><p style=\"text-align:left;margin:0px;color:black;text-indent:1px;float:left;width:20px\">"), "\n");
            xml = xml.Replace(HttpUtility.HtmlEncode("</p><p style=\"text-align:right;margin:0px;color:black;\">"), "                                  ");
            xml = xml.Replace(HttpUtility.HtmlEncode("</p>"), "");

            if (xml != null && width != null && height != null && bg != null
                    && filename != null && format != null)
            {
                //xml
                xmlSave = HttpUtility.UrlDecode(xmlSave);
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlSave);
                var physicalPath = Path.Combine(Server.MapPath("~/Upload"), "struktur.xml");
                xdoc.Save(physicalPath);

                //png
                var imagePath = Path.Combine(Server.MapPath("~/Upload"), "struktur.png");
                System.Drawing.Image image = mxUtils.CreateImage(int.Parse(width), int.Parse(height),
                    ColorTranslator.FromHtml(bg));
                Graphics g = Graphics.FromImage(image);
                g.SmoothingMode = SmoothingMode.HighQuality;
                mxSaxOutputHandler handler = new mxSaxOutputHandler(new mxGdiCanvas2D(g));
                handler.Read(new XmlTextReader(new StringReader(xml)));
                image.Save(imagePath, ImageFormat.Png);
                //HttpContext.Response.ContentType = "image/" + format;
                //HttpContext.Response.AddHeader("Content-Disposition",
                //  "attachment; filename=" + filename);

                //MemoryStream memStream = new MemoryStream();
                //image.Save(memStream, ImageFormat.Png);
                //memStream.WriteTo(HttpContext.Response.OutputStream);
            }
            //HttpContext.Response.StatusCode = 200; /* OK */
            //}
            //else
            //{
            //    HttpContext.Response.StatusCode = 400; /* Bad Request */
            //}
        }
        #endregion
    }
}