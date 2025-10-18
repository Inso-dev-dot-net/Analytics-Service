using Analytics.Contracts;

namespace Analytics.Ingestor.Messaging;

public interface IEventDeserializer
{
    // Превратить сырой JSON (value) в EventDto; логировать/кидать исключение на невалидных
    EventDto Deserialize(string json);
}