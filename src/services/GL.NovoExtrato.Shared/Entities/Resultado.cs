namespace GL.NovoExtrato.Shared.Entities
{
    public class Resultado
    {
        public Resultado(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
        public bool Sucesso { get; private set; }
        public string Mensagem { get; set; }
    }
}