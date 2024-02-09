using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpaceshipController : MonoBehaviour
{
    public List<EnemySpaceShooter> Enemies;

    public float Speed;
    public float BulletSpeed;
    public GameObject bulletPrefab;
    public Transform BulletSpawnHere;

    public TextMeshProUGUI textValue,hpValue;
    public int score;
    public int hitponts;

    private int storeHP;
    public GameObject GameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        storeHP = hitponts;
    }

    // Update is called once per frame
    void Update()
    {
        textValue.text = score.ToString();
        hpValue.text = hitponts.ToString();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet(); 
        }

        if (hitponts <= 0)
        {
            GameOverScreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveInput = new Vector3(horizontalInput,0,0);
        transform.position += Time.deltaTime * Speed * moveInput;

    }

    public void SpawnBullet()
    {
        //Instantiate to clone a game object
        GameObject bullet = Instantiate(bulletPrefab, BulletSpawnHere.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(0f, BulletSpeed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            hitponts--;
            Destroy(collision.gameObject);
        }
    }

    public void RestartGame()
    {

        //Delays the call of a method in Ienumerator
        StartCoroutine(DelayEnemiesActive());
        
        hitponts = storeHP;
        GameOverScreen.SetActive(false);
    }
    IEnumerator DelayEnemiesActive()
    {
        yield return new WaitForSeconds(0.125f);
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].transform.position = Enemies[i].InitialPosition;
            Enemies[i].HealthReset();
            Enemies[i].gameObject.SetActive(true);
        }
    }

}
