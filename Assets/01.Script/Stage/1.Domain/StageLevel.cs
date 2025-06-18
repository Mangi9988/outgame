using System;
using Unity.Tutorials.Core.Editor;
using UnityEngine.ProBuilder.Shapes;

public class StageLevel
{
    // 기획 데이터
    public readonly string Name;
    public readonly Sprite Icon;                // 아이콘
    public readonly int StartLevel;             // 시작 레벨
    public readonly int EndLevel;               // 종료 레벨
    public float HealthFactor;                  // 체력 배율
    public float DamageFactor;                  // 데미지 배율
    public float Duration => 60f;               // 레벨 지속 시간
    public readonly float SpawnInterval;        // 스폰 주기
    public readonly float SpawnRate;            // 스폰 확률
    
    // 상태 데이터
    public int CurrentLevel;    // StartLevel ~ EndLevel

    public StageLevel(StageLevelSO so, int currentLevel) : this(so.Name, so.StartLevel, so.EndLevel, so.HealthFactor,
        so.DamageFactor, so.SpawnInterval, so.SpawnRate, currentLevel)
    {
        
    }
    
    public StageLevel(string name, int startLevel, int endLevel, float healthFactor, float damageFactor,
        float spawnInterval, float spawnRate, int currentLevel)
    {
        // if (icon == null)
        // {
        //     throw new ArgumentNullException("아이콘이 없습니다.");
        // }

        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("올바르지 않은 name입니다.");
        }
        
        if (startLevel < 0 || endLevel <= startLevel)
        {
            throw new Exception("시작 레벨이 올바르지 않습니다.");
        }
        
        if (endLevel < 0 || startLevel <= endLevel)
        {
            throw new Exception("종료 레벨이 올바르지 않습니다.");
        }

        if (healthFactor < 1)
        {
            throw new Exception("체력 배율이 올바르지 않습니다.");
        }

        if (damageFactor < 1)
        {
            throw new Exception("공격력 배율이 올바르지 않습니다.");
        }

        if (spawnInterval <= 0)
        {
            throw new Exception("스폰 주기가 올바르지 않습니다.");
        }

        if (spawnRate <= 0 || 100 < spawnRate)
        {
            throw new Exception("스폰 확률이 올바르지 않습니다");
        }

        if (currentLevel < startLevel || endLevel < currentLevel)
        {
            throw new Exception("현재 레벨이 올바르지 않습니다");
        }
        
        StartLevel = startLevel;
        EndLevel = endLevel;
        HealthFactor = healthFactor;
        DamageFactor = damageFactor;
        SpawnInterval = spawnInterval;
        SpawnRate = spawnRate;
        CurrentLevel = currentLevel;
    }

    public bool TryLevelUp(float progressTime)
    {
        if (progressTime >= Duration)
        {
            CurrentLevel += 1;
            return true;
        }
        
        return false;
    }

    public bool IsClear()
    {
        return CurrentLevel == EndLevel;
    }
}
