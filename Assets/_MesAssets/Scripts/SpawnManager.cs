using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _ennemiBase = default(GameObject);
    [SerializeField] private GameObject _conteneurEnnemy = default(GameObject);
    [SerializeField] private float _delaiApparitionEnnemi1 = 3f;

    private void Start()
    {
        StartCoroutine(ApparitionEnnemisBase());    
    }

    IEnumerator ApparitionEnnemisBase()
    {
        while (Player.Instance.ViesJoueur > 0)
        {
            Vector3 positionApparition = new Vector3(Random.Range(-8.3f, 8.3f), 7f, 0f);
            GameObject newEnemy = Instantiate(_ennemiBase, positionApparition, Quaternion.identity);
            newEnemy.transform.parent = _conteneurEnnemy.transform;

            yield return new WaitForSeconds(_delaiApparitionEnnemi1);
        }

    }


}
