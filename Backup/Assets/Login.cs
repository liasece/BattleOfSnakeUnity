using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class Login : MonoBehaviour {

    GameObject food;
    GameObject friend;
    GameObject enemy;
    int[,] map;

    // Use this for initialization
    void Start () {
        Gaming.Instance.GameState = Gaming.GAMEMODE_BEGIN;
        map = new int[Gaming.GAME_H, Gaming.GAME_W];
        food = GameObject.Find("Food");
        food.transform.gameObject.SetActive(false);
        friend = GameObject.Find("Friend");
        friend.transform.gameObject.SetActive(false);
        enemy = GameObject.Find("Enemy");
        enemy.transform.gameObject.SetActive(false);

        cube_li_m = new List<GameObject>();
        cube_li_e = new List<GameObject>();
    }
    List<GameObject> cube_li_m;
    List<GameObject> cube_li_e;
	// Update is called once per frame
	void Update () {
        if (Gaming.Instance.game_online_flag==1 && HaveChange())
        {
            int i = 0;
            int j = 0;

            i = Gaming.Instance.snake_li_m.Count-1;
            j = 0;
            while(Gaming.Instance.snake_li_m.Count > cube_li_m.Count)
            {
                GameObject cubeClone = Instantiate(friend.transform.gameObject);
                cubeClone.GetComponent<BoxMove>().SetXY(Gaming.Instance.snake_li_m[i].x,
                                                        Gaming.GAME_H-1-Gaming.Instance.snake_li_m[i].y);
                cubeClone.SetActive(true);
                cube_li_m.Insert(0,cubeClone);
                i--;
            }
            for(i = 0; i < cube_li_m.Count; i++)
            {
                cube_li_m[i].GetComponent<BoxMove>().SetTXTY(Gaming.Instance.snake_li_m[i].x,
                                                        Gaming.GAME_H - 1 - Gaming.Instance.snake_li_m[i].y);
            }


            i = 0;
            j = 0;
            while (Gaming.Instance.snake_li_e.Count > cube_li_e.Count)
            {
                GameObject cubeClone = Instantiate(enemy.transform.gameObject);
                cubeClone.GetComponent<BoxMove>().SetXY(Gaming.Instance.snake_li_e[i].x,
                                                        Gaming.GAME_H - 1 - Gaming.Instance.snake_li_e[i].y);
                cubeClone.SetActive(true);
                cube_li_e.Insert(0,cubeClone);
                i++;
            }
            for (i = 0; i < cube_li_e.Count; i++)
            {
                cube_li_e[i].GetComponent<BoxMove>().SetTXTY(Gaming.Instance.snake_li_e[i].x,
                                                        Gaming.GAME_H - 1 - Gaming.Instance.snake_li_e[i].y);
            }


            food.gameObject.SetActive(true);
            food.GetComponent<BoxMove>().SetXY(Gaming.Instance.food.x, Gaming.GAME_H - 1 - Gaming.Instance.food.y);

            for (i = 0; i < Gaming.GAME_H; i++)
            {
                for (j = 0; j < Gaming.GAME_W; j++)
                {
                    map[i, j] = Gaming.Instance.game_map[i, j];
                }
            }
        }
        else if (Gaming.Instance.GameState == Gaming.GAMEMODE_BEGIN)
        {
            Gaming.Instance.GameState = Gaming.GAMEMODE_COMMING;
            Gaming.Instance.sendFromatToServer(Gaming.JION_IN, 0);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
        {
            Gaming.Instance.sendFromatToServer(Gaming.TURN_FLAG, Gaming.TURN_UP);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Gaming.Instance.sendFromatToServer(Gaming.TURN_FLAG, Gaming.TURN_DOWN);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Gaming.Instance.sendFromatToServer(Gaming.TURN_FLAG, Gaming.TURN_RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Gaming.Instance.sendFromatToServer(Gaming.TURN_FLAG, Gaming.TURN_LEFT);
        }
    }

    private void OnDestroy()
    {
        for(int i=0;i< cube_li_m.Count; i++)
        {
            Destroy(cube_li_m[i]);
        }
        for (int i = 0; i < cube_li_e.Count; i++)
        {
            Destroy(cube_li_e[i]);
        }
        Destroy(food);
        Destroy(friend);
        Destroy(enemy);
    }

    bool HaveChange()
    {
        for(int i = 0; i < Gaming.GAME_H; i++)
        {
            for(int j = 0; j < Gaming.GAME_W; j++)
            {
                if (map[i, j] != Gaming.Instance.game_map[i, j])
                {
                    return true;
                }
            }
        }
        return false;
    }
}
