using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFS_A002
{

    public class EnemyWaveSpawner : MonoBehaviour
    {
        public int EnemyCount { get; private set; } = 0;
        [SerializeField] private bool isSpawnable = true;
        [SerializeField] private bool isFirstWave = true;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Transform[] enemySpawnPositions;

        public event Action<int> OnUpdateEnemyNumEvent;
        public event Action<bool, bool> OnUpdateSpawnableStateEvent;



        private void Update()
        {
            if (Keyboard.current == null || !Keyboard.current.spaceKey.wasPressedThisFrame)
                return;


            if (Keyboard.current.spaceKey.wasPressedThisFrame && isSpawnable)
            {
                if (!SpawnEnemies())
                    return;

                isSpawnable = false;

                if (isFirstWave)
                {
                    isFirstWave = false;
                }

                OnUpdateSpawnableStateEvent?.Invoke(isSpawnable, isFirstWave);

            }
        }

        // 새로운 bool을 만들어서 상태를 저장할 필요없이
        // 메서드의 반환값을 bool로 설정해서 검사를진행 
        private bool SpawnEnemies()
        {
            // null 검사
            if (enemyPrefab == null || enemySpawnPositions == null || enemySpawnPositions.Length == 0) // 배열의 길이가 0인 경우도 포함.
            {
                Debug.Log("enemyPrefab 혹은 enemySpawnPositions가 제대로 할당되어있는지 확인이 필요합니다.");
                return false;
            }

            // null 검사 2 -> AI의 도움을 받은 부분
            for (int i = 0; i < enemySpawnPositions.Length; i++)
            {
                if (enemySpawnPositions[i] == null)
                    return false;
            }

            for (int i = 0; i < enemySpawnPositions.Length; i++)
            {
                Enemy enemy = Instantiate(
                    enemyPrefab,
                    enemySpawnPositions[i].position,
                    Quaternion.identity);

                EnemyCount++;
                OnUpdateEnemyNumEvent?.Invoke(EnemyCount);

                enemy.OnEnemyDiedEvent += OnUpdateEnemyNum;

                Debug.Log($"{enemy.gameObject.name}가 {i + 1}마리 스폰되었습니다.");

            }

            // 적 스폰이 모두 성공적으로 완료 되었을 때만 true 반환
            return true;
        }


        private void OnUpdateEnemyNum()
        {
            EnemyCount--;
            OnUpdateEnemyNumEvent?.Invoke(EnemyCount);

            if (EnemyCount <= 0)
            {
                isSpawnable = true;
                OnUpdateSpawnableStateEvent?.Invoke(isSpawnable, isFirstWave);
            }
            else
            {
                Debug.Log($"Enemy Count가 {EnemyCount}마리로 업데이트 되었습니다.");

            }
        }
    }
}