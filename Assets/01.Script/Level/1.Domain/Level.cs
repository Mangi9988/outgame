using System;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Normal,
    Elite,
    Boss
}

public class Level
{
    // 몇 레벨인지 (1 이상, 최대 레벨 이하)
    public int CurrentLevel { get; private set; }

    // 최대 레벨 (1 이상, ReadOnly)
    public int MaxLevel { get; }

    // 몬스터의 공격력 증가치 (0 이상, 10%는 0.1로 표현)
    public float MonsterAttackIncrease { get; private set; }

    // 몬스터의 체력 증가치 (0 이상, 10%는 0.1로 표현)
    public float MonsterHealthIncrease { get; private set; }

    // 몬스터의 스폰 주기 감소치 (0~1, 0%~100%)
    public float SpawnIntervalDecrease { get; private set; }

    // 몬스터의 스폰 최대치 (1 이상)
    public int MaxSpawnCount { get; private set; }

    // 레벨 유지 시간 (1초 이상)
    public float LevelDuration { get; private set; }

    // 엘리트 확률 (enum별 float, 0~1)
    public Dictionary<MonsterType, float> SpawnRate { get; private set; }

    private readonly LevelSO _levelSO;

    public Level(
        int currentLevel,
        int maxLevel,
        float monsterAttackIncrease,
        float monsterHealthIncrease,
        float spawnIntervalDecrease,
        int maxSpawnCount,
        float levelDuration,
        Dictionary<MonsterType, float> spawnRate,
        LevelSO levelSO)
    {
        if (maxLevel < 1) 
            throw new ArgumentOutOfRangeException(nameof(maxLevel));
        if (currentLevel < 1 || currentLevel > maxLevel) 
            throw new ArgumentOutOfRangeException(nameof(currentLevel));
        if (monsterAttackIncrease < 0f) 
            throw new ArgumentOutOfRangeException(nameof(monsterAttackIncrease));
        if (monsterHealthIncrease < 0f) 
            throw new ArgumentOutOfRangeException(nameof(monsterHealthIncrease));
        if (spawnIntervalDecrease < 0f || spawnIntervalDecrease > 1f) 
            throw new ArgumentOutOfRangeException(nameof(spawnIntervalDecrease));
        if (maxSpawnCount < 1) 
            throw new ArgumentOutOfRangeException(nameof(maxSpawnCount));
        if (levelDuration < 1f) 
            throw new ArgumentOutOfRangeException(nameof(levelDuration));
        if (spawnRate == null) 
            throw new ArgumentNullException(nameof(spawnRate));
        if (levelSO == null)
            throw new ArgumentNullException(nameof(levelSO));

        CurrentLevel = currentLevel;
        MaxLevel = maxLevel;
        MonsterAttackIncrease = monsterAttackIncrease;
        MonsterHealthIncrease = monsterHealthIncrease;
        SpawnIntervalDecrease = spawnIntervalDecrease;
        MaxSpawnCount = maxSpawnCount;
        LevelDuration = levelDuration;
        SpawnRate = new Dictionary<MonsterType, float>(spawnRate);
        _levelSO = levelSO;
    }

    public Level(LevelSO levelSO)
    {
        CurrentLevel = 1;
        MaxLevel = 100; // 예시로 최대 레벨을 100으로 설정
        MonsterAttackIncrease = levelSO.MonsterAttackIncrease;
        MonsterHealthIncrease = levelSO.MonsterHealthIncrease;
        SpawnIntervalDecrease = levelSO.SpawnCycleDecrease;
        MaxSpawnCount = levelSO.MaxSpawnCount;
        LevelDuration = levelSO.LevelDuration;
        SpawnRate = new Dictionary<MonsterType, float>
        {
            { MonsterType.Normal, 1f - levelSO.EliteProbability }, // 일반 몬스터 확률
            { MonsterType.Elite, levelSO.EliteProbability }, // 엘리트 몬스터 확률
            { MonsterType.Boss, 0f } // 보스 몬스터 확률은 초기에는 0
        };
        _levelSO = levelSO ?? throw new ArgumentNullException(nameof(levelSO), "LevelSO cannot be null");

    }


    public void IncreaseLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
            MonsterAttackIncrease += _levelSO.MonsterAttackIncrease;
            MonsterHealthIncrease += _levelSO.MonsterHealthIncrease;
            SpawnIntervalDecrease += _levelSO.SpawnCycleDecrease;
            MaxSpawnCount += _levelSO.MaxSpawnCount;
            LevelDuration += _levelSO.LevelDuration;

            // 엘리트 확률(SpawnRate) 갱신
            if (SpawnRate.ContainsKey(MonsterType.Elite))
                SpawnRate[MonsterType.Elite] = _levelSO.EliteProbability;
        }
        else
        {
            throw new InvalidOperationException("Already at maximum level.");
        }
    }
}
