using System;

namespace Covid19Data.Domain.Services
{
    public interface IJsonService
    {
        object Deserialize(string json, Type type);
    }
}