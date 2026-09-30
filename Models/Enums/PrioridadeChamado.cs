using System.Runtime.Serialization;

namespace DeskFlow.Api.Models.Enums
{
    public enum PrioridadeChamado
    {
        [EnumMember(Value = "Baixa")]
        Baixa,
        [EnumMember(Value = "Média")]
        Media,
        [EnumMember(Value = "Alta")]
        Alta
    }
}