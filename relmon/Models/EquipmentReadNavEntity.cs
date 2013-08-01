using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Models
{
    public class EquipmentReadNavEntity
    {
        public enum color
        {
            RED,
            YELLOW,
            GREEN
        }

        public EquipmentReadNavEntity()
        { 
            operation = 100;
            bec = 100;
            boc = 100;
            monitoring = 100;
            performance = 100;
            cm_program = 100;
            maintenance = 100;
            pm_program = 100;
            overhaul = 100;
            spares = 100;
            lifecycle_spare = 100;
            main_act_spare = 100;
            safe_operation = 100;
            safeguard = 100;
            accesories = 100;
            score = 100;
            capacity = 100;
            quality = 100;
            sertifikasi = 100;
        }

        public int id { get; set; }
        public int id_equipment { get; set; }
        public Nullable<double> operation { get; set; }
        public Nullable<double> boc { get; set; }
        public Nullable<double> bec { get; set; }
        public Nullable<double> monitoring { get; set; }
        public Nullable<double> performance { get; set; }
        public Nullable<double> cm_program { get; set; }
        public Nullable<double> maintenance { get; set; }
        public Nullable<double> pm_program { get; set; }
        public Nullable<double> overhaul { get; set; }
        public Nullable<double> spares { get; set; }
        public Nullable<double> lifecycle_spare { get; set; }
        public Nullable<double> main_act_spare { get; set; }
        public Nullable<double> safe_operation { get; set; }
        public Nullable<double> safeguard { get; set; }
        public Nullable<double> accesories { get; set; }
        public Nullable<double> score { get; set; }
        public Nullable<double> capacity { get; set; }
        public Nullable<double> quality { get; set; }
        public Nullable<double> sertifikasi { get; set; }
        public Nullable<int> status_read_nav { get; set; }
        public string mtl { get; set; }

        public string equipment_tag_number { get; set; }
        public string equipment_name { get; set; }
        public Nullable<byte> status { get; set; }
        public double avail_inherent { get; set; }
        public double avail_operational { get; set; }

        public void setMtl() {
            int monitoringColor = (int)color.GREEN;
            int safeOperationColor = (int)color.GREEN;

            //monitoring
            if(this.monitoring < 50){
                monitoringColor = (int)color.RED;
            }else if (this.monitoring < 70){
                if(monitoringColor == (int)color.GREEN){
                    monitoringColor = (int)color.YELLOW;
                }
            }

            if (this.performance < 30)
            {
                monitoringColor = (int)color.RED;
            }
            else if (this.performance < 50)
            {
                if (monitoringColor == (int)color.GREEN)
                {
                    monitoringColor = (int)color.YELLOW;
                }
            }

            //safe operation
            if (this.safe_operation < 70)
            {
                safeOperationColor = (int)color.RED;
            }
            else if (this.safe_operation < 80)
            {
                if (safeOperationColor == (int)color.GREEN)
                {
                    safeOperationColor = (int)color.YELLOW;
                }
            }

            if (this.safeguard < 70)
            {
                safeOperationColor = (int)color.RED;
            }
            else if (this.safeguard < 80)
            {
                if (safeOperationColor == (int)color.GREEN)
                {
                    safeOperationColor = (int)color.YELLOW;
                }
            }

            int result = Math.Min(monitoringColor, safeOperationColor);
            if (result == 0) {
                mtl = "red";
            }
            else if (result == 1)
            {
                mtl = "yellow";
            }
            else {
                mtl = "green";
            }
        }
    }
}