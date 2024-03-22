using UnityEngine;

// 플레이어 Input을 담당
public class PlayerInput : MonoBehaviour
{
    // 조이스틱 에셋 https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631
    //private VariableJoystick moveJoystick;
    //private AttackJoystick attackJoystick;

    public Vector3 GetMoveDirection()
    {
#if UNITY_EDITOR
        Debug.Log(Input.GetAxis("Horizontal"));
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
#else
        return new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);
#endif
    }

    public Vector3 GetAttackDirection()
    {
        // 공격 방향
        // return new Vector3(attackJoystick.Horizontal, 0, attackJoystick.Vertical);
        return Vector3.zero;
    }

    // 회피 버튼 onclick.AddListener에 추가할 것
    public bool Dodge() => Input.GetKeyDown(KeyCode.Space);


    // 공격 버튼 onclick.AddListener에 추가할 것
    public bool Attack() => Input.GetKey(KeyCode.Z);

}
