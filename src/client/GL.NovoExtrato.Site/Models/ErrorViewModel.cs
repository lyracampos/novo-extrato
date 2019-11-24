namespace GL.NovoExtrato.Site.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            Titulo = "Ocorreu um erro";
            Subtitulo = "Ocorreu um erro";
            Mensagem = "Ocorreu um erro";
        }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Mensagem { get; set; }
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void Error500()
        {
            Titulo = "Ocorreu um erro interno";
            Subtitulo = "O serviço chamado está indisponível no momento.";
            Mensagem = "Não foi possível estabelecer conexão com o serviço solicitado, tente novamente.";
        }
    }
}