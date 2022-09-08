using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Season : MonoBehaviour
{
    public enum SeasonMode
    {
        SUMMER_SOLSTICE,
        WINTER_SOLSTICE
    }
    
    public static SeasonMode season;

    public static Dictionary<SeasonMode, bool[]> seasonRouteMapping = new Dictionary<SeasonMode, bool[]>
    {
        { SeasonMode.SUMMER_SOLSTICE, new bool[] { true, false, false } },
        { SeasonMode.WINTER_SOLSTICE, new bool[] { true, true, true } },
    };
    // Start is called before the first frame update
    void Start()
    {
        Random rd = new Random();
        int num = rd.Next(1, 3);
        if (num == 1) season = SeasonMode.SUMMER_SOLSTICE;
        else if (num == 2) season = SeasonMode.WINTER_SOLSTICE;
        Debug.Log((int)season);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static int getSeason()
    {
        return (int)season;
    }
}