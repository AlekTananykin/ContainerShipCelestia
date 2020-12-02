using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RayShooter : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnGUI()
    {
        
        Camera camera = Camera.main;

        int size = 20;
        float posX = camera.pixelWidth / 2 - size / 4;
        float poxY = camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, poxY, size, size), "*");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera camera = Camera.main;
            Vector3 point = new Vector3(
                camera.pixelWidth / 2, camera.pixelHeight / 2, 0);

            Ray ray = camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (null != target)
                {
                    
                    target.ReactToHit();
                }
                else
                    StartCoroutine(ShareIndicator(hit.point));
            }
        }
    }

    private IEnumerator ShareIndicator(Vector3 point)
    {
        GameObject sphere = 
            GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = point;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
