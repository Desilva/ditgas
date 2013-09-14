using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Configuration;

namespace relmon.Models
{
    public class PageItem
    {
        public string text { get; set; }
        public string name { get; set; }

        public PageItem()
        {
            text = "";
            name = "";
        }

        public PageItem(string text, string name)
        {
            this.text = text;
            this.name = name;
        }

        public static PageItem All = new PageItem("All", "PRT");

        public static PageItem Home = new PageItem("Home", "/Home");

        public static PageItem ProfilDitGas = new PageItem("Profil Dit. Gas", "PRTprofilditgas");
        public static PageItem ProfilDitGas_Profil = new PageItem("Profil", "/ProfilDitgas");
        public static PageItem ProfilDitGas_VisiMisi = new PageItem("Visi Misi", "/DirektoratDitgas");
        public static PageItem ProfilDitGas_Organisasi = new PageItem("Organisasi", "/OrganisasiDitgas");

        public static PageItem SHEQMS = new PageItem("SHE - QMS", "/hse");
        public static PageItem SHEQMS_DitGas = new PageItem("Ditgas", "PRTSHEQMSDitGas");
        public static PageItem SHEQMS_DitGas_KebijakanHSE = new PageItem("Kebijakan HSE", "/SHEQMSAdmin/KebijakanHSE");
        public static PageItem SHEQMS_DitGas_SafetyTalk = new PageItem("Safety Talk", "/SHEQMSAdmin/SafetyTalk");
        //public static PageItem SHEQMS_Ap = new PageItem("Anak Perusahaan", "PRTSHEQMSAp");


        public static PageItem DataBisnis = new PageItem("Data Bisnis", "PRTdatabisnis");

        public static PageItem DataBisnis_DitGas = new PageItem("Dit. Gas", "/DataBisnisDitGas");
        public static PageItem DataBisnis_DitGas_ADART = new PageItem("AD/ART", "/DataBisnisDitGas/AdArt");
        public static PageItem DataBisnis_DitGas_RJPP = new PageItem("RJPP", "/DataBisnisDitGas/Rjpp");
        public static PageItem DataBisnis_DitGas_RKAP = new PageItem("RKAP", "/DataBisnisDitGas/Rkap");
        public static PageItem DataBisnis_DitGas_BussinessReport = new PageItem("Business Report", "/DataBisnisDitGas/BussinessReport");
        public static PageItem DataBisnis_DitGas_KPI = new PageItem("KPI", "/DataBisnisDitGas/Kinerja");
        public static PageItem DataBisnis_DitGas_ProjectStatus = new PageItem("Project Status", "/DataBisnisDitGas/ProjectStatus");
        public static PageItem DataBisnis_DitGas_Product = new PageItem("Product", "/DataBisnisDitGas/Product");

        public static PageItem DataBisnis_JMG = new PageItem("JMG", "/DataBisnisJmg");

        public static PageItem DataBisnis_Ap = new PageItem("Anak Perusahaan", "/DataBisnisAp");
        public static PageItem DataBisnis_ApCompany = new PageItem("", "PRTDataBisnisAp");
        public static PageItem DataBisnis_Ap_Profil = new PageItem("Profil", "/DataBisnisAp/Profil/");
        public static PageItem DataBisnis_Ap_StrukturOrganisasi = new PageItem("Struktur Organisasi", "/DataBisnisAp/StrukturOrganisasi/");
        public static PageItem DataBisnis_Ap_ADART = new PageItem("AD/ART", "/DataBisnisAp/AdArt");
        public static PageItem DataBisnis_Ap_RJPP = new PageItem("RJPP", "/DataBisnisAp/Rjpp");
        public static PageItem DataBisnis_Ap_RKAP = new PageItem("RKAP", "/DataBisnisAp/Rkap");
        public static PageItem DataBisnis_Ap_BussinessReport = new PageItem("Business Report", "/DataBisnisAp/BussinessReport");
        public static PageItem DataBisnis_Ap_KPI = new PageItem("KPI", "/DataBisnisAp/Kinerja");
        public static PageItem DataBisnis_Ap_ProjectStatus = new PageItem("Project Status", "/DataBisnisAp/ProjectStatus");
        public static PageItem DataBisnis_Ap_Product = new PageItem("Product", "/DataBisnisAp/Product");

