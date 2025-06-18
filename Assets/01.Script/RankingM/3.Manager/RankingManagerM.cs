using System;
using UnityEngine;
using System.Collections.Generic;



public class RankingManagerM : MonoBehaviourSingleton<RankingManagerM>
{
    private RankingRepositoryM _repository;
    private List<RankingM> _rankings;
    private RankingM _myRanking;
    
    public event Action OnDataChanged;
    protected override void Awake()
    {
        base.Awake();
        
        Init();
    }

    private void Init()
    {
        _repository = new RankingRepositoryM();
        
        List<RankingSaveDataM> saveDataList = _repository.Load();
        
        _rankings = new List<RankingM>();
        foreach (RankingSaveDataM saveData in saveDataList)
        {
            RankingM ranking = new RankingM(saveData.Email, saveData.NickName, saveData.Score);
            _rankings.Add(ranking);

            if (ranking.Email == AccountManager.Instance.CurrentAccount.Email)
            {
                _myRanking = ranking;
            }
        }

        if (_myRanking == null)
        {
            AccountDTO me = AccountManager.Instance.CurrentAccount;
            _myRanking = new RankingM(me.Email, me.Nickname, 0);
            
            _rankings.Add(_myRanking);
        }
        
        Sort();
        
        OnDataChanged?.Invoke();
    }

    private void Sort()
    {
        _rankings.Sort((x, y) => x.Score.CompareTo(y.Score));

        for (int i = 0; i < _rankings.Count; i++)
        {
            _rankings[i].SetRank(i + 1);
        }
    }
}