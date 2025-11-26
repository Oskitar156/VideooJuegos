using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VideooJuegos
{
    public partial class Registro : Form
    {
        Label mensajeError;
        private Usuarios usuario = new Usuarios();
        private List<Usuarios> usuarios;
        private Usuarios usuarioSeleccionado = new Usuarios();
        private bool esEdicion = false;
        private string emailOld;

        public Registro()
        {
            InitializeComponent();
            usuarios = usuario.ReadDataFromJson();
        }

        public Registro(Usuarios usuarioSeleccionado)
        {
            InitializeComponent();
            usuarios = usuario.ReadDataFromJson();
            this.usuarioSeleccionado = usuarioSeleccionado;

            txtNombre.Text = usuarioSeleccionado.Nombres;
            txtCorreo.Text = usuarioSeleccionado.Email;
            txtContraseña.Text = usuarioSeleccionado.Password;

            btnRegistrar.Text = "Actualizar";

            esEdicion = true;
            emailOld = usuarioSeleccionado.Email;
        }

        private void CrearLabel(TextBox textbox)
        {
            mensajeError = new System.Windows.Forms.Label();
            mensajeError.Text = $"El campo {textbox.Tag} es requerido";
            mensajeError.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height);
            mensajeError.ForeColor = Color.White;
            mensajeError.AutoSize = true;
            mensajeError.Tag = "error";
            mensajeError.Font = new Font("Georgia", 12, FontStyle.Italic);
            mensajeError.BackColor = Color.Transparent;

            Controls.Add(mensajeError);
        }

        private void CrearLabelError(string mensaje, TextBox textbox)
        {
            mensajeError = new System.Windows.Forms.Label();
            mensajeError.Text = mensaje;
            mensajeError.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height);
            mensajeError.ForeColor = Color.White;
            mensajeError.AutoSize = true;
            mensajeError.Tag = "error";
            mensajeError.Font = new Font("Georgia", 12, FontStyle.Italic);
            mensajeError.BackColor = Color.Transparent;

            Controls.Add(mensajeError);
        }

        private bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Validación simple: debe contener @ y un punto después del @
            int posicionArroba = email.IndexOf('@');

            if (posicionArroba <= 0) // No tiene @ o está al inicio
                return false;

            int posicionPunto = email.IndexOf('.', posicionArroba);

            if (posicionPunto <= posicionArroba + 1) // No tiene punto después del @ o está justo después
                return false;

            if (posicionPunto == email.Length - 1) // El punto está al final
                return false;

            return true;
        }

        private bool ValidateForm()
        {
            var isValid = true;

            // Eliminar mensajes de error anteriores
            foreach (var control in Controls.OfType<Label>().ToArray())
            {
                if (control.Tag != null && control.Tag.ToString() == "error")
                    Controls.Remove(control);
            }

            // Validar campos vacíos
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    if (textBox.Text == "")
                    {
                        CrearLabel(textBox);
                        isValid = false;
                    }
                }
            }

            // Validar formato de correo
            if (!string.IsNullOrEmpty(txtCorreo.Text) && !EsEmailValido(txtCorreo.Text))
            {
                CrearLabelError("Formato de correo inválido", txtCorreo);
                isValid = false;
            }

            // Validar que las contraseñas coincidan
            if (txtContraseña.Text != TxtCContraseña.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error");
                isValid = false;
            }

            return isValid;
        }

        private void ActualizarUsuario()
        {
            var isValid = ValidateForm();

            if (!isValid)
                return;

            foreach (var u in usuarios)
            {
                if (u.Email.ToLower() == txtCorreo.Text.ToLower() && u.Email != emailOld)
                {
                    MessageBox.Show("Ya existe otro usuario con ese correo.", "Duplicado");
                    return;
                }
            }

            Usuarios usuarioEncontrado = usuarios.FirstOrDefault(x => x.Email == emailOld);

            usuarioEncontrado.Nombres = txtNombre.Text;
            usuarioEncontrado.Email = txtCorreo.Text;
            usuarioEncontrado.Password = txtContraseña.Text;

            usuario.SaveToJson(usuarios);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CrearUsuario()
        {
            var isValid = ValidateForm();

            if (isValid)
            {
                foreach (var u in usuarios)
                {
                    if (u.Email.ToLower() == txtCorreo.Text.ToLower())
                    {
                        MessageBox.Show("Ya existe un usuario con ese correo.", "Duplicado");
                        return;
                    }
                }

                Usuarios nuevo = new Usuarios();
                nuevo.Nombres = txtNombre.Text;
                nuevo.Email = txtCorreo.Text;
                nuevo.Password = txtContraseña.Text;
                nuevo.Rol = "Usuario";

                usuarios.Add(nuevo);
                nuevo.SaveToJson(usuarios);

                MessageBox.Show("Usuario registrado con éxito", "Éxito");

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (esEdicion)
                ActualizarUsuario();
            else
                CrearUsuario();
        }

        private void Registro_Load(object sender, EventArgs e)
        {

        }
    }
}