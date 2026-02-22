using System;
using System.Collections.Generic;
using System.Text;

namespace Commerzia_App.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        // Otros elementos si la API los devuelve, como el nombre o roles
    }
}
