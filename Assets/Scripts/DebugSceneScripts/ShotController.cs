using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotController : MonoBehaviour
{
    public GameObject shot;
    public float reloadTime;
    public TextMeshProUGUI reloadText;

    public moveBug[] bugScripts;
    public GameObject bugParentObject;

    private int currShots;
    private int maxShots;

    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;

    public AudioSource missSource;

    // Vector3 shot1pos = new Vector3(230f, 329f, 1301f);
    // Vector3 shot2pos = new Vector3(290f, 329f, 1301f);
    // Vector3 shot3pos = new Vector3(350f, 329f, 1301f);

    // GameObject shot1;
    // GameObject shot2;
    // GameObject shot3;

    // Start is called before the first frame update
    void Start()
    {
        reloadText.text = "";
        maxShots = 3;
        currShots = 3;
        // shot1 = Instantiate(shot, shot1pos, shot.transform.rotation, this.transform);
        // shot2 = Instantiate(shot, shot2pos, shot.transform.rotation, this.transform);
        // shot3 = Instantiate(shot, shot3pos, shot.transform.rotation, this.transform);
        bugScripts = bugParentObject.GetComponentsInChildren<moveBug>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            currShots--;

            if (currShots == 0) {
                shot1.SetActive(false);
                shot2.SetActive(false);
                shot3.SetActive(false);
                Reload();
            }

            if (currShots == 1) {
                shot1.SetActive(true);
                shot2.SetActive(false);
                shot3.SetActive(false);
            }

            if (currShots == 2) {
                shot1.SetActive(true);
                shot2.SetActive(true);
                shot3.SetActive(false);
            }

            if (currShots == 3) {
                shot1.SetActive(true);
                shot2.SetActive(true);
                shot3.SetActive(true);
            }

            if (missSource)
            {
                missSource.Play();
            }
        }

    }

    void Reload() {

        // StartCoroutine(timearg(reloadTime));

        reloadText.text = "Reloading...";
        setIsNotShootable();
        Invoke(nameof(Reloading), reloadTime);
    }

    private void Reloading()
    {
        setIsShootable();

        currShots = maxShots;

        reloadText.text = "";

        shot1.SetActive(true);
        shot2.SetActive(true);
        shot3.SetActive(true);
    }

    IEnumerator timearg(int reloadTime)
    {   
        reloadText.text = "Reloading...";
        setIsNotShootable();

        yield return new WaitForSecondsRealtime(reloadTime);

        setIsShootable();

        currShots = maxShots;

        shot1.SetActive(true);
        shot2.SetActive(true);
        shot3.SetActive(true);
        
    }

    void setIsShootable() {
        foreach (moveBug bug in bugScripts) {
            bug.isShootable = true;
        } 
    }

    void setIsNotShootable() {
        foreach (moveBug bug in bugScripts) {
            bug.isShootable = false;
        }
    }
}
