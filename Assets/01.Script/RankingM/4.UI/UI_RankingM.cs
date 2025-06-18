using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_RankingM : MonoBehaviour
{
    public List<UI_RankingSlotM> RankingSlots;
    public UI_RankingSlotM MyRankingSlot;

    private void Start()
    {
        Refresh();
        
        RankingManagerM.Instance.OnDataChanged += Refresh;
    }

    public void Refresh()
    {
        var rankings = RankingManagerM.Instance.Rankings;
        
        int index = 0;
        foreach (var ui_ranking in RankingSlots)
        {
            ui_ranking.Refresh(rankings[index]);
            index++;
        }

        MyRankingSlot.Refresh(RankingManagerM.Instance.MyRanking);
    }
}