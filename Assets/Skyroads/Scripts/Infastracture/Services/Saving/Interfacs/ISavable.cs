namespace Trell.Skyroads.Infrastructure.Saving
{
    public interface ISavable : IReadable
    {
        public void Save(SaveData saveData);
    }
}