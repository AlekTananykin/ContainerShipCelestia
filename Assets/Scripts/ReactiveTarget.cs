using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReactToHit()
    {
        EnemyAI behaviour = GetComponent<EnemyAI>();
        if (null != behaviour)
            behaviour.SetAlive(false);
        else
            Debug.Log("behaviour == null");


        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        this.transform.Rotate(0, -75, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);

    }
}