        public static PageItem DataBisnis_Afiliasi = new PageItem("Afiliasi", "/DataBisnisAfiliasi");
        public static PageItem DataBisnis_AfiliasiCompanyParent = new PageItem("", "PRTDataBisnisAfiliasiParent");
        public static PageItem DataBisnis_AfiliasiCompany = new PageItem("", "PRTDataBisnisAfiliasi");
        public static PageItem DataBisnis_Afiliasi_Profil = new PageItem("Profil", "/DataBisnisAfiliasi/Profil/");
        public static PageItem DataBisnis_Afiliasi_StrukturOrganisasi = new PageItem("Struktur Organisasi", "/DataBisnisAfiliasi/StrukturOrganisasi/");
        public static PageItem DataBisnis_Afiliasi_ADART = new PageItem("AD/ART", "/DataBisnisAfiliasi/AdArt");
        public static PageItem DataBisnis_Afiliasi_RJPP = new PageItem("RJPP", "/DataBisnisAfiliasi/Rjpp");
        public static PageItem DataBisnis_Afiliasi_RKAP = new PageItem("RKAP", "/DataBisnisAfiliasi/Rkap");
        public static PageItem DataBisnis_Afiliasi_BussinessReport = new PageItem("Business Report", "/DataBisnisAfiliasi/BussinessReport");
        public static PageItem DataBisnis_Afiliasi_KPI = new PageItem("KPI", "/DataBisnisAfiliasi/Kinerja");
        public static PageItem DataBisnis_Afiliasi_ProjectStatus = new PageItem("Project Status", "/DataBisnisAfiliasi/ProjectStatus");
        public static PageItem DataBisnis_Afiliasi_Product = new PageItem("Product", "/DataBisnisAfiliasi/Product");


        public static PageItem SDM = new PageItem("SDM", "PRTSDM");
        public static PageItem SDM_DitGas = new PageItem("Ditgas", "PRTSDMDitGas");
        public static PageItem SDM_Ap = new PageItem("Anak Perusahaan", "PRTSDMAp");
        public static PageItem SDM_Afiliasi = new PageItem("Afiliasi", "PRTSDMAfiliasi");
        public static PageItem SDM_AturanSDM = new PageItem("Aturan SDM", "PRTSDMAturan");

        public static PageItem AturanRegulasi = new PageItem("Aturan & Regulasi", "PRTAturanRegulasi");
        public static PageItem AturanRegulasi_Pertamina = new PageItem("Aturan PERTAMINA", "/AturanRegulasi");
        public static PageItem AturanRegulasi_External = new PageItem("Aturan External", "/AturanRegulasi/AturanKementrian");
        public static PageItem AturanRegulasi_Charter = new PageItem("Charter", "/AturanRegulasi/Pedoman");
        public static PageItem AturanRegulasi_GCG = new PageItem("GCG", "/AturanRegulasi/GCG");

        public static PageItem Kontak = new PageItem("Kontak", "/Kontak");

        public static PageItem Kalendar = new PageItem("Kalendar", "/Kalendar");

        public PageItem ConcatText(string text)
        {
            return new PageItem(this.text + text, name);
        }

        public PageItem ConcatName(string name)
        {
            return new PageItem(this.text, this.name + name);
        }
    }

    public class PageTree
    {
        public PageItem pageItem { get; set; } 
        public List<PageTree> child { get; set; }

        //public bool defaultViewValue { get; set; }
        //public bool defaultCreateValue { get; set; }
        //public bool defaultUpdateValue { get; set; }
        //public bool defaultDeleteValue { get; set; }
        //public bool defaultPrintValue { get; set; }

        public bool enableView { get; set; }
        public bool enableCreate { get; set; }
        public bool enableUpdate { get; set; }
        public bool enableDelete { get; set; }
        public bool enablePrint { get; set; }

