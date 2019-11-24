using System.ComponentModel.DataAnnotations;
namespace GL.NovoExtrato.Site.Models
{
    public class CriarClientesModel
    {
        [Required(ErrorMessage ="Informe a quantidade de clientes")]
        public int Quantidade { get; set; }
    }
}
