using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPATIBILITY_MATRIX
{
    internal class funciones
    {

        // funcion para buscar el lri_id en PSC
        // comparamos cada lri del datagrid2 RETRIEVAL pasado por parametro en la funcion con datos del datagrid1  datagrid1PSC
        public (string, string, string, string) buscar_lri(string lri_id, string hw, string sw, string equipment_name,
            System.Windows.Forms.DataGridView dataGridView1,System.Windows.Forms.CheckBox checkBoxVOID)
        {
            string sw_PSC = " ", hw_PSC = " ", status = "INCOMPATIBLE HW", sw_encontrado = "", hw_encontrado = "", namePSC = "";
            bool encontrado_lri = false, encontrado_hw = false, encontrado_sw = false, no_reported = false;
            {

                for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                {
                    // obtenemos lri en fichero PSC
                    string lri_PSC = dataGridView1.Rows[fila].Cells[0].Value.ToString();

                    if (lri_id == lri_PSC)  // si encontramos lri_id de RETREIVAL es igual al lri_PSC
                    {
                        // si harware es 0000 NO REPORTA NADA EL LRI
                        namePSC = dataGridView1.Rows[fila].Cells[1].Value.ToString();
                        if (hw != "0000")
                        {    // marcamos en datagrid1 con verde cada linea que se comprueba del PSC
                            dataGridView1.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;

                            encontrado_lri = true;
                            hw_PSC = dataGridView1.Rows[fila].Cells[3].Value.ToString();
                            if (hw == hw_PSC)   // si el hw PSC = hw RETREIVAL
                            {
                                encontrado_hw = true;
                                sw_PSC = dataGridView1.Rows[fila].Cells[5].Value.ToString();
                                if (checkBoxVOID.Checked == true) // si marcamos void lo tratamos como compatible en psc independientemente lo que devuelva lri en retrieval
                                {
                                    if (sw == sw_PSC || sw_PSC == "VOID")  // si el sw == sw RETREIVAL 
                                    {
                                        encontrado_sw = true;
                                        sw_encontrado = sw_PSC;
                                        hw_encontrado = hw_PSC;
                                    }
                                }
                                else
                                {
                                    if (sw == sw_PSC)  // si el sw == sw RETREIVAL 
                                    {
                                        encontrado_sw = true;
                                        sw_encontrado = sw_PSC;
                                        hw_encontrado = hw_PSC;
                                    }
                                }
                            }
                        }
                        else
                        {
                            status = "NO REPORTADO";
                            return (namePSC, sw_encontrado, hw_encontrado, status);
                        }
                    }
                }

            }

            if (encontrado_sw == true && encontrado_hw == true && encontrado_lri == true && no_reported == false)
            {
                status = "OK";
                return (namePSC, sw_encontrado, hw_encontrado, status);
            }
            else if (encontrado_sw == false && encontrado_hw == true && encontrado_lri == true && no_reported == false)
            {
                status = "INCOMPATIBLE SW";
                return (namePSC, sw_encontrado, hw_encontrado, status);
            }
            else if (encontrado_sw == false && encontrado_hw == false && encontrado_lri == true && no_reported == false)
            {
                status = "INCOMPATIBLE HW";
                return (namePSC, sw_encontrado, hw_encontrado, status);
            }

            else if (encontrado_sw == false && encontrado_hw == false && encontrado_lri == false && no_reported == false)
            {
                status = "LRI NO EXISTE PSC";
                return (namePSC, sw_encontrado, hw_encontrado, status);
            }
            else
            {
                status = "NO REPORTADO";
                return (namePSC, sw_encontrado, hw_encontrado, status);
            }
        }
    }
}
