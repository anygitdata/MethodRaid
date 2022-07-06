using System.ComponentModel.DataAnnotations;

namespace MethodRaid.Domain.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Display(Name = "Фамилия И.О.")]
        public string ClientName { get; set; }
    }
}
