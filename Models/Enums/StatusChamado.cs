using System.Runtime.Serialization;

namespace DeskFlow.Api.Models.Enums
{
    public enum StatusChamado
    {
        [EnumMember(Value = "Aberto")]
        Aberto,
        [EnumMember(Value = "Em Andamento")]
        EmAndamento,
        [EnumMember(Value = "Fechado")]
        Fechado
    }
}