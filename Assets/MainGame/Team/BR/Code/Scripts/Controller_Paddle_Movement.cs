using UnityEngine;

public class Controller_Paddle_Movement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    [SerializeField] private float m_MaxMoveDistance;

    public void MoveUp()
    {
        if (transform.position.y < m_MaxMoveDistance)
            transform.position += Vector3.up * m_Speed * Time.deltaTime;
    }

    public void MoveDown()
    {
        if (transform.position.y > -m_MaxMoveDistance)
            transform.position += Vector3.down * m_Speed * Time.deltaTime;
    }
}