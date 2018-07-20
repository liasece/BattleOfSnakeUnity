using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Cooder
{
    public Cooder(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Cooder()
    {
        x = 0;
        y = 0;
    }
    public int x;
    public int y;
}

public class Gaming {

    private int game_state;

    public int GameState
    {
        get
        {
            return game_state;
        }
        set
        {
            game_state = value;
        }
    }

    public const int GAME_H = 30;
    public const int GAME_W = 30;

    public const int GAMEMODE_BEGIN = 10;
    public const int GAMEMODE_LINKED = 20;
    public const int GAMEMODE_COMMING = 30;
    public const int GAMEMODE_GAMING = 40;
    public const int GAMEMODE_DONE = 50;


    public const int TURN_LEFT = 'a';
    public const int TURN_RIGHT = 'd';
    public const int TURN_UP = 'w';
    public const int TURN_DOWN = 's';

    public const int SER_FLAG = 1;
    public const int CLI_FLAG = 2;

    public const int FOOD_FLAG = 3;

    public const int MAP_FLAG = 11;
    public const int JION_IN = 12;
    public const int COM_DONE = 13;
    public const int GAME_STATE_FLAG = 14;

    public const int TURN_FLAG = 21;

    public const int WIN_FLAG = 31;
    public const int LOS_FLAG = 32;

    public static Gaming _instance;


    public static Gaming Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Gaming();
            }
            return _instance;
        }
    }

    public int game_online_flag = 0;
    private int game_sc_mode=CLI_FLAG;
    private int game_move_per_ms=1000;
    public int win_los_flag = 0;

    public Cooder food;
    public int[,] game_map;
    // public int[,] controlMap;
    public List<Cooder> snake_li_m;
    public List<Cooder> snake_li_e;

    public void onRecv(byte[] data,int length)
    {
        gameCmd(data);

        if (win_los_flag != 0)
        {
            if (win_los_flag == WIN_FLAG)
            {
                Debug.Log("You WIN! You WIN! You WIN!\n");
            }
            else if (win_los_flag == LOS_FLAG)
            {
                Debug.Log("LOSER! LOSER! LOSER! LOSER!\n");
            }
            else
            {
                Debug.Log("Unknow Flag!\n");
                Debug.Log(game_move_per_ms);
            }
            return;
        }
    }
    
    private void gameCmd(byte[] data)
    {
        ByteStreamBuff _tmpbuff = new ByteStreamBuff(data);
        int flag = _tmpbuff.Read_Byte();
        switch(flag)
        {
            case MAP_FLAG:
                int[,] tmp_game_map = new int[GAME_H, GAME_W];
                List<Cooder> tmp_li_m = new List<Cooder>();
                List<Cooder> tmp_li_e = new List<Cooder>();
                int nodeSum;
                int x, y;
                nodeSum = _tmpbuff.Read_Int();
                for (int i = 0; i < nodeSum; i++)
                {
                    x = _tmpbuff.Read_Byte();
                    y = _tmpbuff.Read_Byte();
                    tmp_game_map[x,y] = SER_FLAG;
                    if(game_sc_mode==SER_FLAG)
                        tmp_li_m.Add(new Cooder(x, y));
                    else
                        tmp_li_e.Add(new Cooder(x, y));
                }
                nodeSum = _tmpbuff.Read_Int();
                for (int i = 0; i < nodeSum; i++)
                {
                    x = _tmpbuff.Read_Byte();
                    y = _tmpbuff.Read_Byte();
                    tmp_game_map[x,y] = CLI_FLAG;
                    if (game_sc_mode == CLI_FLAG)
                        tmp_li_m.Add(new Cooder(x, y));
                    else
                        tmp_li_e.Add(new Cooder(x, y));
                }

                food.x=_tmpbuff.Read_Byte();
                food.y=_tmpbuff.Read_Byte();

                game_map = tmp_game_map;
                snake_li_m = tmp_li_m;
                snake_li_e = tmp_li_e;
                break;
            case COM_DONE:
                game_online_flag = 1;
                game_sc_mode = _tmpbuff.Read_Byte();
                game_move_per_ms = _tmpbuff.Read_Int();
                // GAME_H = _tmpbuff.Read_Int();
                // GAME_W = _tmpbuff.Read_Int();
                game_map = new int[GAME_H, GAME_W];
                snake_li_m = new List<Cooder>();
                snake_li_e = new List<Cooder>();
                food=new Cooder(0,0);


                // controlMap = new int[GAME_H, GAME_W];
                // for(int i=0;i<GAME_H;++i){
                //     for(int j=0;j<GAME_W;++j){
                //         controlMap[i,j]=_tmpbuff.Read_Byte();
                //     }
                // }
                //Debug.Log("COM_DOWN");
                break;
            case GAME_STATE_FLAG:
                win_los_flag = _tmpbuff.Read_Byte() ;
                break;
            case FOOD_FLAG:
                break;
            default:
                Debug.Log("Unknow flag!");
                break;
        }
    }

    public void sendFromatToServer(int flag, byte dire)
    {
        ByteStreamBuff _tmpbuff = new ByteStreamBuff();
        /*_tmpbuff.Write_Int(1314);
        _tmpbuff.Write_Float(99.99f);
        _tmpbuff.Write_UniCodeString("Claine");
        _tmpbuff.Write_UniCodeString("123456");
        SocketManager.Instance.SendMsg(eProtocalCommand.sc_binary_login, _tmpbuff);*/

        switch (flag)
        {
            case MAP_FLAG:
                _tmpbuff.Write_Byte(MAP_FLAG);
                break;
            case JION_IN:
                _tmpbuff.Write_Byte(JION_IN);
                break;
            case TURN_FLAG:
                _tmpbuff.Write_Byte(TURN_FLAG);
                _tmpbuff.Write_Byte(dire);
                break;
            case GAME_STATE_FLAG:
                _tmpbuff.Write_Byte(GAME_STATE_FLAG);
                _tmpbuff.Write_Int(1);
                break;
            default:
                break;
        }

        SocketManager.Instance.SendMsg(_tmpbuff);

        return;
    }
}
