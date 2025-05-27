namespace Trell.Skyroads.Infrastructure.Saving
{
    public interface IPersistantPlayerProgressService : IService
    {
        SaveData SaveData { get; set; }
    }
}