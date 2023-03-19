using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CurrentSpawner : MonoBehaviour
{
    public float sineWaveHeight = 100f;
    public float spawnRate = 0.1f;
    float chargeDecreaseTimer = 0.1f;

    private Vector2 defaultPosition;

    public GameObject chargeIndicator;
    public TextMeshProUGUI scoreText;

    private AudioSource electricHum;

    [SerializeField]
    private float chargePercentage;

    private float chargeTime;

    public GameObject nodePrefab;

    private float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        electricHum = GetComponent<AudioSource>();
        defaultPosition = transform.position;

        electricHum.Play();
        electricHum.Pause();
        
        RectTransform image = chargeIndicator.GetComponent<RectTransform>();

        image.sizeDelta = new Vector2(0.0f, image.sizeDelta.y);
        nextSpawn = spawnRate;
    }

void Update()
{
    // Decrease the chargePercentage variable when the timer reaches 0
    if (chargeDecreaseTimer <= 0.0f) {
        chargePercentage -= 1.0f;
        chargeDecreaseTimer = 0.1f;
        scoreText.text = chargePercentage.ToString("#");
    } else {
        if (chargePercentage > 0) {
            chargeDecreaseTimer -= Time.deltaTime;
        }
    }

    // Load the TitleScreen when the chargePercentage reaches 100%
    if (chargePercentage >= 100.0f) {
        SceneManager.LoadScene("TitleScreen");
    }

    float time = Time.time * 1.35f;

    float h = Mathf.Sin(time) + Mathf.Sin(4 * time);
    h /= 2.0f;

    h *= sineWaveHeight;

    transform.position = new Vector3(defaultPosition.x, defaultPosition.y + h, transform.position.z);

    chargeTime -= Time.deltaTime;
    if (chargeTime <= 0.0f) {
        electricHum.Pause();
    } else {
        electricHum.UnPause();
    }

    nextSpawn -= Time.deltaTime;
    if (nextSpawn <= 0.0f) {
        nextSpawn += spawnRate;
        GameObject obj = Instantiate(nodePrefab, transform.position, Quaternion.identity, transform.parent);

        CurrentNode node = obj.GetComponent<CurrentNode>();
        node.spawner = this;
    }
}


    public void ChargeBattery(float amount)
    {
        chargePercentage += amount;

        if (chargePercentage > 100.0f)
        {
            chargePercentage = 100.0f;
        }
        
        RectTransform image = chargeIndicator.GetComponent<RectTransform>();
        
        image.sizeDelta = new Vector2(chargePercentage, image.sizeDelta.y);

        scoreText.text = chargePercentage.ToString("#");
        chargeTime = 0.1f;
    }
}
