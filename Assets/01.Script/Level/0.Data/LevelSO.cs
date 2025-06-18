using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [Header("���� ���� ��ġ")]
    [SerializeField] private float monsterAttackIncrease;// ���� ���ݷ� ������
    [SerializeField] private float monsterHealthIncrease; // ���� ü�� ������

    [Header("���� ����")]
    [SerializeField] private float spawnCycleDecrease; // ���� ���� �ֱ� ����ġ(�� ����)
    [SerializeField] private int maxSpawnCount; // ���� ���� �ִ�ġ

    [Header("���� ����")]
    [SerializeField] private float levelDuration; // ���� ���� �ð�(�� ����)
    [SerializeField, Range(0f, 1f)] private float eliteProbability; // ����Ʈ Ȯ��(0~1)

    // �ܺο��� ���� �� �ֵ��� ������Ƽ �߰� (�ʿ��)
    public float MonsterAttackIncrease => monsterAttackIncrease;
    public float MonsterHealthIncrease => monsterHealthIncrease;
    public float SpawnCycleDecrease => spawnCycleDecrease;
    public int MaxSpawnCount => maxSpawnCount;
    public float LevelDuration => levelDuration;
    public float EliteProbability => eliteProbability;
}
