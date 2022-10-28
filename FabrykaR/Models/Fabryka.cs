using System.ComponentModel.DataAnnotations;

namespace FabrykaR.Models
{

    public class Hala
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; } = String.Empty;
        public string? Adres { get; set; }

        public virtual ICollection<Maszyna>? Maszyny { get; set; }
    }

    public class Maszyna
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; } = String.Empty;
        public Nullable<System.DateTime> Data_uruchomienia { get; set; }
        public int HalaId { get; set; }

        public virtual Hala? Hala { get; set; }
        public virtual ICollection<OperatorMaszyna>? OperatorMaszyna { get; set; }
    }

    public class Operator
    {
        [Key]
        public int Id { get; set; }
        public string Nazwisko { get; set; } = String.Empty;
        public string Imie { get; set; } = String.Empty;
        public double? Placa { get; set; }

        public virtual ICollection<OperatorMaszyna>? OperatorMaszyna { get; set; }
    }

    public class OperatorMaszyna
    {
        public int OperatorId { get; set; }
        public Operator? Operator { get; set; }
        public int MaszynaId { get; set; }
        public Maszyna? Maszyna { get; set; }
    }

}
