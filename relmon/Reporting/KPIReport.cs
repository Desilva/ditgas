namespace relmon.Reporting
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for KPIReport.
    /// </summary>
    public partial class KPIReport : Telerik.Reporting.Report
    {
        public KPIReport(int id,int tahun)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.sqlDataSource1.Parameters.Clear();
            this.sqlDataSource1.Parameters.Add("@comp_id", System.Data.DbType.Int32, id);
            this.sqlDataSource1.Parameters.Add("@tahun", System.Data.DbType.Int32, tahun);
            this.sqlDataSource2.Parameters.Clear();
            this.sqlDataSource2.Parameters.Add("@comp_id", System.Data.DbType.Int32, id);
            this.sqlDataSource2.Parameters.Add("@tahun", System.Data.DbType.Int32, tahun);
            this.sqlDataSource3.Parameters.Clear();
            this.sqlDataSource3.Parameters.Add("@comp_id", System.Data.DbType.Int32, id);
            this.sqlDataSource3.Parameters.Add("@tahun", System.Data.DbType.Int32, tahun);
            this.sqlDataSource4.Parameters.Clear();
            this.sqlDataSource4.Parameters.Add("@comp_id", System.Data.DbType.Int32, id);
            this.sqlDataSource4.Parameters.Add("@tahun", System.Data.DbType.Int32, tahun);
            //this.table1.DataSource = sqlDataSource1;
            //this.table6.DataSource = sqlDataSource3;
        }

        
    }
}