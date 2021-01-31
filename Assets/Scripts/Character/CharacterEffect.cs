using UnityEngine;

public class CharacterEffect : MonoBehaviour
{
    [Header("ParticleSystem")]
    [SerializeField] ParticleSystem particleMoveEffect = null;

    void Start()
    {
        Initialization();
    }

    private void Initialization() // ÃÊ±âÈ­
    {
        particleMoveEffect?.Stop();
    }

    public void PlayEffects(GroundType groundType, float speedNormalized)
    {
        if (groundType == GroundType.None || particleMoveEffect is null) return;

        if (speedNormalized > 0.1f)
        {
            if (particleMoveEffect.isPlaying) return;

            particleMoveEffect.Play();
        }
        else
        {
            StopEffects();
        }
    }

    public void StopEffects()
    {
        if (particleMoveEffect is null) return;

        particleMoveEffect.Stop();
    }
}
