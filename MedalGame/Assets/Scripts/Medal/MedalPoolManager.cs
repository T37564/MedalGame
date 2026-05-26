using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// メダルをオブジェクトプールで管理する処理
/// </summary>
public class MedalPoolManager : MonoBehaviour
{
    // メダルの初期数
    private const int DEFAULT_POOL_SIZE = 100;

    // メダルの最大数
    private const int MAX_POOL_SIZE = 200;

    [Header("複製するメダル")]
    [SerializeField] private GameObject medalPrefab = null;

    // Unity公式オブジェクトプール
    private ObjectPool<GameObject> medalPool = null;

    /// <summary>
    /// オブジェクトプールの初期化
    /// </summary>
    private void Awake()
    {
        medalPool = new ObjectPool<GameObject>(
           CreateMedal,     // 新しく作る
           OnTakeMedal,     // 使用開始
           OnReturnMedal,   // 戻す
           OnDestroyMedal,  // 削除
           true,            // 同じオブジェクトへの重複返却を防ぐ
           100,             // 初期数
           200              // 最大数
         );

        // 事前生成
        for (int i = 0; i < 100; i++)
        {
            GameObject medal = medalPool.Get();
            medalPool.Release(medal);
        }
    }


    /// <summary>
    /// メダルを複製する処理
    /// </summary>
    private GameObject CreateMedal()
    {
        return Instantiate(medalPrefab);
    }

    /// <summary>
    /// メダルを取り出し表示する処理
    /// </summary>
    private void OnTakeMedal(GameObject medal)
    {
        medal.SetActive(true);
    }


    /// <summary>
    /// メダルをプールに戻して非表示にする処理
    /// </summary>
    private void OnReturnMedal(GameObject medal)
    {
        medal.SetActive(false);
    }

    /// <summary>
    /// メダルを削除する処理
    /// </summary>
    private void OnDestroyMedal(GameObject medal)
    {
        Destroy(medal);
    }


    /// <summary>
    /// 外部からメダルを借りれるようにする処理
    /// </summary>
    public GameObject GetMedal()
    {
        return medalPool.Get();
    }
}
