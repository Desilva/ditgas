using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using relmon.Models;

namespace relmon.Utilities
{
    public class ExcelReader
    {
        #region const & var
        private RelmonStoreEntities db = new RelmonStoreEntities();
        private Dictionary<string, int> DbId; //Mapping kode excel sama ID database

        //Nama Sheet
        private const string SHEET_PERUSAHAAN = "Perusahaan";
        private const string SHEET_JABATAN = "Jabatan";
        private const string SHEET_EMPLOYEE = "Employee";

        //Starting Row For Each Sheet
        private const int START_PERUSAHAAN = 2;
        private const int START_JABATAN = 2;
        private const int START_EMPLOYEE = 2;

        //Sheet Perusahaan
        private const int PERUSAHAAN_KODE       = 0;
        private const int PERUSAHAAN_NAMA       = 1;
        private const int PERUSAHAAN_PARENT     = 2;
        private const int PERUSAHAAN_DESKRIPSI  = 3;

        //Sheet Jabatan
        private const int JABATAN_KODE          = 0;
        private const int JABATAN_NAMA          = 1;
        private const int JABATAN_PERUSAHAAN    = 2;
        private const int JABATAN_PARENT        = 3;
        private const int JABATAN_GOLONGAN      = 4;
        private const int JABATAN_COST_CENTRE   = 5;
        private const int JABATAN_DESKRIPSI     = 6;

        //Sheet Employee
        private const int EMPLOYEE_NOPEK = 0;
        private const int EMPLOYEE_NAMA = 1;
        private const int EMPLOYEE_TGL_LAHIR = 2;
        private const int EMPLOYEE_TGL_MPPK = 3;
        private const int EMPLOYEE_TGL_PENSIUN = 4;
        private const int EMPLOYEE_SUB_AREA = 5;
        private const int EMPLOYEE_TGL_AKTIF = 6;
        private const int EMPLOYEE_TMT_DAPEN_DPLK = 7;
        private const int EMPLOYEE_JABATAN = 8;
        private const int EMPLOYEE_TMT_JABATAN = 9;
        private const int EMPLOYEE_TMT_GOL_JABATAN = 10;
        private const int EMPLOYEE_GOL_UPAH_PERSERO = 11;
        private const int EMPLOYEE_TMT_GOL_UPAH_PERSERO = 12;
        private const int EMPLOYEE_GOL_UPAH_AP = 13;
        private const int EMPLOYEE_TMT_GOL_UPAH_AP = 14;
        private const int EMPLOYEE_LAYERING = 15;
        private const int EMPLOYEE_PENDIDIKAN_TERAKHIR = 16;
        private const int EMPLOYEE_SUSUNAN_KELUARGA = 17;

        #endregion

        public List<string> LoadData(string filename)
        {
            Excel.Application app;
            Excel.Workbook book;
            Excel.Range ShtRange;
            List<object> temp;
            List<string> err;
            int i, j, sheetNum = 0;
            DbId = new Dictionary<string, int>();

            app = new Excel.Application();
            book = app.Workbooks.Open(Filename: filename);

            err = new List<string>();
            foreach (Excel.Worksheet sheet in book.Sheets)
            {
                ShtRange = sheet.UsedRange;
                string a = sheet.Name;
                for (i = 2; i <= ShtRange.Rows.Count; i++)
                {
                    temp = new List<object>();
                    for (j = 1; j <= ShtRange.Columns.Count; j++)
                    {
                        if ((ShtRange.Cells[i, j] as Excel.Range).Value2 == null)
                            temp.Add("");
                        else
                            temp.Add((ShtRange.Cells[i, j] as Excel.Range).Value2.ToString());
                    }

                    string errTemp = "";
                    if (sheetNum == 0) //Perusahaan
                    {
                        errTemp = savePerusahaan(temp, i);
                    }
                    else if (sheetNum == 1) // Jabatan
                    {
                        errTemp = saveJabatan(temp, i);
                    }
                    else if (sheetNum == 2) // Employee
                    {
                        errTemp = saveEmployee(temp, i);
                    }
                    if (errTemp != "")
                    {
                        err.Add(errTemp);
                    };
                }
                sheetNum++;
            }
            book.Close(true, Missing.Value, Missing.Value);
            app.Quit();

            return err;
        }

