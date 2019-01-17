using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncTAT
{
    public partial class ConfigConexion : Form
    {
        AppSettings settings = new AppSettings();
        public ConfigConexion()
        {
            InitializeComponent();
            cargar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            probar();
        }
        private void cargar()
        {
            string sett = settings.getConnectionString("TAT001Entities");
            int inicio = 0, fin = 0;
            inicio = sett.IndexOf("data source=");
            if (inicio >= 0)
            {
                inicio += "data source=".Length;
                sett = sett.Remove(0, inicio);
                inicio = sett.IndexOf(";");
                txtServer.Text = sett.Substring(0, inicio);
            }
            inicio = sett.IndexOf("catalog=");
            if (inicio > 0)
            {
                inicio += "catalog=".Length;
                sett = sett.Remove(0, inicio);
                inicio = sett.IndexOf(";");
                txtBD.Text = sett.Substring(0, inicio);
            }
            inicio = sett.IndexOf("user id=");
            if (inicio > 0)
            {
                inicio += "user id=".Length;
                sett = sett.Remove(0, inicio);
                inicio = sett.IndexOf(";");
                txtUser.Text = sett.Substring(0, inicio);
            }
            inicio = sett.IndexOf("password=");
            if (inicio > 0)
            {
                inicio += "password=".Length;
                sett = sett.Remove(0, inicio);
                inicio = sett.IndexOf(";");
                txtPass.Text = sett.Substring(0, inicio);
            }
        }
        private bool probar()
        {
            try
            {
                //string connstring = string.Format("metadata = res://*/TAT01.csdl|res://*/TAT01.ssdl|res://*/TAT01.msl;provider=System.Data.SqlClient;provider connection string='data source={0};initial catalog={1};user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework'", txtServer.Text, txtBD.Text, txtUser.Text, txtPass.Text);
                string connstring = string.Format("data source={0};initial catalog={1};user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework", txtServer.Text, txtBD.Text, txtUser.Text, txtPass.Text);
                //string connstring = string.Format("Data Source={0};Database={1};User id={2};Password={3};", txtServer.Text, txtBD.Text, txtUser.Text, txtPass.Text);
                if (settings.openConn(connstring))
                {
                    MessageBox.Show("Correcto","Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error de Conexión",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                return false;
            }
        }
        private void guardar()
        {
            if (probar())
            {
                string connstring2 = string.Format("data source={0};initial catalog={1};user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework", txtServer.Text, txtBD.Text, txtUser.Text, txtPass.Text);
                string connstring = string.Format("metadata = res://*/TAT01.csdl|res://*/TAT01.ssdl|res://*/TAT01.msl;provider=System.Data.SqlClient;provider connection string='data source={0};initial catalog={1};user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework'", txtServer.Text, txtBD.Text, txtUser.Text, txtPass.Text);
                try
                {
                    settings.saveConfig("TAT001Entities", connstring);                   
                    //db.Database.Connection.Close();
                    this.DialogResult = DialogResult.OK;
                    //MessageBox.Show("Correcto", "Guardar configuracion", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Guardar configuración", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    this.DialogResult = DialogResult.None;
                }
                
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
