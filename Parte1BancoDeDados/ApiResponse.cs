namespace Parte1BancoDeDados
{
    public class ApiResponse
    {
        public object Result { get; }
        public string message { get; set; }
        public int codigo { get; set; }
        public ApiResponse(object result, int cod, string menssagem)
        {
            codigo = cod;
            message = menssagem;
            Result = result;
        }
    }
}
