using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushBehavior : MonoBehaviour
{
    public AudioSource source;
    public ParticleSystem particles;
    public Material BurstMat;
    public Light _light;
    public float LightMultiplier;
    public AnimationCurve lightAnimation;

    private void Start()
    {
        Application.targetFrameRate = -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ColorPicker"))
        {
            PlayFeedback(other);
        }
    }

    void PlayFeedback(Collider other)
    {
        StopAllCoroutines();

        BurstMat.SetColor("_TintColor", other.GetComponent<MeshRenderer>().sharedMaterial.color);
        particles.Play();
        source.Play();
        StartCoroutine(LightAnimation());
    }

    IEnumerator LightAnimation()
    {
        _light.color = BurstMat.GetColor("_TintColor");
        float val = 0f;

        while (val < 1f)
        {
            _light.intensity = lightAnimation.Evaluate(val);

            yield return new WaitForEndOfFrame();
            val += Time.deltaTime;
        }

        _light.intensity = 0f;
    }
}
