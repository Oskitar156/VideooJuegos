using System;
using System.Drawing;
using System.Windows.Forms;

namespace VideooJuegos
{
    public class FormAgregarATienda : Form
    {
        private TextBox txtPrecio;
        private TextBox txtStock;
        private Button btnAceptar;
        private Button btnCancelar;
        private Label lblPrecio;
        private Label lblStock;
        private Label lblTitulo;

        public decimal Precio { get; private set; }
        public int Stock { get; private set; }

        public FormAgregarATienda()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.lblPrecio = new Label();
            this.txtPrecio = new TextBox();
            this.lblStock = new Label();
            this.txtStock = new TextBox();
            this.btnAceptar = new Button();
            this.btnCancelar = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Agregar a Tienda";
            this.Size = new Size(400, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);

            // lblTitulo
            this.lblTitulo.Text = "Ingresa el precio y stock del juego:";
            this.lblTitulo.Location = new Point(30, 20);
            this.lblTitulo.Size = new Size(340, 30);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // lblPrecio
            this.lblPrecio.Text = "Precio ($):";
            this.lblPrecio.Location = new Point(30, 70);
            this.lblPrecio.Size = new Size(100, 20);
            this.lblPrecio.ForeColor = Color.LimeGreen;
            this.lblPrecio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // txtPrecio
            this.txtPrecio.Location = new Point(30, 95);
            this.txtPrecio.Size = new Size(330, 25);
            this.txtPrecio.Font = new Font("Segoe UI", 10F);
            this.txtPrecio.Text = "19.99";

            // lblStock
            this.lblStock.Text = "Stock (unidades):";
            this.lblStock.Location = new Point(30, 130);
            this.lblStock.Size = new Size(150, 20);
            this.lblStock.ForeColor = Color.Yellow;
            this.lblStock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // txtStock
            this.txtStock.Location = new Point(30, 155);
            this.txtStock.Size = new Size(330, 25);
            this.txtStock.Font = new Font("Segoe UI", 10F);
            this.txtStock.Text = "10";

            // btnAceptar
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Location = new Point(170, 200);
            this.btnAceptar.Size = new Size(90, 35);
            this.btnAceptar.BackColor = Color.Green;
            this.btnAceptar.ForeColor = Color.White;
            this.btnAceptar.FlatStyle = FlatStyle.Flat;
            this.btnAceptar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAceptar.Click += BtnAceptar_Click;

            // btnCancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new Point(270, 200);
            this.btnCancelar.Size = new Size(90, 35);
            this.btnCancelar.BackColor = Color.Gray;
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancelar.Click += BtnCancelar_Click;

            // Agregar controles al formulario
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            // Validar precio
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("El precio debe ser un número válido mayor o igual a 0.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            // Validar stock
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número entero mayor o igual a 0.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            // Guardar valores
            this.Precio = precio;
            this.Stock = stock;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}