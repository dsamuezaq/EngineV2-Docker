using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SystemViewModel
{
    public class LoginViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email es requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email no es válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        /// <summary>
        /// Recuerdame
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
