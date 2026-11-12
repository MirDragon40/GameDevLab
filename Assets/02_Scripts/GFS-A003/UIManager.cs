using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GFS_A003
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TimeStateManager timeStateManager;
        [SerializeField] private Slider timerSlider;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI stateText;


        private void OnEnable()
        {
            timeStateManager.OnUpdateCurrentStateEvent += OnStateUIUpadate;
            timeStateManager.OnUpdateTiemerEvent += OnTimerUIUpdate;
        }

        private void OnDisable()
        {
            timeStateManager.OnUpdateCurrentStateEvent -= OnStateUIUpadate;
            timeStateManager.OnUpdateTiemerEvent -= OnTimerUIUpdate;

        }

        private void Start()
        {
            init();
        }



        private void init()
        {
            if (timeStateManager == null || timerSlider == null || timerText == null || stateText == null)
            {
                Debug.Log("UIManager의 프로퍼티에 객체들이 제대로 할당되지 않았습니다.");
                return;
            }
            else
            {
                timerSlider.value = 1;
                timerText.text = Mathf.CeilToInt(timeStateManager.currentTimer).ToString();
                stateText.text = timeStateManager.currentState.ToString();
            }
        }

        private void OnStateUIUpadate(State currentState)
        {
            stateText.text = timeStateManager.currentState.ToString();
        }

        private void OnTimerUIUpdate(float timer, float maxTimer)
        {
            timerText.text = Mathf.CeilToInt(timeStateManager.currentTimer).ToString();
            timerSlider.value = timer / maxTimer;
        }
    }
}