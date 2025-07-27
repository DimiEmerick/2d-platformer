using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateGameObject : MonoBehaviour
{
    public void Paralax(float speed)
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
