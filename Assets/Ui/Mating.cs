using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mating : MonoBehaviour {
    void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;    //这是设置背景填充的
        bb.fontSize = 40;       //当然，这是字体大小

        if (Gaming.Instance.win_los_flag == Gaming.WIN_FLAG)
        {
            bb.normal.textColor = new Color(0, 1, 0);   //设置字体颜色的
            GUI.Label(new Rect(20, 20, 200, 200), "你赢了", bb);
        }
        else if (Gaming.Instance.win_los_flag == Gaming.LOS_FLAG)
        {
            bb.normal.textColor = new Color(1, 0, 0);   //设置字体颜色的
            GUI.Label(new Rect(20, 20, 200, 200), "你输了", bb);
        }
        else if (Gaming.Instance.game_online_flag == 0)
        {
            bb.normal.textColor = new Color(0, 0, 1);   //设置字体颜色的
            GUI.Label(new Rect(20, 20, 200, 200), "匹配中", bb);
        }
    }
}
