using System;

namespace VideooJuegos
{
    public class JuegoTienda
    {
        public long Id { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public JuegoTienda()
        {
        }

        public JuegoTienda(long id, decimal precio, int stock)
        {
            Id = id;
            Precio = precio;
            Stock = stock;
        }
    }
}