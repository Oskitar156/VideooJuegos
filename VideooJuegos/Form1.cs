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

            foreach (var control in Controls.OfType<Label>().ToArray())
                if (control.Tag != null && control.Tag.ToString() == "error")
                    Controls.Remove(control);

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

            var userFound = usuarios.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (userFound != null)
            {
                MessageBox.Show($"Bienvenido {userFound.Nombres}", "Inicio exitoso");

                Interfaz panel = new Interfaz();
                panel.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos", "Error");
            }
        }
    }
}
