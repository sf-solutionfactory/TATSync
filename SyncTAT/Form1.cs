using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using EntityFramework.BulkInsert.Extensions;
using EqualityComparer;
//Nuget Install-Package AutoClosingMessageBox
namespace SyncTAT
{
    public partial class Form1 : Form
    {
        static List<CLIENTE> CLIENTEs = new List<CLIENTE>();
        static List<CUENTAA> cUENTAAs = new List<CUENTAA>();
        static List<CUENTAGL> cUENTAGLs = new List<CUENTAGL>();
        static List<UMEDIDA> uMEDIDAs = new List<UMEDIDA>();
        static List<UMEDIDAT> uMEDIDATs = new List<UMEDIDAT>();
        static List<PROVEEDOR> PROVEEDORs = new List<PROVEEDOR>();
        static List<MONEDA> MONEDAs = new List<MONEDA>();
        static List<MATERIAL> pRESUPUESTOPSC = new List<MATERIAL>();
        static List<SOCIEDAD> SOCIEDADs = new List<SOCIEDAD>();
        static List<TCAMBIO> TCAMBIOs = new List<TCAMBIO>();
        static List<PAIS> PAISs = new List<PAIS>();
        static List<IIMPUESTO> iIMPUESTOs = new List<IIMPUESTO>();
        static List<IMPUESTO> iMPUESTOs = new List<IMPUESTO>();
        static List<MATERIAL> mATERIALs = new List<MATERIAL>();
        static List<MATERIALT> mATERIALTs = new List<MATERIALT>();
        static List<MATERIALVKE> mATERIALVKEs = new List<MATERIALVKE>();
        static List<MATERIALGP> mATERIALGPs = new List<MATERIALGP>();
        static List<MATERIALGPT> mATERIALGPTs = new List<MATERIALGPT>();
        Entities db = new Entities();
        SettingsPersonal set = new SettingsPersonal();        
        string rutaL = "";
        String logarchivos = "";
        int segundos = 0;
        public Form1()
        {
            InitializeComponent();
            cargarConfig();
            DialogResult res = AutoClosingMessageBox.Show("Iniciar sincronización automática", "Inicio de sincronización automática", 5000, MessageBoxButtons.YesNo, DialogResult.OK);
            if (res == DialogResult.Yes)
            {
                if (!String.IsNullOrEmpty(txtArchivoS.Text)&& !String.IsNullOrEmpty(txtArchivo.Text))
                {
                    cargaS(true);
                }
                dudSegundos.Value = set.IndexSegundos;
                segundos = Convert.ToInt32(dudSegundos.Value) * 1000;
                timer1.Interval = segundos;
                timer1.Start();
            }
        }

