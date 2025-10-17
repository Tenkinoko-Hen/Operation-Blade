using System;
using UnityEngine;

namespace ViewController.Player
{
    public class PlayerBullet : MonoBehaviour
    {
        public Vector2 _dirction;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(_dirction*Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.name.StartsWith("Enemy"))
            {
                GameUI.instance._gamePass.SetActive(true);
                other.gameObject.SetActive(false);
                Time.timeScale = 0;

            }
        }

        
    }
}
