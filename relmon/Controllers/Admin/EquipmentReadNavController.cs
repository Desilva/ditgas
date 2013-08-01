using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using Telerik.Web.Mvc;

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class EquipmentReadNavController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /EquipmentReadNav/

        public ActionResult Index()
        {
            return PartialView();
        }
        #region edit
        //
        // POST: /EquipmentReadNav/Edit/5
        [HttpPost]
        public ActionResult Edit(equipment_readiness_nav equipment_readiness_nav)
        {
            if (ModelState.IsValid)
            {
                //caalculate and save read nav
                //CalculateReadNav CalcuateReadNav = new CalculateReadNav(equipment_readiness_nav);

                equipment temp = db.equipments.Find(equipment_readiness_nav.id_equipment);
                temp.status_read_nav = 1;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }

        //
        // POST: /EquipmentReadNav/Edit/5
        [HttpPost]
        public ActionResult EditNoteSave(string note)
        {
            equipment_read_nav_note eqn = new equipment_read_nav_note()
            {
                note = note
            };
            db.equipment_read_nav_note.Add(eqn);
            db.SaveChanges();
            return Json(false);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // Ajax select binding
        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return binding();
        }

        //select data equipment read nav
        private ViewResult binding()
        {
            var model = from o in db.equipment_readiness_nav
                        select new EquipmentReadNavEntity
                        {
                            id = o.id,
                            equipment_tag_number = o.equipment.tag_num,
                            equipment_name = o.equipment.nama,
                            status = o.equipment.status,
                            avail_inherent = o.equipment.equipment_paf.Where(a => a.id_equipment == o.id_equipment).OrderByDescending(a => a.tanggal).Select(a => a.avail_measured).FirstOrDefault(),
                            avail_operational = o.equipment.equipment_paf.Where(a => a.id_equipment == o.id_equipment).OrderByDescending(a => a.tanggal).Select(a => a.avail_calculated).FirstOrDefault(),
                            bec = o.bec,
                            status_read_nav = o.equipment.status_read_nav
                        };
            return View(new GridModel<EquipmentReadNavEntity>
            {
                Data = model.ToList()
            });
        }

        public JsonResult GetDetail(int id)
        {
            var model = from o in db.equipment_readiness_nav
                        where o.id == id
                        select new EquipmentReadNavEntity
                        {
                            id = o.id,
                            id_equipment = o.id_equipment,
                            equipment_tag_number = o.equipment.tag_num,
                            equipment_name = o.equipment.nama,
                            accesories = o.accesories,
                            bec = o.bec,
                            boc = o.boc,
                            capacity = o.capacity,
                            cm_program = o.cm_program,
                            lifecycle_spare = o.lifecycle_spare,
                            main_act_spare = o.main_act_spare,
                            maintenance = o.maintenance,
                            monitoring = o.monitoring,
                            operation = o.operation,
                            overhaul = o.overhaul,
                            performance = o.performance,
                            pm_program = o.pm_program,
                            quality = o.quality,
                            safe_operation = o.safe_operation,
                            safeguard = o.safeguard,
                            score = o.score,
                            sertifikasi = o.sertifikasi,
                            spares = o.spares,
                            avail_inherent = o.equipment.equipment_paf.Where(a => a.id_equipment == o.id_equipment).OrderByDescending(a => a.tanggal).Select(a => a.avail_measured).FirstOrDefault(),
                        };
            return Json(new { eqReadNav = model.ToList() });
        }

        //
        // GET: /EquipmentReadNav/EditNote
        public JsonResult EditNote()
        {
            string note = "";
            if(db.equipment_read_nav_note.Count() > 0)
                note = db.equipment_read_nav_note.ToList().Last().note;

            return Json(new { eqNote = note });
        } 

    }
}