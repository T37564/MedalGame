using UnityEngine;

/// <summary>
/// メダルを発射するクラス
/// </summary>
public class MedalLauncher : MonoBehaviour
{
    // メダルの発射速度
    private readonly float LAUNCH_SPEED = 20.0f;

    [Header("メダルプール管理クラス")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    [Header("メダルを発射する位置")]
    [SerializeField] private Transform launcherPoint = null;

    /// <summary>
    /// メダルを取り出して発射する
    /// </summary>
    public void LaunchMedal(Vector3 dropPoint)
    {
        // メダルを取り出す
        GameObject rentedMedal = medalPoolManager.GetMedal();

        // メダルの制御クラスを取得
        MedalController medalController = rentedMedal.GetComponent<MedalController>();
        Rigidbody medalRigidbody = medalController.MedalRigidbody;

        // 前回使用時の回転をリセットする
        rentedMedal.transform.rotation = Quaternion.identity;

        // 発射前に速度をリセットする
        medalRigidbody.linearVelocity = Vector3.zero;
        medalRigidbody.angularVelocity = Vector3.zero;

        // Rigidbody付きオブジェクトはPhysicsシミュレーションによって位置管理されるため、
        // Transform.positionではなくRigidbody.positionを使用して移動させる
        medalRigidbody.position = launcherPoint.position;

        // 発射方向ベクトルを計算
        Vector3 direction = (dropPoint - medalRigidbody.position).normalized * LAUNCH_SPEED;


        // 発射方向の速度を設定する
        medalRigidbody.linearVelocity = direction;
    }
}
