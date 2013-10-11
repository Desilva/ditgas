namespace relmon.Reporting
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Demografi_Organisasi.
    /// </summary>
    public partial class Demografi_Organisasi : Telerik.Reporting.Report
    {
        public Demografi_Organisasi(int comp_id)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            this.sqlDataSource1.Parameters.Clear();
            this.sqlDataSource1.Parameters.Add("@comp_id", System.Data.DbType.Int32, comp_id);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}