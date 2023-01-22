using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreator : MonoBehaviour
{
    [SerializeField] private float minx = -3.5f;
    [SerializeField] private float maxx = -0.5f;
    [SerializeField] private float miny = -1.5f;
    [SerializeField] private float maxy = 0.8f;
    public GameObject prefab;
    private float cooldown;
    [SerializeField] private float cooldownrange = 3f;
    private Vector3 fireposition;
    // Start is called before the first frame update
    void Start()
    {
        fireposition = new Vector3(Random.Range(minx, maxx), Random.Range(miny, maxy),1f);
        cooldown = Random.Range(cooldownrange-1, cooldownrange+1);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minx, miny, 0), new Vector3(maxx, miny, 0));
        Gizmos.DrawLine(new Vector3(minx, maxy, 0), new Vector3(maxx, maxy, 0));
        Gizmos.DrawLine(new Vector3(minx, maxy, 0), new Vector3(minx, miny, 0));
        Gizmos.DrawLine(new Vector3(maxx, maxy, 0), new Vector3(maxx, miny, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <= 0) {
            Instantiate(prefab, fireposition, new Quaternion(0,0,0,0));
            fireposition = new Vector3(Random.Range(-3.5f, -0.5f), Random.Range(-1.5f, 0.8f),1f);
            cooldown = Random.Range(cooldownrange-1, cooldownrange+1);
        }
        cooldown -= Time.deltaTime;
    }
}
