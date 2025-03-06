using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public GameObject rotateLoadingObject;
    public float rotationSpeed;

    public void Start()
    {
        StartCoroutine(RotateLoading());
    }

    private IEnumerator RotateLoading()
    {
        float timer = 0f;
        while (timer < 3f)
        {
            rotateLoadingObject.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
