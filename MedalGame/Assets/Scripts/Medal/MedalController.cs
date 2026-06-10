using UnityEngine;

/// <summary>
/// メダルの管理や回収処理を行うクラス
/// </summary>
public class MedalController : MonoBehaviour
{
    // 衝突したときに反応してほしいタグ名
    private readonly string COLLECTION_POSITION_TAG = "CollectionPosition";

    // メダルプール管理クラス参照用
    private MedalPoolManager medalPoolManager = null;

    /// <summary>
    /// 自分自身についているRigidbodyを取得して保持するプロパティ
    /// </summary>
    public Rigidbody MedalRigidbody { get; private set; }

    /// <summary>
    /// 必要なコンポーネントや参照を取得する
    /// </summary>
    private void Awake()
    {
        MedalRigidbody = GetComponent<Rigidbody>();
        medalPoolManager = FindAnyObjectByType<MedalPoolManager>();
    }

    /// <summary>
    /// 回収エリアに衝突したらメダルを返却する
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // 回収エリアに衝突したらメダルを返却する
        if (collision.gameObject.CompareTag(COLLECTION_POSITION_TAG))
        {
            ReturnMedal();
        }
    }

    /// <summary>
    /// メダルをオブジェクトプールへ返却する
    /// </summary>
    public void ReturnMedal()
    {
        medalPoolManager.ReturnMedal(gameObject);
    }
}
