using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Data
{
    public class DataSeeders
    {
        public static async void Iniialize(IServiceProvider serviceProvider){
            using (var scope = serviceProvider.CreateScope()){
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();  // Obtén el UserManager
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();  // Obtén el RoleManager

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
                        Email = "admin@idwm.cl.",
                        UserName="admin@idwm.cl.",
                        genderId = 1,
                        enable = true,
                    };
                    //Asignamos la contraseña al usuario.
                    var result = await userManager.CreateAsync(user, "P4ssw0rd!");
                    if (result.Succeeded)
                    {
                        // Asignamos el rol de "Administrador".
                        await userManager.AddToRoleAsync(user, "Administrador");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }
                }
                //Se guardan los cambios
                await serviceProvider.GetRequiredService<ApplicationDbContext>().SaveChangesAsync();
            }
            
        }
    }
}