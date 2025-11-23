namespace VideooJuegos
{
    partial class Interfaz
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interfaz));
            this.flowLayoutPanelCatalogo = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelTienda = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cATÁLOGOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tIENDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.flowLayoutPanelCatalogo.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelCatalogo
            // 
            this.flowLayoutPanelCatalogo.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelCatalogo.Controls.Add(this.flowLayoutPanelTienda);
            this.flowLayoutPanelCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelCatalogo.Location = new System.Drawing.Point(0, 32);
            this.flowLayoutPanelCatalogo.Name = "flowLayoutPanelCatalogo";
            this.flowLayoutPanelCatalogo.Size = new System.Drawing.Size(632, 316);
            this.flowLayoutPanelCatalogo.TabIndex = 1;
            // 
            // flowLayoutPanelTienda
            // 
            this.flowLayoutPanelTienda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTienda.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelTienda.Name = "flowLayoutPanelTienda";
            this.flowLayoutPanelTienda.Size = new System.Drawing.Size(200, 0);
            this.flowLayoutPanelTienda.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cATÁLOGOToolStripMenuItem,
            this.tIENDAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 32);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cATÁLOGOToolStripMenuItem
            // 
            this.cATÁLOGOToolStripMenuItem.Font = new System.Drawing.Font("Cooper Black", 15.75F);
            this.cATÁLOGOToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.cATÁLOGOToolStripMenuItem.Name = "cATÁLOGOToolStripMenuItem";
            this.cATÁLOGOToolStripMenuItem.Size = new System.Drawing.Size(148, 28);
            this.cATÁLOGOToolStripMenuItem.Text = "CATÁLOGO";
            this.cATÁLOGOToolStripMenuItem.Click += new System.EventHandler(this.cATÁLOGOToolStripMenuItem_Click_1);
            this.cATÁLOGOToolStripMenuItem.MouseEnter += new System.EventHandler(this.MenuItem_MouseEnter);
            this.cATÁLOGOToolStripMenuItem.MouseLeave += new System.EventHandler(this.MenuItem_MouseLeave);
            // 
            // tIENDAToolStripMenuItem
            // 
            this.tIENDAToolStripMenuItem.Font = new System.Drawing.Font("Cooper Black", 15.75F);
            this.tIENDAToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.tIENDAToolStripMenuItem.Name = "tIENDAToolStripMenuItem";
            this.tIENDAToolStripMenuItem.Size = new System.Drawing.Size(112, 28);
            this.tIENDAToolStripMenuItem.Text = "TIENDA";
            this.tIENDAToolStripMenuItem.Click += new System.EventHandler(this.tIENDAToolStripMenuItem_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Font = new System.Drawing.Font("Cooper Black", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnterior.Location = new System.Drawing.Point(396, 0);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(56, 32);
            this.btnAnterior.TabIndex = 3;
            this.btnAnterior.Text = "◀";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Font = new System.Drawing.Font("Cooper Black", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguiente.Location = new System.Drawing.Point(470, 0);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(56, 32);
            this.btnSiguiente.TabIndex = 3;
            this.btnSiguiente.Text = "▶";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // Interfaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(632, 348);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.flowLayoutPanelCatalogo);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Interfaz";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.Interfaz_Load);
            this.flowLayoutPanelCatalogo.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCatalogo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTienda;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cATÁLOGOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tIENDAToolStripMenuItem;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
    }
}