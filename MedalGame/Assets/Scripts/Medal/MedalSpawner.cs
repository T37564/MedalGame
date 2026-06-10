using UnityEngine;

/// <summary>
/// オブジェクトプールからメダルを取り出し初期配置するクラス
/// </summary>
public class MedalSpawner : MonoBehaviour
{
    // 下段初期メダル枚数
    private readonly int INITIAL_MEDAL_COUNT_LOWER = 70;
    // 上段初期メダル枚数
    private readonly int INITIAL_MEDAL_COUNT_UPPER = 20;

    // メダルの生成位置をランダムにするための範囲
    // 下段のランダム範囲
    private readonly float RANDOM_RANGE_LOWER_X = 35.0f;
    private readonly float RANDOM_RANGE_LOWER_Z = 12.0f;
    // 上段のランダム範囲
    private readonly float RANDOM_RANGE_UPPER_X = 10.0f;
    private readonly float RANDOM_RANGE_UPPER_Z = 15.0f;



    [Header("メダルプール管理クラス")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    [Header("メダルを最初に配置するときに使用する位置")]
    [SerializeField] private Transform medalSpawnerPositionLower = null;
    [SerializeField] private Transform medalSpawnerPositionUpper = null;

    [Header("両端にあるガードオブジェクト")]
    [SerializeField] private GameObject[] sideGuard = null;

    /// <summary>
    /// ゲーム開始時にメダルを初期配置する
    /// </summary>
    private void Start()
    {
        // 下段と上段で分けて生成する
        SpawnMedal(INITIAL_MEDAL_COUNT_LOWER, medalSpawnerPositionLower, RANDOM_RANGE_LOWER_X, RANDOM_RANGE_LOWER_Z);
        SpawnMedal(INITIAL_MEDAL_COUNT_UPPER, medalSpawnerPositionUpper, RANDOM_RANGE_UPPER_X, RANDOM_RANGE_UPPER_Z);

        // 両端のガードオブジェクトを有効にする
        foreach (GameObject guard in sideGuard)
        {
            guard.SetActive(true);
        }
    }

    /// <summary>
    /// メダルをオブジェクトプールから取り出し、指定した位置にランダムに配置する処理
    /// </summary>
    private void SpawnMedal(int spawnCount, Transform medalSpawnerPosition, float randomRangeX, float randomRangeZ)
    {
        // 指定枚数分のメダルを配置する
        for (int i = 0; i < spawnCount; i++)
        {
            // メダルを取り出す
            GameObject rentedMedal = medalPoolManager.GetMedal();

            // メダルの制御クラスを取得する
            MedalController medalController = rentedMedal.GetComponent<MedalController>();

            // ランダムな生成位置を決定する
            Vector3 generationPosition = medalSpawnerPosition.position + new Vector3(Random.Range(-randomRangeX, randomRangeX),
                                                                                     0.0f,
                                                                                     Random.Range(-randomRangeZ, randomRangeZ)
                                                                                     );

            // Rigidbody側の位置を変更する
            // Rigidbody.positionを使用して、物理演算による位置補正の不整合を防ぐ
            medalController.MedalRigidbody.position = generationPosition;
        }
    }
}
