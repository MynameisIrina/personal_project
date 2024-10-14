using UnityEngine;


public class FlameParticleSystem : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
       other.GetComponent<HealthBarManager>().TakeDamage(0.05f);
    }

}
