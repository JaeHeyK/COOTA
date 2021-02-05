using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    None,
    Dirt,
}

public class Ground : MonoBehaviour
{
    [SerializeField]
    private GroundType groundType = GroundType.None;

    private Player player = null;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        // CollsionStay를 사용하므로 매 프레임 체크할 때 마다 GetComponent 사용하기엔 비효율적이니 캐싱
        player = PlayerController.Instance.gameObject.GetComponent<Player>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.EnterGround(groundType);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.ExitGround();
        }
    }
}
