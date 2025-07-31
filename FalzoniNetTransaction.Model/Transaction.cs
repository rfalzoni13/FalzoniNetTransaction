using FalzoniNetTransaction.Model.Annotations;
using System.ComponentModel.DataAnnotations;

namespace FalzoniNetTransaction.Model
{
    public class Transaction
    {
        // Primeira Opção: Checagem via Data Annotations
        [Required(ErrorMessage = "O valor deve ser preenchido")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "A transação não deve possuir valor negativo")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "A data e hora devem ser preenchidas")]
        [MinDate(ErrorMessage = "A transação deve ocorrer em uma data anterior à atual")]
        public DateTime? DataHora { get; set; }
    }
}
