using UnityEngine;

/// <summary>
/// プッシャーを特定位置へ往復移動させる処理
/// </summary>
public class PusherController : MonoBehaviour
{
    // プッシャーの速度
    private const float FORWARD_SPEED = 3.0f;
    private const float BACK_SPEED = 5.0f;

    // 到着判定差
    private const float ARRIVAL_JUDGMENT_DIFFERENCE = 0.01f;



    [Header("プッシャー本体")]
    [SerializeField] private Transform upperSection = null;

    [Header("プッシャーRigidbody")]
    [SerializeField] private Rigidbody upperSectionRigidbody = null;

    [Header("プッシャー開始位置")]
    [SerializeField] private Transform startPoint = null;

    [Header("プッシャー終了位置")]
    [SerializeField] private Transform endPoint = null;



    // 現在向かっている目的地
    private Transform currentTarget = null;

    //　現在の速度
    private float currentSpeed = 0.0f;


    /// <summary>
    /// プッシャーを移動させるための目的地、速度を入れる
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
        PusherMovement(currentTarget);

        // 現在位置と目的地の距離を測定
        // Distance(位置A, 位置B)
        if (Vector3.Distance(
            upperSection.position,
            currentTarget.position) < ARRIVAL_JUDGMENT_DIFFERENCE)
        {
            // 行き先切り替え
            currentTarget = currentTarget == endPoint ? startPoint : endPoint;

            // 速度切り替え
            currentSpeed = currentTarget == endPoint ? FORWARD_SPEED : BACK_SPEED;
        }
    }


    /// <summary>
    /// 特定のポジションにプッシャーを移動させる処理
    /// </summary>
    private void PusherMovement(Transform movePoint)
    {
        // 一定速度で移動させるためMoveTowardsを使用
        // MoveTowards(現在位置, 目的地, 移動量);
        Vector3 nextPosition =
            Vector3.MoveTowards(
                upperSectionRigidbody.position,
                movePoint.position,
                currentSpeed * Time.fixedDeltaTime
                );

        // Rigidbody側の位置を変更する
        upperSectionRigidbody.MovePosition(nextPosition);
    }
}
