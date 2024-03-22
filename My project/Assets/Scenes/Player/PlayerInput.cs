using UnityEngine;

// �÷��̾� Input�� ���
public class PlayerInput : MonoBehaviour
{
    // ���̽�ƽ ���� https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631
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
        // ���� ����
        // return new Vector3(attackJoystick.Horizontal, 0, attackJoystick.Vertical);
        return Vector3.zero;
    }

    // ȸ�� ��ư onclick.AddListener�� �߰��� ��
    public bool Dodge() => Input.GetKeyDown(KeyCode.Space);


    // ���� ��ư onclick.AddListener�� �߰��� ��
    public bool Attack() => Input.GetKey(KeyCode.Z);

}
