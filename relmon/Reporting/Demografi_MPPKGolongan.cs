namespace relmon.Reporting
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Report2.
    /// </summary>
    public partial class Demografi_MPPKGolongan : Telerik.Reporting.Report
    {
        public Demografi_MPPKGolongan(int comp_id)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.sqlDataSource1.Parameters.Clear();
            this.sqlDataSource1.Parameters.Add("@comp_id", System.Data.DbType.Int32, comp_id);
            this.sqlDataSource2.Parameters.Clear();
            this.sqlDataSource2.Parameters.Add("@comp_id", System.Data.DbType.Int32, comp_id);
            this.sqlDataSource3.Parameters.Clear();
            this.sqlDataSource3.Parameters.Add("@comp_id", System.Data.DbType.Int32, comp_id);
            this.sqlDataSource4.Parameters.Clear();
            this.sqlDataSource5.Parameters.Add("@comp_id", System.Data.DbType.Int32, comp_id);
            this.sqlDataSource5.Parameters.Clear();
            this.sqlDataSource5.Parameters.Add("@comp_id", System.Data.DbType.Int32, comp_id);
            this.sqlDataSource6.Parameters.Clear();
            this.sqlDataSource6.Parameters.Add("@comp_id", System.Data.DbType.Int32, comp_id);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}