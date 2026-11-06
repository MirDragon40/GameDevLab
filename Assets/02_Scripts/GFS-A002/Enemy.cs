using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFS_A002
{

    public class Enemy : MonoBehaviour
    {
        // 최대 체력 필요 없을 듯하여 안넣음. 현재 체력으로 관리
        [SerializeField] private int EnemyCurrentHP = 70;
        [SerializeField] private int damagePerClick = 15;

        // 중복 방지를 위해 isDead 라는 상태를 저장해준다. 
        private bool isDead = false;
        public event Action OnEnemyDiedEvent;



        private void Update()
        {
            CheckEnemyClick();
        }



        // 마우스 클릭 시 공격 받으면 데미지 부여
        private void CheckEnemyClick()
        {
            // AI 수정제안 부분
            if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
                return;

            Camera camera = Camera.main;
            if (camera == null)
                return;

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = camera.ScreenPointToRay(mousePosition);


            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {

                    Debug.Log($"{this.name}클릭 감지 ");

                    // 클릭 시 우선 체력 감소 
                    EnemyCurrentHP -= damagePerClick;

                    // 현재 적 체력 0보다 작을 시
                    if (EnemyCurrentHP <= 0)
                    {
                        EnemyDie();
                    }
                    else
                    {
                        Debug.Log($"적 체력{damagePerClick}감소, 현재 적의 체력은 {EnemyCurrentHP}입니다.");
                        return;
                    }
                }
            }

        }


        // 사망시, 자기자신 제거
        private void EnemyDie()
        {
            if (isDead)
                return;

            // 이벤트 처리 중 이 함수가 다시 호출되더라도 중복 사망 처리를 막도록,
            // 이벤트 발행 전에 사망 상태를 확정한다.
            isDead = true;

            OnEnemyDiedEvent?.Invoke();
            Debug.Log($"몬스터 한 마리가 죽었습니다.");
            Destroy(this.gameObject);

        }


    }
}