using System.ComponentModel.DataAnnotations.Schema;

namespace MaxBox.MVCExample.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Prijs { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public string Afkorting { get; set; }
        public int CategorieId { get; set; }
        [ForeignKey("CategorieId")]
        public virtual ProductCategory Category { get; set; }
        public bool IsBeschikbaar { get; set; }
        public Status Status { get; set; }
        public Product()
        {
            IsBeschikbaar = true;
            Beschrijving = "Lorem ipsum dolor sit amet";
        }
    }
}