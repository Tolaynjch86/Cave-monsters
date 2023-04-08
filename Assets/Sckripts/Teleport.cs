using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform spawnPoint;
    private bool _stayInTrigger;
    private Transform _player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _stayInTrigger)
        {
            _player.GetComponent<CharacterController>().enabled = false;
            _player.position = spawnPoint.position;
            _player.GetComponent<CharacterController>().enabled = true;
            _stayInTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _stayInTrigger = true;
            _player = other.transform;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _stayInTrigger = false;
            _player = null;
        }

    }
}
