using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Data
{
    public class DataSeeders
    {
        public static void Iniialize(IServiceProvider serviceProvider){
            using (var scope = serviceProvider.CreateScope()){
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                if(!context.Roles.Any()){
                    context.Roles.AddRange(
                        new Role {name = "Administrador"},
                        new Role {name = "Cliente"}
                    );
                }
               if(!context.Genders.Any()){
                    context.Genders.AddRange(
                        new Gender {name = "Femenino"},
                        new Gender {name = "Masculino"},
                        new Gender {name = "Prefiero no decirlo"},                        
                        new Gender {name = "Otro"}
                    );
                }
                if(!context.Categories.Any()){
                    context.Categories.AddRange(
                        new Category {name = "Poleras"},
                        new Category {name = "Gorros"},
                        new Category {name = "Juguetería"},
                        new Category {name = "Alimentación"},
                        new Category {name = "Libros"}
                    );
                }
                context.SaveChanges();

                if(!context.Users.Any())
                {
                    var user = new User { 
                        rut = "20.416.699-4", 
                        name = "Ignacio Mancilla",
                        birthDate = new DateOnly(2000,10,25), 
                        email = "admin@idwm.cl.",
                        genderId = 1,
                        password = "P4ssw0rd",
                        enabled = true,
                        roleId = 1
                    };
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
            
        }
    }
}