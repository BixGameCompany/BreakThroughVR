using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }else
        if (collision.gameObject.layer == 12)
        {
            GameObject.Find("VR Rig").GetComponent<PlayerStats>().DamagePlayer(5);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }
}
