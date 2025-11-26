using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace VideooJuegos
{
    public class FormEditarJuego : Form
    {
        private TextBox txtTitulo;
        private TextBox txtPlataforma;
        private TextBox txtPrecio;
        private TextBox txtRating;
        private TextBox txtStock; // ✅ NUEVO: Reemplaza txtImagen
        private Button btnGuardar;
        private Button btnCancelar;
        private Label lblTitulo;
        private Label lblPlataforma;
        private Label lblPrecio;
        private Label lblRating;
        private Label lblStock; // ✅ NUEVO: Reemplaza lblImagen
        private CardVideoJuegos _card;

        public FormEditarJuego(CardVideoJuegos card)
        {
            _card = card;
            InitializeComponent();
            CargarDatos();

            txtTitulo.Enabled = false;
            txtPlataforma.Enabled = false;
            txtRating.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.txtTitulo = new TextBox();
            this.txtPlataforma = new TextBox();
            this.txtPrecio = new TextBox();
            this.txtRating = new TextBox();
            this.txtStock = new TextBox(); // ✅ NUEVO
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();
            this.lblTitulo = new Label();
            this.lblPlataforma = new Label();
            this.lblPrecio = new Label();
            this.lblRating = new Label();
            this.lblStock = new Label(); // ✅ NUEVO

            this.SuspendLayout();

            // Form
            this.Text = "Editar Juego";
            this.Size = new Size(450, 380);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);

            // lblTitulo
            this.lblTitulo.Text = "Título:";
            this.lblTitulo.Location = new Point(30, 30);
            this.lblTitulo.Size = new Size(100, 20);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // txtTitulo
            this.txtTitulo.Location = new Point(30, 55);
            this.txtTitulo.Size = new Size(380, 25);
            this.txtTitulo.Font = new Font("Segoe UI", 10F);

            // lblPlataforma
            this.lblPlataforma.Text = "Plataforma:";
            this.lblPlataforma.Location = new Point(30, 90);
            this.lblPlataforma.Size = new Size(100, 20);
            this.lblPlataforma.ForeColor = Color.White;
            this.lblPlataforma.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // txtPlataforma
            this.txtPlataforma.Location = new Point(30, 115);
            this.txtPlataforma.Size = new Size(380, 25);
            this.txtPlataforma.Font = new Font("Segoe UI", 10F);

            // lblPrecio
            this.lblPrecio.Text = "Precio ($):";
            this.lblPrecio.Location = new Point(30, 150);
            this.lblPrecio.Size = new Size(100, 20);
            this.lblPrecio.ForeColor = Color.LimeGreen;
            this.lblPrecio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // txtPrecio
            this.txtPrecio.Location = new Point(30, 175);
            this.txtPrecio.Size = new Size(180, 25);
            this.txtPrecio.Font = new Font("Segoe UI", 10F);

            // lblStock ✅ NUEVO
            this.lblStock.Text = "Stock:";
            this.lblStock.Location = new Point(230, 150);
            this.lblStock.Size = new Size(100, 20);
            this.lblStock.ForeColor = Color.Yellow;
            this.lblStock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // txtStock ✅ NUEVO
            this.txtStock.Location = new Point(230, 175);
            this.txtStock.Size = new Size(180, 25);
            this.txtStock.Font = new Font("Segoe UI", 10F);

            // lblRating
            this.lblRating.Text = "Rating:";
            this.lblRating.Location = new Point(30, 210);
            this.lblRating.Size = new Size(100, 20);
            this.lblRating.ForeColor = Color.White;
            this.lblRating.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // txtRating
            this.txtRating.Location = new Point(30, 235);
            this.txtRating.Size = new Size(380, 25);
            this.txtRating.Font = new Font("Segoe UI", 10F);

            // btnGuardar
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new Point(230, 290);
            this.btnGuardar.Size = new Size(90, 35);
            this.btnGuardar.BackColor = Color.Green;
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnGuardar.Click += BtnGuardar_Click;

            // btnCancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new Point(330, 290);
            this.btnCancelar.Size = new Size(90, 35);
            this.btnCancelar.BackColor = Color.Gray;
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancelar.Click += BtnCancelar_Click;

            // Agregar controles al formulario
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.lblPlataforma);
            this.Controls.Add(this.txtPlataforma);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.lblStock); // ✅ NUEVO
            this.Controls.Add(this.txtStock); // ✅ NUEVO
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.txtRating);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CargarDatos()
        {
            txtTitulo.Text = _card.Titulo;
            txtPlataforma.Text = _card.Plataforma;

            // Intentar leer precio y stock directamente desde tienda.json
            var juegoTienda = TiendaManager.ObtenerJuego(_card.Id);
            if (juegoTienda != null)
            {
                // Mostrar solo el número en la caja de texto (permitir edición)
                txtPrecio.Text = juegoTienda.Precio.ToString("0.##", CultureInfo.CurrentCulture);
                txtStock.Text = juegoTienda.Stock.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                // Fallback: extraer desde la card si no existe en JSON
                string precioTexto = _card.Precio ?? "";
                // Quitar prefijos como "Precio:" y símbolos
                if (precioTexto.StartsWith("Precio:", StringComparison.OrdinalIgnoreCase))
                    precioTexto = precioTexto.Replace("Precio:", "").Trim();

                if (precioTexto.StartsWith("$"))
                    precioTexto = precioTexto.Replace("$", "").Trim();

                txtPrecio.Text = precioTexto;

                // Extraer stock desde Genero si viene en el formato "Stock: X unidades"
                string stockTexto = _card.Genero ?? "";
                if (stockTexto.StartsWith("Stock:", StringComparison.OrdinalIgnoreCase))
                {
                    stockTexto = stockTexto.Replace("Stock:", "").Replace("unidades", "").Trim();
                }
                txtStock.Text = stockTexto;
            }

            // Extraer solo el número del rating
            string ratingTexto = _card.Rating ?? "";
            if (ratingTexto.StartsWith("Rating:", StringComparison.OrdinalIgnoreCase))
            {
                ratingTexto = ratingTexto.Replace("Rating:", "").Trim();
            }
            txtRating.Text = ratingTexto;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("El título no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el precio sea un número válido
            if (!string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal precio) || precio < 0)
                {
                    MessageBox.Show("El precio debe ser un número válido mayor o igual a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // ✅ NUEVO: Validar stock
            if (!string.IsNullOrWhiteSpace(txtStock.Text))
            {
                if (!int.TryParse(txtStock.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out int stock) || stock < 0)
                {
                    MessageBox.Show("El stock debe ser un número entero mayor o igual a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Validar que el rating sea un número válido
            if (!string.IsNullOrWhiteSpace(txtRating.Text))
            {
                if (!double.TryParse(txtRating.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double rating) || rating < 0 || rating > 100)
                {
                    MessageBox.Show("El rating debe ser un número entre 0 y 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Aplicar los cambios a la card
            _card.Titulo = txtTitulo.Text.Trim();
            _card.Plataforma = txtPlataforma.Text.Trim();

            decimal? precioNullable = null;
            int? stockNullable = null;

            // Formatear el precio en COP con prefijo "Precio:" en la card y guardar numérico en JSON
            if (!string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                decimal.TryParse(txtPrecio.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal precio);
                precioNullable = precio;

                var culture = new CultureInfo("es-CO");
                _card.Precio = $"Precio: {precio.ToString("C0", culture)}";

                // Asegurar que el label de precio sea visible en la card
                _card.PrecioVisible = true;
            }

            _card.Rating = txtRating.Text.Trim();

            // ✅ NUEVO: Guardar stock y actualizar propiedad Genero de la card
            if (!string.IsNullOrWhiteSpace(txtStock.Text))
            {
                int.TryParse(txtStock.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out int stock);
                stockNullable = stock;
                _card.Genero = $"Stock: {stock} unidades";
            }

            // Actualizar JSON (si hay cambios)
            if (precioNullable.HasValue || stockNullable.HasValue)
            {
                TiendaManager.ActualizarJuego(_card.Id, precioNullable, stockNullable);
            }

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