using TMPro;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class LoadingController : MonoBehaviour
    {
        private int _maxAssets = 0;
        private int _currentAssets = 0;
        [SerializeField] private TextMeshProUGUI assetsLoadedProgressTxt;
        
        public void Init(int maxAssets)
        {
            _maxAssets = maxAssets;
        }

        public void UpdateAssetsLoaded(int assetsLoaded)
        {
            _currentAssets += assetsLoaded;
            assetsLoadedProgressTxt.text = $"Loading {_currentAssets} of {_maxAssets} assets.";
        }
    }
}