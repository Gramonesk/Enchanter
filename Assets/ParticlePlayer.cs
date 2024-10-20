using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public Color color = Color.white;
    public ParticleSystem particle;
    public void PlayParticle()
    {
        var main = particle.main;
        main.startColor = color;
        particle.Play();
    }
}
