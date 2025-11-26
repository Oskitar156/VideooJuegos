using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace VideooJuegos
{

    public partial class Interfaz : Form
    {
        private Color normalBack = Color.Black;
        private Color normalFore = Color.White;
        private Color hoverBack = Color.FromArgb(40, 40, 40);
        private Color hoverFore = Color.White;
        private string clientId = "6y6h5fn6r5qhrlwcql2w4zvq3n0ky9";
        private string clientSecret = "z7b4aldrtv5r48ert3dlemw1fn3vm4";
        int limit = 15;
        int offset = 0;
        IgdbManager manager;

        private string filtroActual = "Top Rating";
        private string busquedaActual = "";

        public string UsuarioActualEmail { get; set; }
        public string UsuarioActualRol { get; set; }

        public Interfaz()
        {
            InitializeComponent();
            this.Load += Interfaz_Load;
            comboFiltro.Items.Add("Top Rating");
            comboFiltro.Items.Add("Top Reviews");
            comboFiltro.Items.Add("Más nuevos");
            comboFiltro.Items.Add("A–Z");

            comboFiltro.SelectedIndex = 0;
            comboFiltro.SelectedIndexChanged += ComboFiltro_SelectedIndexChanged;

            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            flowLayoutFavoritos.Visible = false;

            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Renderer = new MyMenuRenderer();

            btnBuscar.Click += BtnBuscar_Click;
            txtBuscar.KeyDown += TxtBuscar_KeyDown;
        }

        private async Task InicializarInterfazAsync()
        {
            try
            {
                string accessToken = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                manager = new IgdbManager(clientId, accessToken);

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
                card.Id = juego.Id;
                card.Titulo = juego.Name;

                card.Plataforma = (juego.Platforms != null && juego.Platforms.Any())
                    ? string.Join(", ", juego.Platforms.Select(p => p.Name))
                    : "N/D";

                // EN CATÁLOGO: Mostrar GÉNERO
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

                card.EsAdmin = (UsuarioActualRol == "Admin");
                card.MostrarBotonEditar = false; // NO mostrar botón editar en catálogo

                // Ocultar el label de precio en el catálogo
                card.PrecioVisible = false;

                flowLayoutPanelCatalogo.Controls.Add(card);
            }
        }

        public class MyMenuRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

                if (e.Item.Selected && !e.Item.Pressed)
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(80, 80, 80)))
                        e.Graphics.FillRectangle(b, rect);

                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    using (Brush b = new SolidBrush(Color.Black))
                        e.Graphics.FillRectangle(b, rect);

                    e.Item.ForeColor = Color.White;
                }
            }
        }

        private async void Interfaz_Load(object sender, EventArgs e)
        {
            await InicializarInterfazAsync();

            flowLayoutPanelCatalogo.Visible = true;
            flowLayoutPanelCatalogo.BringToFront();
            flowLayoutPanelTienda.Visible = false;
            flowLayoutFavoritos.Visible = false;
        }

        private void cATÁLOGOToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            flowLayoutPanelTienda.Visible = false;
            flowLayoutFavoritos.Visible = false;

            flowLayoutPanelCatalogo.Visible = true;
            flowLayoutPanelCatalogo.BringToFront();

            btnAnterior.Visible = true;
            btnSiguiente.Visible = true;

            flowLayoutPanelCatalogo.Refresh();
        }

        private async void tIENDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flowLayoutPanelCatalogo.Visible = false;
            flowLayoutPanelTienda.Visible = true;
            flowLayoutFavoritos.Visible = false;
            btnAnterior.Visible = false;
            btnSiguiente.Visible = false;
            await CargarTienda();
        }

        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                item.BackColor = hoverBack;
                item.ForeColor = hoverFore;
            }
        }

        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                item.BackColor = normalBack;
                item.ForeColor = normalFore;
            }
        }

        private async Task CargarPagina()
        {
            if (manager == null)
            {
                string accessToken = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                manager = new IgdbManager(clientId, accessToken);
            }

            string query = GenerarQueryFiltro(filtroActual, limit, offset);

            List<IgdbGame> juegos = await manager.GetGamesAsync(query);
            CargarJuegosEnCatalogo(juegos);

            btnAnterior.Enabled = offset > 0;
            btnSiguiente.Enabled = juegos.Count == limit;
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (offset >= limit)
            {
                offset -= limit;

                if (!string.IsNullOrEmpty(busquedaActual))
                    await CargarPaginaBusquedaAsync();
                else
                    await CargarPagina();
            }
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            offset += limit;

            if (!string.IsNullOrEmpty(busquedaActual))
                await CargarPaginaBusquedaAsync();
            else
                await CargarPagina();
        }

        private async void ComboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtroActual = comboFiltro.SelectedItem.ToString();
            offset = 0;
            await AplicarFiltroAsync();
        }

        private async Task AplicarFiltroAsync()
        {
            if (manager == null)
            {
                string token = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                manager = new IgdbManager(clientId, token);
            }

            string query = GenerarQueryFiltro(filtroActual, limit, offset);

            List<IgdbGame> juegos = await manager.GetGamesAsync(query);
            CargarJuegosEnCatalogo(juegos);

            btnAnterior.Enabled = offset > 0;
            btnSiguiente.Enabled = juegos.Count == limit;
        }

        private string GenerarQueryFiltro(string filtro, int limit, int offset)
        {
            switch (filtro)
            {
                case "Top Rating":
                    return $@"
                fields id,name,rating,first_release_date,genres.name,platforms.name,cover.url;
                where rating != null;
                sort rating desc;
                limit {limit};
                offset {offset};
            ";

                case "Top Reviews":
                    return $@"
                fields id,name,aggregated_rating,first_release_date,genres.name,platforms.name,cover.url;
                where aggregated_rating != null;
                sort aggregated_rating desc;
                limit {limit};
                offset {offset};
            ";

                case "Más nuevos":
                    return $@"
                fields id,name,first_release_date,rating,genres.name,platforms.name,cover.url;
                where first_release_date != null;
                sort first_release_date desc;
                limit {limit};
                offset {offset};
            ";

                case "A–Z":
                    return $@"
                fields id,name,rating,first_release_date,genres.name,platforms.name,cover.url;
                sort name asc;
                limit {limit};
                offset {offset};
            ";
            }

            return "";
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
            busquedaActual = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(busquedaActual))
            {
                MessageBox.Show("Ingresa un nombre para buscar.");
                return;
            }

            offset = 0;
            await CargarPaginaBusquedaAsync();
        }

        private async Task CargarPaginaBusquedaAsync()
        {
            if (manager == null)
            {
                string token = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                manager = new IgdbManager(clientId, token);
            }

            string query = $@"
            search ""{busquedaActual}"";
            fields id,name,rating,genres.name,platforms.name,cover.url,first_release_date;
            limit {limit};
            offset {offset};
            ";

            try
            {
                List<IgdbGame> juegos = await manager.GetGamesAsync(query);
                CargarJuegosEnCatalogo(juegos);

                btnAnterior.Enabled = offset > 0;
                btnSiguiente.Enabled = juegos.Count == limit;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message);
            }
        }

        private async Task CargarTienda()
        {
            flowLayoutPanelTienda.Visible = true;
            flowLayoutPanelTienda.BringToFront();

            try
            {
                flowLayoutPanelTienda.Controls.Clear();
                flowLayoutPanelTienda.AutoScroll = true;
                flowLayoutPanelTienda.WrapContents = true;
                flowLayoutPanelTienda.FlowDirection = FlowDirection.LeftToRight;

                // CARGAR JUEGOS CON PRECIO Y STOCK
                List<JuegoTienda> juegosTienda = TiendaManager.Cargar();

                if (juegosTienda == null || juegosTienda.Count == 0)
                {
                    MessageBox.Show("No hay juegos en la tienda. Agrega algunos desde el catálogo.", "Tienda vacía");
                    return;
                }

                if (manager == null)
                {
                    string accessToken = await IgdbTokenManager.GetTokenAsync(clientId, clientSecret);
                    manager = new IgdbManager(clientId, accessToken);
                }

                // Obtener solo los IDs para la consulta a la API
                List<long> idsJuegos = juegosTienda.Select(j => j.Id).ToList();
                string ids = string.Join(",", idsJuegos);

                string query = $@"
            fields id,name,rating,first_release_date,genres.name,platforms.name,cover.url;
            where id = ({ids});
        ";

                List<IgdbGame> juegosDeLaApi = await manager.GetGamesAsync(query);

                if (juegosDeLaApi == null || juegosDeLaApi.Count == 0)
                {
                    MessageBox.Show("No se pudieron cargar los juegos de la API.", "Error");
                    return;
                }

                // Crear las tarjetas con la información combinada
                foreach (var juegoApi in juegosDeLaApi)
                {
                    // Buscar el precio y stock del juego en la tienda
                    var juegoTienda = juegosTienda.Find(j => j.Id == juegoApi.Id);

                    if (juegoTienda == null)
                        continue; // Si no se encuentra, saltar este juego

                    CardVideoJuegos card = new CardVideoJuegos();

                    card.Id = juegoApi.Id;
                    card.Titulo = juegoApi.Name;

                    card.Plataforma = (juegoApi.Platforms != null && juegoApi.Platforms.Any())
                        ? string.Join(", ", juegoApi.Platforms.Select(p => p.Name))
                        : "N/D";

                    // Formatear precio en moneda colombiana y mostrar prefijo "Precio:"
                    var culture = new CultureInfo("es-CO");
                    card.Precio = $"Precio: {juegoTienda.Precio.ToString("C0", culture)}";

                    // Mostrar el label de precio en la vista tienda
                    card.PrecioVisible = true;

                    card.Rating = juegoApi.Rating > 0 ? juegoApi.Rating.ToString("0.0") : "N/D";

                    if (juegoApi.Cover != null && !string.IsNullOrEmpty(juegoApi.Cover.Url))
                    {
                        string url = juegoApi.Cover.Url.StartsWith("//")
                            ? "https:" + juegoApi.Cover.Url
                            : juegoApi.Cover.Url;

                        card.Imagen = url;
                    }

                    // Usar la propiedad Genero para mostrar el stock
                    card.Genero = $"Stock: {juegoTienda.Stock} unidades";

                    // Visibilidad y textos de botones según rol
                    card.EsAdmin = (UsuarioActualRol == "Admin");

                    // Asegurar que el botón de acción (btnCard) muestre "Eliminar" en la vista tienda
                    card.TextoBoton = "Eliminar";

                    // Mostrar/ocultar el botón de acción según rol (solo admin puede eliminar)
                    card.MostrarBoton = (UsuarioActualRol == "Admin");

                    // Mostrar/ocultar botón editar según rol (solo admin puede editar)
                    card.MostrarBotonEditar = (UsuarioActualRol == "Admin");

                    flowLayoutPanelTienda.Controls.Add(card);
                }

                flowLayoutPanelTienda.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la tienda: " + ex.Message, "Error");
            }
        }

        private void flowLayoutFavoritos_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}