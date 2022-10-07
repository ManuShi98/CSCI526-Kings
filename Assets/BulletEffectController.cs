using UnityEngine;

public class BulletEffectController : MonoBehaviour
{
    void Update()
    {
        if(!GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
