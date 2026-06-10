using UnityEngine;

/// <summary>
/// プッシャーを指定した位置間で往復移動させるクラス
/// </summary>
public class PusherController : MonoBehaviour
{
    // プッシャーの速度
    private readonly float FORWARD_SPEED = 2.0f;
    private readonly float BACK_SPEED = 3.0f;

    // 到着とみなす距離
    private readonly float ARRIVAL_THRESHOLD = 0.01f;


    [Header("プッシャー本体")]
    [SerializeField] private Transform upperSectionTransform = null;

    [Header("プッシャーRigidbody")]
    [SerializeField] private Rigidbody upperSectionRigidbody = null;

    [Header("プッシャー開始位置")]
    [SerializeField] private Transform startPoint = null;

    [Header("プッシャー終了位置")]
    [SerializeField] private Transform endPoint = null;


    // 現在向かっている目的地
    private Transform currentTarget = null;

    // 現在の速度
    private float currentSpeed = 0.0f;


    /// <summary>
    /// プッシャーの移動先と速度を初期化する
    /// </summary>
    private void Start()
    {
        currentTarget = endPoint;
        currentSpeed = FORWARD_SPEED;
    }


    /// <summary>
    /// プッシャーを往復移動させる
    /// </summary>
    private void FixedUpdate()
    {
        MovePusher(currentTarget);

        // 現在位置と目的地の距離を測定
        // Distance(位置A, 位置B)
        if (Vector3.Distance(upperSectionTransform.position, currentTarget.position) < ARRIVAL_THRESHOLD)
        {
            // 行き先切り替え
            currentTarget = (currentTarget == endPoint) ? startPoint : endPoint;

            // 速度切り替え
            currentSpeed = (currentTarget == endPoint) ? FORWARD_SPEED : BACK_SPEED;
        }
    }


    /// <summary>
    /// 指定した目標地点へプッシャーを移動させる
    /// </summary>
    private void MovePusher(Transform targetPoint)
    {
        // 一定速度で移動させるためMoveTowardsを使用
        // MoveTowards(現在位置, 目的地, 移動量);
        Vector3 nextPosition = Vector3.MoveTowards(upperSectionRigidbody.position, targetPoint.position, currentSpeed * Time.fixedDeltaTime);

        // Rigidbody側の位置を変更する
        upperSectionRigidbody.MovePosition(nextPosition);
    }
}
