using Services.UI;
using UnityEngine;
using Zenject;

namespace GameLogics.UILogics
{
    public class MenuController : MonoBehaviour
    {
        [Inject]
        public UIService _uiService;

        public void ClickStartButton()
        {
            _uiService.OnStartButtonClicked();
        }
    }
}