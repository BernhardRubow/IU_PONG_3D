using UnityEngine;

public class PlayerMoveComponent : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    public void MoveUp()
    {
        transform.position += Vector3.up * m_Speed * Time.deltaTime;
    }

    public void MoveDown()
    {
        transform.position += Vector3.down * m_Speed * Time.deltaTime;
    }
}