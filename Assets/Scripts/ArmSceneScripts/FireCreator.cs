using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreator : MonoBehaviour
{
    [SerializeField] private float minx = -3.5f;
    [SerializeField] private float maxx = -0.5f;
    [SerializeField] private float miny = -1.5f;
    [SerializeField] private float maxy = 0.8f;
    [SerializeField] private int fireHealth = 5;
    [SerializeField] private float fireTimer = 5f;
    public GameObject prefab;
    private float cooldown;
    [SerializeField] private float cooldownrange = 6f;
    private Vector3 fireposition;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        fireposition = new Vector3(Random.Range(minx, maxx), Random.Range(miny, maxy),0);
        cooldown = Random.Range(cooldownrange-1, cooldownrange+1);
        isGameOver = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minx, miny, 0), new Vector3(maxx, miny, 0));
        Gizmos.DrawLine(new Vector3(minx, maxy, 0), new Vector3(maxx, maxy, 0));
        Gizmos.DrawLine(new Vector3(minx, maxy, 0), new Vector3(minx, miny, 0));
        Gizmos.DrawLine(new Vector3(maxx, maxy, 0), new Vector3(maxx, miny, 0));
    }
    public void setLost()
    {
        isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if((cooldown <= 0) && !isGameOver) {
            GameObject firebox = Instantiate(prefab, fireposition, new Quaternion(0,0,0,0));
            firebox.SendMessage("setHealth", fireHealth);
            firebox.SendMessage("setTime", fireTimer);
            firebox.SendMessage("setCharge", (ChargeTimer) GetComponentInParent(typeof(ChargeTimer)));
            fireposition = new Vector3(Random.Range(minx, maxx), Random.Range(miny, maxy),0);
            cooldown = Random.Range(cooldownrange-1, cooldownrange+1);
        }
        cooldown -= Time.deltaTime;
    }
}
