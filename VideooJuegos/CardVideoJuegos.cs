using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideooJuegos
{
    public class CardVideoJuegos : UserControl
    {
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label labelPrecio;
        private Button btnCard;
        private Button btnEditar;
        private Label label1;
        private bool _esAdmin;

        public bool EsAdmin
        {
            get { return _esAdmin; }
            set
            {
                _esAdmin = value;
                btnCard.Visible = value;
                btnEditar.Visible = value;
            }
        }

        public bool MostrarBoton
        {
            get { return btnCard.Visible; }
            set
            {
                btnCard.Visible = value;
            }
        }

        // Propiedad para controlar si se muestra el botón editar
        public bool MostrarBotonEditar
        {
            get { return btnEditar.Visible; }
            set { btnEditar.Visible = value; }
        }

        public string TextoBoton
        {
            get { return btnCard.Text; }
            set { btnCard.Text = value; }
        }

        public CardVideoJuegos()
        {
            InitializeComponent();
        }

        public string Titulo
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public string Plataforma
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }

        // Propiedad para Género (en catálogo)
        public string Genero
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        // Propiedad para Precio (en tienda) -> usa labelPrecio dedicado
        public string Precio
        {
            get { return labelPrecio.Text; }
            set { labelPrecio.Text = value; }
        }

        // Nueva propiedad para controlar la visibilidad del label de precio
        public bool PrecioVisible
        {
            get { return labelPrecio.Visible; }
            set { labelPrecio.Visible = value; }
        }

        public string Rating
        {
            get { return label4.Text; }
            set { label4.Text = $"Rating: {value}"; }
        }

        public string Imagen
        {
            get { return pictureBox1.ImageLocation; }
            set { pictureBox1.ImageLocation = value; }
        }

        public long Id { get; set; }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPrecio = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCard = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(26, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "Titulo";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(26, 330);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 44);
            this.label2.TabIndex = 1;
            this.label2.Text = "Plataforma";
            // 
            // labelPrecio
            // 
            this.labelPrecio.BackColor = System.Drawing.Color.Transparent;
            this.labelPrecio.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrecio.ForeColor = System.Drawing.Color.LimeGreen;
            this.labelPrecio.Location = new System.Drawing.Point(27, 374);
            this.labelPrecio.Name = "labelPrecio";
            this.labelPrecio.Size = new System.Drawing.Size(247, 21);
            this.labelPrecio.TabIndex = 1;
            this.labelPrecio.Text = "Precio";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(27, 401);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Stock";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(27, 429);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Rating";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(27, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(247, 243);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCard
            // 
            this.btnCard.AutoSize = true;
            this.btnCard.BackColor = System.Drawing.Color.YellowGreen;
            this.btnCard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCard.Font = new System.Drawing.Font("Cooper Black", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCard.Location = new System.Drawing.Point(153, 463);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(99, 33);
            this.btnCard.TabIndex = 2;
            this.btnCard.Text = "Agregar";
            this.btnCard.UseVisualStyleBackColor = false;
            this.btnCard.Click += new System.EventHandler(this.btnCard_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.AutoSize = true;
            this.btnEditar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditar.Font = new System.Drawing.Font("Cooper Black", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(42, 463);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(95, 33);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Visible = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // CardVideoJuegos
            // 
            this.BackgroundImage = global::VideooJuegos.Properties.Resources.card;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnCard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelPrecio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "CardVideoJuegos";
            this.Size = new System.Drawing.Size(300, 522);
            this.Load += new System.EventHandler(this.CardVideoJuegos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!EsAdmin)
            {
                MessageBox.Show("Solo los administradores pueden editar juegos.");
                return;
            }

            // Crear y mostrar el formulario de edición
            FormEditarJuego formEditar = new FormEditarJuego(this);
            if (formEditar.ShowDialog() == DialogResult.OK)
            {
                this.Refresh();
                MessageBox.Show("Juego actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            if (!EsAdmin)
            {
                MessageBox.Show("Solo los administradores pueden gestionar juegos.");
                return;
            }

            // ✅ CARGAR LISTA DE JUEGOS (AHORA SON OBJETOS JuegoTienda)
            var lista = TiendaManager.Cargar();

            if (btnCard.Text == "Eliminar")
            {
                var confirmacion = MessageBox.Show(
                    $"¿Estás seguro de eliminar '{this.Titulo}' de la tienda?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    // ✅ ELIMINAR USANDO EL MÉTODO DE TiendaManager
                    TiendaManager.EliminarJuego(this.Id);
                    MessageBox.Show("Juego eliminado de la tienda.");

                    this.Parent?.Controls.Remove(this);
                    this.Dispose();
                }
                return;
            }

            // ✅ VERIFICAR SI YA EXISTE (BUSCAR POR ID EN LA LISTA)
            if (lista.Any(j => j.Id == this.Id))
            {
                MessageBox.Show("Este juego ya está en la tienda.");
                return;
            }

            // ✅ MOSTRAR DIÁLOGO PARA INGRESAR PRECIO Y STOCK
            FormAgregarATienda formAgregar = new FormAgregarATienda();
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                decimal precio = formAgregar.Precio;
                int stock = formAgregar.Stock;

                // ✅ AGREGAR USANDO EL MÉTODO DE TiendaManager
                TiendaManager.AgregarJuego(this.Id, precio, stock);
                MessageBox.Show($"Juego agregado a la tienda.\nPrecio: ${precio:F2}\nStock: {stock} unidades");
            }
        }

        private void CardVideoJuegos_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelPrecio_Click(object sender, EventArgs e)
        {

        }
    }
}