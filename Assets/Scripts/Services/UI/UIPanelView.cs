using UnityEngine;
using Zenject;

namespace Services.UI
{
    public class UIPanelView : MonoBehaviour
    {
        [SerializeField]
        private UIPanelType _type;

        [Inject]
        private UIService _uiService;

        public UIPanelType Type => _type;

        private void Awake()
        {
            _uiService.Register(this);
        }
        
        private void OnDestroy()
        {
            _uiService.Unregister(this);
        }
    }
}