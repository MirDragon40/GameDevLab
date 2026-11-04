using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] private int healAmount;



    public void Interact(Player target)
    {
        // 1. target의 HP 회복 메서드 호출
        // 2. bool 반환값 저장

        bool isSucceedRecovering = target.RecoverHP(healAmount);

        // 3. 회복 성공 여부 확인

        // 4. 성공했다면 아이템 제거 메서드 호출

        if (isSucceedRecovering)
        {
            Debug.Log("회복 아이템 제거");

            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