        private string savePerusahaan(List<object> data, int i)
        {
            string err = "";

            if (data[PERUSAHAAN_KODE].ToString() != "")
            {
                if (data[PERUSAHAAN_NAMA].ToString() != "")
                {
                    string namaPerusahaan = data[PERUSAHAAN_NAMA].ToString();
                    company c = db.companies
                        .Where(w => w.nama == namaPerusahaan)
                        .FirstOrDefault();

                    if (c == null) //Add perusahaan baru
                    {
                        company newCompany = new company()
                        {
                            nama = data[PERUSAHAAN_NAMA].ToString(),
                            parent = (data[PERUSAHAAN_PARENT].ToString() != "") ? DbId[data[PERUSAHAAN_PARENT].ToString()] : (int?) 0,
                            deskripsi = data[PERUSAHAAN_DESKRIPSI].ToString()
                        };
                        db.companies.Add(newCompany);
                        db.SaveChanges();
                        DbId[data[PERUSAHAAN_KODE].ToString()] = newCompany.id;
                    }
                    else //Update perusahaan
                    {
                        DbId[data[PERUSAHAAN_KODE].ToString()] = c.id;
                        if (data[PERUSAHAAN_PARENT].ToString() != "") c.parent = DbId[data[PERUSAHAAN_PARENT].ToString()];
                        if (data[PERUSAHAAN_DESKRIPSI].ToString() != "") c.deskripsi = data[PERUSAHAAN_DESKRIPSI].ToString();
                        db.Entry(c).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    err = "Nama Perusahaan pada row-"+i+" Kosong";
                }
            }
            else
            {
                err = "Kode Perusahaan pada row-" + i + " Kosong";
            }

            return err;
        }

        private string saveJabatan(List<object> data, int i)
        {
            string err = "";

            if (data[JABATAN_KODE].ToString() != "")
            {
                if (data[JABATAN_NAMA].ToString() != "")
                {
                    if (data[JABATAN_PERUSAHAAN].ToString() != "")
                    {
                        string namaJabatan = data[JABATAN_NAMA].ToString();
                        int idPerusahaan = DbId[data[JABATAN_PERUSAHAAN].ToString()];
                        users_jabatan uj = db.users_jabatan
                            .Where(w => w.nama == namaJabatan)
                            .Where(w => w.company_id == idPerusahaan)
                            .FirstOrDefault();

                        if (uj == null) //Add jabatan baru
                        {
                            users_jabatan newJabatan = new users_jabatan()
                            {
                                nama = data[JABATAN_NAMA].ToString(),
                                parent = (data[JABATAN_PARENT].ToString() != "") ? DbId[data[JABATAN_PARENT].ToString()] : (int?)null,
                                deskripsi = data[JABATAN_DESKRIPSI].ToString(),
                                company_id = (data[JABATAN_PERUSAHAAN].ToString() != "") ? DbId[data[JABATAN_PERUSAHAAN].ToString()] : (int?)null,
                                golongan = data[JABATAN_GOLONGAN].ToString(),
                                //harus dimasukin dulu ID nya, siapatau cost_centre nya t diri sendiri. last_id gmn? biar sekalian sama update gimana?
                                //cost_centre = (data[JABATAN_COST_CENTRE].ToString() != "") ? DbId[data[JABATAN_COST_CENTRE].ToString()] : (int?)null
                            };
                            db.users_jabatan.Add(newJabatan);
                            db.SaveChanges();
                            DbId[data[JABATAN_KODE].ToString()] = newJabatan.id;
                        }
                        else //Update jabatan
                        {
                            DbId[data[JABATAN_KODE].ToString()] = uj.id;
                            if (data[JABATAN_PARENT].ToString() != "") uj.parent = DbId[data[JABATAN_PARENT].ToString()];
                            if (data[JABATAN_GOLONGAN].ToString() != "") uj.golongan = data[JABATAN_GOLONGAN].ToString();
                            //if (data[JABATAN_COST_CENTRE].ToString() != "") uj.cost_centre = DbId[data[JABATAN_COST_CENTRE].ToString()];
                            if (data[JABATAN_DESKRIPSI].ToString() != "") uj.deskripsi = data[JABATAN_DESKRIPSI].ToString();
                            db.Entry(uj).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        uj = db.users_jabatan
                            .Where(w => w.nama == namaJabatan)
                            .Where(w => w.company_id == idPerusahaan)
                            .FirstOrDefault();
                        if (data[JABATAN_COST_CENTRE].ToString() != "") uj.cost_centre = DbId[data[JABATAN_COST_CENTRE].ToString()];
                        db.Entry(uj).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        err = "Perusahaan pada row-" + i + " Jabatan Kosong";
                    }
                }
                else
                {
                    err = "Nama Jabatan pada row-" + i + " Kosong";
                }
            }
            else
            {
                err = "Kode Jabatan pada row-" + i + " Kosong";
            }

            return err;
        }

        private string saveEmployee(List<object> data, int i)
        {
            string err = "";

            if (data[EMPLOYEE_NOPEK].ToString() != "")
            {
                string noPekEmployee = data[EMPLOYEE_NOPEK].ToString();
                user u = db.users
                    .Where(w => w.no_pekerja == noPekEmployee)
                    .FirstOrDefault();

                if (u == null) //Add user baru
                {
                    user newUser = new user()
                    {
                        no_pekerja = data[EMPLOYEE_NOPEK].ToString(),
                        fullname = data[EMPLOYEE_NAMA].ToString(),
                        tgl_lahir = data[EMPLOYEE_TGL_LAHIR].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_LAHIR].ToString())) : (DateTime?)null,
                        tgl_mppk = data[EMPLOYEE_TGL_MPPK].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_MPPK].ToString())) : (DateTime?)null,
                        tgl_pensiun = data[EMPLOYEE_TGL_PENSIUN].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_PENSIUN].ToString())) : (DateTime?)null,
                        sub_area = data[EMPLOYEE_SUB_AREA].ToString(),
                        tgl_aktif = data[EMPLOYEE_TGL_AKTIF].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_AKTIF].ToString())) : (DateTime?)null,
                        tmt_dapen_dplk = data[EMPLOYEE_TMT_DAPEN_DPLK].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_DAPEN_DPLK].ToString())) : (DateTime?)null,
                        tmt_jabatan = data[EMPLOYEE_TMT_JABATAN].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_JABATAN].ToString())) : (DateTime?)null,
                        tmt_gol_jabatan = data[EMPLOYEE_TMT_GOL_JABATAN].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_GOL_JABATAN].ToString())) : (DateTime?)null,
                        gol_upah_persero = data[EMPLOYEE_GOL_UPAH_PERSERO].ToString(),
                        tmt_gol_upah_persero = data[EMPLOYEE_TMT_GOL_UPAH_PERSERO].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_GOL_UPAH_PERSERO].ToString())) : (DateTime?)null,
                        gol_upah_ap = data[EMPLOYEE_GOL_UPAH_AP].ToString(),
                        tmt_gol_upah_ap = data[EMPLOYEE_TMT_GOL_UPAH_AP].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_GOL_UPAH_AP].ToString())) : (DateTime?)null,
                        layering = data[EMPLOYEE_LAYERING].ToString(),
                        pendidikan_terakhir = data[EMPLOYEE_PENDIDIKAN_TERAKHIR].ToString(),
                        susunan_keluarga = data[EMPLOYEE_SUSUNAN_KELUARGA].ToString(),
                        rm_role = (data[EMPLOYEE_JABATAN].ToString() != "") ? DbId[data[EMPLOYEE_JABATAN].ToString()] : (int?)null,
                    };
                    db.users.Add(newUser);
                    db.SaveChanges();
                    DbId[data[EMPLOYEE_NOPEK].ToString()] = newUser.id;
                }
                else //Update user
                {
                    DbId[data[EMPLOYEE_NOPEK].ToString()] = u.id;
                    if (data[EMPLOYEE_NAMA].ToString() != "") u.fullname = data[EMPLOYEE_NAMA].ToString();
                    if (data[EMPLOYEE_TGL_LAHIR].ToString() != "") u.tgl_lahir = data[EMPLOYEE_TGL_LAHIR].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_LAHIR].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_TGL_MPPK].ToString() != "") u.tgl_mppk = data[EMPLOYEE_TGL_MPPK].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_MPPK].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_TGL_PENSIUN].ToString() != "") u.tgl_pensiun = data[EMPLOYEE_TGL_PENSIUN].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_PENSIUN].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_SUB_AREA].ToString() != "") u.sub_area = data[EMPLOYEE_SUB_AREA].ToString();
                    if (data[EMPLOYEE_TGL_AKTIF].ToString() != "") u.tgl_aktif = data[EMPLOYEE_TGL_AKTIF].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TGL_AKTIF].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_TMT_DAPEN_DPLK].ToString() != "") u.tmt_dapen_dplk = data[EMPLOYEE_TMT_DAPEN_DPLK].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_DAPEN_DPLK].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_TMT_JABATAN].ToString() != "") u.tmt_jabatan = data[EMPLOYEE_TMT_JABATAN].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_JABATAN].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_TMT_GOL_JABATAN].ToString() != "") u.tmt_gol_jabatan = data[EMPLOYEE_TMT_GOL_JABATAN].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_GOL_JABATAN].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_GOL_UPAH_PERSERO].ToString() != "") u.gol_upah_persero = data[EMPLOYEE_GOL_UPAH_PERSERO].ToString();
                    if (data[EMPLOYEE_TMT_GOL_UPAH_PERSERO].ToString() != "") u.tmt_gol_upah_persero = data[EMPLOYEE_TMT_GOL_UPAH_PERSERO].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_GOL_UPAH_PERSERO].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_GOL_UPAH_AP].ToString() != "") u.gol_upah_ap = data[EMPLOYEE_GOL_UPAH_AP].ToString();
                    if (data[EMPLOYEE_TMT_GOL_UPAH_AP].ToString() != "") u.tmt_gol_upah_ap = data[EMPLOYEE_TMT_GOL_UPAH_AP].ToString() != "" ? DateTime.FromOADate(double.Parse(data[EMPLOYEE_TMT_GOL_UPAH_AP].ToString())) : (DateTime?)null;
                    if (data[EMPLOYEE_LAYERING].ToString() != "") u.layering = data[EMPLOYEE_LAYERING].ToString();
                    if (data[EMPLOYEE_PENDIDIKAN_TERAKHIR].ToString() != "") u.pendidikan_terakhir = data[EMPLOYEE_PENDIDIKAN_TERAKHIR].ToString();
                    if (data[EMPLOYEE_SUSUNAN_KELUARGA].ToString() != "") u.susunan_keluarga = data[EMPLOYEE_SUSUNAN_KELUARGA].ToString();
                    if (data[EMPLOYEE_JABATAN].ToString() != "") u.rm_role = (data[EMPLOYEE_JABATAN].ToString() != "") ? DbId[data[EMPLOYEE_JABATAN].ToString()] : (int?)null;
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                err = "No Pekerja pada row-" + i + " Kosong";
            }

            return err;
        }

        public string generateError(List<string> err) {
            string html = "";
            if(err.Count > 0){
                html += "<b>LOG</b>";
                html += "<br/>";
                html += "<ul>";
                foreach (string x in err) {
                    html += "<li>";
                    html += x;
                    html += "</li>";
                }
                html += "</ul>";
            }
            return html;
        }
    }

}