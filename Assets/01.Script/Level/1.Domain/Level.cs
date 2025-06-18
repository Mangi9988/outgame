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
    // �� �������� (1 �̻�, �ִ� ���� ����)
    public int CurrentLevel { get; private set; }

    // �ִ� ���� (1 �̻�, ReadOnly)
    public int MaxLevel { get; }

    // ������ ���ݷ� ����ġ (0 �̻�, 10%�� 0.1�� ǥ��)
    public float MonsterAttackIncrease { get; private set; }

    // ������ ü�� ����ġ (0 �̻�, 10%�� 0.1�� ǥ��)
    public float MonsterHealthIncrease { get; private set; }

    // ������ ���� �ֱ� ����ġ (0~1, 0%~100%)
    public float SpawnIntervalDecrease { get; private set; }

    // ������ ���� �ִ�ġ (1 �̻�)
    public int MaxSpawnCount { get; private set; }

    // ���� ���� �ð� (1�� �̻�)
    public float LevelDuration { get; private set; }

    // ����Ʈ Ȯ�� (enum�� float, 0~1)
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
        MaxLevel = 100; // ���÷� �ִ� ������ 100���� ����
        MonsterAttackIncrease = levelSO.MonsterAttackIncrease;
        MonsterHealthIncrease = levelSO.MonsterHealthIncrease;
        SpawnIntervalDecrease = levelSO.SpawnCycleDecrease;
        MaxSpawnCount = levelSO.MaxSpawnCount;
        LevelDuration = levelSO.LevelDuration;
        SpawnRate = new Dictionary<MonsterType, float>
        {
            { MonsterType.Normal, 1f - levelSO.EliteProbability }, // �Ϲ� ���� Ȯ��
            { MonsterType.Elite, levelSO.EliteProbability }, // ����Ʈ ���� Ȯ��
            { MonsterType.Boss, 0f } // ���� ���� Ȯ���� �ʱ⿡�� 0
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

            // ����Ʈ Ȯ��(SpawnRate) ����
            if (SpawnRate.ContainsKey(MonsterType.Elite))
                SpawnRate[MonsterType.Elite] = _levelSO.EliteProbability;
        }
        else
        {
            throw new InvalidOperationException("Already at maximum level.");
        }
    }
}
