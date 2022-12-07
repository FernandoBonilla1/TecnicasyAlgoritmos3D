using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField] private int _cantPoints;
    [SerializeField] private Target _target;
   // [SerializeField] private GameObject _colliders;
    [SerializeField] private AudioSource _targetAudioSource;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        //_audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            _player.SetPoints(_cantPoints);
            _targetAudioSource.Play();
            _target.DropTarget();
            //_colliders.SetActive(false);
        }
    }
}
