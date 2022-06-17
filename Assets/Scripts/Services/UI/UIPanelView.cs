using UnityEngine;
using Zenject;

namespace Services.UI
{
    public class UIPanelView : MonoBehaviour
    {
        [SerializeField]
        private UIPanelType _type;

        private UIService _uiService;

        public UIPanelType Type => _type;
        
        [Inject]
        public void Init(UIService uiService)
        {
            _uiService = uiService;
        }
        
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