using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMedScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private ParticleSystem _healingEffectPrefab;
    private ParticleSystem _healingEffect;

    public void Awake()
    {
        _healingEffect = Instantiate(_healingEffectPrefab);
        _healingEffect.transform.position = this.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        _healingEffect.Play();
        controller.AddHealth(25);
        Destroy(this.gameObject, 1f);
    }
}
