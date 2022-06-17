using Services.UI;
using UnityEngine;
using Zenject;

namespace GameLogics.UILogics
{
    public class MenuController : MonoBehaviour
    {
        private UIService _uiService;
        
        [Inject]
        public void Init(UIService ui)
        {
            _uiService = ui;
        }
        
        public void ClickStartButton()
        {
            _uiService.OnStartButtonClicked();
        }
    }
}