        private bool validity { get; set; }

        public PageTree()
        {
            this.pageItem = new PageItem();
            this.child = new List<PageTree>();

            //this.defaultViewValue = false;
            //this.defaultCreateValue = false;
            //this.defaultUpdateValue = false;
            //this.defaultDeleteValue = false;
            //this.defaultPrintValue = false;

            this.enableView = true;
            this.enableCreate = true;
            this.enableUpdate = true;
            this.enableDelete = true;
            this.enablePrint = true;

            this.validity = true;
        }

        public PageTree(PageItem pageItem)
        {
            this.pageItem = new PageItem(pageItem.text, pageItem.name);
            this.child = new List<PageTree>();

            //this.defaultViewValue = false;
            //this.defaultCreateValue = false;
            //this.defaultUpdateValue = false;
            //this.defaultDeleteValue = false;
            //this.defaultPrintValue = false;

            this.enableView = true;
            this.enableCreate = true;
            this.enableUpdate = true;
            this.enableDelete = true;
            this.enablePrint = true;

            this.validity = true;
        }

        public PageTree(PageItem pageItem, List<PageTree> child)
        {
            this.pageItem = new PageItem(pageItem.text, pageItem.name);
            this.child = child;

            //this.defaultViewValue = false;
            //this.defaultCreateValue = false;
            //this.defaultUpdateValue = false;
            //this.defaultDeleteValue = false;
            //this.defaultPrintValue = false;

            this.enableView = true;
            this.enableCreate = true;
            this.enableUpdate = true;
            this.enableDelete = true;
            this.enablePrint = true;

            this.validity = true;
        }

        public PageTree setEnableView(bool value)
        {
            this.enableView = value;
            foreach (PageTree child in this.child)
            {
                child.setEnableView(value);
            }
            return this; 
        }

        public PageTree setEnableCreate(bool value)
        {
            this.enableView = value;
            foreach (PageTree child in this.child)
            {
                child.setEnableCreate(value);
            }
            return this;
        }

        public PageTree setEnableUpdate(bool value)
        {
            this.enableView = value;
            foreach (PageTree child in this.child)
            {
                child.setEnableUpdate(value);
            }
            return this;
        }

        public PageTree setEnableDelete(bool value)
        {
            this.enableView = value;
            foreach (PageTree child in this.child)
            {
                child.setEnableDelete(value);
            }
            return this;
        }

        public PageTree setEnablePrint(bool value)
        {
            this.enableView = value;
            foreach (PageTree child in this.child)
            {
                child.setEnablePrint(value);
            }
            return this;
        }

        //return List<Tuple<text, pageName>>
        public List<Tuple<string, string>> toArray(int levelLimit=-1)
        {
            return _ToArray(0, levelLimit);
        }

        public PageTree Find(string pageName)
        {
            if (this.pageItem.name == pageName)
            {
                return this;
            }
            else
            {
                foreach (PageTree child in this.child)
                {
                    var childFound = child.Find(pageName);
                    if (childFound.isValid())
                    {
                        return childFound;
                    }
                }
                return this.setInvalid();
            }
        }

        public PageTree AddChild(List<PageTree> child)
        {
            this.child = child;
            return this;
        }

        public bool isValid()
        {
            return this.validity;
        }

        private List<Tuple<string, string>> _ToArray(int level, int levelLimit)
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            result.Add(new Tuple<string, string>(this.pageItem.text, this.pageItem.name));
            if (level != levelLimit)
            {
                foreach (PageTree child in this.child)
                {
                    foreach (Tuple<string, string> childItem in child._ToArray(level + 1, levelLimit))
                    {
                        result.Add(childItem);
                    }
                }
            }
            return result;
        }

