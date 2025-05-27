using System.IO;
using System;
using OfficeOpenXml;

namespace Convetidor_plantillas_excel
{
    public partial class Form1 : Form
    {
        static Excel file = new Excel();
        public Form1()
        {
            InitializeComponent();
            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "XLSX files|*.xlsx";
            od.Multiselect = false;
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (od.FileNames.Length > 0)
                {

                    lblpath.Text = Convert.ToString(od.FileNames[0]);
                    file._path = lblpath.Text;
                    dtgridInfo.DataSource = file.read_Excel();
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún archivo.");
                }
            }
            else
            {
                MessageBox.Show("El usuario ha cancelado la selección.");
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          MessageBox.Show(  file.report_Excel(dtgridInfo) );
            this.Close();
           
        }
    }
}
