using System.ComponentModel.DataAnnotations;

namespace L01_2021CL650_2021SC601.Models
{
    public class pedidos
    {
        [Key]

        public int pedidoId { get; set; }
        public int motoristaId { get; set; }
        public int clienteId { get; set; } 
        public int platoId { get; set; }
        public int cantidad {  get; set; }
        public decimal precio { get; set; }

    }
}
