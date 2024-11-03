using System.ComponentModel.DataAnnotations;

namespace SUbProject_02_MovieApp.DTOModels.UserDTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
