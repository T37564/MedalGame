using UnityEngine;

/// <summary>
/// メダルを発射するクラス
/// </summary>
public class MedalLauncher : MonoBehaviour
{
    // メダルの発射速度
    private const float LAUNCH_SPEED = 20.0f;



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

        // メダルのRigidbodyを取得
        Rigidbody medalRigidbody = rentedMedal.GetComponent<Rigidbody>();

        // 回転が残らないようにrigidbodyをリセットする
        rentedMedal.transform.rotation = Quaternion.identity;

        // 発射前に速度をリセットする
        medalRigidbody.linearVelocity = Vector3.zero;
        medalRigidbody.angularVelocity = Vector3.zero;

        // Rigidbody付きオブジェクトはPhysicsシミュレーションによって位置管理されるため、
        // Transform.positionではなくRigidbody.positionを使用して移動させる
        medalRigidbody.position = launcherPoint.position;

        // 発射方向
        Vector3 target = dropPoint;

        // 発射方向ベクトルを計算
        Vector3 direction = (target - medalRigidbody.position).normalized * LAUNCH_SPEED;


        // 発射方向に力を加える
        medalRigidbody.linearVelocity = direction;

        // メダルの枚数を減算する
        medalManager.RemoveMedal();
    }
}
