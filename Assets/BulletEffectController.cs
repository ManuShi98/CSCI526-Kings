using UnityEngine;

public class BulletEffectController : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Debug.Log("stop");
    }
    void Update()
    {
        if(!GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
