using System.ComponentModel.DataAnnotations;


namespace L01_2021CL650_2021SC601.Models
{
    public class motoristas
    {
        [Key]

        public int motoristaId { get; set; }
        public string nombreMotorista { get; set; }

    }
}
