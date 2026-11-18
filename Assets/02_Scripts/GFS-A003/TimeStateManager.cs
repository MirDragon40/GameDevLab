using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


namespace GFS_A003
{

    public enum State
    {
        Idle,
        Playing,
        Paused,
        Ended,

    }

    public class TimeStateManager : MonoBehaviour
    {
        public State currentState { get; private set; } = State.Idle;
        public float currentTimer { get; private set;} = 30;
        public int currentSecond { get; private set; } = 30;
        [SerializeField] private float maxTimer = 30;

        public event Action<State> OnUpdateCurrentStateEvent;
        public event Action<int, float> OnUpdateTiemerEvent;


        private void Awake()
        {
            // 처음 실행 시 상태
            currentState = State.Idle;
            currentTimer = maxTimer;
            currentSecond = Mathf.CeilToInt(currentTimer);
        }

        private void Update()
        {

            HandleInput();

            if (currentState == State.Playing)
            {
                OnTimeUpdate();
            }
            else
            {
                return;
            }
        }

        // 말그대로 시간을 줄이는 메서드
        private void OnTimeUpdate()
        {
            // 현재 타이머 업데이트 
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }
            
            if (currentTimer <= 0)
            {
                currentTimer = 0;
            }

            int nextSecond = Mathf.CeilToInt(currentTimer);

            // 이전 표시 초와 달라졌을 때만 저장하고 알린다.
            // 의미 있는 변화가 있을 때만 반응하고 싶을 떄 쓰는 방식
            if (nextSecond != currentSecond)
            {
                currentSecond = nextSecond;
                OnUpdateTiemerEvent?.Invoke(currentSecond, maxTimer);
            }

            if (currentTimer == 0)
            {
                ChangeState(State.Ended);
            }

            Debug.Log($"현재 남은 시간: {currentTimer} / 현재 상태: {currentState}");


        }


        private void HandleInput()
        {
            // null 검사 
            if (Keyboard.current == null)
                return;


            switch (currentState)
            {
                case State.Idle:
                    if (Keyboard.current.enterKey.wasPressedThisFrame)
                    {
                        currentTimer = maxTimer;
                        ChangeState(State.Playing);
                    }
                    break;

                case State.Playing:
                    // P 입력 검사
                    if (Keyboard.current.pKey.wasPressedThisFrame)
                    {
                        ChangeState(State.Paused);
                    }
                    break;

                case State.Paused:
                    if (Keyboard.current.pKey.wasPressedThisFrame)
                    {
                        ChangeState(State.Playing);
                    }
                    break;

                case State.Ended:
                    if (Keyboard.current.rKey.wasPressedThisFrame)
                    {
                        currentTimer = maxTimer;
                        ChangeState(State.Playing);
                    }
                    break;
            }

        }

        private void ChangeState(State changeState)
        {
            // 상태 변경되는 메서드 
            // 상태 변경 시 이벤트 발생 
            if(changeState == currentState)
            {
                return;
            }

            currentState = changeState;
            OnUpdateCurrentStateEvent?.Invoke(currentState);
            Debug.Log($"상태가 변경되었습니다. 현재 상태: {currentState}");

        }




    }

}

