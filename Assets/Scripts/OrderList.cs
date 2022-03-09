using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderList : MonoBehaviour
{
    //-1 = nothing
    //0 = Joe with whip
    //1 = Joe
    //2 = slush
    //3 = brain
    private int[,] tutorial = new int[4, 4] { { 3, -1,-1,-1 }, { 2, -1,-1,-1 }, { 1, -1,-1,-1 }, { 0, -1, -1, -1 } };//4 orders with 4 items
    private int[,] level1 = new int[10, 4] { { 3, -1, -1, -1 }, { 3, -1, -1, -1 }, { 2, -1, -1, -1 }, { 3, 3, -1, -1 }, { 3, -1, -1, -1 }, { 3, 2, -1, -1 }, { 3, -1, -1, -1 }, { 3, 3, -1, -1 }, { 3, 2, -1, -1 }, { 2, -1, -1, -1 } };
    private int[,] level2 = new int[10, 4] { { 3, -1, -1, -1 }, { 2, -1, -1, -1 }, { 2, -1, -1, -1 }, { 3, 3, -1, -1 }, { 3, 2, -1, -1 }, { 1, -1, -1, -1 }, { 3, -1, -1, -1 }, { 1, 1, -1, -1 }, { 3, 1, -1, -1 }, { 1, -1, -1, -1 } };
    private int[,] level3 = new int[10, 4] { { 2, 2, -1, -1 }, { 3, -1, -1, -1 }, { 3, 3, -1, -1 }, { 2, 3, -1, -1 }, { 1, 1, -1, -1 }, { 1, 2, -1, -1 }, { 0, -1, -1, -1 }, { 3, 1, -1, -1 }, { 3, 3, -1, -1 }, { 0, -1, -1, -1 } };
    private int[,] level4 = new int[10, 4] { { 0, -1, -1, -1 }, { 0, -1, -1, -1 }, { 3, 3, -1, -1 }, { 2, 3, -1, -1 }, { 1, 0, -1, -1 }, { 1, 2, -1, -1 }, { 0, -1, -1, -1 }, { 3, 1, -1, -1 }, { 0, 3, -1, -1 }, { 0, -1, -1, -1 } };

    private int[,] level5 = new int[15, 4] { { 0, -1, -1, -1 }, { 0, -1, -1, -1 }, { 1, 3, -1, -1 }, { 2, 3, -1, -1 }, { 0, 0, -1, -1 }, { 2, 3, 3, -1 }, { 0, 1, 1, -1 }, { 3, 1, 3, -1 }, { 0, 3, 3, -1 }, { 0, 1, -1, -1 }, { 2, 2, 3, -1 }, { 3, 3, 3, -1 }, { 0, 0, 0, -1 }, { 2, 3, -1, -1 }, { 2, 3, -1, -1 } };
    private int[,] level6 = new int[15, 4] { { 0, 0, 3, -1 }, { 3, 3, 2, -1 }, { 2, 3, 1, -1 }, { 3, 3, 1, -1 }, { 1, 3, -1, -1 }, { 1, 1, 2, -1 }, { 0, 2, 3, -1 }, { 3, 1, 1, -1 }, { 0, 3, -1, -1 }, { 0, 1, -1, -1 }, { 0, 1, -1, -1 }, { 3, 2, 3, -1 }, { 0, 1, 2, -1 }, { 3, 1, -1, -1 }, { 0, 0, 0, -1 } };
    public int[,] getOrder (int levelIndex){
        switch (levelIndex)
        {
            case 0: return tutorial;
            case 1: return level1;
                case 2: return level2;
                case 3: return level3;
                    case 4: return level4;
                case 5: return level5;
                case 6: return level6;

                default: return tutorial;
        }

    }
}
