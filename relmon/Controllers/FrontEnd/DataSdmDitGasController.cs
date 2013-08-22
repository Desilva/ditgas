﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using relmon.Models;
using relmon.Utilities;

namespace relmon.Controllers.FrontEnd
{
    public class DataSdmDitGasController : Controller
    {
        //
        // GET: /DataSdmDitGas/
        private RelmonStoreEntities db = new RelmonStoreEntities();
        
        public ActionResult Index()
        {
            ViewBag.selectedMenu = "sdm";
            if (isUserAuthorized())
            {
                ViewBag.stat = "SDM";
                return View();
            }
            return RedirectToAction("LogOn", "Account", new { returnURL = "/DataSdmDitGas" });
        }

        //
        //Select ajax binding
        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return binding();
        }

        private bool isUserAuthorized()
        {
            if (Session["role"] != null)
            {
                int userRole = (int) Session["role"];
                if (userRole > 0)
                {
                    var usersJabatan = db.users_jabatan.Where(uj => uj.id == userRole).FirstOrDefault();
                    var userCompany = db.companies.Where(c => c.id == usersJabatan.company_id).FirstOrDefault();
                    
                    if (userCompany.parent == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        private List<users_jabatan> getChildrenJabatan(int jabatanId, int level)
        {
            List<users_jabatan> result = new List<users_jabatan>();

            var usersJabatan = db.users_jabatan.Where(uj => uj.parent == jabatanId).ToList();
            foreach (users_jabatan uj_child in usersJabatan)
            {
                uj_child.level = level;
                result.Add(uj_child);
                if (db.users_jabatan.Where(uj => uj.parent == uj_child.id).Count() > 0)
                {
                    List<users_jabatan> result_child = new List<users_jabatan>();
                    result_child = getChildrenJabatan(uj_child.id, level+1);
                    foreach (users_jabatan uj_child2 in result_child)
                    {
                        uj_child2.level = level;
                        result.Add(uj_child2);
                    }
                }
            }
            return result;
        }

        private List<user> getChildrenUser(int jabatanId)
        {
            List<user> result = new List<user>();
            var allChildren = getChildrenJabatan(jabatanId, 0);

            foreach (users_jabatan uj_child in allChildren)
            {
                var usersInJabatan = db.users.Where(u => u.rm_role == uj_child.id).ToList();

                if (usersInJabatan.Count() == 0)
                {
                    result.Add(new user
                    {
                        fullname = "Vacant",
                        users_jabatan_parent = new users_jabatan_parent
                        {
                            id = uj_child.id,
                            company_id = uj_child.company_id,
                            deskripsi = uj_child.deskripsi,
                            parent = uj_child.parent,
                            nama = uj_child.nama,
                            golongan = uj_child.golongan,
                            nama_parent = db.users_jabatan.Find(uj_child.parent).nama,
                            nama_cost_centre = (uj_child.cost_centre == null) ? "" : db.users_jabatan.Find(uj_child.cost_centre).nama
                        }
                    });
                }
                else
                {
                    foreach (user u_child in usersInJabatan)
                    {
                        users_jabatan ujp = db.users_jabatan.Where(uj => uj.id == u_child.rm_role).FirstOrDefault();
                        u_child.users_jabatan_parent = new users_jabatan_parent
                        {
                            id = ujp.id,
                            company_id = ujp.company_id,
                            deskripsi = ujp.deskripsi,
                            parent = ujp.parent,
                            nama = ujp.nama,
                            golongan = ujp.golongan,
                            nama_parent = db.users_jabatan.Find(ujp.parent).nama,
                            nama_cost_centre = (ujp.cost_centre == null) ? "" : db.users_jabatan.Find(ujp.cost_centre).nama
                        };
                        result.Add(u_child);
                    }
                }
            }
            
            return result;
        }

        private List<user> getChildrenUserSorted(int jabatanId)
        {
            List<user> resultTemp = getChildrenUser(jabatanId);
            List<user> result = new List<user>();

            foreach (user u in resultTemp)
            {


                result.Add(u);
            }

            return result;
        }

        private List<user> getChildrenUserPensiun(int jabatanId)
        {
            List<user> result = new List<user>();
            var allChildren = getChildrenJabatan(jabatanId, 0);
            DateTime oneYearFromNow = DateTime.Now.AddYears(1);

            foreach (users_jabatan uj_child in allChildren)
            {
                var usersInJabatan = db.users
                    .Where(u => u.rm_role == uj_child.id)
                    .Where(u => (oneYearFromNow >= u.tgl_pensiun) || (u.status_pekerja == "promosi") || (u.status_pekerja == "mutasi"))
                    .ToList();

                foreach (user u_child in usersInJabatan)
                {
                    users_jabatan ujp = db.users_jabatan.Where(uj => uj.id == u_child.rm_role).FirstOrDefault();
                    u_child.users_jabatan_parent = new users_jabatan_parent
                    {
                        id = ujp.id,
                        company_id = ujp.company_id,
                        deskripsi = ujp.deskripsi,
                        parent = ujp.parent,
                        nama = ujp.nama,
                        golongan = ujp.golongan,
                        nama_parent = db.users_jabatan.Find(ujp.parent).nama
                    };
                    if (DateTime.Now.AddMonths(6) >= u_child.tgl_pensiun) // kalo tgl setengah taun
                    {
                        u_child.status_pekerja = "MPPK";
                        db.Entry(u_child).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                        u_child.status_pensiun = "red";
                    }
                    else if (DateTime.Now.AddMonths(12) >= u_child.tgl_pensiun) // kalo masih setaun
                    {
                        u_child.status_pekerja = "MPPK";
                        db.Entry(u_child).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                        u_child.status_pensiun = "yellow";
                    }
                    else if (u_child.status_pekerja == "Promosi") // kalo promosi
                    {
                        u_child.status_pensiun = "-";
                    }
                    else //if (u_child.status_pekerja == "Mutasi") // kalo mutasi
                    {
                        u_child.status_pensiun = "-";
                    }
                    result.Add(u_child);
                }
            }

            return result;
        }

        //select employes
        private ViewResult binding()
        {
            int userRole = (int)Session["role"];
            List<user> allChildren = getChildrenUserSorted(userRole);

            return View(new GridModel<user>
            {
                Data = allChildren
            });

            //List<EmployeeWrapper> eList = new List<EmployeeWrapper>();
            //for (int i = 0; i <= 5; i++)
            //{
            //    EmployeeWrapper e = new EmployeeWrapper
            //    {
            //        id = 1,
            //        no_pekerja = "00703828",
            //        nama = "Eddy Sjahbuddin",
            //        jabatan = "Direktur Komersial&Teknis Pertagas Niaga",
            //        lokasi = "R500 - Korporat",
            //        direktorat = "1121 - Kantor Pusat PERTAGAS",
            //        tempat_lahir = "BOGOR",
            //        jenis_kelamin = "Laki-Laki",
            //        agama = "Islam",
            //        npwp = "14.104.467.7-002.000",
            //        tanggal_dinas_aktif = DateTime.Now,
            //        notice = "Pensiun dalam 35 hari"
            //    };
            //    eList.Add(e);
            //}

            //return View(new GridModel<EmployeeWrapper>
            //{
            //    Data = eList
            //});
        }

        //select employes yg mau pensiun
        private ViewResult bindingPensiun()
        {
            int userRole = (int)Session["role"];
            List<user> allChildren = getChildrenUserPensiun(userRole);

            return View(new GridModel<user>
            {
                Data = allChildren
            });
        }

        //select employes
        private ViewResult bindingKandidat()
        {
            //int userRole = (int)Session["role"];
            //var allChildren = getChildrenUser(userRole);

            //return View(new GridModel<user>
            //{
            //    Data = allChildren
            //});
            List<EmployeeWrapper> eList = new List<EmployeeWrapper>();
            //for (int i = 0; i < 1; i++)
            //{
            EmployeeWrapper e = new EmployeeWrapper
            {
                id = 1,
                no_pekerja = "00703828",
                nama = "Eko",
                jabatan = "Direktur Komersial&Teknis Pertagas Niaga",
                lokasi = "R500 - Korporat",
                direktorat = "1121 - Kantor Pusat PERTAGAS",
                tempat_lahir = "BOGOR",
                jenis_kelamin = "Laki-Laki",
                agama = "Islam",
                npwp = "14.104.467.7-002.000",
                tanggal_dinas_aktif = DateTime.Now,
                notice = "Pensiun dalam 35 hari"
            };
            eList.Add(e);

            e = new EmployeeWrapper
            {
                id = 1,
                no_pekerja = "728696",
                nama = "Rico Amanto R",
                jabatan = "Direktur Komersial&Teknis Pertagas Niaga",
                lokasi = "R500 - Korporat",
                direktorat = "1121 - Kantor Pusat PERTAGAS",
                tempat_lahir = "BOGOR",
                jenis_kelamin = "Laki-Laki",
                agama = "Islam",
                npwp = "14.104.467.7-002.000",
                tanggal_dinas_aktif = DateTime.Now,
                notice = "Pensiun dalam 35 hari"
            };
            eList.Add(e);

            e = new EmployeeWrapper
            {
                id = 1,
                no_pekerja = "718692",
                nama = "Johannes Daud F. Saragih",
                jabatan = "Direktur Komersial&Teknis Pertagas Niaga",
                lokasi = "R500 - Korporat",
                direktorat = "1121 - Kantor Pusat PERTAGAS",
                tempat_lahir = "BOGOR",
                jenis_kelamin = "Laki-Laki",
                agama = "Islam",
                npwp = "14.104.467.7-002.000",
                tanggal_dinas_aktif = DateTime.Now,
                notice = "Pensiun dalam 35 hari"
            };
            eList.Add(e);
            //}

            return View(new GridModel<EmployeeWrapper>
            {
                Data = eList
            });
        }

        //
        //Select ajax binding
        [GridAction]
        public ActionResult _SelectMppAjaxEditing()
        {
            return bindingPensiun();
        }

        //
        //Select ajax binding
        [GridAction]
        public ActionResult _SelectKandidatAjaxEditing()
        {
            return bindingKandidat();
        }

        public ActionResult Demografi()
        {
            ViewBag.selectedMenu = "sdm";
            if (isUserAuthorized())
            {
                ViewBag.stat = "SDM";
                return View();
            }
            return RedirectToAction("LogOn", "Account", new { returnURL = "/DataSdmDitGas/Demografi" });
        }

        public ActionResult MasaJabatan()
        {
            ViewBag.selectedMenu = "sdm";
            if (isUserAuthorized())
            {
                ViewBag.stat = "SDM";
                return View();
            }
            return RedirectToAction("LogOn", "Account", new { returnURL = "/DataSdmDitGas/MasaJabatan" });
        }
        
        public ActionResult Detail(int userId)
        {
            if (userId == 0)
            {
                return RedirectToAction("Index");
            }
            if (isUserAuthorized())
            {
                // ngambil detail user
                var userselected = db.users.Where(u => u.id == userId).FirstOrDefault();
                var ujp = db.users_jabatan.Where(uj => uj.id == userselected.rm_role).FirstOrDefault();
                userselected.users_jabatan_parent = new users_jabatan_parent
                {
                    id = ujp.id,
                    company_id = ujp.company_id,
                    deskripsi = ujp.deskripsi,
                    parent = ujp.parent,
                    nama = ujp.nama,
                    golongan = ujp.golongan
                };
                ViewBag.userselected = userselected;

                // ngambil pendidikan user
                var pendidikanList = db.users_pendidikan.Where(u => u.user_id == userId).ToList();
                ViewBag.pendidikanList = pendidikanList;

                // ngambil kursus user
                var kursusList = db.users_kursus.Where(u => u.user_id == userId).ToList();
                ViewBag.kursusList = kursusList;

                // ngambil sertifikasi & keanggotaan profesi user
                var sertifikasiList = db.users_sertifikasi.Where(u => u.user_id == userId).ToList();
                ViewBag.sertifikasiList = sertifikasiList;

                // ngambil pengalaman kerja di pertamina user
                var pengalamanKerjaPertaminaList = db.users_pengalaman_kerja.Where(u => u.user_id == userId).Where(u => u.nama_perusahaan == "Pertamina").ToList();
                ViewBag.pengalamanKerjaPertaminaList = pengalamanKerjaPertaminaList;

                // ngambil pengalaman kerja di luar pertamina user
                var pengalamanKerjaLuarList = db.users_pengalaman_kerja.Where(u => u.user_id == userId).Where(u => u.nama_perusahaan != "Pertamina").ToList();
                ViewBag.pengalamanKerjaLuarList = pengalamanKerjaLuarList;

                // ngambil status terakhir user
                // var pendidikanList = db.users_pendidikan.Where(u => u.user_id == userId).ToList();
                // ViewBag.pendidikanList = pendidikanList;

                // ngambil riwayat gol_upah_persero user
                var riwayatGolUpahPerseroList = db.users_riwayat_gol_upah_persero.Where(u => u.user_id == userId).ToList();
                ViewBag.riwayatGolUpahPerseroList = riwayatGolUpahPerseroList;

                // ngambil nilai kinerja user
                var nilaiKinerjaList = db.users_nilai_kinerja.Where(u => u.user_id == userId).ToList();
                ViewBag.nilaiKinerjaList = nilaiKinerjaList;

                // ngambil penugasan user
                var penugasanList = db.users_penugasan.Where(u => u.user_id == userId).ToList();
                ViewBag.penugasanList = penugasanList;

                // ngambil penghargaan user
                var penghargaanList = db.users_penghargaan.Where(u => u.user_id == userId).ToList();
                ViewBag.penghargaanList = penghargaanList;

                // ngambil tindakan disiplin user
                var tindakanDisiplinList = db.users_tindakan_disiplin.Where(u => u.user_id == userId).ToList();
                ViewBag.tindakanDisiplinList = tindakanDisiplinList;

                // ngambil data keluarga user
                var keluargaList = db.users_keluarga.Where(u => u.user_id == userId).ToList();
                ViewBag.keluargaList = keluargaList;

                return View();
            }
            return RedirectToAction("LogOn", "Account", new { returnURL = "/DataSdmDitGas" });
        }



        #region menu demografi

        public ActionResult Org()
        {
            DemografiGenerator demografiGenerator = new DemografiGenerator();
            Tuple<TableContainer, List<ChartContainer>> result = demografiGenerator.getDemografiOrganisasi(0);
            ViewBag.tableContent = result.Item1;
            ViewBag.chartContent = result.Item2;
            ViewBag.heightGrafik = 600;

            return PartialView();
        }

        public ActionResult DemografiSide()
        {
            return PartialView();
        }

        public ActionResult DemografiTable(string kategori, int countGrafik)
        {
            DemografiGenerator demografiGenerator = new DemografiGenerator();

            if (kategori == "lahirvsgolongan")
            {
                Tuple<TableContainer, List<ChartContainer>> result = demografiGenerator.getDemografiLahirGolongan(0);
                ViewBag.tableContent = result.Item1;
                ViewBag.chartContent = result.Item2;
                ViewBag.heightGrafik = 600;
            }
            else if (kategori == "lahirvspendidikan")
            {
                Tuple<TableContainer, List<ChartContainer>> result = demografiGenerator.getDemografiLahirPendidikan(0);
                ViewBag.tableContent = result.Item1;
                ViewBag.chartContent = result.Item2;
                ViewBag.heightGrafik = 600;
            }
            else if (kategori == "golonganvspendidikan")
            {
                Tuple<TableContainer, List<ChartContainer>> result = demografiGenerator.getDemografiGolonganPendidikan(0);
                ViewBag.tableContent = result.Item1;
                ViewBag.chartContent = result.Item2;
                ViewBag.heightGrafik = 600;
            }
            else if (kategori == "mppkvsgolongan")
            {
                Tuple<TableContainer, List<ChartContainer>> result = demografiGenerator.getDemografiGolonganMPPK(0);
                ViewBag.tableContent = result.Item1;
                ViewBag.chartContent = result.Item2;
                ViewBag.heightGrafik = 250;
            }
            else if (kategori == "mppkvspendidikan")
            {
                Tuple<TableContainer, List<ChartContainer>> result = demografiGenerator.getDemografiPendidikanMPPK(0);
                ViewBag.tableContent = result.Item1;
                ViewBag.chartContent = result.Item2;
                ViewBag.heightGrafik = 250;
            }
            else //if (kategori == "organisasi")
            {
                Tuple<TableContainer, List<ChartContainer>> result = demografiGenerator.getDemografiOrganisasi(0);
                ViewBag.tableContent = result.Item1;
                ViewBag.chartContent = result.Item2;
                ViewBag.heightGrafik = 600;
            }

            ViewBag.countGrafik = countGrafik;
            return PartialView();
        }

        #endregion

    }
}