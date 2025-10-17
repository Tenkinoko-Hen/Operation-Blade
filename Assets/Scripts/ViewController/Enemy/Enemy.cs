using UnityEngine;

namespace ViewController.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Player.Player _player;
        public EnemyBullet _enemyBullet;
        public enum States
        {
            FollowPlayer,
            Shoot
        }

        public States _state = States.FollowPlayer;

        public float _currentSeconds = 0;
        public float _followTime = 3f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Application.targetFrameRate = 60;
        }

        // Update is called once per frame
        void Update()
        {
            if (_state == States.FollowPlayer)
            {
                if (_currentSeconds >= _followTime)
                {
                    _state = States.Shoot;
                    _currentSeconds = 0f;
                }

                var directionToPlayer = (_player.transform.position - this.transform.position).normalized;
                this.transform.Translate(directionToPlayer*Time.deltaTime);            
                _currentSeconds += Time.deltaTime;

            }
            else if(_state == States.Shoot)
            {
                
                _currentSeconds +=Time.deltaTime;
                if (_currentSeconds >= 1.0f)
                {
                    _followTime = Random.Range(2, 4);
                    _currentSeconds = 0;
                    _state = States.FollowPlayer;
                }

                if (Time.frameCount % 20 == 0)
                {
                    var directionToPlayer = (_player.transform.position - this.transform.position).normalized;
                    
                    var enemyBullet = Instantiate(_enemyBullet);
                    enemyBullet.transform.position = this.transform.position;
                    enemyBullet._dirction = directionToPlayer;
                    enemyBullet.gameObject.SetActive(true);
                }
            }
        }
    }
}
