using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public float projectileSpeed;
        public float spawnTime;
        private float _spawnTimeCounter;
        private Vector2 _spawnPosition;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void UpdateAttack(bool attackPressed, Vector2 moveDirection)
        {
            if (!(Time.time > _spawnTimeCounter)) return;
            if (!attackPressed) return;
           
            
            StartCoroutine(FireDelay(moveDirection));

            return;

            IEnumerator FireDelay(Vector2 direction)
            {
                _spawnTimeCounter = Time.time + spawnTime;
                yield return  new WaitForSeconds(0.17f);
              
                var projectileClone = Instantiate(projectilePrefab, UpdateSpawnPosition(), Quaternion.identity);
               
                projectileClone.TryGetComponent(out Rigidbody2D rb2D);
                rb2D.linearVelocity = direction * projectileSpeed;
                
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb2D.transform.rotation = Quaternion.Euler(0,0, angle);
                
                //_spawnTimeCounter = Time.time + spawnTime;
                Destroy(projectileClone, 4f);
            }

            Vector2 UpdateSpawnPosition()
            {
                _spawnPosition.x = _transform.localPosition.x + (moveDirection.x / 2);
                _spawnPosition.y = _transform.localPosition.y + (moveDirection.y / 2);

                if (_spawnPosition == Vector2.zero)
                {
                    _spawnPosition.x = _transform.position.x + 0.5f;
                }
                return _spawnPosition;
            }
        }
        
    }
}