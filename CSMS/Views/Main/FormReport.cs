using CSMS.Controllers.Main;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace CSMS.Views.Main
{
    public partial class FormReport : Form
    {
        public DateTime startDate;
        MainController mainController = new MainController();
        private void FormReport_Load(object sender, EventArgs e)
        {
            string date = startDate.ToString("yyyy-MM-dd");
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource ds = new ReportDataSource("Customer", mainController.Report(date));
            reportViewer1.LocalReport.DataSources.Add(ds);
            ReportParameterCollection reports= new ReportParameterCollection();
            reports.Add(new ReportParameter ("SumGrandTotal", mainController.DashBord("SELECT SUM(Grand_Total) FROM Orders WHERE Orders_Date LIKE '" + date + "'")));
            reportViewer1.LocalReport.SetParameters(reports);
            this.reportViewer1.RefreshReport();
        }

        public FormReport()
        {
            InitializeComponent();
        }



    }
}

