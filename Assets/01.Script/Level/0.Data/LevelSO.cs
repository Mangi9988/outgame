using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [Header("몬스터 증가 수치")]
    [SerializeField] private float monsterAttackIncrease;// 몬스터 공격력 증가값
    [SerializeField] private float monsterHealthIncrease; // 몬스터 체력 증가값

    [Header("스폰 관련")]
    [SerializeField] private float spawnCycleDecrease; // 몬스터 스폰 주기 감소치(초 단위)
    [SerializeField] private int maxSpawnCount; // 몬스터 스폰 최대치

    [Header("레벨 설정")]
    [SerializeField] private float levelDuration; // 레벨 유지 시간(초 단위)
    [SerializeField, Range(0f, 1f)] private float eliteProbability; // 엘리트 확률(0~1)

    // 외부에서 읽을 수 있도록 프로퍼티 추가 (필요시)
    public float MonsterAttackIncrease => monsterAttackIncrease;
    public float MonsterHealthIncrease => monsterHealthIncrease;
    public float SpawnCycleDecrease => spawnCycleDecrease;
    public int MaxSpawnCount => maxSpawnCount;
    public float LevelDuration => levelDuration;
    public float EliteProbability => eliteProbability;
}
