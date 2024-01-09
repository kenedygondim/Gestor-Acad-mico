using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico
{
    public class Seed (GestorAcademicoContext context) 
    {
        private readonly GestorAcademicoContext gestorAcademicoContext = context;

        public void SeedDataContext()
        {
            if (!gestorAcademicoContext.Turns.Any())
            {
                var turns = new List<Turn>()
                {
                    new() { TurnCourse = "Matutino" },
                    new() { TurnCourse = "Vespertino" },
                    new() { TurnCourse = "Noturno" },
                    new() { TurnCourse = "Integral" }
                 };

                gestorAcademicoContext.Turns.AddRange(turns);
                gestorAcademicoContext.SaveChanges();
            }


            if (!gestorAcademicoContext.CategoriesCourse.Any())
            {
                var categories = new List<CategoryCourse>()
                    {
                    new () { Name = "Técnologo" },
                    new () { Name = "Licenciatura" },
                    new () { Name = "Bacharelado" },
                    new () { Name = "Técnico" },
                    new () { Name = "Pós-graduação"},
                    new () { Name = "Cursos livres"}
                };

                gestorAcademicoContext.CategoriesCourse.AddRange(categories);
                gestorAcademicoContext.SaveChanges();
            };
        }
    }



}
