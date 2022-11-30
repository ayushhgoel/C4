using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputScript : MonoBehaviour
{
    public int column;
    public GameManager gm;

    void OnMouseDown()
    {
        gm.SelectColumn(column);
    }

    //private void OnMouseOver()
    //{
       /// gm.HoverColumn(column);
   // }
}
 