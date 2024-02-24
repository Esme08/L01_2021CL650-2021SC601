using System.ComponentModel.DataAnnotations;


namespace L01_2021CL650_2021SC601.Models
{
    public class platos
    {
        [Key]

        public int platoId { get; set; }
        public string nombrePlato { get; set; }
        public decimal precio { get; set; }

    }
}
