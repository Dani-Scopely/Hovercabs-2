using DG.Tweening;
using Hovercabs.Models;
using UnityEngine;

namespace Utils
{
    public class DoTweenTest : MonoBehaviour
    {
        private Sequence _sequence;
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ExecuteTween();    
            }
        }

        private void ExecuteTween()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DORotate(new Vector3(0, 0, 45), 0.1f));
            _sequence.Append(transform.DOLocalMoveX(-4f, 0.2f));
            _sequence.Append(transform.DORotate(new Vector3(0, 0, 0), 0.1f));
        }
    }
}
