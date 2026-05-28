using UnityEngine;

/// <summary>
/// 他クラスで使用するRigidbodyを保持するクラス
/// </summary>
public class MedalController : MonoBehaviour
{
    // 自分自身についているRigidbodyを取得して保持するプロパティ
    public Rigidbody MedalRigidbody { get; private set; }

    // メダルプール管理クラス参照用
    private MedalPoolManager medalPoolManager = null;

    /// <summary>
    /// Rigidbodyを取得してプロパティに保持する処理
    /// </summary>
    private void Awake()
    {
        MedalRigidbody = GetComponent<Rigidbody>();
        medalPoolManager = FindAnyObjectByType<MedalPoolManager>();
    }

    /// <summary>
    /// 特定の位置にメダルが落下したときにメダルを返却する処理
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // メダルが地面に衝突したとき、メダルを返却する
        if (collision.gameObject.CompareTag("CollectionPosition"))
        {
            ResetRigidBody();
            medalPoolManager.ReturnMedal(gameObject);
            Debug.Log("メダルが地面に衝突したため、メダルを返却しました");
        }
    }


    /// <summary>
    /// 回転が残らないようにrigidbodyをリセットする処理    
    /// </summary>
    private void ResetRigidBody()
    {
        // Rigidbodyの速度をリセットする
        MedalRigidbody.linearVelocity = Vector3.zero;
        MedalRigidbody.angularVelocity = Vector3.zero;
    }

}
