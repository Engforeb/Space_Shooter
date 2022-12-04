using Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
namespace Infrastructure.Services.SaveLoad
{
    public class SavedLoadService : ISavedLoadService
    {
        private const string ProgressKey = "ProgressKey";
        
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        
        public SavedLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }
        
        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters) 
                progressWriter.UpdateProgress(_progressService.Progress);
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}