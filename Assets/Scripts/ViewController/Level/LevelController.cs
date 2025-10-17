using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ViewController;
using ViewController.Enemy;
using ViewController.Player;

public class LevelController : MonoBehaviour
{

    public TileBase _groundTile;

    public Tilemap _groundTileMap;


    public Player _Player;
    public Enemy _Enemy;
    public List<string> _InitRoom { get; set; } = new List<string>()
    {
        "1111111111",
        "1        1",
        "1        1",
        "1        1",
        "1  @     1",
        "1        1",
        "1      e 1",
        "1        1",
        "1        1",
        "1111111111",

    };

    private void Awake()
    {
        _Player.gameObject.SetActive(false);
        _Enemy.gameObject.SetActive(false);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _InitRoom.Count; i++)
        {
            var rowCode = _InitRoom[i];
            for (int j = 0; j < rowCode.Length; j++)
            {
                if(rowCode[j] == '1')
                    _groundTileMap.SetTile(new Vector3Int(j,i,0),_groundTile);
                else if (rowCode[j] == '@')
                {
                    var player = Instantiate(_Player);
                    Global._Player = player;
                    player.transform.position = new Vector3(j + 0.5f, i+0.5f, 0);
                    player.gameObject.SetActive(true);
                }
                else if (rowCode[j] == 'e')
                {
                    var enemy = Instantiate(_Enemy);
                    Global._Enemy = enemy;
                    enemy.transform.position = new Vector3(j, i, 0);
                    enemy.gameObject.SetActive(true);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