        private void btnExaminarS_Click(object sender, EventArgs e)
        {
            this.txtArchivoS.Text = examinarRutaC(txtArchivoS.Text);
        }
        private void btnExaminarP_Click(object sender, EventArgs e)
        {
            this.txtArchivo.Text = examinarRutaC(txtArchivo.Text);
        }
        private void btnExaminarL_Click(object sender, EventArgs e)
        {
            this.txtArchivoL.Text = examinarRutaC(txtArchivoL.Text);
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            textBoxLog.ResetText();
            if (!String.IsNullOrEmpty(txtArchivoS.Text)&& !String.IsNullOrEmpty(txtArchivo.Text))
            {
                cargaS(false);
            }
            else
            {
                MessageBox.Show("Debe ingresar una ruta para los archivos SAP");
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarDatosTAT();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            pRESUPUESTOPSC.Clear();
            SOCIEDADs.Clear();
        }
        private void guardarConfiguracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardarConfig();
        }
        private void configuracionDeCorreoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigCorreo conf = new ConfigCorreo();
            DialogResult re = conf.ShowDialog();
        }
        private void configuracionDeConexionBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigConexion conf = new ConfigConexion();
            DialogResult re = conf.ShowDialog();
            MessageBox.Show("Salga del programa y vuelva a entrar para aplicar los cambios de conexión", "Guardar configuración", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        private void cargarConfig()
        {
            txtArchivoS.Text = set.ArchivoSAP;
            txtArchivo.Text = set.ArchivoProc;
            txtArchivoL.Text = set.ArchivoLog;
            AppSettings settings = new AppSettings();
            string sett = settings.getConnectionString("TAT001Entities");
            int inicio = 0, fin = 0;
            inicio = sett.IndexOf("user id=");
            if (inicio > 0)
            {
                inicio += "user id=".Length;
                sett = sett.Remove(0, inicio);
                inicio = sett.IndexOf(";");
                txtUsuario.Text = sett.Substring(0, inicio);
            }
            //txtUsuario.Text = set.Usuario;
            //if (set.FechaAnual != null) mtxtFecha.Text = set.FechaAnual.ToString("dd/MM/yyyy");
            dudSegundos.Value = set.IndexSegundos;
            segundos = Convert.ToInt32(dudSegundos.Value) * 1000;
            bool ban = set.cerrar;
            chkCerrar.Checked = ban;
        }
        private void guardarConfig()
        {
            set.ArchivoSAP = txtArchivoS.Text;
            set.ArchivoProc = txtArchivo.Text;
            set.ArchivoLog = txtArchivoL.Text;
            set.Usuario = txtUsuario.Text;
            //set.FechaAnual = Convert.ToDateTime(mtxtFecha.Text);
            set.IndexSegundos = Convert.ToInt32(dudSegundos.Value);
            segundos = Convert.ToInt32(dudSegundos.Value) * 1000;
            set.cerrar = chkCerrar.Checked;
            set.Save();
        }
        private bool validarArchivoS()
        {
            if (String.IsNullOrEmpty(txtArchivoS.Text) == false)
            {
                DirectoryInfo files = new DirectoryInfo(txtArchivoS.Text);
                if (files.Exists)
                {
                        if (String.IsNullOrEmpty(txtArchivoL.Text) == false)                            
                        {
                            return true;
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Agrege la carpeta contenedora del log", "Error de directorio", segundos, MessageBoxButtons.OK, DialogResult.OK);
                            grabarLog("Agrege la carpeta contenedora del log");
                            return false;
                        }
                }
                else
                {
                    AutoClosingMessageBox.Show("No existe el directorio para carga de SAP", "Error de directorio", segundos, MessageBoxButtons.OK, DialogResult.OK);
                    grabarLog("No existe el directorio para carga de SAP");
                    return false;
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Llene el campo de archivos SAP", "Error al Guardar", segundos, MessageBoxButtons.OK, DialogResult.OK);
                grabarLog("Llene el campo de archivos SAP");
                return false;
            }
        }

        private void cargaACCO(StreamReader strem, string filename)
        {
            var cuentas = db.CUENTAA.ToList();
            var cuentagls = db.CUENTAGL.ToList();
            GenericEqualityComparer<CUENTAA> pks = new GenericEqualityComparer<CUENTAA>((a1, a2) => a1.SOCIEDAD_ID == a2.SOCIEDAD_ID && a1.CLAVE == a2.CLAVE && a1.CUENTA_ID == a2.CUENTA_ID);
            GenericEqualityComparer<CUENTAGL> pks1 = new GenericEqualityComparer<CUENTAGL>((a1, a2) => a1.ID == a2.ID);
            int nregistros = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "" && lines[1] != "" && lines[2] != "")
                {
                    decimal c = decimal.Parse(lines[2]);
                    bool existeCta = cuentagls.Any(x => x.ID == c);
                    if (!existeCta)
                    {
                        CUENTAGL cgl = new CUENTAGL();
                        cgl.ID = c;
                        cgl.NOMBRE = lines[4] != "" ? lines[4] : null;
                        cgl.ACTIVO = true;
                        if (!cUENTAGLs.Contains(cgl, pks1))
                        {
                            cUENTAGLs.Add(cgl);
                            db.CUENTAGL.Add(cgl);
                            db.SaveChanges();
                        }
                    }
                    var existeregistro = cuentas.SingleOrDefault(t => t.SOCIEDAD_ID == lines[0] && t.CLAVE == lines[1] && t.CUENTA_ID == c);
                    if (existeregistro == null)
                    {
                        try
                        {
                            CUENTAA cuenta = new CUENTAA();
                            cuenta.SOCIEDAD_ID = lines[0];
                            cuenta.CLAVE = lines[1];
                            cuenta.CUENTA_ID = decimal.Parse(lines[2]);
                            cuenta.NOMBRE1 = lines[3] != "" ? lines[3] : null;
                            cuenta.NOMBRE2 = lines[4] != "" ? lines[4] : null;
                            cuenta.ACTIVO = lines[5] != "" ? Convert.ToBoolean(Convert.ToUInt16(lines[5])) : false;
                            if (!cUENTAAs.Contains(cuenta,pks))
                            {
                                cUENTAAs.Add(cuenta);
                                nregistros++;
                            }
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nregistros + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nregistros + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nregistros + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nregistros + " registros nuevos.");
            }
        }
        private void cargaCUST(StreamReader strem, string filename)
        {
            var paises = db.CLIENTE.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != ""&&lines[1] != ""&&lines[2] != ""&& lines[3] != "")
                {
                    var existeregistro = paises.Where(t => t.VKORG == lines[0] && t.VTWEG==lines[1] && t.SPART == lines[2] && t.KUNNR == lines[3]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            CLIENTE cliente = new CLIENTE();
                            cliente.VKORG = lines[0];
                            cliente.VTWEG = lines[1] != "" ? lines[1] : null;
                            cliente.SPART = lines[2] != "" ? lines[2] : null;
                            cliente.KUNNR = lines[3] != "" ? lines[3] : null;
                            cliente.NAME1 = lines[4] != "" ? lines[4] : null;
                            cliente.STCD1 = lines[5] != "" ? lines[5] : null;
                            cliente.STCD2 = lines[6] != "" ? lines[6] : null;
                            cliente.LAND = lines[7] != "" ? lines[7] : null;
                            cliente.REGION = lines[8] != "" ? lines[8] : null;
                            cliente.SUBREGION = lines[9] != "" ? lines[9] : null;
                            cliente.REGIO = lines[10] != "" ? lines[10] : null;
                            cliente.ORT01 = lines[11] != "" ? lines[11] : null;
                            cliente.STRAS_GP = lines[12] != "" ? lines[12] : null;
                            cliente.PSTLZ = lines[13] != "" ? lines[13] : null;
                            cliente.CONTAC = lines[14] != "" ? lines[14] : null;
                            cliente.CONT_EMAIL = lines[15] != "" ? lines[15] : null;
                            cliente.PARVW = lines[16] != "" ? lines[16] : null;
                            cliente.PAYER = lines[17] != "" ? lines[17] : null;
                            cliente.GRUPO = lines[18] != "" ? lines[18] : null;
                            cliente.SPRAS = lines[19] != "" ? lines[19] : null;
                            cliente.ACTIVO = lines[20] != "" ? Convert.ToBoolean(Convert.ToUInt16(lines[20])) : false;
                            cliente.BDESCRIPCION = lines[21] != "" ? lines[21] : null;
                            cliente.BANNER = lines[22] != "" ? lines[22] : null;
                            cliente.CANAL = lines[23] != "" ? lines[23] : null;
                            cliente.BZIRK = lines[24] != "" ? lines[24] : null;
                            cliente.KONDA = lines[25] != "" ? lines[25] : null;
                            cliente.VKGRP = lines[26] != "" ? lines[26] : null;
                            cliente.VKBUR = lines[27] != "" ? lines[27] : null;
                            cliente.BANNERG = lines[28] != "" ? lines[28] : null;
                            cliente.PROVEEDOR_ID = lines[29] != "" ? lines[29] : null;
                            cliente.USUARIO_ID = lines[30] != "" ? lines[30] : null;
                            cliente.EXPORTACION = lines[31] != "" ? lines[31] : null;
                            CLIENTEs.Add(cliente);
                            nuevor++;
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaCOCO(StreamReader strem, string filename)
        {
            var paises = db.SOCIEDAD.ToList();
            int nuevor = 0;
            string[] lines;
            bool error=false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro = paises.Where(t => t.BUKRS == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            SOCIEDAD sociedad = new SOCIEDAD();
                            sociedad.BUKRS = lines[0];
                            sociedad.BUTXT = lines[1] != "" ? lines[1] : null;
                            sociedad.ORT01 = lines[2] != "" ? lines[2] : null;
                            sociedad.LAND = lines[3] != "" ? lines[3] : null;
                            sociedad.SUBREGIO = lines[4] != "" ? lines[4] : null;
                            sociedad.WAERS = lines[5] != "" ? lines[5] : null;
                            sociedad.SPRAS = lines[6] != "" ? lines[6] : null;
                            sociedad.NAME1 = lines[7] != "" ? lines[7] : null;
                            sociedad.KTOPL = lines[8] != "" ? lines[8] : null;
                            sociedad.ACTIVO = Convert.ToBoolean(Convert.ToInt16(lines[9]));
                            sociedad.REGION = lines[10] != "" ? lines[10] : null;
                            nuevor++;
                            SOCIEDADs.Add(sociedad);
                        }
                        catch (Exception e)
                        {
                            grabarLog(e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaCURR(StreamReader strem, string filename)
        {
            var paises = db.MONEDA.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro = paises.Where(t => t.WAERS == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            MONEDA moneda = new MONEDA();
                            moneda.WAERS = lines[0];
                            moneda.ISOCD = lines[1] != "" ? lines[1] : null;
                            moneda.ALTWR = lines[2] != "" ? lines[2] : null;
                            moneda.LTEXT = lines[3] != "" ? lines[3] : null;
                            moneda.KTEXT = lines[4] != "" ? lines[4] : null;
                            moneda.ACTIVO = lines[5] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[5])) : false;
                            nuevor++;
                            MONEDAs.Add(moneda);
                        }
                        catch (Exception e)
                        {
                            grabarLog(e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaVENDOR(StreamReader strem, string filename)
        {
            var paises = db.PROVEEDOR.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro = paises.Where(t => t.ID.TrimEnd() == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            PROVEEDOR proveedor = new PROVEEDOR();
                            proveedor.ID = lines[0];
                            proveedor.NOMBRE = lines[1] != "" ? lines[1] : null;
                            proveedor.SOCIEDAD_ID = lines[2] != "" ? lines[2] : null;
                            proveedor.PAIS_ID = lines[3] != "" ? lines[3] : null;
                            proveedor.ACTIVO = lines[4] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[4])) : false;
                            nuevor++;
                            PROVEEDORs.Add(proveedor);
                        }
                        catch (Exception e)
                        {
                            grabarLog(e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaEXCH(StreamReader strem, string filename)
        {
            var paises = db.TCAMBIO.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != ""&& lines[1] != ""&& lines[2] != ""&& lines[3] != "")
                {
                    var fecha = new DateTime(Convert.ToInt16(lines[3].Substring(0, 4)), Convert.ToInt16(lines[3].Substring(4, 2)), Convert.ToInt16(lines[3].Substring(6, 2)));
                    var existeregistro = paises.Where(t => t.KURST.TrimEnd() == lines[0] && t.FCURR == lines[1] && t.TCURR == lines[2] && t.GDATU == fecha).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            TCAMBIO tcambio = new TCAMBIO();
                            tcambio.KURST = lines[0];
                            tcambio.FCURR = lines[1];
                            tcambio.TCURR = lines[2];
                            tcambio.GDATU = new DateTime(Convert.ToInt16(lines[3].Substring(0, 4)), Convert.ToInt16(lines[3].Substring(4, 2)), Convert.ToInt16(lines[3].Substring(6, 2)));
                            tcambio.UKURS = lines[4] != "" ? Convert.ToDecimal(lines[4]) : 0;
                            nuevor++;
                            TCAMBIOs.Add(tcambio);
                        }
                        catch (Exception e)
                        {
                            grabarLog(e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaCNTY(StreamReader strem, string filename)
        {
            var paises = db.PAIS.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro=paises.Where(t => t.LAND == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            PAIS pais = new PAIS();
                            pais.LAND = lines[0];
                            pais.SPRAS = lines[1] != "" ? lines[1] : null;
                            pais.LANDX = lines[2] != "" ? lines[2] : null;
                            pais.IMAGE = lines[3] != "" ? lines[3] : null;
                            pais.ACTIVO = lines[4] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[4])) : false;
                            pais.SOCIEDAD_ID = lines[5] != "" ? lines[5] : null;
                            pais.DECIMAL = lines[6];
                            pais.MILES = lines[7];
                            pais.BACKORDER = lines[8] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[8])) : false;
                            nuevor++;
                            PAISs.Add(pais);
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo "+filename+", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaMAT(StreamReader strem, string filename)
        {
            var paises = db.MATERIAL.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro = paises.Where(t => t.ID == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            MATERIAL material = new MATERIAL();
                            material.ID = lines[0];
                            material.MTART = lines[1] != "" ? lines[1] : null;
                            material.MATKL_ID = lines[2] != "" ? lines[2] : null;
                            material.MAKTX = lines[3] != "" ? lines[3] : null;
                            material.MAKTG = lines[4] != "" ? lines[4] : null;
                            material.MEINS = lines[5] != "" ? lines[5] : null;
                            material.PUNIT = lines[6] != "" ?Convert.ToDecimal(lines[6]) : 0;
                            material.ACTIVO = lines[7] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[7])) : false;
                            material.CTGR = lines[8] != "" ? lines[8] : null;
                            material.BRAND = lines[9] != "" ? lines[9] : null;
                            material.MATERIALGP_ID = lines[10] != "" ? lines[10] : null;
                            nuevor++;
                            mATERIALs.Add(material);
                        }
                        catch (Exception e)
                        {
                            grabarLog(e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaMAKT(StreamReader strem, string filename)
        {
            var paises = db.MATERIALT.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "" && lines[1] != "")
                {
                    var existeregistro = paises.Where(t => t.SPRAS == lines[0] && t.MATERIAL_ID == lines[1]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            MATERIALT material = new MATERIALT();
                            material.SPRAS = lines[0];
                            material.MATERIAL_ID = lines[1];
                            material.MAKTX = lines[2];
                            material.MAKTG = lines[3];
                            nuevor++;
                            mATERIALTs.Add(material);
                        }
                        catch (Exception e)
                        {
                            grabarLog(e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaMVKE(StreamReader strem, string filename)
        {
            var paises = db.MATERIALVKE.ToList();
            //GenericEqualityComparer<MATERIALVKE> pks = new GenericEqualityComparer<MATERIALVKE>((a1, a2) => a1.MATERIAL_ID == a2.MATERIAL_ID && a1.VKORG == a2.VKORG && a1.VTWEG == a2.VTWEG);
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != ""&& lines[1] != ""&& lines[2] != "")
                {
                    var existeregistro = paises.Where(t => t.MATERIAL_ID == lines[0]&& t.VKORG == lines[1]&& t.VTWEG.TrimEnd() == lines[2]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            MATERIALVKE material = new MATERIALVKE();
                            material.MATERIAL_ID = lines[0];
                            material.VKORG = lines[1] != "" ? lines[1] : null;
                            material.VTWEG = lines[2] != "" ? lines[2] : null;
                            material.ACTIVO = lines[3] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[3])) : false;
                        //if (!paises.Contains(material, pks))
                        //{
                            nuevor++;
                            mATERIALVKEs.Add(material);
                        //}
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaUOMS(StreamReader strem, string filename)
        {
            var paises = db.UMEDIDA.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro = paises.Where(t => t.MSEHI.TrimEnd() == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            UMEDIDA medida = new UMEDIDA();
                            medida.MSEHI = lines[0];
                            medida.ACTIVO = lines[1] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[1])) : false;
                            nuevor++;
                            uMEDIDAs.Add(medida);
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaUOMTEXTS(StreamReader strem, string filename)
        {
            var paises = db.UMEDIDAT.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != ""&& lines[1] != "")
                {
                    var existeregistro = paises.Where(t => t.SPRAS_ID == lines[0] && t.MSEHI.TrimEnd() == lines[1]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            UMEDIDAT medida = new UMEDIDAT();
                            medida.SPRAS_ID = lines[0];
                            medida.MSEHI = lines[1];
                            medida.MSEH3 = lines[2]!=""?lines[2]:null;
                            medida.MSEH6 = lines[3] != "" ? lines[3] : null;
                            medida.MSEHT = lines[4] != "" ? lines[4] : null;
                            medida.MSEHL = lines[5] != "" ? lines[5] : null;
                            nuevor++;
                            uMEDIDATs.Add(medida);
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaTAX(StreamReader strem, string filename)
        {
            var paises = db.IIMPUESTO.ToList();
            GenericEqualityComparer<IIMPUESTO> pks = new GenericEqualityComparer<IIMPUESTO>((a1, a2) => a1.LAND == a2.LAND && a1.MWSKZ == a2.MWSKZ);
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != ""&& lines[1] != "")
                {
                    var existeregistro = paises.Where(t => t.LAND == lines[0]&& t.MWSKZ == lines[1]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            IIMPUESTO iIMPUESTO = new IIMPUESTO();
                            iIMPUESTO.LAND = lines[0];
                            iIMPUESTO.MWSKZ = lines[1];
                            iIMPUESTO.KSCHL = lines[2] != "" ? lines[2] : null;
                            iIMPUESTO.KBETR = lines[3] != "" ?Convert.ToDecimal(lines[3]) : 0;
                            iIMPUESTO.ACTIVO = lines[4] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[4])) : false;
                            if (!iIMPUESTOs.Contains(iIMPUESTO, pks))
                            {
                                iIMPUESTOs.Add(iIMPUESTO);
                                nuevor++;
                            }
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaMATGROUP(StreamReader strem, string filename)
        {
            var paises = db.MATERIALGP.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro = paises.Where(t => t.ID == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            MATERIALGP material = new MATERIALGP();
                            material.ID = lines[0];
                            material.DESCRIPCION = lines[1]!=""?lines[1]:null;
                            material.ACTIVO = lines[2] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[2])) : false;
                            material.EXCLUIR = lines[3] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[3])) : false;
                            material.UNICA = lines[4] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[4])) : false;
                            nuevor++;
                            mATERIALGPs.Add(material);
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaMATG(StreamReader strem, string filename)
        {
            var paises = db.MATERIALGPT.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "" && lines[1] != "")
                {
                    var existeregistro = paises.Where(t => t.SPRAS_ID == lines[0] && t.MATERIALGP_ID == lines[1]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            MATERIALGPT material = new MATERIALGPT();
                            material.SPRAS_ID = lines[0];
                            material.MATERIALGP_ID = lines[1];
                            material.TXT50 = lines[2] != "" ? lines[2] : null;
                            nuevor++;
                            mATERIALGPTs.Add(material);
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaTAXTEXT(StreamReader strem, string filename)
        {
            var paises = db.IMPUESTO.ToList();
            int nuevor = 0;
            string[] lines;
            bool error = false;
            while (strem.Peek() > -1)
            {
                lines = strem.ReadLine().Split('|');
                if (lines[0] != "")
                {
                    var existeregistro = paises.Where(t => t.MWSKZ == lines[0]).SingleOrDefault();
                    if (existeregistro == null)
                    {
                        try
                        {
                            IMPUESTO iMPUESTO = new IMPUESTO();
                            iMPUESTO.MWSKZ = lines[0];
                            iMPUESTO.ACTIVO = lines[1] != "" ? Convert.ToBoolean(Convert.ToInt16(lines[1])) : false;
                            nuevor++;
                            iMPUESTOs.Add(iMPUESTO);
                        }
                        catch (Exception e)
                        {
                            grabarLog("Archivo " + filename + "." + e.Message);
                            error = true;
                        }
                    }
                }
                else
                {
                    grabarLog("Un registro en el archivo " + filename + ", no tiene el formato requerido.");
                    error = true;
                }
            }
            if (!error)
            {
                logarchivos += filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con éxito. Se encontraron " + nuevor + " registros nuevos.");
            }
            else
            {
                logarchivos += filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos." + Environment.NewLine;
                grabarLog(filename + " cargado con con error. Se encontraron " + nuevor + " registros nuevos.");
            }
        }
        private void cargaS(bool automatico)
        {
            logarchivos = "";
            crearLog();
            grabarLog("Iniciando Sincronización con usuario "+ txtUsuario.Text);           
            FileInfo file;
            //StreamReader strem;
            string[] nombre;
            if (validarArchivoS())
            {
                grabarLog("Validación de directorios correcta");
                try
                {
                    var files = Directory.EnumerateFiles(txtArchivoS.Text, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".txt") || s.EndsWith(".TXT")).ToArray();
                    grabarLog("Se encontraron " + files.Length + " Archivos de SAP");
                    logarchivos += "Se encontraron " + files.Length + " Archivos de SAP" + Environment.NewLine;
                    for (int j = 0; j < files.Length; j++)
                    {
                        file = new FileInfo(files[j]);
                        nombre = file.Name.Split('_');                
                        grabarLog("Cargando datos del archivo " + file.Name);
                        using (StreamReader strem = new StreamReader(new FileStream(files[j], FileMode.Open, FileAccess.Read, FileShare.Read)))
                        {
                            //strem = new StreamReader(files[j]);
                            if (nombre[1] == "ACCO")
                            {
                                cargaACCO(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "CNTY")
                            {
                                cargaCNTY(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "COCO")
                            {
                                cargaCOCO(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "EXCH")
                            {
                                cargaEXCH(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "MVKE")
                            {
                                cargaMVKE(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "MAKT")
                            {
                                cargaMAKT(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "MAT")
                            {
                                cargaMAT(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "MATGROUP")
                            {
                                cargaMATGROUP(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "MATG")
                            {
                                cargaMATG(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "VENDOR")
                            {
                                cargaVENDOR(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "UOM")
                            {
                                if (nombre[2] == "S" || nombre[2] == "E")
                                {
                                    cargaUOMS(strem, file.Name);
                                    continue;
                                }
                                if (nombre[2] == "TEXTS" || nombre[2] == "TEXTE")
                                {
                                    cargaUOMTEXTS(strem, file.Name);
                                    continue;
                                }
                            }
                            if (nombre[1] == "TAX")
                            {
                                if (nombre[2] == "TEXT")
                                {
                                    cargaTAXTEXT(strem, file.Name);
                                    continue;
                                }
                                else
                                {
                                    cargaTAX(strem, file.Name);
                                    continue;
                                }
                            }
                            if (nombre[1] == "CUST")
                            {
                                cargaCUST(strem, file.Name);
                                continue;
                            }
                            if (nombre[1] == "CURR")
                            {
                                cargaCURR(strem, file.Name);
                                continue;
                            }
                        }
                    }
                    if (files.Length == 0)
                        btnGuardar.Enabled = false;
                    else
                        btnGuardar.Enabled = true;
                    grabarLog("Datos extraídos");
                    textBoxLog.Text = logarchivos;
                        if (automatico)
                        {
                            grabarLog("Carga terminada");
                            guardarDatosTAT();
                        }
                        else
                        {
                            grabarLog("Carga terminada");
                        }
                }
                catch (Exception x)
                {
                    AutoClosingMessageBox.Show("Formato de archivo incorrecto para carga de SAP", "Error de Guardado", segundos, MessageBoxButtons.OK, DialogResult.OK);
                }
            }
        }
        private string mes(string mes)
        {
            string ms = "";
            switch (mes)
            {
                case "Jan":
                    ms = "1";
                    break;
                case "Feb":
                    ms = "2";
                    break;
                case "Mar":
                    ms = "3";
                    break;
                case "Apr":
                    ms = "4";
                    break;
                case "May":
                    ms = "5";
                    break;
                case "Jun":
                    ms = "6";
                    break;
                case "Jul":
                    ms = "7";
                    break;
                case "Aug":
                    ms = "8";
                    break;
                case "Sep":
                    ms = "9";
                    break;
                case "Oct":
                    ms = "10";
                    break;
                case "Nov":
                    ms = "11";
                    break;
                case "Dec":
                    ms = "12";
                    break;
            }
            return ms;
        }
        private void guardarDatosTAT()
        {
            //var subdir = Directory.CreateDirectory(String.Concat(txtArchivoS.Text + "/" + "Cargados", DateTime.Today.ToString("ddMMyyyy")));
            var files = Directory.EnumerateFiles(txtArchivoS.Text, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".txt") || s.EndsWith(".TXT")).ToArray();
            textBoxLog.ResetText();
            var catalogo = "";
            try
            {
                using (var context = new Entities())
                {
                    grabarLog("Iniciando GUARDADO DE DATOS a la base de datos");
                        if (PAISs.Count > 0)
                        {
                        catalogo = "País";
                            context.BulkInsert(PAISs);
                            grabarLog("Se insertaron " + PAISs.Count + "registros a la tabla de CNTY.");
                            textBoxLog.Text += "Se insertaron " + PAISs.Count + " registros a la tabla de CNTY." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("CNTY"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (SOCIEDADs.Count > 0)
                        {
                        catalogo = "Sociedad";
                            context.BulkInsert(SOCIEDADs);
                            grabarLog("Se insertaron " + SOCIEDADs.Count + "registros a la tabla de COCO.");
                            textBoxLog.Text += "Se insertaron " + SOCIEDADs.Count + " registros a la tabla de COCO." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("COCO"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (PROVEEDORs.Count > 0)
                        {
                        catalogo = "Proveedor";
                            context.BulkInsert(PROVEEDORs);
                            grabarLog("Se insertaron " + PROVEEDORs.Count + "registros a la tabla de VENDOR.");
                            textBoxLog.Text += "Se insertaron " + PROVEEDORs.Count + " registros a la tabla de VENDOR." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("VENDOR"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (CLIENTEs.Count > 0)
                        {
                        catalogo = "Cliente";
                            context.BulkInsert(CLIENTEs);
                            grabarLog("Se insertaron " + CLIENTEs.Count + "registros a la tabla de CUST.");
                            textBoxLog.Text += "Se insertaron " + CLIENTEs.Count + " registros a la tabla de CUST." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("CUST"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (cUENTAAs.Count > 0)
                        {
                        catalogo = "Cuenta";
                            context.BulkInsert(cUENTAAs);
                        grabarLog("Se insertaron " + cUENTAAs.Count + "registros a la tabla de ACCO.");
                            textBoxLog.Text += "Se insertaron " + cUENTAAs.Count + " registros a la tabla de ACCO." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("ACCO"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (MONEDAs.Count > 0)
                        {
                        catalogo = "Moneda";
                            context.BulkInsert(MONEDAs);
                            grabarLog("Se insertaron " + MONEDAs.Count + "registros a la tabla de CURR.");
                            textBoxLog.Text += "Se insertaron " + MONEDAs.Count + " registros a la tabla de CURR." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("CURR"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (TCAMBIOs.Count > 0)
                        {
                        catalogo = "Tipo de Cambio";
                            context.BulkInsert(TCAMBIOs);
                            grabarLog("Se insertaron " + TCAMBIOs.Count + "registros a la tabla de EXCH.");
                            textBoxLog.Text += "Se insertaron " + TCAMBIOs.Count + " registros a la tabla de EXCH." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("EXCH"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (mATERIALGPs.Count > 0)
                        {
                        catalogo = "Material Grupo";
                            context.BulkInsert(mATERIALGPs);
                            grabarLog("Se insertaron " + mATERIALGPs.Count + "registros a la tabla de MATGROUP.");
                            textBoxLog.Text += "Se insertaron " + mATERIALGPs.Count + " registros a la tabla de MATGROUP." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("MATGROUP"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (mATERIALGPTs.Count > 0)
                        {
                        catalogo = "Material Grupo";
                            context.BulkInsert(mATERIALGPTs);
                            grabarLog("Se insertaron " + mATERIALGPTs.Count + "registros a la tabla de MATG_TEXT.");
                            textBoxLog.Text += "Se insertaron " + mATERIALGPTs.Count + " registros a la tabla de MATG_TEXT." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("MATG_TEXT"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (mATERIALs.Count > 0)
                        {
                        catalogo = "Material";
                            context.BulkInsert(mATERIALs);
                            grabarLog("Se insertaron " + mATERIALs.Count + "registros a la tabla de MAT.");
                            textBoxLog.Text += "Se insertaron " + mATERIALs.Count + " registros a la tabla de MAT." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            var filesplit = file.Name.Split('_');
                            if (filesplit[1] == "MAT")
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (mATERIALTs.Count > 0)
                        {
                        catalogo = "Material";
                            context.BulkInsert(mATERIALTs);
                            grabarLog("Se insertaron " + mATERIALTs.Count + "registros a la tabla de MAKT.");
                            textBoxLog.Text += "Se insertaron " + mATERIALTs.Count + " registros a la tabla de MAKT." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("MAKT"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (mATERIALVKEs.Count > 0)
                        {
                        catalogo = "Material MVKE";
                            context.BulkInsert(mATERIALVKEs);
                            grabarLog("Se insertaron " + mATERIALVKEs.Count + "registros a la tabla de MVKE.");
                            textBoxLog.Text += "Se insertaron " + mATERIALVKEs.Count + " registros a la tabla de MVKE." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("MVKE"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (iMPUESTOs.Count > 0)
                        {
                        catalogo = "Impuestos";
                            context.BulkInsert(iMPUESTOs);
                            grabarLog("Se insertaron " + iMPUESTOs.Count + "registros a la tabla de TAX_TEXT.");
                            textBoxLog.Text += "Se insertaron " + iMPUESTOs.Count + " registros a la tabla de TAX_TEXT." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            var filesplit = file.Name.Split('_');
                            if (filesplit[1] == "TAX" && filesplit[2] == "TEXT")
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (iIMPUESTOs.Count > 0)
                        {
                        catalogo = "Impuestos";
                            context.BulkInsert(iIMPUESTOs);
                            grabarLog("Se insertaron " + iIMPUESTOs.Count + "registros a la tabla de TAX.");
                            textBoxLog.Text += "Se insertaron " + iIMPUESTOs.Count + " registros a la tabla de TAX." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            var filesplit = file.Name.Split('_');
                            if (filesplit[1] == "TAX" && filesplit.Length == 3)
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (uMEDIDAs.Count > 0)
                        {
                        catalogo = "Unidad de Medida";
                            context.BulkInsert(uMEDIDAs);
                            grabarLog("Se insertaron " + uMEDIDAs.Count + "registros a la tabla de UOM.");
                            textBoxLog.Text += "Se insertaron " + uMEDIDAs.Count + " registros a la tabla de UOM." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("UOM_S") || file.Name.Contains("UOM_E"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                        if (uMEDIDATs.Count > 0)
                        {
                        catalogo = "Unidad de Medida";
                            context.BulkInsert(uMEDIDATs);
                            grabarLog("Se insertaron " + uMEDIDATs.Count + "registros a la tabla de UOM_TEXT.");
                            textBoxLog.Text += "Se insertaron " + uMEDIDATs.Count + " registros a la tabla de UOM_TEXT." + Environment.NewLine;
                        }
                        for (int j = 0; j < files.Length; j++)
                        {
                            FileInfo file = new FileInfo(files[j]);
                            if (file.Name.Contains("UOM_TEXTS") || file.Name.Contains("UOM_TEXTE"))
                                file.MoveTo(txtArchivo.Text + "/" + file.Name);
                        }
                }
                guardarConfig();
                AutoClosingMessageBox.Show("Guardado Correctamente", "Guardado", segundos, MessageBoxButtons.OK, DialogResult.OK);
                grabarLog("Guardado Correctamente");
                grabarLog("Fin");
                enviarInforme(false,"");
            }
            catch (Exception e)
            {
                AutoClosingMessageBox.Show("Ocurrio Algo al momento de guardar", "Error al Guardar", segundos, MessageBoxButtons.OK, DialogResult.OK);
                grabarLog("Ocurrió un error al momento de guardar: "+e.Message);
                enviarInforme(true,catalogo);
            }
        }
        //////Menu
        ////private string examinarRuta(string ruta)
        ////{
        ////    string res = "";
        ////    openFileDialog1.Multiselect = false;
        ////    openFileDialog1.DefaultExt = "txt";
        ////    openFileDialog1.Filter = "Archivos de Excel (*.csv;*.CSV)|*.csv;*.CSV";
        ////    DialogResult result = openFileDialog1.ShowDialog();
        ////    if (result == DialogResult.OK)
        ////    {
        ////        res = @openFileDialog1.FileName;
        ////    }
        ////    else
        ////    {
        ////        res = ruta;
        ////    }
        ////    return res;
        ////}

        private string examinarRutaC(string ruta)
        {
            string res = "";
            folderBrowserDialog1.SelectedPath = ruta;
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                res = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                res = ruta;
            }

            return res;
        }
        private void enviarInforme(bool error, string catalogo)
        {
            EnviarEmail email = new EnviarEmail();
            List<string> correos = new List<string>();
            if(error)
            correos.Add(set.ACorreo);
            else
            correos.Add(set.ACorreoTS);
            email.SendMail(correos, set.Puerto.ToString(), set.SSL, set.SMTP, set.DCorreo, set.Contrasenia, false, rutaL,error,catalogo);
        }
        private void crearLog()
        {
            string nombre;
            nombre = "Log" + DateTime.Now.ToString();
            nombre = nombre.Replace(':', '-');
            nombre = nombre.Replace('\\', '-');
            nombre = nombre.Replace('/', '-');
            rutaL = txtArchivoL.Text + @"\" + nombre + ".txt";
            if (File.Exists(rutaL))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rutaL, true))
                {
                    file.WriteLine(" ");
                    file.Close();
                }
            }
            else
            {
                System.IO.File.WriteAllText(rutaL, "SINCRONIZACIÓN:");
            }
            
        }
        private void grabarLog(string text)
        {
            string[] texto = { text };
            File.AppendAllLines(rutaL, texto);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (chkCerrar.Checked)
            {
                Close();
            }
        }

    }
}
