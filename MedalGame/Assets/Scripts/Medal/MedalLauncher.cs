using UnityEngine;

/// <summary>
/// メダルを発射するクラス
/// </summary>
public class MedalLauncher : MonoBehaviour
{
    // メダルの発射速度
    private const float LAUNCH_SPEED = 0.5f;



    [Header("メダルプール管理クラス")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    [Header("メダル枚数管理クラス")]
    [SerializeField] private MedalManager medalManager = null;

    [Header("メダルを発射する位置")]
    [SerializeField] private Transform launcherPoint = null;

    /// <summary>
    /// メダルを一枚生成する処理
    /// </summary>
    public void LaunchMedal(Vector3 dropPoint)
    {
        // メダルの枚数が0以下の場合は発射しない
        if (medalManager.CurrentMedalCount <= 0) return;

        // メダルを取り出す
        GameObject rentedMedal = medalPoolManager.GetMedal();

        // メダルの発射位置をメダルと一緒にする
        rentedMedal.transform.position = launcherPoint.position;

        // メダルのRigidbodyを取得
        Rigidbody medalRigidbody = rentedMedal.GetComponent<Rigidbody>();


        // メダルを落下地点に向けて発射する処理
        // 発射方向と距離を計算
        Vector3 direction = dropPoint - launcherPoint.position;
        medalRigidbody.linearVelocity = direction * LAUNCH_SPEED;

        // メダルの枚数を減算する
        medalManager.RemoveMedal();
    }
}
