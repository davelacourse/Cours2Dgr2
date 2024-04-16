using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float _vitesseEnemy = 7f;

    private void Update()
    {
        MouvementsEnemy();
    }

    private void MouvementsEnemy()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _vitesseEnemy);
        if (transform.position.y < -6f)
        {
            float posX = Random.Range(-8.3f, 8.3f);
            transform.position = new Vector3(posX, 7f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.AjouterScore(10);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Player.Instance.Dommage();
        }


    }
}
