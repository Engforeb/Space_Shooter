using Data;
using UnityEngine;
namespace Infrastructure.Services.SaveLoad
{
    public class SavedLoadService : ISavedLoadService
    {
        private const string ProgressKey = "ProgressKey";
        
        public void SaveProgress()
        {
            
        }
        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}
