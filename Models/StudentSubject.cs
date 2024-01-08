namespace Gestor_Acadêmico.Models
{
    public class StudentSubject
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int StudentId { get; set; }
        public Student Student {  get; set; }
    }
}
