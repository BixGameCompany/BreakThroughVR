using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }else
        if (collision.gameObject.layer == 12)
        {
            GameObject.Find("VR Rig").GetComponent<PlayerStats>().DamagePlayer(10);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }
}
