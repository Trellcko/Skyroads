namespace Trell.Skyroads.Infrastructure.Saving
{
    public interface ISaveService : IService
    {
        SaveData Load();
        void Save();
    }
}