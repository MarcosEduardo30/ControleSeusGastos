using System.ComponentModel;

namespace Domain.Enums
{
    public enum CategoriaEnum
    {
        [Description("Nenhuma")]
        Nenhuma,
        [Description("Alimentacao")]
        Alimentacao,
        [Description("Lazer")]
        Lazer,
        [Description("Roupas")]
        Roupas,
        [Description("Saude")]
        Saude,
        [Description("Outra")]
        Outra
    }
}
