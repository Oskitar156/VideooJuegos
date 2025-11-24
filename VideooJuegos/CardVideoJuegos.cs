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
        private Button btnCard;
        private Label label1;
        private bool _esAdmin;
        public bool EsAdmin
        {
            get { return _esAdmin; }
            set
            {
                _esAdmin = value;
                btnCard.Visible = value; 
            }
        }

        public bool MostrarBoton
        {
            get { return btnCard.Visible; }
            set { btnCard.Visible = value; }
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

        public string Genero
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        public string Rating
        {
            get { return label4.Text; }
            set { label4.Text = $"Rating: {value}$"; }
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(23, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Titulo";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(23, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 44);
            this.label2.TabIndex = 1;
            this.label2.Text = "Plataforma";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(23, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Genero";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(23, 353);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Rating";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(27, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCard
            // 
            this.btnCard.AutoSize = true;
            this.btnCard.BackColor = System.Drawing.Color.YellowGreen;
            this.btnCard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCard.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCard.Location = new System.Drawing.Point(82, 390);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(107, 33);
            this.btnCard.TabIndex = 2;
            this.btnCard.Text = "Agregar";
            this.btnCard.UseVisualStyleBackColor = false;
            this.btnCard.Click += new System.EventHandler(this.btnCard_Click);
            // 
            // CardVideoJuegos
            // 
            this.BackgroundImage = global::VideooJuegos.Properties.Resources.card;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnCard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "CardVideoJuegos";
            this.Size = new System.Drawing.Size(275, 450);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            if (!EsAdmin)
            {
                MessageBox.Show("Solo los administradores pueden gestionar juegos.");
                return;
            }

            var lista = TiendaManager.Cargar();

            if (btnCard.Text == "Eliminar")
            {
                // Eliminar de la tienda
                var confirmacion = MessageBox.Show(
                    $"¿Estás seguro de eliminar '{this.Titulo}' de la tienda?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    lista.Remove(this.Id);
                    TiendaManager.Guardar(lista);
                    MessageBox.Show("Juego eliminado de la tienda.");

                    // 🆕 Eliminar la card visualmente
                    this.Parent?.Controls.Remove(this);
                    this.Dispose();
                }
                return;
            }

            // Verificar si ya existe antes de agregar
            if (lista.Contains(this.Id))
            {
                MessageBox.Show("Este juego ya está en la tienda.");
                return;
            }

            // Agregar a la tienda
            lista.Add(this.Id);
            TiendaManager.Guardar(lista);
            MessageBox.Show("Juego agregado a la tienda.");
        }
    }
}
