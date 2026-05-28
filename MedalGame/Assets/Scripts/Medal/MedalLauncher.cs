using UnityEngine;

public class MedalLauncher : MonoBehaviour
{
    [Header("メダルプール管理クラス")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    [Header("メダルを発射する位置")]
    [SerializeField] private Transform launcherPoint = null;

    /// <summary>
    /// メダルを一枚生成する処理
    /// </summary>
    public void LaunchMedal(Vector3 dropPoint)
    {
        // メダルを取り出す
        GameObject rentedMedal = medalPoolManager.GetMedal();

        // メダルの発射位置をメダルと一緒にする
        rentedMedal.transform.position = launcherPoint.position;

        // メダルのRigidbodyを取得
        Rigidbody medalRigidbody = rentedMedal.GetComponent<Rigidbody>();


        // メダルを落下地点に向けて発射する処理
        // 落下地点までの距離を計算
        Vector3 direction = dropPoint - launcherPoint.position;

        medalRigidbody.linearVelocity = direction * 0.5f;

        medalRigidbody.AddForce(direction * Time.deltaTime, ForceMode.Impulse);
    }
}
