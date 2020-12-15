using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private int _health = 100;
    public bool IsAlive { get; private set; }


    void Start()
    {
        IsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReactToHit(int hitCount)
    {
        _health -= hitCount;

        if (_health <= 0)
        {
            IsAlive = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        this.transform.Rotate(0, -75, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
