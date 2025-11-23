using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideooJuegos
{
    public partial class Interfaz : Form
    {
        private Color normalBack = Color.Black;
        private Color normalFore = Color.White;
        private Color hoverBack = Color.FromArgb(40, 40, 40); // gris oscuro suave
        private Color hoverFore = Color.White;
        private string clientId = "6y6h5fn6r5qhrlwcql2w4zvq3n0ky9";
        private string clientSecret = "z7b4aldrtv5r48ert3dlemw1fn3vm4";
        int limit = 15;
        int offset = 0;
        IgdbManager manager;
        
        public Interfaz()
        {
            InitializeComponent();
            comboFiltro.Items.Add("Top Rating");
            comboFiltro.Items.Add("Top Reviews");
            comboFiltro.Items.Add("Más nuevos");
            comboFiltro.Items.Add("A–Z");

            comboFiltro.SelectedIndex = 0; // por defecto
            comboFiltro.SelectedIndexChanged += ComboFiltro_SelectedIndexChanged;



            this.WindowState = FormWindowState.Maximized; //Pantalla completa
            this.Bounds = Screen.PrimaryScreen.Bounds;

            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;

            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Renderer = new MyMenuRenderer();

            btnBuscar.Click += BtnBuscar_Click;
            txtBuscar.KeyDown += TxtBuscar_KeyDown;

            _ = InicializarInterfazAsync();
        }
                
        private async Task InicializarInterfazAsync()
        {
            try
            {                
                string accessToken = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                IgdbManager manager = new IgdbManager(clientId, accessToken);

                string query = @"
                fields id,name,rating,first_release_date,genres.name,platforms.name,cover.url;
                where rating != null;
                sort rating desc;
                limit 15;
                ";

                List<IgdbGame> juegos = await manager.GetGamesAsync(query);

                CargarJuegosEnCatalogo(juegos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CargarJuegosEnCatalogo(List<IgdbGame> juegos)
        {
            flowLayoutPanelCatalogo.Controls.Clear();
            flowLayoutPanelCatalogo.AutoScroll = true;
            flowLayoutPanelCatalogo.WrapContents = true;

            foreach (var juego in juegos)
            {
                CardVideoJuegos card = new CardVideoJuegos();

                card.Titulo = juego.Name;

                card.Plataforma = (juego.Platforms != null && juego.Platforms.Any())
                    ? string.Join(", ", juego.Platforms.Select(p => p.Name))
                    : "N/D";

                card.Genero = (juego.Genres != null && juego.Genres.Any())
                    ? string.Join(", ", juego.Genres.Select(g => g.Name))
                    : "N/D";

                card.Rating = juego.Rating > 0 ? juego.Rating.ToString("0.0") : "N/D";

                if (juego.Cover != null && !string.IsNullOrEmpty(juego.Cover.Url))
                {
                    string url = juego.Cover.Url.StartsWith("//")
                        ? "https:" + juego.Cover.Url
                        : juego.Cover.Url;

                    card.Imagen = url;
                }

                flowLayoutPanelCatalogo.Controls.Add(card);
            }
        }               

        public class MyMenuRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                // Rectángulo del item
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

                if (e.Item.Selected && !e.Item.Pressed)
                {
                    // HOVER: gris oscuro
                    using (Brush b = new SolidBrush(Color.FromArgb(80, 80, 80)))
                        e.Graphics.FillRectangle(b, rect);

                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    // NORMAL: negro
                    using (Brush b = new SolidBrush(Color.Black))
                        e.Graphics.FillRectangle(b, rect);

                    e.Item.ForeColor = Color.White;
                }
            }
        }

        private void Interfaz_Load(object sender, EventArgs e)
        {

        }

        private void cATÁLOGOToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            flowLayoutPanelCatalogo.Visible = true;
            flowLayoutPanelTienda.Visible = false;

            btnAnterior.Visible = true;
            btnSiguiente.Visible = true;
        }

        private void tIENDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flowLayoutPanelCatalogo.Visible = false;
            flowLayoutPanelTienda.Visible = true;

            btnAnterior.Visible = false;
            btnSiguiente.Visible = false;
        }

        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                item.BackColor = hoverBack;   // tapa el cian con gris oscuro
                item.ForeColor = hoverFore;   // blanco, se ve bien
            }
        }

        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                item.BackColor = normalBack;  // vuelve a negro
                item.ForeColor = normalFore;  // blanco
            }
        }

        private async Task CargarPagina()
        {
            if (manager == null)
            {
                string accessToken = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                manager = new IgdbManager(clientId, accessToken);
            }

            string query = $@"
            fields id,name,rating,first_release_date,genres.name,platforms.name,cover.url;
            sort rating desc;
            limit {limit};
            offset {offset};
            ";

            List<IgdbGame> juegos = await manager.GetGamesAsync(query);
            CargarJuegosEnCatalogo(juegos);

            btnAnterior.Enabled = offset > 0;
            btnSiguiente.Enabled = juegos.Count == limit; // Si la página está llena, puede haber más
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (offset >= limit)
            {
                offset -= limit;
                await CargarPagina();
            }
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            offset += limit;
            await CargarPagina();
        }
        private async void ComboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            await AplicarFiltroAsync();
        }
        private async Task AplicarFiltroAsync()
        {
            if (manager == null)
            {
                string token = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                manager = new IgdbManager(clientId, token);
            }

            string filtro = comboFiltro.SelectedItem.ToString();
            string query = "";

            switch (filtro)
            {
                case "Top Rating":
                    query = @"
                fields id,name,rating,first_release_date,genres.name,platforms.name,cover.url;
                where rating != null;
                sort rating desc;
                limit 15;
            ";
                    break;

                case "Top Reviews":
                    query = @"
                fields id,name,aggregated_rating,first_release_date,genres.name,platforms.name,cover.url;
                where aggregated_rating != null;
                sort aggregated_rating desc;
                limit 15;
            ";
                    break;

                case "Más nuevos":
                    query = @"
                fields id,name,first_release_date,rating,genres.name,platforms.name,cover.url;
                where first_release_date != null;
                sort first_release_date desc;
                limit 15;
            ";
                    break;

                case "A–Z":
                    query = @"
                fields id,name,rating,first_release_date,genres.name,platforms.name,cover.url;
                sort name asc;
                limit 15;
            ";
                    break;
            }

            List<IgdbGame> juegos = await manager.GetGamesAsync(query);
            CargarJuegosEnCatalogo(juegos);
        }
        private async void BtnBuscar_Click(object sender, EventArgs e)
        {
            await BuscarJuegoAsync();
        }

        private async void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                await BuscarJuegoAsync();
            }
        }
        private async Task BuscarJuegoAsync()
        {
            string texto = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show("Ingresa un nombre para buscar.");
                return;
            }

            if (manager == null)
            {
                string token = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                manager = new IgdbManager(clientId, token);
            }

            string query = $@"
        search ""{texto}"";
        fields id,name,rating,genres.name,platforms.name,cover.url,first_release_date;
        limit 20;
    ";

            try
            {
                List<IgdbGame> juegos = await manager.GetGamesAsync(query);

                if (juegos.Count == 0)
                {
                    MessageBox.Show("No se encontraron juegos con ese nombre.");
                    return;
                }

                CargarJuegosEnCatalogo(juegos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
    }
}