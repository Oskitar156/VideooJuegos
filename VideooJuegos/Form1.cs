using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VideooJuegos
{
    public partial class Form1 : Form
    {
        private List<Usuarios> usuarios;
        private Usuarios usuario = new Usuarios();

        public Form1()
        {
            InitializeComponent();

            // Crear administradores por defecto
            Administrador admin = new Administrador();
            admin.CrearAdministradoresPorDefecto();

            txtContraseñaLogin.PasswordChar = '●';

            // Cargar usuarios del JSON
            usuarios = usuario.ReadDataFromJson();
        }

        private void CrearLabel(TextBox textbox)
        {
            Label label = new Label();
            label.Text = $"El campo {textbox.Tag} es requerido";
            label.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height);
            label.ForeColor = Color.White;
            label.AutoSize = true;
            label.Tag = "error";
            label.Font = new Font("Georgia", 8, FontStyle.Italic);
            label.BackColor = Color.Transparent;
            Controls.Add(label);
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            // Borrar errores anteriores
            foreach (var control in Controls.OfType<Label>().ToArray())
                if (control.Tag != null && control.Tag.ToString() == "error")
                    Controls.Remove(control);

            // Validar campos vacíos
            foreach (Control control in Controls)
                if (control is TextBox textBox && textBox.Text.Trim() == "")
                {
                    CrearLabel(textBox);
                    isValid = false;
                }

            return isValid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var formRegistro = new Registro();
            formRegistro.ShowDialog();

            // Recargar usuarios
            usuarios = usuario.ReadDataFromJson();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                MessageBox.Show("Por favor completa los campos requeridos", "Error");
                return;
            }

            string email = txtCorreoLogin.Text;
            string password = txtContraseñaLogin.Text;

            // Buscar usuario en JSON
            var userFound = usuarios.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (userFound != null)
            {
                MessageBox.Show($"Bienvenido {userFound.Nombres}", "Inicio exitoso");

                Interfaz ventana = new Interfaz();
                ventana.UsuarioActualEmail = userFound.Email;
                ventana.UsuarioActualRol = userFound.Rol;

                ventana.FormClosed += (s, args) => Application.Exit();

                ventana.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos", "Error");
            }
        }


        private void txtContraseñaLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
            }
        }
    }
}
