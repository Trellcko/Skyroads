using Trell.Skyroads.Extra;
using Trell.Skyroads.Gameplay.Score;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.Saving
{
    public class SaveService : ISaveService
    {
        private readonly IPersistantPlayerProgressService _persistantPlayerProgressService;

        private readonly IScore _score;
        
        private const string Progress = "Progress";

        public SaveService(IPersistantPlayerProgressService persistantPlayerProgressService, IScore score)
        {
            _persistantPlayerProgressService = persistantPlayerProgressService;
            _score = score;
        }
        
        public SaveData Load() => 
            PlayerPrefs.GetString(Progress).ToDeserialize<SaveData>();

        public void Save() 
        {
            _score.Save(_persistantPlayerProgressService.SaveData);
            string json = _persistantPlayerProgressService.SaveData.ToJson();
            Debug.Log($"SaveData: \n {json}");
            PlayerPrefs.SetString(Progress, json);
        }
            
    }
}