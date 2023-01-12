using System;
using System.Text.Json.Serialization;

namespace TicketBook.Controllers
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CategoryClass
    {
        Critical = 1,
        Important = 2,
        Low = 3
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusClass
    {
        Open = 1,
        Done = 2
    }
}