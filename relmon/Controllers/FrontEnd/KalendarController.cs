using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using System.Web.Security;

namespace relmon.Controllers.FrontEnd
{
    public class CalendarActionResponseModel
    {
        public String Status;
        public Int64 Source_id;
        public Int64 Target_id;

        public CalendarActionResponseModel(String status, Int64 source_id, Int64 target_id)
        {
            Status = status;
            Source_id = source_id;
            Target_id = target_id;
        }
    }


    public class KalendarController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "kalendar";
            return View();
        }

        public JsonResult Data()
        {//events for loading to scheduler

            var authorize = this.CheckUser();
            ////var authorize = "user";
            var events = db.kalendar_events;
            if (String.Compare(authorize, "admin", true) == 0)
            {
                var items = from p in events
                            where p.type.ToLower().Contains("umum")
                            select p;
                List<object> listResult = new List<object>();
                foreach (var result in items.ToList())
                {
                    string pattern = "yyyy-MM-dd HH:mm:ss";
                    string start_date = result.start_date.ToString(pattern);
                    string end_date = result.end_date.ToString(pattern);
                    
                    listResult.Add(new
                    {

                        result.id,
                        result.text,
                        start_date,
                        end_date,
                        result.place,
                        result.description,
                        result.type,
                        result.priority,
                        result.create_by,
                        result.group_id
                    });
                }
                return Json(listResult);
            }
            else if (String.Compare(authorize, "user", true) == 0)
            {



                var create = (Int32)Int64.Parse(Session["id"].ToString());
                var member = Session["name"].ToString();
                var items = (from p in db.kalendar_events
                             where p.type.ToLower().Contains("umum") || p.create_by == create
                             select p
                            ).Union
                            (
                                from p in db.kalendar_events
                                join q in db.kalendar_group on p.group_id equals q.group_id
                                where q.member.ToLower() == member
                                select p
                            ).Distinct();
                List<object> listResult = new List<object>();
                foreach (var result in items.ToList())
                {
                    string pattern = "yyyy-MM-dd HH:mm:ss";
                    string start_date = result.start_date.ToString(pattern);
                    string end_date = result.end_date.ToString(pattern);
                    listResult.Add(new
                    {

                        result.id,
                        result.text,
                        start_date,
                        end_date,
                        result.place,
                        result.description,
                        result.type,
                        result.priority,
                        result.create_by,
                        result.group_id
                    });
                }
                return Json(listResult);
            }
            else
            {
                var items = (from p in events
                             where p.type.ToLower().Contains("umum")
                             select p).ToList();
                List<object> listResult = new List<object>();
                foreach (var result in items.ToList())
                {
                    string pattern = "yyyy-MM-dd HH:mm:ss";
                    string start_date = result.start_date.ToString(pattern);
                    string end_date = result.end_date.ToString(pattern);
                    listResult.Add(new
                    {

                        result.id,
                        result.text,
                        start_date,
                        end_date,
                        result.place,
                        result.description,
                        result.type,
                        result.priority,
                        result.create_by,
                        result.group_id
                    });
                }
                return Json(listResult);
            }

        }




        public ActionResult Save(FormCollection actionValues)
        {
            String ids = actionValues["ids"];
            String action_type = actionValues[ids+"_!nativeeditor_status"];

            Int64 source_id = Int64.Parse(actionValues[ids+"_id"]);
            Int64 target_id = source_id;

            var authorize = this.CheckUser();
            //var authorize = "user";
            var data = db;
            try
            {
                kalendar_events changedEvent = new kalendar_events();
                changedEvent.id = (Int32)Int64.Parse(actionValues[ids+"_id"]);
                changedEvent.text = actionValues[ids + "_text"];
                changedEvent.start_date = Convert.ToDateTime(actionValues[ids+"_start_date"]);
                changedEvent.end_date = Convert.ToDateTime(actionValues[ids + "_end_date"]);
                changedEvent.place = actionValues[ids+"_place"];
                changedEvent.description = actionValues[ids+"_description"];
                changedEvent.type = actionValues[ids+"_type"];
                changedEvent.priority = actionValues[ids+"_priority"];
                if (String.Compare(authorize, "user", true) == 0)
                {
                    switch (action_type)
                    {
                        case "inserted":
                            string[] AllStrings = actionValues[ids + "_group_id"].Split(',');
                            if (String.Compare(AllStrings[0], "") != 0)
                            {
                                Int32 getMax = 1;
                                try
                                {
                                    getMax = db.kalendar_group.Max(o => o.group_id) + 1;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                                for (int i = 0; i < AllStrings.Length; i++)
                                {
                                    kalendar_group newMember = new kalendar_group();
                                    newMember.group_id = getMax;
                                    newMember.member = AllStrings[i];
                                    
                                    data.kalendar_group.Add(newMember);
                                }
                                changedEvent.group_id = getMax;
                            }

                            //changedEvent.type = "pribadi";
                            changedEvent.create_by = (Int32)Int64.Parse(Session["id"].ToString());
                            data.kalendar_events.Add(changedEvent);
                            break;
                        case "deleted":
                            var del_query =
                                        from kalendar_group in db.kalendar_group
                                        where
                                          kalendar_group.group_id == changedEvent.group_id
                                        select kalendar_group;
                            foreach (var del in del_query)
                            {
                                data.kalendar_group.Attach(del);
                                data.kalendar_group.Remove(del);
                            }
                            changedEvent = data.kalendar_events.SingleOrDefault(ev => ev.id == source_id);
                            data.kalendar_events.Remove(changedEvent);
                            break;
                        default: // "updated"                          
                            var getId = (from p in db.kalendar_events
                                         where p.id == changedEvent.id
                                         select new { group_id = p.group_id });
                            var gId = 0;
                            foreach (var g in getId)
                            {
                                gId = (Int32)g.group_id;
                            }
                            var del_query2 =
                                       from kalendar_group in db.kalendar_group
                                       where
                                         kalendar_group.group_id == gId
                                       select kalendar_group;
                            foreach (var del in del_query2)
                            {
                                data.kalendar_group.Attach(del);
                                data.kalendar_group.Remove(del);
                            }
                            string[] AllStrings2 = actionValues[ids + "_group_id"].Split(',');
                            if (String.Compare(AllStrings2[0], "") != 0)
                            {
                                for (int i = 0; i < AllStrings2.Length; i++)
                                {
                                    kalendar_group newMember = new kalendar_group();
                                    newMember.group_id = gId;
                                    newMember.member = AllStrings2[i];
                                    data.kalendar_group.Add(newMember);
                                }
                            }
                            var eventToUpdate = data.kalendar_events.SingleOrDefault(ev => ev.id == source_id);
                            changedEvent.group_id = gId;
                            TryUpdateModel(changedEvent);
                            break;
                    }
                }
                else
                {
                    switch (action_type)
                    {
                        case "inserted":
                            changedEvent.type = "umum";
                            changedEvent.create_by = (Int32)Int64.Parse(Session["id"].ToString());
                            data.kalendar_events.Add(changedEvent);
                            break;
                        case "deleted":
                            changedEvent = data.kalendar_events.SingleOrDefault(ev => ev.id == source_id);
                            data.kalendar_events.Remove(changedEvent);
                            break;
                        default: // "updated"                          
                            changedEvent = data.kalendar_events.SingleOrDefault(ev => ev.id == source_id);
                            TryUpdateModel(changedEvent);
                            break;
                    }
                }

                data.SaveChanges();
                target_id = changedEvent.id;
            }
            catch (Exception a)
            {
                action_type = "error";
            }
            return View(new CalendarActionResponseModel(action_type, source_id, target_id));
        }

        [HttpPost]
        public JsonResult GetShareList()
        {
            var find = Request["find"];
            if (find != "null" && find != null)
            {
                var listResultTemp = (from x in db.kalendar_group
                                      where (x.group_id.Equals(find))
                                      select new
                                      {
                                          id = x.group_id,
                                          member = x.member
                                      }).ToList();

                List<object> listResult = new List<object>();
                foreach (var result in listResultTemp)
                {
                    listResult.Add(new
                    {

                        member = result.member
                    });
                }

                return Json(listResult);
            }
            else
            {
                return Json(null);
            }

        }

        [HttpPost]
        public JsonResult GetFindResult()
        {
            var find = Request["find"];
            if (find == null)
            {
                var listResultTemp = (from x in db.users
                                      where !(x.fullname.ToLower().Contains(Session["name"].ToString()))
                                      select new
                                      {
                                          fullname = x.fullname
                                      }).OrderBy(y => y.fullname).ToList();
                List<object> listResult = new List<object>();
                foreach (var result in listResultTemp)
                {
                    listResult.Add(new
                    {
                        fullname = result.fullname
                    });
                }

                return Json(listResult);
            }
            else
            {
                var listResultTemp = (from x in db.users
                                      where (x.fullname.ToLower().Contains(find.ToLower()))
                                      select new
                                      {
                                          fullname = x.fullname
                                      }).ToList();
                List<object> listResult = new List<object>();
                foreach (var result in listResultTemp)
                {
                    listResult.Add(new
                    {
                        fullname = result.fullname
                    });
                }

                return Json(listResult);
            }



        }

        public string CheckUser()
        {
            if (Session["role"] != null)
            {
                int userRole = (int)Session["role"];
                if (userRole > 0)
                {
                    return "user";
                }
                else
                {
                    return "admin";
                }
            }
            else
            {
                return "public";
            }

        }
    }
}
