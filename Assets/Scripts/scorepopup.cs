using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// after destroying asteriod score popup showing
public class scorepopup : MonoBehaviour
{
    private void Start()
    {
        // it will rotate the popup score to camera position
        transform.LookAt(Camera.main.transform);

        // after time t it will destroy this object
        Destroy(gameObject, 1f);
    }


}
