using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RankingSlotM : MonoBehaviour
{
    public TextMeshProUGUI RankTextUI;
    public TextMeshProUGUI NickNameTextUI;
    public TextMeshProUGUI ScoreTextUI;

    public void Refresh(RankingDTOM ranking)
    {
        RankTextUI.text = ranking.Rank.ToString("N0");
        NickNameTextUI.text = ranking.Nickname;
        ScoreTextUI.text = ranking.Score.ToString("N0");
    }
}