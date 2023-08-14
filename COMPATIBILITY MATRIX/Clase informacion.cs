using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPATIBILITY_MATRIX
{
    public class Clase_informacion
    {
       // muestra informacion de la imagen de MAESAL
        public void evento_imagen( PictureBox imagen)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "MAESAL";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(imagen, "Aviones de Combate C.16");
        }
        // MUESTRA INFORMACION BOTON PSC
        public void evento_load_excel(Button boton_excel)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "CARGA FICHERO PSC ";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(boton_excel, "Carga fichero EXCEL que contiene PSC a chequear");
        }
        // MUESTRA INFORMACION BOTON RETREIVAL
        public void evento_load_retrieval(Button boton_excel)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "CARGA FICHERO RETRIEVAL ";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(boton_excel, "Carga fichero glu_ess_config_eti...");
        }
        // MUESTRA INFORMACION DEL RATIOBUTTON PSC
        public void evento_radiobutton_PSC(RadioButton radiobuttonPSC)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "MUESTRA FICHERO PSC ";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(radiobuttonPSC, "Muestra el fichero PSC cargado");
        }
        // MUESTRA INFORMACION DEL RATIOBUTTON RETREIVAL
        public void evento_radiobutton_RETREIVAL(RadioButton radiobuttonRETREIVAL)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "MUESTRA FICHERO RETREIVAL ";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(radiobuttonRETREIVAL, "Muestra el fichero glu_ess_config_eti... cargado");
        }
        // MUESTRA INFORMACION DEL RATIOBUTTON CHECK
        public void evento_radiobutton_CHECK(RadioButton radiobuttonCHECK)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "MUESTRA FICHERO COMPATIBILIDAD ";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(radiobuttonCHECK, "Muestra el resultado de la compatibilidad de equipos");
        }
        // MUESTRA INFORMACION DEL BOTON CHECK
        public void evento_CHECK(Button buttonCHECK)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "COMPRUEBA LA COMPATIBILIDAD DE EQUIPOS";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(buttonCHECK, "Realiza la comprobación de la compatibilidad de equipos");
        }
        // MUESTRA INFORMACION DEL BOTON PRINT
        public void evento_PRINT(Button buttonPRINT)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "IMPRIME REPORTE";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(buttonPRINT, "Realiza la impresión del fichero seleccionado");
        }
        // MUESTRA INFORMACION DEL CHECKBOX OK
        public void evento_CheckboxOK (CheckBox checkboxOK)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "SELECCIÓN LRI's COMPATIBLES";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(checkboxOK, "Realiza la impresión de los LRI's Compatibles ");
        }

        // MUESTRA INFORMACION DEL CHECKBOX INCOMPATIBLE
        public void evento_CheckboxINCOMPATIBLE(CheckBox checkboxINCOMPATIBLE)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "SELECCIÓN LRI's INCOMPATIBLES";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(checkboxINCOMPATIBLE, "Realiza la impresión de los LRI's Incompatibles ");
        }
        // MUESTRA INFORMACION DEL CHECKBOX No REPORTADO
        public void evento_CheckboxNO_REPORTADO(CheckBox checkboxNO_REPORTADO)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "SELECCIÓN LRI's QUE NO RESPONDEN";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(checkboxNO_REPORTADO, "Realiza la impresión de los LRI's que no transmiten información ");
        }
        // MUESTRA INFORMACION DEL CHECKBOX No ENCONTRADO
        public void evento_CheckboxNO_ENCONTRADO(CheckBox checkboxNO_ENCONTRADO)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "SELECCIÓN LRI's QUE NO EXISTEN EN ARCHIVO PSC";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(checkboxNO_ENCONTRADO, "Realiza la impresión de los LRI's que no se encuentran en arvhivo PSC ");
        }
        // MUESTRA INFORMACION DEL CHECKBOX VOID
        public void evento_Checkbox_VOID(CheckBox checkboxVOID)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "SW=VOID COMO COMPATIBLE EN COMPATIBILITY MATRIX";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(checkboxVOID, "Realiza el chequeo de COMPATIBILITY MATRIX con  PSC (SW = VOID) como compatible ");
        }
        // MUESTRA INFORMACION DEL NUMERO DE LRI COMPATIBLE
        public void evento_LABELOK(Label labelOK)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "LRI's COMPATIBLES";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(labelOK, "Número de LRI's Compatibles encontrados en RETRIEVAL");
        }
        // MUESTRA INFORMACION DEL NUMERO DE LRI NO COMPATIBLES
        public void evento_LABEL_INCOMPATIBLE(Label labelINCOMPATIBLE)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "LRI's INCOMPATIBLES";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(labelINCOMPATIBLE, "Número de LRI's Incompatibles encontrados en RETRIEVAL");
        }
        // MUESTRA INFORMACION DEL NUMERO DE LRI QUE NOTRANSMITEN INFORMACION
        public void evento_LABEL_NO_REPORTADO(Label label_NO_REPORTADO)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "LRI's QUE NO TRANSMITEN INFORMACIÒN";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(label_NO_REPORTADO, "Número de LRI's que no transmiten información encontrados en RETRIEVAL");
        }
        // MUESTRA INFORMACION DEL NUMERO DE LRI NO ENCONTRADOS EN PSC
        public void evento_LABEL_NO_ENCONTRADO(Label label_NO_ENCONTRADO)
        {
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "LRI's QUE NO ENCONTRADO EN PSC";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(label_NO_ENCONTRADO, "Número de LRI's no encontrados en PSC ");
        }
    }
}
