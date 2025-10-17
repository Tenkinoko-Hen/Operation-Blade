using UnityEngine;

namespace ViewController.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerBullet _playerBullet;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
        
            transform.Translate(new Vector3(horizontal, vertical, 0)*Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                var playerBullet = Instantiate(_playerBullet);
                playerBullet.transform.position = this.transform.position;
                playerBullet._dirction = Vector2.right;
                playerBullet.gameObject.SetActive(true);
            }
        }
    }
}
