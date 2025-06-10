using System;
using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;
    private void Update()
    {
        Refresh();
        
        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);
        
        GoldCountText.text = $"Gold: {gold.Value}";
        DiamondCountText.text = $"Diamond: {diamond.Value}";
        
        BuyHealthText.color = gold.HaveEnough(300) ? Color.green : Color.red;
    }

    public void BuyHealth()
    {
        Debug.Log("Buying Health");

        // 구글 검색 : "묻지 말고 시켜라"
        // 현재 문제
        // 1. 도메인의 규칙이 UI에?
        // 2. 규칙이 바뀌면 UI까지 와서 수정해야 한다.
        // 3. '사다'라는 행위는 '커렌시 도메인'의 중요한 역할
        // 4. '사다'라는 행위는 상점 / 
        if (CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300))
        {
            var player = FindFirstObjectByType<PlayerCharacterController>();
            Health health = player.GetComponent<Health>();
            health.Heal(100);
        }
    }
}
