using Parte1BancoDeDados.Data.DTO;
using Parte1BancoDeDados.infra.connection;

namespace Parte1BancoDeDados.Data
{
    public class User
    {
        public List<UserEntity> getAllUsers() 
        {
            PostgreConnection dbConnection = new PostgreConnection();
            List<UserEntity> AllUsers = new List<UserEntity>();

            AllUsers = dbConnection.DbConnection();

            return AllUsers;
        }

        public StatusReturn CreateUser(UserEntity user)
        {
            PostgreConnection postgreConnection  = new PostgreConnection();
            UserDTO userDTO = new UserDTO();

            userDTO.cpf = Convert.ToString(user.cpf);
            userDTO.nome = user.nome;
            userDTO.data_nascimento = Convert.ToDateTime(user.data_nascimento);

            if(userDTO.cpf.Length < 11)
            {
                userDTO.cpf = userDTO.cpf.PadLeft(11, '0');
            }

            var users = postgreConnection.InsertUser(userDTO);

            return users;
        }

        public UserDTO GetUser(int cpf)
        {
            PostgreConnection postgreConnection = new PostgreConnection();
            UserDTO userDTO = new UserDTO();

            string formatCpf = Convert.ToString(cpf);

            var result = postgreConnection.GetUserByCpf(formatCpf);

            userDTO.cpf = Convert.ToString(result.cpf).PadLeft(11, '0');
            userDTO.nome = result.nome;
            userDTO.data_nascimento = Convert.ToDateTime(result.data_nascimento);

            return userDTO;

        }
    }
}