using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Parte1BancoDeDados.Data.DTO;

namespace Parte1BancoDeDados.infra.connection
{
    public class PostgreConnection
    {
        public StatusReturn InsertUser(UserDTO userEntity) 
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = atv-bd-parte1.ct9f8zzzguw3.us-east-1.rds.amazonaws.com; Port = 5432; User Id = professor; Password = professor; Database = bd_atv_bd_parte1;");
            StatusReturn status = new StatusReturn();            
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection= conn;
                cmd.CommandText = "INSERT INTO usuario VALUES(@cpf, @nome, @data_nascimento)";

                cmd.Parameters.AddWithValue("@cpf", userEntity.cpf);
                cmd.Parameters.AddWithValue("@nome", userEntity.nome);
                cmd.Parameters.AddWithValue("@data_nascimento", Convert.ToDateTime(userEntity.data_nascimento));

                conn.Open();

                if(cmd.ExecuteNonQuery() > 0)
                {
                    status.codigoErro = 0;
                    status.message = "Usuario salvo com sucesso";
                }
            }
            catch(Exception ex) 
            {
                status.codigoErro = 1;
                status.message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return status;
        }
        public List<UserEntity> DbConnection()
        {
            List<UserEntity> users = new List<UserEntity>();
            NpgsqlConnection conn = new NpgsqlConnection ("Server = atv-bd-parte1.ct9f8zzzguw3.us-east-1.rds.amazonaws.com; Port = 5432; User Id = professor; Password = professor; Database = bd_atv_bd_parte1;");
            try
            {
                NpgsqlCommand sql = new NpgsqlCommand();
                sql.CommandText = "SELECT * FROM usuario";
                sql.Connection = conn;
                conn.Open();

                NpgsqlDataReader dataReader = sql.ExecuteReader();

                while(dataReader.Read ())
                {
                    UserEntity user = new UserEntity ();
                    user.cpf = (int)Convert.ToInt64(dataReader["cpf"]);
                    user.nome = Convert.ToString(dataReader["nome"]);
                    user.data_nascimento = Convert.ToString(dataReader["data_nascimento"]);

                    users.Add (user);
                }
                
            }
            catch(Exception ex) 
            {
                Console.WriteLine (ex.Message); 
            }
            finally
            {
                conn.Close();
            }
            return users;
        }
        public UserEntity GetUserByCpf(string cpf)
        {
            UserEntity user = new UserEntity();
            NpgsqlConnection conn = new NpgsqlConnection("Server = atv-bd-parte1.ct9f8zzzguw3.us-east-1.rds.amazonaws.com; Port = 5432; User Id = professor; Password = professor; Database = bd_atv_bd_parte1;");
            try
            {
                NpgsqlCommand sql = new NpgsqlCommand();
                sql.CommandText = $"SELECT * FROM usuario WHERE cpf = '{cpf.PadLeft(11, '0')}'";
                sql.Connection = conn;
                conn.Open();

                NpgsqlDataReader dataReader = sql.ExecuteReader();

                if(dataReader.Read())
                {
                    user.cpf = (int)Convert.ToInt64(dataReader["cpf"]);
                    user.nome = Convert.ToString(dataReader["nome"]);
                    user.data_nascimento = Convert.ToString(dataReader["data_nascimento"]);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return user;
        }
    }
}
