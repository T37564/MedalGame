using UnityEngine;

/// <summary>
/// 他クラスで使用するRigidbodyを保持するクラス
/// </summary>
public class MedalController : MonoBehaviour
{
    // 自分自身についているRigidbodyを取得して保持するプロパティ
    public Rigidbody MedalRigidbody { get; private set; }

    /// <summary>
    /// Rigidbodyを取得してプロパティに保持する処理
    /// </summary>
    private void Awake()
    {
        MedalRigidbody = GetComponent<Rigidbody>();
    }
}
