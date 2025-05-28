using System.IO;
using System;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Drawing.Chart; 
namespace Convetidor_plantillas_excel
{
    public class Excel
    {
      public  string _path {  get; set; }

        public  DataTable read_Excel() {
            DataTable dt = new DataTable();
            try
            {
                if (File.Exists(_path))
                {
                    using (var package = new ExcelPackage(new FileInfo(_path)))
                    {
                      
                        // Obtener la primera hoja de trabajo
                        var worksheet = package.Workbook.Worksheets[0];

                        // Leemos las cabeceras (primera fila)
                        bool firstRow = true;
                        for (int row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
                        {
                            var dataRow = dt.NewRow();

                            for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                            {
                                // Si es la primera fila, agregamos las columnas a la DataTable
                                if (firstRow)
                                {
                                    dt.Columns.Add(worksheet.Cells[row, col].Text);
                                }
                                else
                                {
                                    // Si no es la primera fila, agregamos los datos
                                    dataRow[col - 1] = worksheet.Cells[row, col].Text;
                                }
                            }

                            if (!firstRow)
                            {
                                dt.Rows.Add(dataRow);
                            }

                            firstRow = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El archivo no existe");
                }
            }
            catch (Exception ex) { 
            MessageBox.Show("Error al leer el archivo: "+ex.Message);
            }
            return dt;
        }
        public string report_Excel(DataGridView dataGrid)
        {
            using (var package = new ExcelPackage())
            {
                try
                {
                    // Crear un DataTable para almacenar los datos del DataGridView
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Producto", typeof(string));
                    dataTable.Columns.Add("Fecha", typeof(DateTime));
                    dataTable.Columns.Add("Ingreso Total", typeof(decimal));

                    // Llenar el DataTable con los datos del DataGridView
                    foreach (DataGridViewRow row in dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            var producto = row.Cells["Producto"].Value.ToString();
                            var fecha = DateTime.Parse(row.Cells["Fecha"].Value.ToString());
                            var ingresoTotal = decimal.Parse(row.Cells["Ingreso Total"].Value.ToString());

                            dataTable.Rows.Add(producto, fecha, ingresoTotal);
                        }
                    }

                    // Agrupar los datos por Producto y Año
                    var ventasPorProducto = dataTable.AsEnumerable()
                        .GroupBy(r => new
                        {
                            Producto = r["Producto"].ToString(),
                            Año = ObtenerAnioDeFecha(r["Fecha"].ToString())
                        })
                        .Select(g => new
                        {
                            Producto = g.Key.Producto,
                            Año = g.Key.Año,
                            TotalVentas = g.Sum(r => r.Field<decimal>("Ingreso Total"))
                        })
                        .Where(x => x.Año == 2023 || x.Año == 2025)
                        .OrderBy(x => x.Producto)
                        .ThenBy(x => x.Año)
                        .ToList();

                    // Crear un nuevo archivo Excel para mostrar el Dashboard
                    using (var newPackage = new ExcelPackage())
                    {
                        var newWorksheet = newPackage.Workbook.Worksheets.Add("Dashboard");

                        // Escribir las cabeceras de los datos
                        newWorksheet.Cells[1, 1].Value = "Producto";
                        newWorksheet.Cells[1, 2].Value = "Ventas 2023";
                        newWorksheet.Cells[1, 3].Value = "Ventas 2025";
                        newWorksheet.Cells[1, 4].Value = "Diferencia (2023- 2025)";

                        // Escribir los datos de ventas por producto y año, y calcular la diferencia
                        int rowIndex = 2;
                        var productos = ventasPorProducto.Select(x => x.Producto).Distinct().ToList();
                        foreach (var producto in productos)
                        {
                            decimal ventas2023 = ventasPorProducto.Where(x => x.Producto == producto && x.Año == 2023)
                                                                   .Sum(x => x.TotalVentas);
                            decimal ventas2025 = ventasPorProducto.Where(x => x.Producto == producto && x.Año == 2025)
                                                                   .Sum(x => x.TotalVentas);
                            decimal diferencia = 0;
                            // comprobacion para que para que el numero no sea negativo
                           
                            
                           diferencia = ventas2023 - ventas2025; 

                            newWorksheet.Cells[rowIndex, 1].Value = producto;
                            newWorksheet.Cells[rowIndex, 2].Value = ventas2023;
                            newWorksheet.Cells[rowIndex, 3].Value = ventas2025;
                            newWorksheet.Cells[rowIndex, 4].Value = diferencia; // Columna para la diferencia

                            rowIndex++;
                        }

                        // Crear el gráfico de pastel para mostrar la diferencia de ventas entre 2023 y 2025
                        var barChart = newWorksheet.Drawings.AddChart("DiferenciaVentas", eChartType.ColumnClustered) as ExcelBarChart;
                        barChart.SetPosition(2, 0, 6, 0); // Posición del gráfico en la hoja
                        barChart.SetSize(800, 400); // Tamaño del gráfico

                        // Seleccionar los rangos: diferencias y productos
                        var diferenciaRange = newWorksheet.Cells[2, 4, rowIndex - 1, 4]; // Columna D: Diferencia
                        var productoRange = newWorksheet.Cells[2, 1, rowIndex - 1, 1];   // Columna A: Producto

                        // Agregar datos al gráfico
                        var series = barChart.Series.Add(diferenciaRange, productoRange);
                        series.Header = "Diferencia de Ventas (2023 - 2025)";
                       
                        // Configurar detalles del gráfico
                        barChart.Title.Text = "Diferencia de Ventas por Producto";
                        barChart.YAxis.Title.Text = "Diferencia en Ventas";
                        barChart.XAxis.Title.Text = "Producto";
                        barChart.DataLabel.ShowValue = true;
                        barChart.Title.Text = "Comparativa de Diferencia de Ventas (2023 vs 2025)";

                        // Guardar el archivo Excel con el gráfico
                        var outputFile = new FileInfo(@"C:\Project C#\archivos Excel\Dashboar.xlsx");
                        newPackage.SaveAs(outputFile);
                    }
                }
                catch (Exception ex)
                {
                    return ("Error: " + ex.Message);
                }

                return "Archivo creado con exito";
            } 
        }


        private int ObtenerAnioDeFecha(string fechaString)
        {
            DateTime fecha;
            if (DateTime.TryParse(fechaString, out fecha))
            {
                return fecha.Year; // Retorna el año si la conversión es exitosa
            }
            else
            {
                // Si la fecha no es válida, puedes devolver un valor por defecto o lanzar una excepción
                // Por ejemplo, lanzar una excepción si la fecha no es válida:
                throw new Exception($"La fecha '{fechaString}' no es válida.");
            }
        }
    } 
        
    }

