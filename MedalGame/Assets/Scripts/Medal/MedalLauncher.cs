using System.Drawing;
using UnityEngine;

public class MedalLauncher : MonoBehaviour
{
    [Header("メダルプール管理クラス")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    /// <summary>
    /// メダルを一枚生成する処理
    /// </summary>
    public void LaunchMedal(Transform dropPoint)
    {
        // メダルを取り出す
        GameObject rentedMedal = medalPoolManager.GetMedal();

        // メダルのRigidbodyを取得
        Rigidbody medalRigidbody = rentedMedal.GetComponent<Rigidbody>();

        // メダルを落下地点に向けて発射する処理
        // 落下地点までの距離を計算
        Vector3 direction = (dropPoint.position - transform.position).normalized;

        // 上方向を加える
        Vector3 launchDirection = direction + Vector3.up * 0.5f;

        launchDirection.Normalize();

        float launchForce = 50.0f;

        medalRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }
}
