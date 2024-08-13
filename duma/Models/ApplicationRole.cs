using Microsoft.AspNet.Identity.EntityFramework;
namespace duma.Models
{
    public class ApplicationRole : IdentityRole
    {
        //функционал от IdentityRole плюс добавляет новое свойство Description, которое будет содержать описание роли
        public ApplicationRole() { }
        public string Description { get; set; }
    }
}