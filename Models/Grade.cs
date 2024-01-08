using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public decimal Frequence { get; set; }

        [Range(0, 10)]
        public decimal FirstAvaliation { get; set; }
        [Range(0, 10)]
        public decimal SecondAvaliation { get; set; }
        [Range(0, 10)]
        public decimal Activities { get; set; }
        [Range(0, 10)]
        public decimal Balance { get; private set; }

        public StudentGrade Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }


        public void CalculateBalance()
        {
            decimal calculated = CalculatedBalance();
            Balance = calculated;
        }

        private decimal CalculatedBalance()
        {
            if(FirstAvaliation != null && SecondAvaliation != null && Activities != null)
            {
                decimal balance = (FirstAvaliation + SecondAvaliation + Activities) / 3;
                return balance;
            }

            return 0;
        }
    }
}
