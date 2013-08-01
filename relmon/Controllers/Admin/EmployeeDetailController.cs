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
using System.Collections.Specialized;

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class EmployeeDetailController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /EmployeeDetail/

        // Ajax select binding
        [GridAction]
        public ActionResult _SelectAjaxEditingDetailEmployee(int userId, string kategori)
        {
            return bindingDetailEmployee(userId, kategori);
        }

        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditingDetailEmployee(int id, int userId, string kategori)
        {
            deleteDetailEmployee(id, kategori);
            return bindingDetailEmployee(userId, kategori);
        }

        //select data user
        private ViewResult bindingDetailEmployee(int userId, string kategori)
        {
            switch (kategori)
            {
                case "Pendidikan": return View(new GridModel<users_pendidikan>
                {
                    Data = (List<users_pendidikan>)
                    (from r in db.users_pendidikan
                    where r.user_id == userId
                    select r).ToList()
                });
                case "Kursus": return View(new GridModel<users_kursus>
                {
                    Data = (List<users_kursus>)
                    (from r in db.users_kursus
                     where r.user_id == userId
                     select r).ToList()
                });
                case "Keluarga": return View(new GridModel<users_keluarga>
                {
                    Data = (List<users_keluarga>)
                    (from r in db.users_keluarga
                     where r.user_id == userId
                     select r).ToList()
                });
                case "NilaiKinerja": return View(new GridModel<users_nilai_kinerja>
                {
                    Data = (List<users_nilai_kinerja>)
                    (from r in db.users_nilai_kinerja
                     where r.user_id == userId
                     select r).ToList()
                });
                case "PengalamanKerjaLuar": return View(new GridModel<users_pengalaman_kerja>
                {
                    Data = (List<users_pengalaman_kerja>)
                    (from r in db.users_pengalaman_kerja
                     where r.user_id == userId
                     where r.nama_perusahaan != "Pertamina"
                     select r).ToList()
                });
                case "PengalamanKerjaPertamina": return View(new GridModel<users_pengalaman_kerja>
                {
                    Data = (List<users_pengalaman_kerja>)
                    (from r in db.users_pengalaman_kerja
                     where r.user_id == userId
                     where r.nama_perusahaan == "Pertamina"
                     select r).ToList()
                });
                case "Penghargaan": return View(new GridModel<users_penghargaan>
                {
                    Data = (List<users_penghargaan>)
                    (from r in db.users_penghargaan
                     where r.user_id == userId
                     select r).ToList()
                });
                case "Penugasan": return View(new GridModel<users_penugasan>
                {
                    Data = (List<users_penugasan>)
                    (from r in db.users_penugasan
                     where r.user_id == userId
                     select r).ToList()
                });
                case "RiwayatGolUpahPersero": return View(new GridModel<users_riwayat_gol_upah_persero>
                {
                    Data = (List<users_riwayat_gol_upah_persero>)
                    (from r in db.users_riwayat_gol_upah_persero
                     where r.user_id == userId
                     select r).ToList()
                });
                case "Sertifikasi": return View(new GridModel<users_sertifikasi>
                {
                    Data = (List<users_sertifikasi>)
                    (from r in db.users_sertifikasi
                     where r.user_id == userId
                     select r).ToList()
                });
                //case "TindakanDisiplin": return View(new GridModel<users_tindakan_disiplin>
                default: return View(new GridModel<users_tindakan_disiplin>
                {
                    Data = (List<users_tindakan_disiplin>)
                    (from r in db.users_tindakan_disiplin
                     where r.user_id == userId
                     select r).ToList()
                });

            }
        }

        public JsonResult addDetailEmployee(string kategori)
        {
            return Json(new
            {
                kategori = kategori
            });
        }

        public JsonResult GetDetailDetailEmployee(int id, string kategori)
        {
            object result = castModelJson(kategori, getModel(kategori).Find(id));
            return Json(new
            {
                kategori = kategori,
                data = result
            });
        }

        #region detail employee CrUDe

        //insert data user
        [HttpPost]
        public void createDetailEmployee()
        {
            NameValueCollection requestValue = new NameValueCollection(Request.Form);
            getModel(requestValue["kategori"]).Add(castModelCreate(requestValue["kategori"], requestValue));
            db.SaveChanges();
        }

        //update data user
        [HttpPost]
        public void updateDetailEmployee()
        {
            NameValueCollection requestValue = new NameValueCollection(Request.Form);
            db.Entry(castModelUpdate(requestValue["kategori"], requestValue)).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data user
        private void deleteDetailEmployee(int id, string kategori)
        {
            getModel(kategori).Remove(getModel(kategori).Find(id));
            db.SaveChanges();
        }

        private DbSet getModel(string kategori)
        {
            if (kategori == "Keluarga")
            {
                return db.users_keluarga;
            }
            else if (kategori == "Kursus")
            {
                return db.users_kursus;
            }
            else if (kategori == "NilaiKinerja")
            {
                return db.users_nilai_kinerja;
            }
            else if (kategori == "Pendidikan")
            {
                return db.users_pendidikan;
            }
            else if ((kategori == "PengalamanKerjaLuar") || (kategori == "PengalamanKerjaPertamina"))
            {
                return db.users_pengalaman_kerja;
            }
            else if (kategori == "Penghargaan")
            {
                return db.users_penghargaan;
            }
            else if (kategori == "Penugasan")
            {
                return db.users_penugasan;
            }
            else if (kategori == "RiwayatGolUpahPersero")
            {
                return db.users_riwayat_gol_upah_persero;
            }
            else if (kategori == "Sertifikasi")
            {
                return db.users_sertifikasi;
            }
            else /*if (kategori == "TindakanDisiplin")*/
            {
                return db.users_tindakan_disiplin;
            }
        }

        private object castModelCreate(string kategori, NameValueCollection obj)
        {
            object result;
            if (kategori == "Keluarga")
            {
                result = new users_keluarga
                {
                    user_id = int.Parse(obj["user_id"]),
                    hubungan = obj["hubungan"],
                    nama = obj["nama"],
                    tempat_lahir = obj["tempat_lahir"],
                    tgl_lahir = (obj["tgl_lahir"] == "")?(DateTime?)null:DateTime.Parse(obj["tgl_lahir"])
                };
            }
            else if (kategori == "Kursus")
            {
                result = new users_kursus
                {
                    user_id = int.Parse(obj["user_id"]),
                    tipe = obj["tipe"],
                    nama = obj["nama"],
                    lokasi = obj["lokasi"],
                    hasil = obj["hasil"],
                    tgl_awal = (obj["tgl_awal"] == "")?(DateTime?)null:DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = (obj["tgl_akhir"] == "")?(DateTime?)null:DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if (kategori == "NilaiKinerja")
            {
                result = new users_nilai_kinerja
                {
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else if (kategori == "Pendidikan")
            {
                result = new users_pendidikan
                {
                    user_id = int.Parse(obj["user_id"]),
                    lokasi = obj["lokasi"],
                    tingkat = obj["tingkat"],
                    tgl_awal = (obj["tgl_awal"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = (obj["tgl_akhir"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if ((kategori == "PengalamanKerjaLuar") || (kategori == "PengalamanKerjaPertamina"))
            {
                result = new users_pengalaman_kerja
                {
                    user_id = int.Parse(obj["user_id"]),
                    nama_perusahaan = obj["nama_perusahaan"],
                    jabatan = obj["jabatan"],
                    lokasi = obj["lokasi"],
                    tgl_awal = (obj["tgl_awal"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = (obj["tgl_akhir"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if (kategori == "Penghargaan")
            {
                result = new users_penghargaan
                {
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else if (kategori == "Penugasan")
            {
                result = new users_penugasan
                {
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else if (kategori == "RiwayatGolUpahPersero")
            {
                result = new users_riwayat_gol_upah_persero
                {
                    user_id = int.Parse(obj["user_id"]),
                    gol_upah_persero = obj["gol_upah_persero"],
                    tgl_awal = DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if (kategori == "Sertifikasi")
            {
                result = new users_sertifikasi
                {
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else /*if (kategori == "TindakanDisiplin")*/
            {
                result = new users_tindakan_disiplin
                {
                    user_id = int.Parse(obj["user_id"])
                };
            }
            return result;
        }

        private object castModelUpdate(string kategori, NameValueCollection obj)
        {
            object result;
            if (kategori == "Keluarga")
            {
                result = new users_keluarga
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"]),
                    hubungan = obj["hubungan"],
                    nama = obj["nama"],
                    tempat_lahir = obj["tempat_lahir"],
                    tgl_lahir = (obj["tgl_lahir"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_lahir"])
                };
            }
            else if (kategori == "Kursus")
            {
                result = new users_kursus
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"]),
                    tipe = obj["tipe"],
                    nama = obj["nama"],
                    lokasi = obj["lokasi"],
                    hasil = obj["hasil"],
                    tgl_awal = (obj["tgl_awal"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = (obj["tgl_akhir"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if (kategori == "NilaiKinerja")
            {
                result = new users_nilai_kinerja
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else if (kategori == "Pendidikan")
            {
                result = new users_pendidikan
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"]),
                    lokasi = obj["lokasi"],
                    tingkat = obj["tingkat"],
                    tgl_awal = (obj["tgl_awal"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = (obj["tgl_akhir"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if ((kategori == "PengalamanKerjaLuar") || (kategori == "PengalamanKerjaPertamina"))
            {
                result = new users_pengalaman_kerja
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"]),
                    nama_perusahaan = obj["nama_perusahaan"],
                    jabatan = obj["jabatan"],
                    lokasi = obj["lokasi"],
                    tgl_awal = (obj["tgl_awal"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = (obj["tgl_akhir"] == "") ? (DateTime?)null : DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if (kategori == "Penghargaan")
            {
                result = new users_penghargaan
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else if (kategori == "Penugasan")
            {
                result = new users_penugasan
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else if (kategori == "RiwayatGolUpahPersero")
            {
                result = new users_riwayat_gol_upah_persero
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"]),
                    gol_upah_persero = obj["gol_upah_persero"],
                    tgl_awal = DateTime.Parse(obj["tgl_awal"]),
                    tgl_akhir = DateTime.Parse(obj["tgl_akhir"])
                };
            }
            else if (kategori == "Sertifikasi")
            {
                result = new users_sertifikasi
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"])
                };
            }
            else /*if (kategori == "TindakanDisiplin")*/
            {
                result = new users_tindakan_disiplin
                {
                    id = int.Parse(obj["id"]),
                    user_id = int.Parse(obj["user_id"])
                };
            }
            return result;
        }

        private object castModelJson(string kategori, object dbSetResult)
        {
            object result;
            if (kategori == "Keluarga")
            {
                users_keluarga objTemp = (users_keluarga)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                    hubungan = objTemp.hubungan,
                    nama = objTemp.nama,
                    tempat_lahir = objTemp.tempat_lahir,
                    tgl_lahir = objTemp.tgl_lahir.HasValue ? ((DateTime)objTemp.tgl_lahir).ToString("dd/MM/yyyy") : ""
                };
            }
            else if (kategori == "Kursus")
            {
                users_kursus objTemp = (users_kursus)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                    tipe = objTemp.tipe,
                    nama = objTemp.nama,
                    lokasi = objTemp.lokasi,
                    hasil = objTemp.hasil,
                    tgl_awal = objTemp.tgl_awal.HasValue ? ((DateTime)objTemp.tgl_awal).ToString("dd/MM/yyyy") : "",
                    tgl_akhir = objTemp.tgl_akhir.HasValue ? ((DateTime)objTemp.tgl_akhir).ToString("dd/MM/yyyy") : ""
                };
            }
            else if (kategori == "NilaiKinerja")
            {
                users_nilai_kinerja objTemp = (users_nilai_kinerja)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                };
            }
            else if (kategori == "Pendidikan")
            {
                users_pendidikan objTemp = (users_pendidikan)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                    tingkat = objTemp.tingkat,
                    lokasi = objTemp.lokasi,
                    tgl_awal = objTemp.tgl_awal.HasValue ? ((DateTime)objTemp.tgl_awal).ToString("dd/MM/yyyy") : "",
                    tgl_akhir = objTemp.tgl_akhir.HasValue ? ((DateTime)objTemp.tgl_akhir).ToString("dd/MM/yyyy") : ""
                };
            }
            else if ((kategori == "PengalamanKerjaLuar") || (kategori == "PengalamanKerjaPertamina"))
            {
                users_pengalaman_kerja objTemp = (users_pengalaman_kerja)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                    nama_perusahaan = objTemp.nama_perusahaan,
                    jabatan = objTemp.jabatan,
                    lokasi = objTemp.lokasi,
                    tgl_awal = objTemp.tgl_awal.HasValue ? ((DateTime)objTemp.tgl_awal).ToString("dd/MM/yyyy") : "",
                    tgl_akhir = objTemp.tgl_akhir.HasValue ? ((DateTime)objTemp.tgl_akhir).ToString("dd/MM/yyyy") : ""
                };
            }
            else if (kategori == "Penghargaan")
            {
                users_penghargaan objTemp = (users_penghargaan)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                };
            }
            else if (kategori == "Penugasan")
            {
                users_penugasan objTemp = (users_penugasan)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                };
            }
            else if (kategori == "RiwayatGolUpahPersero")
            {
                users_riwayat_gol_upah_persero objTemp = (users_riwayat_gol_upah_persero)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                    gol_upah_persero = objTemp.gol_upah_persero,
                    tgl_awal = ((DateTime)objTemp.tgl_awal).ToString("dd/MM/yyyy"),
                    tgl_akhir = ((DateTime)objTemp.tgl_akhir).ToString("dd/MM/yyyy")
                };
            }
            else if (kategori == "Sertifikasi")
            {
                users_sertifikasi objTemp = (users_sertifikasi)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                };
            }
            else /*if (kategori == "TindakanDisiplin")*/
            {
                users_tindakan_disiplin objTemp = (users_tindakan_disiplin)dbSetResult;
                result = new
                {
                    id = objTemp.id,
                    user_id = objTemp.user_id,
                };
            }
            return result;
        }

        #endregion

        #region detail employee view

        public ActionResult KeluargaView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult KursusView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult NilaiKinerjaView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult PendidikanView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult PengalamanKerjaPertaminaView(int userId)
        {
            ViewBag.userId = userId;
            ViewBag.nama_perusahaan = "Pertamina";
            return PartialView();
        }

        public ActionResult PengalamanKerjaLuarView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult PenghargaanView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult PenugasanView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult RiwayatGolUpahPerseroView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult SertifikasiView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        public ActionResult TindakanDisiplinView(int userId)
        {
            ViewBag.userId = userId;
            return PartialView();
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //sampah tapi sayang dibuangnya
        //private void deleteDetailEmployee(int id, string kategori)
        //{
        //    if (kategori == "Keluarga")
        //    {
        //        users_keluarga users_keluarga = db.users_keluarga.Find(id);
        //        db.users_keluarga.Remove(users_keluarga);
        //    }
        //    else if (kategori == "Kursus")
        //    {
        //        users_kursus users_kursus = db.users_kursus.Find(id);
        //        db.users_kursus.Remove(users_kursus);
        //    }
        //    else if (kategori == "NilaiKinerja")
        //    {
        //        users_nilai_kinerja users_nilai_kinerja = db.users_nilai_kinerja.Find(id);
        //        db.users_nilai_kinerja.Remove(users_nilai_kinerja);
        //    }
        //    else if (kategori == "Pendidikan")
        //    {
        //        users_pendidikan users_pendidikan = db.users_pendidikan.Find(id);
        //        db.users_pendidikan.Remove(users_pendidikan);
        //    }
        //    else if ((kategori == "PengalamanKerjaLuar") || (kategori == "PengalamanKerjaPertamina"))
        //    {
        //        users_pengalaman_kerja users_pengalaman_kerja = db.users_pengalaman_kerja.Find(id);
        //        db.users_pengalaman_kerja.Remove(users_pengalaman_kerja);
        //    }
        //    else if (kategori == "Penghargaan")
        //    {
        //        users_penghargaan users_penghargaan = db.users_penghargaan.Find(id);
        //        db.users_penghargaan.Remove(users_penghargaan);
        //    }
        //    else if (kategori == "Penugasan")
        //    {
        //        users_penugasan users_penugasan = db.users_penugasan.Find(id);
        //        db.users_penugasan.Remove(users_penugasan);
        //    }
        //    else if (kategori == "RiwayatGolUpahPersero")
        //    {
        //        users_riwayat_gol_upah_persero users_riwayat_gol_upah_persero = db.users_riwayat_gol_upah_persero.Find(id);
        //        db.users_riwayat_gol_upah_persero.Remove(users_riwayat_gol_upah_persero);
        //    }
        //    else if (kategori == "Sertifikasi")
        //    {
        //        users_sertifikasi users_sertifikasi = db.users_sertifikasi.Find(id);
        //        db.users_sertifikasi.Remove(users_sertifikasi);
        //    }
        //    else /*if (kategori == "TindakanDisiplin")*/
        //    {
        //        users_tindakan_disiplin users_tindakan_disiplin = db.users_tindakan_disiplin.Find(id);
        //        db.users_tindakan_disiplin.Remove(users_tindakan_disiplin);
        //    }
        //}

    }
}