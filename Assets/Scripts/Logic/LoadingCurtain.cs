using System.Collections;
using UnityEngine;

namespace Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup curtain;
        private WaitForSeconds wait;

        private void Awake()
        {
            wait = new WaitForSeconds(0.03f);
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            curtain.alpha = 1;
        }

        public void Hide() => 
            StartCoroutine(FadeIn());
        
        private IEnumerator FadeIn()
        {
            while (curtain.alpha > 0)
            {
                curtain.alpha -= 0.03f;
                yield return wait;
            }
            
            gameObject.SetActive(false);
        }
    }
}
