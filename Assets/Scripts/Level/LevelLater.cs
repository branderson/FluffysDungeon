﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player;

namespace Assets.Scripts.Level
{
    public class LevelLater : LevelManager
    {
        [SerializeField] private int WaitFrames = 30;
        [SerializeField] private GameObject player;
        [SerializeField] public LayerMask WallLayer;
        [SerializeField] public LayerMask EnemyLayer;
        [SerializeField] public LayerMask FogLayer;
        [SerializeField] public LayerMask StaircaseLayer;
        [SerializeField] public LayerMask ItemLayer;
        [SerializeField] private GameObject fogOfWar;
        private Transform spawn;
        private int frameCounter = 0;
        private bool count = true;
        private PlayerController playerController;
        private List<AbstractMovable> enemyList; 

        public const float UnitSize = 16f;

        // Use this for initialization
        private void Start()
        {
            spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
            enemyList = FindObjectsOfType<AbstractMovable>().ToList();
            playerController = FindObjectOfType<PlayerController>();
            playerController.transform.position = spawn.position;
            print(enemyList.Count);
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    Instantiate(fogOfWar, new Vector3(i*16, j*16, 0), Quaternion.identity);
                }
            }
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void FixedUpdate()
        {
            if (count)
            {
                if (frameCounter < WaitFrames)
                {
                    frameCounter += 1;
                }
                else
                {
                    frameCounter = 0;
                    count = false;
                    // Trigger player canAct
                    playerController.EnableAction();
                }
            }
        }

        public void EnableCount()
        {
            foreach (AbstractMovable movable in enemyList)
            {
                movable.Move();
            }
            count = true;
        }

        public void DestroyObject(AbstractMovable destroy)
        {
            enemyList.Remove(destroy);
            Destroy(destroy.gameObject);
        }
    }
}