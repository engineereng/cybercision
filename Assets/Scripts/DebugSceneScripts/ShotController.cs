using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotController : MonoBehaviour
{
    public GameObject shot;
    public int reloadTime;
    public TextMeshProUGUI reloadText;

    public moveBug[] bugScripts;
    public GameObject bugParentObject;

    private int currShots;
    private int maxShots;

    Vector3 shot1pos = new Vector3(3.8f, 4.17f, 0f);
    Vector3 shot2pos = new Vector3(4.5f, 4.17f, 0f);
    Vector3 shot3pos = new Vector3(5.2f, 4.17f, 0f);

    GameObject shot1;
    GameObject shot2;
    GameObject shot3;

    // Start is called before the first frame update
    void Start()
    {
        reloadText.text = "";
        maxShots = 3;
        currShots = 3;
        shot1 = Instantiate(shot, shot1pos, shot.transform.rotation, this.transform);
        shot2 = Instantiate(shot, shot2pos, shot.transform.rotation, this.transform);
        shot3 = Instantiate(shot, shot3pos, shot.transform.rotation, this.transform);
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