        private PageTree setInvalid()
        {
            this.validity = false;
            return this;
        }
    }

    public class ACLContainer
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        public PageTree pageList { get; set; }

        public ACLContainer()
        {
            List<PageTree> dataBisnisApChild = new List<PageTree>();
            List<PageTree> dataBisnisAfiliasiChild = new List<PageTree>();

            //Anak Perusahaan
            List<company> companiesAnakPerusahaan = db.companies.Where(x => x.parent == 0).ToList();
            foreach (company c in companiesAnakPerusahaan)
            {
                dataBisnisApChild.Add(new PageTree(PageItem.DataBisnis_ApCompany.ConcatName(c.nama).ConcatText(c.nama), new List<PageTree>
                    {
                        new PageTree(PageItem.DataBisnis_Ap_Profil.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_StrukturOrganisasi.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_ADART.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_RJPP.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_RKAP.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_BussinessReport.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_KPI.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_ProjectStatus.ConcatName(c.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Ap_Product.ConcatName(c.id.ToString()))
                    }
                ));
                
                //Afiliasi
                List<company> companiesAfiliasi = db.companies.Where(x => x.parent == c.id).ToList();
                List<PageTree> dataBisnisAfiliasiChildTemp = new List<PageTree>();
                foreach (company c2 in companiesAfiliasi)
                {
                    dataBisnisAfiliasiChildTemp.Add(new PageTree(PageItem.DataBisnis_AfiliasiCompany.ConcatName(c2.nama).ConcatText(c2.nama), new List<PageTree>
                    {
                        new PageTree(PageItem.DataBisnis_Afiliasi_Profil.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_StrukturOrganisasi.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_ADART.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_RJPP.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_RKAP.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_BussinessReport.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_KPI.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_ProjectStatus.ConcatName(c2.id.ToString())),
                        new PageTree(PageItem.DataBisnis_Afiliasi_Product.ConcatName(c2.id.ToString()))
                    }
                    ));
                }
                dataBisnisAfiliasiChild.Add(new PageTree(PageItem.DataBisnis_AfiliasiCompanyParent.ConcatName(c.nama).ConcatText(c.nama), dataBisnisAfiliasiChildTemp));
            }
            
            pageList = new PageTree(PageItem.All, new List<PageTree>
            {
                new PageTree(PageItem.Home).setEnableView(false),
                new PageTree(PageItem.ProfilDitGas, new List<PageTree>
                {
                    new PageTree(PageItem.ProfilDitGas_Profil),
                    new PageTree(PageItem.ProfilDitGas_VisiMisi),
                    new PageTree(PageItem.ProfilDitGas_Organisasi)
                }).setEnableView(false),
                new PageTree(PageItem.SHEQMS, new List<PageTree>
                {
                    new PageTree(PageItem.SHEQMS_DitGas)
                    //,
                    //new PageTree(PageItem.SHEQMS_Ap)
                }),
                new PageTree(PageItem.DataBisnis, new List<PageTree>
                {
                    new PageTree(PageItem.DataBisnis_DitGas, new List<PageTree>
                    {
                        new PageTree(PageItem.DataBisnis_DitGas_ADART),
                        new PageTree(PageItem.DataBisnis_DitGas_RJPP),
                        new PageTree(PageItem.DataBisnis_DitGas_RKAP),
                        new PageTree(PageItem.DataBisnis_DitGas_BussinessReport),
                        new PageTree(PageItem.DataBisnis_DitGas_KPI),
                        new PageTree(PageItem.DataBisnis_DitGas_ProjectStatus),
                        new PageTree(PageItem.DataBisnis_DitGas_Product)
                    }),
                    new PageTree(PageItem.DataBisnis_JMG),
                    new PageTree(PageItem.DataBisnis_Ap, dataBisnisApChild),
                    new PageTree(PageItem.DataBisnis_Afiliasi, dataBisnisAfiliasiChild)
                }),
                new PageTree(PageItem.SDM, new List<PageTree>
                {
                    new PageTree(PageItem.SDM_DitGas),
                    new PageTree(PageItem.SDM_Ap),
                    new PageTree(PageItem.SDM_Afiliasi),
                    new PageTree(PageItem.SDM_AturanSDM)
                }),
                new PageTree(PageItem.AturanRegulasi, new List<PageTree>
                {
                    new PageTree(PageItem.AturanRegulasi_Pertamina),
                    new PageTree(PageItem.AturanRegulasi_External),
                    new PageTree(PageItem.AturanRegulasi_Charter),
                    new PageTree(PageItem.AturanRegulasi_GCG)
                }).setEnableView(false),
                new PageTree(PageItem.Kontak).setEnableView(false),
                new PageTree(PageItem.Kalendar).setEnableView(false)
            });
        }

        public bool getPageDefaultAction(string pageName, string action)
        {
            return _GetPageDefaultAction(pageName, action, this.pageList);
        }

        public string getPageNameFromText(string text, string action)
        {
            return _GetPageNameFromText(text, this.pageList);
        }

        public string getTextFromPageName(string pageName, string action)
        {
            return _GetTextFromPageName(pageName, this.pageList);
        }

        private bool _GetPageDefaultAction(string pageName, string action, PageTree pageTree)
        {
            if (pageName == pageTree.pageItem.name) //kalo udah ketemu nama yg sama
            {
                switch (action)
                {
                    case "view": return pageTree.enableView;
                    case "create": return pageTree.enableCreate;
                    case "update": return pageTree.enableUpdate;
                    case "delete": return pageTree.enableDelete;
                    case "print": return pageTree.enablePrint;
                    default: return true;
                }
            }
            else
            {
                foreach (PageTree pageTree2 in pageTree.child)
                {
                    if (!_GetPageDefaultAction(pageName, action, pageTree2))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string _GetPageNameFromText(string text, PageTree pageTree)
        {
            if (text == pageTree.pageItem.text) //kalo udah ketemu nama yg sama
            {
                return pageTree.pageItem.name;
            }
            else
            {
                foreach (PageTree pageTree2 in pageTree.child)
                {
                    return _GetPageNameFromText(text, pageTree2);
                }
                return "";
            }
        }

        private string _GetTextFromPageName(string pageName, PageTree pageTree)
        {
            if (pageName == pageTree.pageItem.name) //kalo udah ketemu nama yg sama
            {
                return pageTree.pageItem.text;
            }
            else
            {
                foreach (PageTree pageTree2 in pageTree.child)
                {
                    return _GetTextFromPageName(pageName, pageTree2);
                }
                return "";
            }
        }

#region komen lebar dibuang
        //public ACLContainer()
        //{
            

        //    pageList = new Dictionary<string, object> {{"text", "All"}, {"pagename", "PRT"}, {"child", new List<PageItem>
        //    {
        //        new Dictionary<string, object> {{"text", "Home"}, {"pagename", "/Home"}, {"child", new List<PageItem>()}},
        //        new Dictionary<string, object> {{"text", "Profil Dit. Gas"}, {"pagename", "PRTprofilditgas"}, {"child", new List<PageItem>
        //        {
        //            new Dictionary<string, object> {{"text", "Profil"}, {"pagename", "/ProfilDitgas"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Visi Misi"}, {"pagename", "/DirektoratDitgas"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Organisasi"}, {"pagename", "/OrganisasiDitgas"}, {"child", new List<PageItem>()}}
        //        }},
        //        new Dictionary<string, object> {{"text", "SHE - QMS"}, {"pagename", "/hse"}, {"child", new List<PageItem>
        //        {
        //            new Dictionary<string, object> {{"text", "Ditgas"}, {"pagename", ""}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Anak Perusahaan"}, {"pagename", ""}, {"child", new List<PageItem>()}}
        //        }},
        //        new Dictionary<string, object> {{"text", "Data Bisnis"}, {"pagename", "PRTdatabisnis"}, {"child", new List<PageItem>
        //        {
        //            new Dictionary<string, object> {{"text", "Dit. Gas"}, {"pagename", "/DataBisnisDitGas"}, {"child", new List<PageItem>
        //            {
        //                new Dictionary<string, object> {{"text", "AD/ART"}, {"pagename", "/DataBisnisDitGas/AdArt"}, {"child", new List<PageItem>()}},
        //                new Dictionary<string, object> {{"text", "RJPP"}, {"pagename", "/DataBisnisDitGas/Rjpp"}, {"child", new List<PageItem>()}},
        //                new Dictionary<string, object> {{"text", "RKAP"}, {"pagename", "/DataBisnisDitGas/Rkap"}, {"child", new List<PageItem>()}},
        //                new Dictionary<string, object> {{"text", "Business Report"}, {"pagename", "/DataBisnisDitGas/BussinessReport"}, {"child", new List<PageItem>()}},
        //                new Dictionary<string, object> {{"text", "KPI"}, {"pagename", "/DataBisnisDitGas/Kinerja"}, {"child", new List<PageItem>()}},
        //                new Dictionary<string, object> {{"text", "Project Status"}, {"pagename", "/DataBisnisDitGas/ProjectStatus"}, {"child", new List<PageItem>()}},
        //                new Dictionary<string, object> {{"text", "Product"}, {"pagename", "/DataBisnisDitGas/Product"}, {"child", new List<PageItem>()}}
        //            }},
        //            new Dictionary<string, object> {{"text", "JMG"}, {"pagename", "/DataBisnisJmg"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Anak Perusahaan"}, {"pagename", "/DataBisnisAp", dataBisnisApChild},
        //            new Dictionary<string, object> {{"text", "Afiliasi"}, {"pagename", "/DataBisnisAfiliasi"}, {"child", new List<PageItem>()}}
        //        }},
        //        new Dictionary<string, object> {{"text", "SDM"}, {"pagename", "/"}, {"child", new List<PageItem>
        //        {
        //            new Dictionary<string, object> {{"text", "Ditgas"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Anak Perusahaan"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Afiliasi"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Aturan SDM"}, {"pagename", "/"}, {"child", new List<PageItem>()}}
        //        }},
        //        new Dictionary<string, object> {{"text", "Aturan & Regulasi"}, {"pagename", "/"}, {"child", new List<PageItem>
        //        {
        //            new Dictionary<string, object> {{"text", "Aturan PERTAMINA"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Aturan External"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "Charter"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //            new Dictionary<string, object> {{"text", "GCG"}, {"pagename", "/"}, {"child", new List<PageItem>()}}
        //        }},
        //        new Dictionary<string, object> {{"text", "Kontak"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //        new Dictionary<string, object> {{"text", "Kalendar"}, {"pagename", "/"}, {"child", new List<PageItem>()}},
        //        new Dictionary<string, object> {{"text", "Lain - Lain"}, {"pagename", "/"}, {"child", new List<PageItem>()}}
        //    }};
        //}

        //public ACLContainer()
        //{
        //    List<PageItem> dataBisnisApChild = new List<PageItem>();
        //    List<company> companiesAnakPerusahaan = db.companies.Where(c => c.parent == 0).ToList();
        //    foreach (company c in companiesAnakPerusahaan)
        //    {
        //        dataBisnisApChild.Add(new List<PageItem> {c.nama, "PRT"+c.nama, new List<PageItem>
        //            {
        //                new List<PageItem> {"Profil", "/DataBisnisAp/Profil/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"Struktur Organisasi", "/DataBisnisAp/StrukturOrganisasi/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"AD/ART", "/DataBisnisAp/AdArt/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"RJPP", "/DataBisnisAp/Rjpp/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"RKAP", "/DataBisnisAp/Rkap/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"Business Report", "/DataBisnisAp/BussinessReport/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"KPI", "/DataBisnisAp/Kinerja/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"Project Status", "/DataBisnisAp/ProjectStatus/"+c.id, new List<PageItem>()},
        //                new List<PageItem> {"Product", "/DataBisnisAp/Product/"+c.id, new List<PageItem>()}
        //            }
        //        });
        //    }

        //    pageList = new List<PageItem> {"All", "PRT", new List<PageItem>
        //    {
        //        new List<PageItem> {"Home", "/Home", new  List<PageItem>()},
        //        new List<PageItem> {"Profil Dit. Gas", "PRTprofilditgas", new List<PageItem>
        //        {
        //            new List<PageItem> {"Profil", "/ProfilDitgas", new List<PageItem>()},
        //            new List<PageItem> {"Visi Misi", "/DirektoratDitgas", new List<PageItem>()},
        //            new List<PageItem> {"Organisasi", "/OrganisasiDitgas", new List<PageItem>()}
        //        }},
        //        new List<PageItem> {"SHE - QMS", "/hse", new List<PageItem>
        //        {
        //            new List<PageItem> {"Ditgas", "", new List<PageItem>()},
        //            new List<PageItem> {"Anak Perusahaan", "", new List<PageItem>()}
        //        }},
        //        new List<PageItem> {"Data Bisnis", "PRTdatabisnis", new List<PageItem>
        //        {
        //            new List<PageItem> {"Dit. Gas", "/DataBisnisDitGas", new List<PageItem>
        //            {
        //                new List<PageItem> {"AD/ART", "/DataBisnisDitGas/AdArt", new List<PageItem>()},
        //                new List<PageItem> {"RJPP", "/DataBisnisDitGas/Rjpp", new List<PageItem>()},
        //                new List<PageItem> {"RKAP", "/DataBisnisDitGas/Rkap", new List<PageItem>()},
        //                new List<PageItem> {"Business Report", "/DataBisnisDitGas/BussinessReport", new List<PageItem>()},
        //                new List<PageItem> {"KPI", "/DataBisnisDitGas/Kinerja", new List<PageItem>()},
        //                new List<PageItem> {"Project Status", "/DataBisnisDitGas/ProjectStatus", new List<PageItem>()},
        //                new List<PageItem> {"Product", "/DataBisnisDitGas/Product", new List<PageItem>()}
        //            }},
        //            new List<PageItem> {"JMG", "/DataBisnisJmg", new List<PageItem>()},
        //            new List<PageItem> {"Anak Perusahaan", "/DataBisnisAp", dataBisnisApChild},
        //            new List<PageItem> {"Afiliasi", "/DataBisnisAfiliasi", new List<PageItem>()}
        //        }},
        //        new List<PageItem> {"SDM", "/", new List<PageItem>
        //        {
        //            new List<PageItem> {"Ditgas", "/", new List<PageItem>()},
        //            new List<PageItem> {"Anak Perusahaan", "/", new List<PageItem>()},
        //            new List<PageItem> {"Afiliasi", "/", new List<PageItem>()},
        //            new List<PageItem> {"Aturan SDM", "/", new List<PageItem>()}
        //        }},
        //        new List<PageItem> {"Aturan & Regulasi", "/", new List<PageItem>
        //        {
        //            new List<PageItem> {"Aturan PERTAMINA", "/", new List<PageItem>()},
        //            new List<PageItem> {"Aturan External", "/", new List<PageItem>()},
        //            new List<PageItem> {"Charter", "/", new List<PageItem>()},
        //            new List<PageItem> {"GCG", "/", new List<PageItem>()}
        //        }},
        //        new List<PageItem> {"Kontak", "/", new List<PageItem>()},
        //        new List<PageItem> {"Kalendar", "/", new List<PageItem>()},
        //        new List<PageItem> {"Lain - Lain", "/", new List<PageItem>()}
        //    }};

            //pageList = new object[] {"", "PRT", new object[]
            //{
            //    new object[] {"Home", "/Home", new object[]{}},
            //    new object[] {"Profil Dit. Gas", "PRTprofilditgas", new object[]
            //    {
            //        new object[] {"Profil", "/ProfilDitgas", new object[]{}},
            //        new object[] {"Visi Misi", "/DirektoratDitgas", new object[]{}},
            //        new object[] {"Organisasi", "/OrganisasiDitgas", new object[]{}}
            //    }},
            //    new object[] {"SHE - QMS", "/hse", new object[]
            //    {
            //        new object[] {"Ditgas", "", new object[]{}},
            //        new object[] {"Anak Perusahaan", "", new object[]{}}
            //    }},
            //    new object[] {"Data Bisnis", "PRTdatabisnis", new object[]
            //    {
            //        new object[] {"Dit. Gas", "/DataBisnisDitGas", new object[]
            //        {
            //            new object[] {"AD/ART", "/DataBisnisDitGas/AdArt", new object[]{}},
            //            new object[] {"RJPP", "/DataBisnisDitGas/Rjpp", new object[]{}},
            //            new object[] {"RKAP", "/DataBisnisDitGas/Rkap", new object[]{}},
            //            new object[] {"Business Report", "/DataBisnisDitGas/BussinessReport", new object[]{}},
            //            new object[] {"KPI", "/DataBisnisDitGas/Kinerja", new object[]{}},
            //            new object[] {"Project Status", "/DataBisnisDitGas/ProjectStatus", new object[]{}},
            //            new object[] {"Product", "/DataBisnisDitGas/Product", new object[]{}}
            //        }},
            //        new object[] {"JMG", "/DataBisnisJmg", new object[]{}},
            //        new object[] {"Anak Perusahaan", "/DataBisnisAp", new object[]
            //        {
            //            new object[] {"AD/ART", "/DataBisnisAp/Profil", new object[]{}},
            //            new object[] {"RJPP", "/DataBisnisAp/Struktur", new object[]{}},
            //            new object[] {"AD/ART", "/DataBisnisAp/AdArt", new object[]{}},
            //            new object[] {"RJPP", "/DataBisnisAp/Rjpp", new object[]{}},
            //            new object[] {"RKAP", "/DataBisnisAp/Rkap", new object[]{}},
            //            new object[] {"Business Report", "/DataBisnisAp/BussinessReport", new object[]{}},
            //            new object[] {"KPI", "/DataBisnisAp/Kinerja", new object[]{}},
            //            new object[] {"Project Status", "/DataBisnisAp/ProjectStatus", new object[]{}},
            //            new object[] {"Product", "/DataBisnisAp/Product", new object[]{}}
            //        }},
            //        new object[] {"Afiliasi", "/DataBisnisAfiliasi", new object[]{}}
            //    }},
            //    new object[] {"SDM", "/", new object[]
            //    {
            //        new object[] {"Ditgas", "/", new object[]{}},
            //        new object[] {"Anak Perusahaan", "/", new object[]{}},
            //        new object[] {"Afiliasi", "/", new object[]{}},
            //        new object[] {"Aturan SDM", "/", new object[]{}}
            //    }},
            //    new object[] {"Aturan & Regulasi", "/", new object[]
            //    {
            //        new object[] {"Aturan PERTAMINA", "/", new object[]{}},
            //        new object[] {"Aturan External", "/", new object[]{}},
            //        new object[] {"Charter", "/", new object[]{}},
            //        new object[] {"GCG", "/", new object[]{}}
            //    }},
            //    new object[] {"Kontak", "/", new object[]{}},
            //    new object[] {"Kalendar", "/", new object[]{}},
            //    new object[] {"Lain - Lain", "/", new object[]{}}
            //}};
        //}
        //public ACLContainer(int userId)
        //{
            //user_id = userId;
            //pageNames.Add(new Tuple<string, int>("Home", 0));
            //pageNames.Add(new Tuple<string, int>("ProfilDitGas", 0));

            //pageDetails = new Dictionary<string, object>();
            //pageDetails["Home"] = new Tuple<byte, byte, byte, byte, byte>(0, 0, 0, 0, 0);
        //}

        //public void insertACLs(List<acl> ACLs)
        //{
        //    foreach (acl ACL in ACLs)
        //    {
        //        insertACL(ACL);
        //    }
        //}

        //private void insertACL(acl ACL)
        //{
            //if page_name ada di list page
            //pageDetails[ACL.page_name] = new Tuple<byte, byte, byte, byte, byte>(ACL.allow_view, ACL.allow_create, ACL.allow_update, ACL.allow_delete, ACL.allow_print);
            //else
            //{
            //    return false;
            //}
        //}
#endregion
    }
}