using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using relmon.Models;
using dhtmlxConnectors;
using System.Globalization;
//using DHTMLX.Common;
//using DHTMLX.Scheduler;
//using DHTMLX.Scheduler.Authentication;
//using DHTMLX.Scheduler.Controls;
//using DHTMLX.Scheduler.Data;

namespace relmon.Controllers.FrontEnd
{
    public class KalendarController : Controller
    {

        private RelmonStoreEntities db = new RelmonStoreEntities();
        private SchedularDataContext context = new SchedularDataContext();
        //
        // GET: /Kalendar/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "kalendar";

            //var scheduler = new DHXScheduler(this);
            //scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);
            IEnumerable<kalendar_event> items = (from o in context.kalendar_events
                                                 select o)
                        .AsEnumerable();

            var scheduler = new dhtmlxSchedulerConnector(items, "id", "start_date", "end_date", "description");


            scheduler.SetDynamicLoading(30);//enable partial grid loading (30 records per r
            //scheduler.
            //scheduler.LoadData = true;
            //scheduler.EnableDataprocessor = true;
            //scheduler.Data.DataProcessor.UpdateFieldsAfterSave = true;


            return View(scheduler);
        }

        public JsonResult Data()
        {//events for loading to scheduler

            var authorize = this.CheckUser();
            ////var authorize = "user";
            var events = (new SchedularDataContext()).kalendar_events;
            if (String.Compare(authorize, "admin", true) == 0)
            {
                var items = from p in events
                            where p.type.ToLower().Contains("umum")
                            select p;
                List<object> listResult = new List<object>();
                foreach (var result in items.ToList())
                {
                    string pattern = "yyyy-MM-dd HH:mm:ss";
                    DateTime parsedDateStart;
                    DateTime.TryParseExact(result.start_date.ToString(), pattern, null,
                                   DateTimeStyles.None, out parsedDateStart);
                    DateTime parsedDateEnd;
                    DateTime.TryParseExact(result.end_date.ToString(), pattern, null,
                                   DateTimeStyles.None, out parsedDateEnd);
                    listResult.Add(new
                    {

                        result.id,
                        result.text,
                        parsedDateStart,
                        parsedDateEnd,
                        result.description,
                        result.type,
                        result.priority,
                        result.create_by,
                        result.group_id
                    });
                }
                return Json(listResult);
                //var data = new SchedulerAjaxData(items.Select(e => new { e.id, e.text, e.start_date, e.end_date, e.description, e.type, e.priority, e.create_by, e.group_id }));
                //data.DateFormat = "%Y-%m-%d %H:%i";
                //return (data);
            }
            else if (String.Compare(authorize, "user", true) == 0)
            {
                
                
                

                var items = (from p in context.kalendar_events
                             where p.type.ToLower().Contains("umum") || p.create_by == (Int32)Int64.Parse(Session["id"].ToString())
                             select p
                            ).Union
                            (
                                from p in context.kalendar_events
                                join q in context.kalendar_groups on p.group_id equals q.group_id
                                where q.member.ToLower().Contains(Session["name"].ToString())
                                select p
                            ).Distinct();
                List<object> listResult = new List<object>();
                foreach (var result in items.ToList())
                {
                    string pattern = "yyyy-MM-dd HH:mm:ss";
                    DateTime parsedDateStart;
                    DateTime.TryParseExact(result.start_date.ToString(), pattern, null,
                                   DateTimeStyles.None, out parsedDateStart);
                    DateTime parsedDateEnd;
                    DateTime.TryParseExact(result.end_date.ToString(), pattern, null,
                                   DateTimeStyles.None, out parsedDateEnd);
                    listResult.Add(new
                    {

                        result.id,
                        result.text,
                        parsedDateStart,
                        parsedDateEnd,
                        result.description,
                        result.type,
                        result.priority,
                        result.create_by,
                        result.group_id
                    });
                }
                return Json(listResult);
                //var data = new SchedulerAjaxData(items.Select(e => new { e.id, e.text, e.start_date, e.end_date, e.description, e.type, e.priority, e.create_by, e.group_id }));
                //data.DateFormat = "%Y-%m-%d %H:%i";
                //return (data);
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
                    //DateTime parsedDateStart;
                    //DateTime.TryParseExact(result.start_date.ToString(), pattern, null,
                    //               DateTimeStyles.None, out parsedDateStart);
                    //DateTime parsedDateEnd;
                    //DateTime.TryParseExact(result.end_date.ToString(), pattern, null,
                    //               DateTimeStyles.None, out parsedDateEnd);
                    string start_date = result.start_date.ToString(pattern);
                    string end_date = result.end_date.ToString(pattern);
                    listResult.Add(new
                    {

                        result.id,
                        result.text,
                        start_date,
                        end_date,
                        result.description,
                        result.type,
                        result.priority,
                        result.create_by,
                        result.group_id
                    });
                }
                return Json(listResult);
                //var data = new SchedulerAjaxData(items.Select(e => new { e.id, e.text, e.start_date, e.end_date, e.description, e.type, e.priority, e.create_by, e.group_id }));
                //data.DateFormat = "%Y-%m-%d %H:%i";
                //return (data);
            }

        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {

            //Console.WriteLine();
            //return null;

            //var action = new DataAction();
            Console.WriteLine(actionValues.GetType());
            
            return null;

            //var data = new SchedularDataContext();
            //var authorize = this.CheckUser();
            ////var authorize = "user";
            //try
            //{
            //    var changedEvent = DHXEventsHelper.Bind<kalendar_event>(actionValues, new System.Globalization.CultureInfo("en-US"));
            //    if (String.Compare(authorize, "user", true) == 0)
            //    {
            //        switch (action.Type)
            //        {
            //            case DataActionTypes.Insert:
            //                string[] AllStrings = actionValues["group_id"].Split(',');
            //                if (String.Compare(AllStrings[0], "") != 0)
            //                {
            //                    Int32 getMax = 1;
            //                    try
            //                    {
            //                        getMax = context.kalendar_groups.Max(o => o.group_id) + 1;
            //                    }
            //                    catch (Exception e)
            //                    {
            //                        Console.WriteLine(e.Message);
            //                    }

            //                    for (int i = 0; i < AllStrings.Length; i++)
            //                    {
            //                        kalendar_group newMember = new kalendar_group();
            //                        newMember.group_id = getMax;
            //                        newMember.member = AllStrings[i];
            //                        data.kalendar_groups.InsertOnSubmit(newMember);
            //                    }
            //                    changedEvent.group_id = getMax;
            //                }

            //                changedEvent.type = "pribadi";
            //                changedEvent.create_by = (Int32)Int64.Parse(Session["id"].ToString());
            //                data.kalendar_events.InsertOnSubmit(changedEvent);
            //                break;
            //            case DataActionTypes.Delete:
            //                var del_query =
            //                            from kalendar_group in context.kalendar_groups
            //                            where
            //                              kalendar_group.group_id == changedEvent.group_id
            //                            select kalendar_group;
            //                foreach (var del in del_query)
            //                {
            //                    data.kalendar_groups.Attach(del);
            //                    data.kalendar_groups.DeleteOnSubmit(del);
            //                }
            //                changedEvent = data.kalendar_events.SingleOrDefault(ev => ev.id == action.SourceId);
            //                data.kalendar_events.DeleteOnSubmit(changedEvent);
            //                break;
            //            default:// "update"
            //                var getId = (from p in context.kalendar_events
            //                             where p.id == changedEvent.id
            //                             select new { group_id = p.group_id });
            //                var gId = 0;
            //                foreach (var g in getId)
            //                {
            //                    gId = (Int32)g.group_id;
            //                }
            //                var del_query2 =
            //                           from kalendar_group in context.kalendar_groups
            //                           where
            //                             kalendar_group.group_id == gId
            //                           select kalendar_group;
            //                foreach (var del in del_query2)
            //                {
            //                    data.kalendar_groups.Attach(del);
            //                    data.kalendar_groups.DeleteOnSubmit(del);
            //                }
            //                string[] AllStrings2 = actionValues["group_id"].Split(',');
            //                if (String.Compare(AllStrings2[0], "") != 0)
            //                {
            //                    for (int i = 0; i < AllStrings2.Length; i++)
            //                    {
            //                        kalendar_group newMember = new kalendar_group();
            //                        newMember.group_id = gId;
            //                        newMember.member = AllStrings2[i];
            //                        data.kalendar_groups.InsertOnSubmit(newMember);
            //                    }
            //                }
            //                var eventToUpdate = data.kalendar_events.SingleOrDefault(ev => ev.id == action.SourceId);
            //                changedEvent.group_id = gId;
            //                DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        switch (action.Type)
            //        {
            //            case DataActionTypes.Insert:

            //                changedEvent.type = "umum";
            //                changedEvent.create_by = (Int32)Int64.Parse(Session["id"].ToString());
            //                data.kalendar_events.InsertOnSubmit(changedEvent);
            //                break;
            //            case DataActionTypes.Delete:
            //                changedEvent = data.kalendar_events.SingleOrDefault(ev => ev.id == action.SourceId);
            //                data.kalendar_events.DeleteOnSubmit(changedEvent);
            //                break;
            //            default:// "update"  
            //                var eventToUpdate = data.kalendar_events.SingleOrDefault(ev => ev.id == action.SourceId);
            //                DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
            //                break;
            //        }
            //    }
            //    data.SubmitChanges();
            //    action.TargetId = changedEvent.id;
            //}
            //catch (Exception a)
            //{
            //    action.Type = DataActionTypes.Error;
            //    Console.WriteLine(a);
            //}

            //var response = new AjaxSaveResponse(action);
            //return (ContentResult)response;
        }

        [HttpPost]
        public JsonResult GetShareList()
        {
            var find = Request["find"];
            var listResultTemp = (from x in context.kalendar_groups
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
