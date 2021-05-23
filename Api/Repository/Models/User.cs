using System.ComponentModel.DataAnnotations;

namespace Api.Repository.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public int LuckyNumber { get; set; }
    }
}