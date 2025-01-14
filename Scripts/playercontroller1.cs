using System.Collections;
using System.Xml.Schema;
using UnityEngine;

[System.Serializable]
public enum SIDE { left, mid, right }
public class Character : MonoBehaviour
{
    public SIDE m_Side = SIDE.mid;
    float NewXPOS = 0f;
    public bool swipeleft;
    public bool swiperight;
    public float XValue;
    private CharacterController m_char;
    void Start()
    {
        m_char = GetComponent<CharacterController>();
        transform.position = Vector3.zero;
    }
    void Update()
    {
        swipeleft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        swiperight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        if (swipeleft)
        {
            if (m_Side == SIDE.mid)
            {
                NewXPOS = -XValue;
                m_Side = SIDE.left;
            }
            else if (m_Side == SIDE.right)
            {
                NewXPOS = 0;
                m_Side = SIDE.mid;
            }
        }
        else if (swiperight)
        {
            if (m_Side == SIDE.mid)
            {
                NewXPOS = XValue;
                m_Side = SIDE.right;
            }
            else if (m_Side != SIDE.left)
            {
                NewXPOS = 0;
                m_Side = SIDE.mid;
            }
        }
        m_char.Move((NewXPOS - transform.position.x) * Vector3.right);
    }
}
