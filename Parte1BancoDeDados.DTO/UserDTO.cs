using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parte1BancoDeDados.Data.DTO
{
    public class UserDTO
    {
        public string cpf { get; set; }
        public string nome { get; set; }
        public DateTime data_nascimento { get; set; }
    }
}
