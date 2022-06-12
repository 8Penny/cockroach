using UnityEngine;

namespace Settings
{
    public class SettingsContainer : MonoBehaviour
    {
        [SerializeField]
        private GameSettings _gameSettings;

        public GameSettings GameSettings => _gameSettings;
    }
}