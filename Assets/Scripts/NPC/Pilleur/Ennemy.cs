/*******************************************
** Aucouturier Romuald
** 24-01-2020
** 
** 24-01-2020
** 
** Ennemy
**  Instance d'un ennemy qui permet de ralentir le joueur ou les pnjs
**  dans une petit zone
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField]
    private float _beforeStart = 60;

    [SerializeField]
    private float _minTimeBetweenAttack = 120;
    [SerializeField]
    private float _maxTimeBetweenAttack = 150;

    [SerializeField]
    private float _minTimeAttack = 10;
    [SerializeField]
    private float _maxTimeAttack = 15;

    private bool _isAttack = false;
    private IEnumerator _attackCoroutine;

    private float _timeBeforeAttack;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        // lance l'attente avec le temps choisie dans unity
        _timeBeforeAttack = _beforeStart;
    }

    // Permet de savoir si l'ennemie viser est entrain d'attaquer
    public bool GetIsAttack()
    {
        return _isAttack;
    }

    private void FixedUpdate()
    {
        // si l'ennemy n'attaque pas et que le temps d'attente n'est pas assez haut
        // incrémente du temps passer
        if (!_isAttack && _timer < _timeBeforeAttack)
        {
            _timer += Time.deltaTime;
        }
        // sinon s'il n'attaque pas mais que le temps d'attente est bon lance la coroutine
        // d'attaque
        else if (!_isAttack && _timer >= _timeBeforeAttack)
        {
            _attackCoroutine = Attack();
            StartCoroutine(_attackCoroutine);
        }
    }

    [SerializeField]
    private Transform _startAttackPos;
    [SerializeField]
    private Transform _endAttackPos;

    private float _minDivider = 0.3f;
    private float _maxDividier = 0.2f;

    [SerializeField]
    private float _timeDebuff = 2;

    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private LayerMask _pnjLayer;

    private IEnumerator Attack()
    {
        print("Start");
        _isAttack = true;

        // creer le ray ainsi que la distance (direction)
        Ray ray = new Ray(_startAttackPos.position, _endAttackPos.position - _startAttackPos.position);
        float distance = Vector3.Distance(_startAttackPos.position, _endAttackPos.position);

        float timer = Random.Range(_minTimeAttack, _maxTimeAttack);
        float time = 0;

        // tant que le temps d'attaque n'est pas assez long met un raycast pour voir si un joueur
        // ou un auto npc le traverse
        while (time < timer)
        {
            // increment du temps reel le temps passer
            time += Time.deltaTime;

            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red); // debug de la ligne
            if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
            {
                if (((1 << hit.transform.gameObject.layer) & _playerLayer) != 0)
                {
                    // faire l'interaction avec le joueur
                }
                // verify que c'est le bon layer qui est pris
                else if (((1 << hit.transform.gameObject.layer) & _pnjLayer) != 0)
                {
                    // prend l'autoNpc et lance le changement de speed avec le temps d'attente
                    AutoNPC autoNPC = hit.transform.GetComponent<AutoNPC>();
                    autoNPC.ChangeSpeed(Random.Range(_minDivider, _maxDividier), _timeDebuff);
                }
            }

            // passage de la continuité dans la frame suivante
            yield return null;
        }

        print("end");
        StopAttack(); // stop l'attaque
    }

    private void StopAttack()
    {
        // reset tout et met un nouveau temps d'attente
        _isAttack = false;
        _timer = 0;
        _timeBeforeAttack = GenerateTimeBeforeAttack();
    }

    private float GenerateTimeBeforeAttack()
    {
        // retourne 
        return Random.Range(_minTimeBetweenAttack, _maxTimeBetweenAttack);
    }

    public void EnnemiesDead()
    {
        // verify si l'ennemies attaque si c'est le cas arrete la coroutine
        if (!_isAttack)
        {
            return;
        }
        
        StopCoroutine(_attackCoroutine);
        StopAttack();
    }
}
