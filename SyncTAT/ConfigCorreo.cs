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
    public partial class ConfigCorreo : Form
    {
        SettingsPersonal config = new SettingsPersonal();
        EnviarEmail email = new EnviarEmail();
        bool cerrar = true;
        public ConfigCorreo()
        {
            InitializeComponent();
            cargar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cerrar = probar();
            if (cerrar)
            {
                guardar();
            }                        
        }
        private void btnProbar_Click(object sender, EventArgs e)
        {
            probar();
        }
        private void cargar()
        {
            txtDCorreo.Text = config.DCorreo;
            txtACorreo.Text = config.ACorreo;
            txtACorreoTS.Text = config.ACorreoTS;
            txtContrasenia.Text = config.Contrasenia;
            txtPuerto.Text = config.Puerto.ToString();
            txtSMTP.Text = config.SMTP;
            chkSSL.Checked = config.SSL;
        }
        private void guardar()
        {
            config.DCorreo = txtDCorreo.Text;
            config.ACorreo = txtACorreo.Text;
            config.ACorreoTS = txtACorreoTS.Text;
            config.Contrasenia = txtContrasenia.Text;
            config.Puerto = Convert.ToInt32( txtPuerto.Text);  
            config.SMTP = txtSMTP.Text;    
            config.SSL = chkSSL.Checked;
            config.Save();
        }
        private bool probar()
        {
            List<string> emails = new List<string>();
            emails.Add(txtACorreo.Text);
            emails.Add(txtACorreoTS.Text);
            if (email.SendMail(emails, txtPuerto.Text, chkSSL.Checked, txtSMTP.Text, txtDCorreo.Text, txtContrasenia.Text, true, "", false,""))
            {
                MessageBox.Show("Correo enviado, verifique su bandeja", "Configuración de correo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return true;
            }
            else
            {
                MessageBox.Show("Error al enviar correo, verifique sus datos", "Configuración de correo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        private void ConfigCorreo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cerrar == false)
            {
                e.Cancel = true;
            }
        }
    }
}
