using TMPro;
using UnityEngine;

namespace GFS_A002
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private EnemyWaveSpawner enemyWaveSpawner;
        [SerializeField] private TextMeshProUGUI pressSpaceText;
        [SerializeField] private TextMeshProUGUI enemyCountText;
        [SerializeField] private TextMeshProUGUI waveCompleteText;

        // AI의 도움으로 수정한 부분
        private bool hasValidReferences;

        private void Awake()
        {

            hasValidReferences =
               enemyWaveSpawner != null &&
               pressSpaceText != null &&
               enemyCountText != null &&
               waveCompleteText != null;


            if (!hasValidReferences)
            {
                Debug.Log($"{gameObject.name}의 인스펙터 창에서 객체들이 잘 할당되어있는지 확인해주세요.");
                // AI의 도움으로 수정한 부분
                enabled = false;
                return;
            }
            else
            {
                pressSpaceText.gameObject.SetActive(true);
                waveCompleteText.gameObject.SetActive(false);
                UpdateEnemyCount(enemyWaveSpawner.EnemyCount);

            }

        }

        private void OnEnable()
        {
            enemyWaveSpawner.OnUpdateEnemyNumEvent += UpdateEnemyCount;
            enemyWaveSpawner.OnUpdateSpawnableStateEvent += UpdateStateUI;
        }

        private void OnDisable()
        {
            // AI의 도움으로 수정한 부분
            if (enemyWaveSpawner == null)
                return;

            enemyWaveSpawner.OnUpdateEnemyNumEvent -= UpdateEnemyCount;
            enemyWaveSpawner.OnUpdateSpawnableStateEvent -= UpdateStateUI;
        }

        private void UpdateEnemyCount(int enemyCount)
        {
            enemyCountText.text = $"Enemy Count: {enemyCount}";
        }


        private void UpdateStateUI(bool isSpawnable, bool isFirstWave)
        {
            /*if (isSpawnable == true)
            {
                pressSpaceText.gameObject.SetActive(true);

                if (isFirstWave == false)
                    waveCompleteText.gameObject.SetActive(true);
            }
            else
            {
                pressSpaceText.gameObject.SetActive(false); 
                waveCompleteText.gameObject.SetActive(false);
            }*/

            // AI의 도움으로 수정한 부분
            pressSpaceText.gameObject.SetActive(isSpawnable);
            waveCompleteText.gameObject.SetActive(isSpawnable && !isFirstWave);

        }

    }
}
