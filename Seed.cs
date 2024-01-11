using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico
{
    public class Seed (GestorAcademicoContext context) 
    {
        private readonly GestorAcademicoContext gestorAcademicoContext = context;

        public void SeedDataContext()
        {
           if(!gestorAcademicoContext.Courses.Any())
            {
                var courses = new List<Course>() { 
                    
                    new() { CourseName = "Tecnologia em Análise e Desenvolvimento de Sistemas", Turn = "Noturno", OpeningsFirstSemester = 80, OpeningsLastSemester = 40 ,Hours = 2369.25m, Semesters = 6, SemesterDurationInWeeks = 19, Mode = "Presencial" ,CategoryCourse = "Tecnólogo" }
                };

                gestorAcademicoContext.AddRange(courses);
                gestorAcademicoContext.SaveChanges();   
            }

        }
    }



}
