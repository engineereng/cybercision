using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public GameObject unlitShadow;
    public string sceneToLoad;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (IsMouseOverCollider())
        {
            unlitShadow.SetActive(false);

            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            unlitShadow.SetActive(true);
        }
    }

    private bool IsMouseOverCollider()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.collider == boxCollider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
