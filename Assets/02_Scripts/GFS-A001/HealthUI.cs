using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private Player player; 
    [SerializeField] private TextMeshProUGUI hpText;

    // Start로 두는게 안전함
    private void Start()
    {
        hpText.text = $"Health: {player.currentHP}";
    }


    private void OnEnable()
    {
        player.HealthChangedEvent += UpdateHealthText;
    }


    private void OnDisable()
    {
        player.HealthChangedEvent -= UpdateHealthText;
    }


    private void UpdateHealthText(int currentHP)
    {

        hpText.text = $"Health: {currentHP}";
    }

}
