using UnityEngine;

namespace Settings
{
    public class SettingsContainer : MonoBehaviour
    {
        [SerializeField]
        private GameSettings _gameSettings;
        [SerializeField]
        private CockroachSettings _cockroachSettings;
        [SerializeField]
        private GameObject _cockroachPrefab;

        public GameSettings GameSettings => _gameSettings;
        public CockroachSettings CockroachSettings => _cockroachSettings;
        public GameObject CockroachPrefab => _cockroachPrefab;
    }
}