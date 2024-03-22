using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerMediator : MonoBehaviour
{
    [SerializeField]
    private CharacterController charCtrl;

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private float speed;

    private Vector3 lookDir;
    private Vector3 attackDir;
    private Vector3 dodgeDir;

    private bool isDodge;
    private bool isAttack;

    void FixedUpdate()
    {
        Move();
        //Attack();
    }

    private void Move()
    {
        LookAt();
        charCtrl.Move(lookDir * speed * Time.deltaTime);
    }

    // 바라볼 방향을 입력 받고 회전
    private void LookAt()
    {
        lookDir = input.GetMoveDirection();
        lookDir.Normalize();

        if (isDodge)
            lookDir = dodgeDir;

        if (isAttack && !isDodge)
        {
            Quaternion dirQuat = Quaternion.LookRotation(attackDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
        }

        if (!isAttack)
        {
            if (lookDir == Vector3.zero)
                return;

            Quaternion dirQuat = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, dirQuat, 0.3f);
        }
    }

    private void Attack()
    {
        isAttack = input.Attack();
        attackDir = input.GetAttackDirection();
        attackDir.Normalize();
    }
}
