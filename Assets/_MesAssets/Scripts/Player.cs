using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [Header("Propriétés du joueur")]
    [SerializeField] private float _vitesseJoueur = 10f;
    [SerializeField] private GameObject _laserJoueur = default(GameObject);
    [SerializeField] private float _cadenceTir = 0.5f;
    
    [SerializeField] private int _viesJoueur = 3;
    [SerializeField] private GameObject _shield = default(GameObject);  
    public int ViesJoueur => _viesJoueur;

    private float _delai = -1;
    private Animator _animator;
    [SerializeField] private AudioClip _sonLaser = default;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
       
    }

    private void Update()
    {
        MouvementsJoueur();
        if (Input.GetAxis("Fire1") == 1 && Time.time > _delai)
        {
            Instantiate(_laserJoueur, transform.position + new Vector3(0f, 1.11f, 0f), Quaternion.identity);
            _delai= Time.time + _cadenceTir;
            AudioSource.PlayClipAtPoint(_sonLaser, Camera.main.transform.position, 0.5f);
            

        }

    }

    private void MouvementsJoueur()
    {
        float mouvementX = Input.GetAxis("Horizontal");
        float mouvementY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(mouvementX, mouvementY);
        direction.Normalize();

        transform.Translate(direction * Time.deltaTime * _vitesseJoueur);
        transform.position =
            new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 0.5f), 0f);

        if (transform.position.x >= 10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -10f)
        {
            transform.position = new Vector3(10f, transform.position.y, 0f);
        }

        if (direction.x > 0f)
        {
            _animator.SetBool("TurnRight", true);
            //_animator.SetBool("TurnLeft", false);
        }
        else if(direction.x < 0f)
        {
            _animator.SetBool("TurnLeft", true);
            //_animator.SetBool("TurnRight", false);
        }
        else
        {
            _animator.SetBool("TurnRight", false);
            _animator.SetBool("TurnLeft", false);
        }
    }

    public void Dommage()
    {

        if (!_shield.activeSelf)
        {
            _viesJoueur--;
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.ChangeLivesDisplayImage(_viesJoueur);
        }
        else
        {
            _shield.SetActive(false);
        }

        
        if (_viesJoueur <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ActiverShield()
    {
        _shield.SetActive(true);
    }
}
