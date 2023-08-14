using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace COMPATIBILITY_MATRIX
{

    public partial class Matrix : Form
    {
        
        string PSC, Pathtxt, equipment_name, lri_id,sw,hw,serial_number,elapsed_time,lri_old="", lri_selected="";
        bool cargadoPSC=false, cargadoRETRIEVAL=false;
        int ok = 0, incompatible = 0, no_reportado = 0, no_lri = 0;


        //variables utilizada en el boton PRINT para añadir paginas al formulario

        int linesPerPage = 32;
        int count = 0;  // numero de lineas que imprime
        string texto = "";
        int poscab = 240;

        int first_page = 1, posicion_y=280;
        int i = 0, numero_columnas_grid, posx_cab1,posx_cab2,posx_cab3, posx_cab4,posx_cab5, posx_cab6 , posx_cab7 ;
        int posx1, posx2 ,posx3 ,posx4 ,posx5, posx6 ,posx7,fuente;
        string cabecera_col0,cabecera_col1,cabecera_col2,cabecera_col3,cabecera_col4,cabecera_col5,cabecera_col6, cabecera_col_sup1, cabecera_col_sup2;
        string titulo_print,titulo_PSC;

        System.Windows.Forms.DataGridView dataGrid;

        //*******************************************************************************************
        Clase_informacion info = new Clase_informacion(); // instanciamos a la clase informacion
        funciones funcion = new funciones(); // instanciamos a la clase Print
        //****************************************************************************************

        //Boton Imprimir
        private void buttonPrint_Click(object sender, EventArgs e)
        {
           first_page = 1;
            printPreviewDialog1.ShowDialog(); // llamamos al printpreewDialog con el documento printdocument1
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // imprime el grid1 si el radiobuton PSC esta chequeado
            if (radioButtonPSC.Checked)
            {
                dataGrid = dataGridView1;
                cabecera_col0 = "ID"; cabecera_col1 = "EQUIPMENT"; cabecera_col2 = "hw P/N"; cabecera_col3 = "hw P/N code"; cabecera_col4 = "sw P/N";
                cabecera_col5 = "sw mod. code"; cabecera_col6 = "       "; cabecera_col_sup1 = "     "; cabecera_col_sup2 = "     ";
                numero_columnas_grid = 6; posx_cab1 = 42; posx_cab2 = 100; posx_cab3 = 280; posx_cab4 = 420; posx_cab5 = 556;
                posx_cab6 = 656; posx1 = 42; posx2 = 100; posx3 = 280; posx4 = 450; posx5 = 556; posx6 = 725;
                fuente = 9; titulo_print = "       " ;
                titulo_PSC = labelPSC.Text;
            }
            // imprime el grid1 si el radiobuton RETRIEVAL esta chequeado
            if (radioButtonRETRIEVAL.Checked)
            {
                dataGrid = dataGridView2;
                cabecera_col0 = "ID"; cabecera_col1 = "EQUIPMENT"; cabecera_col2 = "serial"; cabecera_col3 = "hw";cabecera_col4 = "sw";
                cabecera_col5 = "elapsed time";cabecera_col6 = "       ";cabecera_col_sup1 = "     ";cabecera_col_sup2 = "     ";
                numero_columnas_grid = 6; posx_cab1 = 72; posx_cab2 = 130; posx_cab3 = 420; posx_cab4 = 490; posx_cab5 = 556;
                posx_cab6 = 626; posx1 = 72; posx2 = 129; posx3 = 420; posx4 = 490; posx5 = 556; posx6 = 626; 
                fuente = 10; titulo_print = "   RETRIEVAL  "+labelAC.Text; titulo_PSC ="";
            }
            // imprime el grid3 si el radiobuton CHECK esta chequeado
            if (radioButtonResult.Checked)
            {
                dataGrid = dataGridView3;
                cabecera_col0 = "ID"; cabecera_col1 = "EQUIPMENT"; cabecera_col2 = "hw";  cabecera_col3 = "sw"; cabecera_col4 = "hw";
                cabecera_col5 = "sw"; cabecera_col6 = "STATUS"; cabecera_col_sup1 = "PSC"; cabecera_col_sup2 = "RTV";
                numero_columnas_grid = 7; posx_cab1 = 72; posx_cab2 = 130; posx_cab3 = 340; posx_cab4 = 410; posx_cab5 = 476;
                posx_cab6 = 546; posx_cab7 = 615;posx1 = 72;posx2 = 129;posx3 =340 ;posx4 =410;posx5 =480; posx6 =550;posx7=620;
                fuente = 12; titulo_print = "COMPATIBILITY MATRIX";
                titulo_PSC = labelPSC.Text;
            }
                // Get the current date.
                DateTime thisDay = DateTime.Today;
                // Display the date in the default (general) format.
                string fecha = thisDay.ToString("d");
                string palabra = "";

                int total = 0;
                try
                { // Create pen.
                    if (first_page == 1)
                    {
                        Pen blackPen = new Pen(Color.Black, 5);

                        // ponemos encabezado
                        e.Graphics.DrawString("MAESAL ", new Font("Arial", 20, FontStyle.Bold), Brushes.Blue, new Point(350, 10));
                        e.Graphics.DrawString("Aviones de Combate C.16 ", new Font("Arial", 16, FontStyle.Bold), Brushes.Blue, new Point(270, 40));
                        e.Graphics.DrawString(titulo_print, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(275, 90));
                        PointF point1 = new PointF(50.0F, 200.0F);
                        PointF point2 = new PointF(780.0F, 200.0F);
                        e.Graphics.DrawLine(blackPen, point1, point2);

                        // Create image.
                        //cargamos imagen donde esta el fichero exe en /bin
                        string root = Application.StartupPath;
                        string root_shm = "/maestranza.png";
                        Image newImage = Image.FromFile((String.Concat(root, root_shm)));


                        // Create coordinates for upper-left corner.

                        // of image and for size of image.
                        float x = 10.0F;
                        float y = 10.0F;
                        float width = 150.0F;
                        float height = 150.0F;

                        // Draw image to screen.
                        e.Graphics.DrawImage(newImage, x, y, width, height);

                        e.Graphics.DrawString(titulo_PSC, new Font("Arial", 16, FontStyle.Bold), Brushes.Violet, new Point(190, 120));
                        e.Graphics.DrawString(labelAC.Text, new Font("Arial", 20, FontStyle.Bold), Brushes.Violet, new Point(350, 160));
                        e.Graphics.DrawString(fecha, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(700, 10));
                        //****************************************************************************************
                    }
                    texto = "";
                    //encabezado 2 ccabecera dek datagrid dependiendo de cual este seleccionado
                    e.Graphics.DrawString(cabecera_col0, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(posx_cab1, poscab)); //72
                    e.Graphics.DrawString(cabecera_col1, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(posx_cab2, poscab));//130
                    e.Graphics.DrawString(cabecera_col2, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(posx_cab3, poscab));//340
                    e.Graphics.DrawString(cabecera_col3, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(posx_cab4, poscab));//410
                    e.Graphics.DrawString(cabecera_col_sup1, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(370, poscab - 20));//370
                    e.Graphics.DrawString(cabecera_col4, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(posx_cab5, poscab));//476
                    e.Graphics.DrawString(cabecera_col5, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(posx_cab6, poscab));//546
                    e.Graphics.DrawString(cabecera_col_sup2, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(505, poscab - 20));//505
                    e.Graphics.DrawString(cabecera_col6, new Font("Arial", 12, FontStyle.Bold), Brushes.Blue, new Point(posx_cab7, poscab));//615

                    // Recorremos las filas del DataGridView hasta que llegemos
                    // a las líneas que nos caben en cada página o al final del grid.

                    while (count < linesPerPage && i < dataGrid.Rows.Count - 1)//mientras no sea numro maximo por hoja
                    {
                        texto = "";
                        for (int columna = 0; columna < numero_columnas_grid; columna++) // mientras columna < 6
                        {
                            palabra = "";
                            if (columna == 0)
                            {
                                // imprimimos columna0
                                palabra = dataGrid.Rows[i].Cells[columna].Value.ToString();
                                texto = palabra;
                                total = texto.Length;
                                e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Black, new Point(posx1, posicion_y));//72
                            }
                            else if (columna == 1)
                            {     // imprimimos columna1
                                palabra = dataGrid.Rows[i].Cells[columna].Value.ToString();
                                texto = palabra;
                                e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Black, new Point(posx2, posicion_y));//129
                            }
                            else if (columna == 6)
                            {   // imprimimos columna6
                                palabra = dataGrid.Rows[i].Cells[columna].Value.ToString();
                                texto = palabra;
                                if (texto == "INCOMPATIBLE SW" || texto == "INCOMPATIBLE hW")
                                    e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Red, new Point(posx7, posicion_y));//620
                                else if (texto == "NO REPORTADO")
                                    e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Orange, new Point(posx7, posicion_y));//620
                                else if (texto == "OK")
                                    e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Green, new Point(posx7, posicion_y));//620
                                else if (texto == "LRI NO EXISTE PSC")
                                    e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Gray, new Point(posx7, posicion_y));//620
                                else
                                    e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Black, new Point(posx7, posicion_y));//620
                            }
                            else if (columna == 2)
                            {   // iprimimos columna2
                                palabra = dataGrid.Rows[i].Cells[columna].Value.ToString();
                                texto = palabra;
                                e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Black, new Point(posx3, posicion_y));//340
                            }
                            else if (columna == 3)
                            {   // imprimimos columna 3
                                palabra = dataGrid.Rows[i].Cells[columna].Value.ToString();
                                texto = palabra;
                                e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Black, new Point(posx4, posicion_y));//410
                            }
                            else if (columna == 4)
                            {   // imprimimos columna 4
                                palabra = dataGrid.Rows[i].Cells[columna].Value.ToString();
                                texto = palabra;
                                e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Black, new Point(posx5, posicion_y));//480
                            }
                            else if (columna == 5)
                            {   // imprimimos columna 5
                                palabra = dataGrid.Rows[i].Cells[columna].Value.ToString();
                                texto = palabra;
                                e.Graphics.DrawString(texto, new Font("Arial", fuente, FontStyle.Bold), Brushes.Black, new Point(posx6, posicion_y));//550
                            }

                        }
                        posicion_y = posicion_y + 25;
                        count++;
                        i++;

                    }

                    //  NOTA  hay que sacarlo fuera del bucle sino no funciona no aumenta pagina para impresion
                    if (i < this.dataGrid.Rows.Count - 1)
                    {
                        e.HasMorePages = true;
                        count = 0;
                        linesPerPage = 32;
                        posicion_y = 280;
                    }
                    else
                    {
                        e.HasMorePages = false;
                        i = 0;
                        posicion_y = 280;
                        linesPerPage = 32;
                    }


                }
                catch (Exception exc) // si ocurre alguna exception se muestra en la variable exc
                {
                    MessageBox.Show(exc.Message.ToString());
                }
            
        }

      

        //checkbosVOID 
        private void checkBoxVOID_CheckedChanged(object sender, EventArgs e)
        {
            // si hay lineas en grid3 limpiamos datagrid3
            if (dataGridView3.Rows.Count > 1)
                dataGridView3.Rows.Clear();
        }

 
        
        
        // muestra informacion del boton checkbox picturebox etc cuando se pasa el raton por ella
        private void Matrix_Load(object sender, EventArgs e)
        {
            
             info.evento_imagen(pictureBoxMaesal);// muestra información de la imagen MAESAL
             info.evento_load_excel(ButtonLoad); // muestra información del boton PSC
             info.evento_load_retrieval(buttonRetreival); // muestra informacion boton RETREIVAL
             info.evento_radiobutton_PSC(radioButtonPSC); // muestra informacion del boton radiobutton PSC
             info.evento_radiobutton_RETREIVAL(radioButtonRETRIEVAL); // muestra informacion del boton radiobutton RETREIVAL
             info.evento_radiobutton_CHECK(radioButtonResult); // muestra informacion del boton radiobutton CHECK
             info.evento_CHECK(button1); // muestra informacion del boton radiobutton CHECK
             info.evento_PRINT(buttonPrint); // muestra informacion del boton PRINT
             info.evento_CheckboxOK(checkBoxOK); // muestra informacion del checkbox OK
             info.evento_CheckboxINCOMPATIBLE(checkBoxINCOMPATIBLE); // muestra informacion del checkbox INCOMPATIBLE
             info.evento_CheckboxNO_REPORTADO(checkBoxNO_RESPUESTA); // muestra informacion del checkbox NO RESPONDE
             info.evento_CheckboxNO_ENCONTRADO(checkBoxNO_ENCONTRADO); // muestra informacion del checkbox NO ENCONTRADO
             info.evento_Checkbox_VOID(checkBoxVOID); // muestra informacion del checkboxVOID
             info.evento_LABELOK(labeloK); // muestra informacion del LABEL OK
             info.evento_LABEL_INCOMPATIBLE(labelIncompatible); // muestra informacion del LABEL INCOMPATIBLE
             info.evento_LABEL_NO_REPORTADO(labelNo_reported); // muestra informacion del LABEL NO REPORTADO
             info.evento_LABEL_NO_ENCONTRADO(labelNo_LRI); // muestra informacion del LABEL labelNo_LRI
        }

     


        // casilla checkbox del datagridview3 para seleccionar el mismo lri en datagridview1 PSC y datagridview2 RETREIVAL
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[7];


            DataGridView datagrid_selected = new DataGridView();
            datagrid_selected = dataGridView3;
            lri_selected = datagrid_selected.CurrentRow.Cells[0].Value.ToString(); // extraemos el lri de la fila seleccionada

           
            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    //quitamos seleccion de la fila en datagrid1 PSC cuando quitamos checkbox en datagrid3 RETREIVAL
                    for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                    {
                        if (dataGridView1.Rows[fila].Cells[0].Value.ToString() == lri_selected)
                        {
                            dataGridView1.Rows[fila].Selected = false;
                           // fila = dataGridView1.Rows.Count + 1;
                        }
                    }
                    //quitamos seleccion de la fila en datagrid2 cuando quitamos checkbox en datagrid3 CHECK
                    for (int fila = 0; fila < dataGridView2.Rows.Count - 1; fila++)
                    {
                        if (dataGridView2.Rows[fila].Cells[0].Value.ToString() == lri_selected)
                        {
                            dataGridView2.Rows[fila].Selected = false;
                            fila = dataGridView2.Rows.Count + 1;
                        }
                    }
                    break;
                case "False": // si marcamos la casilla check de datagrid3 RETREIVAL
                    ch1.Value = true;
                    // seleccion de la fila en datagrid1 PSC cuando quitamos checkbox en datagrid3 CHECK
                                  
                    for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                    {
                        if (dataGridView1.Rows[fila].Cells[0].Value.ToString() == lri_selected)
                        {
                            dataGridView1.Rows[fila].Selected = true;

                           // fila = dataGridView1.Rows.Count + 1;
                        }
                    }
                    // seleccion de la fila en datagrid2 RETREIVAL cuando quitamos checkbox en datagrid3 CHECK
                    for (int fila = 0; fila < dataGridView2.Rows.Count - 1; fila++)
                    {
                        if (dataGridView2.Rows[fila].Cells[0].Value.ToString() == lri_selected)
                        {
                            dataGridView2.Rows[fila].Selected = true;
                            fila = dataGridView2.Rows.Count + 1;
                        }
                    }
                    break;
            }
        }



        // BOTON CHECK
        private void button1_Click(object sender, EventArgs e)
        {
            check();  // llamamos a la funcion check
        }

        


        //funcion chequear compatibility matrix 
        void check()
        {
            // si hay lineas en grid3 limpiamos datagrid3
            if (dataGridView3.Rows.Count > 1)
                dataGridView3.Rows.Clear();

            radioButtonResult.Checked = true;
            dataGridView3.Visible = true;

            dataGridView3.Refresh();
            ok = 0; incompatible = 0; no_reportado = 0; no_lri = 0;
            // recorremos datagrid1 PSC 


            for (int fila = 0; fila < dataGridView2.Rows.Count - 1; fila++)
            {
                string lri_id = dataGridView2.Rows[fila].Cells[0].Value.ToString();
                string equipment_name = dataGridView2.Rows[fila].Cells[1].Value.ToString();
                string hw = dataGridView2.Rows[fila].Cells[3].Value.ToString();
                string sw = dataGridView2.Rows[fila].Cells[4].Value.ToString();

                // buscamos lri en retreival con la funcion buscar pasamos lri,hw,sw del RETREIVAL nos devuelve sw y hw del PSC
                (string namePSC, string sw_PSC, string hw_PSC, string status) = funcion.buscar_lri(lri_id, hw, sw, equipment_name,dataGridView1,checkBoxVOID);
                if (checkBoxOK.Checked == true && status == "OK")
                {
                    string[] row = new string[] { lri_id, namePSC, hw_PSC, sw_PSC, hw, sw, status }; //rellenamos la filas de stringgrid3
                    dataGridView3.Rows.Add(row);
                    ok++;
                    dataGridView3.Rows[fila].Cells[6].Style.BackColor = Color.LightGreen;  // cambiamos el color de  una celda
                    // marcamos check en datagridview2
                    dataGridView2.Rows[fila].Cells[6].Value = true;  // marcamos checkbox 
                }
                else if (checkBoxINCOMPATIBLE.Checked == true && status == "INCOMPATIBLE SW")
                {
                    string[] row = new string[] { lri_id, namePSC, hw_PSC, sw_PSC, hw, sw, status }; //rellenamos la filas de stringgrid3
                    dataGridView3.Rows.Add(row);
                    incompatible++;
                    dataGridView3.Rows[fila].Cells[6].Style.BackColor = Color.Red;
                    // marcamos check en datagridview2
                    dataGridView2.Rows[fila].Cells[6].Value = true;  // marcamos checkbox 
                }
                else if (checkBoxINCOMPATIBLE.Checked == true && status == "INCOMPATIBLE HW")
                {
                    string[] row = new string[] { lri_id, namePSC, hw_PSC, sw_PSC, hw, sw, status }; //rellenamos la filas de stringgrid3
                    dataGridView3.Rows.Add(row);
                    incompatible++;
                    dataGridView3.Rows[fila].Cells[6].Style.BackColor = Color.Red;
                    // marcamos check en datagridview2
                    dataGridView2.Rows[fila].Cells[6].Value = true;  // marcamos checkbox 
                }
                else if (checkBoxNO_RESPUESTA.Checked == true && status == "NO REPORTADO")
                {
                    string[] row = new string[] { lri_id, namePSC, hw_PSC, sw_PSC, hw, sw, status }; //rellenamos la filas de stringgrid3
                    dataGridView3.Rows.Add(row);
                    no_reportado++;
                    dataGridView3.Rows[fila].Cells[6].Style.BackColor = Color.Orange;
                    // marcamos check en datagridview2
                    dataGridView2.Rows[fila].Cells[6].Value = true;  // marcamos checkbox 
                }
                else if (checkBoxNO_ENCONTRADO.Checked == true && status == "LRI NO EXISTE PSC")
                {
                    string[] row = new string[] { lri_id, namePSC, hw_PSC, sw_PSC, hw, sw, status }; //rellenamos la filas de stringgrid3
                    dataGridView3.Rows.Add(row);
                    no_lri++;
                    dataGridView3.Rows[fila].Cells[6].Style.BackColor = Color.Gray;
                    // marcamos check en datagridview2
                    dataGridView2.Rows[fila].Cells[6].Value = true;  // marcamos checkbox 
                }
            }
            // aplicamos suma a los label correspondientes
            labeloK.Text = Convert.ToString(ok);
            labelIncompatible.Text = Convert.ToString(incompatible);
            labelNo_reported.Text = Convert.ToString(no_reportado);
            labelNo_LRI.Text = Convert.ToString(no_lri);
        }


        
        //**********************************************************************************************

        // RADIOBUTTON CHECK
        private void radioButtonResult_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            dataGridView1.Visible = false;
            dataGridView3.Visible = true;
            radioButtonResult.ForeColor = Color.Blue;
            radioButtonRETRIEVAL.ForeColor = Color.Black;
            radioButtonPSC.ForeColor = Color.Black;
        }

        bool add_row = false;
        public Matrix()
        {
            InitializeComponent();
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            // The user wants to exit the application. Close everything down.
            Application.Exit();
        }

        private void radioButtonPSC_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            dataGridView1.Visible = true;
            dataGridView3.Visible = false;
            radioButtonResult.ForeColor = Color.Black;
            radioButtonRETRIEVAL.ForeColor = Color.Black;
            radioButtonPSC.ForeColor = Color.Blue;
        }

        private void radioButtonRETRIEVAL_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible=false;
            dataGridView2.Visible=true;
            dataGridView3.Visible=false;
            radioButtonResult.ForeColor = Color.Black;
            radioButtonRETRIEVAL.ForeColor = Color.Blue;
            radioButtonPSC.ForeColor = Color.Black;
        }

        // BOTON LOAD RETREIVAL
        private void buttonRetreival_Click(object sender, EventArgs e)
        {
            labeloK.Text = Convert.ToString("000");
            labelIncompatible.Text = Convert.ToString("000");
            labelNo_reported.Text = Convert.ToString("000");
            labelNo_LRI.Text = Convert.ToString("000");

            labelAC.ForeColor = Color.DarkViolet;
            bool  no_lri = false;
            if (dataGridView3.Rows.Count > 1)
                dataGridView3.Rows.Clear();

            dataGridView2.Rows.Clear();
            cargadoRETRIEVAL = false;
            radioButtonRETRIEVAL.Checked=true;
            progressBar2.Value = 0;
            OpenFileDialog openFileDialogtxt = new OpenFileDialog();
            openFileDialogtxt.Filter = "TXT files |*.txt";
            if (openFileDialogtxt.ShowDialog() == DialogResult.OK)

            {
                Pathtxt = openFileDialogtxt.FileName; // almacenamos path fichero txt
               
                    try
                    {
                    // Get the no. of columns in the first row.
                      int colCount = dataGridView2.Columns.Count;
               
                       add_row = false;
                       labelAC.Text = "";

                       string[] lines = File.ReadAllLines(Pathtxt);
                    
                       progressBar2.Maximum = lines.Length;
                       progressBar2.Value = progressBar2.Minimum;
                       using (StreamReader reader = new StreamReader(Pathtxt))
                      
                    //leemos archivo txt
                 
                       {
                            string line;
                            
                            equipment_name = "";
                            lri_id = "";
                            serial_number = "";
                            hw = "";
                            sw = "";
                            elapsed_time="";

                            while ((line = reader.ReadLine()) != null)
                            {
                          
                                if (line.Length > 20)  // la linea obtenida tiene que ser mayor que 20 para procesar el tail_number daría error si la longitud 
                                                       // de la linea es menor para obtener el ac_tail
                                {
                                    // obtenemos el ac_tail
                                    if (line.Substring(0, 15) == ".ac_tail_number")
                                    {
                                        labelAC.Text = line.Substring(20,(line.Length-20));
                                    }
                                    else if (line.Substring(0,15) == ".equipment_name")
                                    {
                                        equipment_name=line.Substring(19,(line.Length-19));
                                      
                                    }
                                    else if (line.Substring(0,22) == ".config_data_lri_ident")
                                    {
                                        add_row = false;
                                        lri_id =line.Substring(26,(line.Length-26));
                                        if (lri_id == "00")
                                            no_lri = false;   // no hay identificador de lri 
                                            
                                        else
                                            no_lri=true;
                                    }
                                    else if (line.Substring(0,19) == ".serial_number_code")
                                    {
                                        serial_number = line.Substring(23,(line.Length-23));
                                    }
                                    else if (line.Substring(0,21) == ".sw_modification_code")
                                    {
                                        sw = line.Substring(25, (line.Length - 25));
                                    }
                                    else if (line.Substring(0, 20) == ".hw_part_number_code")
                                    {
                                        hw = line.Substring(24, (line.Length - 24));

                                    }
                                    else if (line.Substring(0,21) == ".lri_elapsed_time_ind")
                                    {
                                        elapsed_time=line.Substring(25,(line.Length-25));
                                        add_row = true;
                                    }
                                    if (no_lri == true)
                                    {
                                        if (add_row == true)
                                        {
                                            string[] row = new string[] { lri_id, equipment_name, serial_number, hw, sw, elapsed_time }; //rellenamos la primera fila
                                            dataGridView2.Rows.Add(row);
                                            add_row=false;
                                            no_lri=false;
                                        }
                                    }
                                  
                                }
                                progressBar2.Value++;
                            }
                       }
                       cargadoRETRIEVAL = true;
                    }

                    catch (Exception ex)
                    {
                    MessageBox.Show(ex.Message);
                    }
                    if (cargadoPSC==true && cargadoRETRIEVAL== true)
                    {
                       button1.Enabled = true;
                    }
             
            }

            // mirar si hay lri repetidos si los hay es que se han añadido datos de otro retrieval
            this.dataGridView2.Sort(this.dataGridView2.Columns["ID"], ListSortDirection.Ascending); // ORDENAMOS datagridview
            if (dataGridView2.Rows.Count > 1)
            {
                lri_old = dataGridView2.Rows[0].Cells[0].Value.ToString();
                string lri_actual;
                for (int fila = 1; fila < dataGridView2.Rows.Count - 1; fila++)
                {
                    lri_actual = dataGridView2.Rows[fila].Cells[0].Value.ToString();
                    if (lri_old == lri_actual)
                    {
                        MessageBox.Show("ID duplicados, borrar archivo y hacer un nuevo RETRIEVAL", "LRI duplicados",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        fila = dataGridView2.Rows.Count + 1;
                        cargadoRETRIEVAL = false; // desactivo boton CHECK
                        labelAC.ForeColor = Color.Red;

                        labelAC.Text = "ID duplicados en file RETRIEVAL";
                    }
                    lri_old = lri_actual;
                }
            }
            dataGridView2.ClearSelection();
        }



        // boton load PSC (archivo excel)
        private void ButtonLoad_Click(object sender, EventArgs e)
        {

            labeloK.Text = Convert.ToString("000");
            labelIncompatible.Text = Convert.ToString("000");
            labelNo_reported.Text = Convert.ToString("000");
            labelNo_LRI.Text = Convert.ToString("000");

            if (dataGridView3.Rows.Count > 1)
                     dataGridView3.Rows.Clear();

           
            radioButtonPSC.Checked = true;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = progressBar2.Minimum;
            cargadoPSC = false;

            OpenFileDialog  openFileDialogexcel= new OpenFileDialog();
            openFileDialogexcel.Filter = "EXCEL files |*.xlsx;*.xls";
            if (openFileDialogexcel.ShowDialog() == DialogResult.OK)
              
            { 
               
                PSC = openFileDialogexcel.FileName;
                int inicio = 0;
                bool sw = true;
                int final = 1;
                for (int i = PSC.Length-1; i > 0; i--)
                {
                    if  (i != PSC[i] &&  PSC[i] != '\\')  // buscamos inicio del nombre PSC para ponerlo en label PSC
                    {
                        if (PSC[i] != '.' && sw==true) // buscamos el final de la extension para no pornerla en label PSC
                        {
                            final++;   
                        }
                        else
                        {
                            sw = false;
                        }
                    }
                    else
                    {
                        inicio = i+1;
                        i = 0;
                    }
                }
                labelPSC.Text = PSC.Substring(inicio, PSC.Length-inicio-final);
            }
            try
            {

                using (var stream = File.Open(PSC, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var data = reader.AsDataSet();
                        dataGridView1.DataSource = data.Tables[0];
                    }
                   
                }
                // cambiamos el headertext por la fila 1 del stringgrid ya que sale column1 colum2 etc
  
                    dataGridView1.Columns[0].HeaderText = Convert.ToString(dataGridView1.Rows[0].Cells[0].Value);
                    dataGridView1.Columns[1].HeaderText = Convert.ToString(dataGridView1.Rows[0].Cells[1].Value);
                    dataGridView1.Columns[2].HeaderText = Convert.ToString(dataGridView1.Rows[0].Cells[2].Value);
                    dataGridView1.Columns[3].HeaderText = Convert.ToString(dataGridView1.Rows[0].Cells[3].Value);
                    dataGridView1.Columns[4].HeaderText = Convert.ToString(dataGridView1.Rows[0].Cells[4].Value);
                    dataGridView1.Columns[5].HeaderText = Convert.ToString(dataGridView1.Rows[0].Cells[5].Value);

                    // eliminamos primera fila que tenia el header actual en la fila 0
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    dataGridView1.Rows.RemoveAt(rowIndex);
                    cargadoPSC = true;
                    progressBar1.Value = 100;
                    dataGridView1.ClearSelection();
                   
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // activamos boton CHECK cuando los dos archivos estan cargados
            if (cargadoPSC == true && cargadoRETRIEVAL == true)
            {
                button1.Enabled = true;
            }
        }
    }
}
