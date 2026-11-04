using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    // 다른 곳에서 쉽게 수정할 수 없도록 캡슐화 필수
    public int currentHP { get; private set; }
    [SerializeField] private int maxHP;
    public event Action<int> HealthChangedEvent;


    // 1. 접촉 감지 메서드 
    // 2. Item에게 상호작용 요청 메서드
    // 3. 회복 요청 메서드


    public void OnTriggerEnter(Collider other)
    {

        Debug.Log($"접촉 감지: {other.name}");

        HealItem item = other.GetComponent<HealItem>();

        if (item != null)
        {
            item.Interact(this);

            Debug.Log("HealItem 객체와의 상호작용을 시작합니다.");
        }
    }



    public bool RecoverHP(int healAmount)
    {

        //회복 가능한지 확인한다.
        if (currentHP >= maxHP)
        {
            Debug.Log("회복 불가능상태");

            //불가능하면 false를 반환한다.
            return false;
        }
        else    //가능하면 HP를 계산하고 변경한다.
        {
            Debug.Log("회복 가능상태");


            if (maxHP - currentHP < healAmount)
            {
                currentHP = maxHP;
                HealthChangedEvent?.Invoke(currentHP);

            }
            else
            {
                currentHP += healAmount;
                HealthChangedEvent?.Invoke(currentHP);
            }

            Debug.Log("회복 완료!");

            //true를 반환한다.
            return true;

        }



    }



}
