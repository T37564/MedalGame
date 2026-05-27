using UnityEngine;

/// <summary>
/// 端によったコインを寄せるギミック
/// </summary>
public class GuardRotator : MonoBehaviour
{
    // ガードの回転速度
    private const float ROTATION_SPEED = 10.0f;

    [Header("ガードの回転角度")]
    [SerializeField] private Vector3 guardRotationAngle = Vector3.zero;

    /// <summary>
    /// ガードを指定した角度まで回転させる
    /// </summary>
    private void Update()
    {
        // 現在の回転角度と目標角度の差を確認する
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(guardRotationAngle)) < 0.5f) return;


        // 現在のガードの回転角度を取得
        Vector3 currentRotation = transform.eulerAngles;

        // 次の回転角度を計算する
        Vector3 nextRotation =
            Vector3.MoveTowards(
                currentRotation,
                guardRotationAngle,
                ROTATION_SPEED * Time.deltaTime
            );

        // ガードの回転を更新
        transform.rotation = Quaternion.Euler(nextRotation);
    }
}
