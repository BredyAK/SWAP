using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTransform : MonoBehaviour
{
    public void TransformFinished()
    {
        gameObject.SetActive(false);
    }
}
