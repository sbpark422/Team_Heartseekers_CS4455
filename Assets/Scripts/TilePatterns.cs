using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilePatterns : MonoBehaviour
{
    public static HashSet<(int, int)>[] patternHalf =
    {
        new HashSet<(int, int)>
        {
            (1,2), (2,2), (3,2), (3,3), (3,4), (4,4),
            (5,4), (5,3), (5,2), (5,1), (6,1), (7,1), 
            (8,1), (9,1), (10,1), (11,1), (11,2), 
            (11,3), (11,4), (10,4), (9,4),(9,5), (9,6), 
            (9,7), (8,7), (7,7), (7,6), (6,6), (5,6), 
            (4,6), (4,7), (4,8), (5,8), (6,8), (6,9)
        },
        new HashSet<(int, int)>
        {
            (1,4), (2,4), (3,4), (4,4), (4,3), (4,2),
            (5,2), (6,2), (7,2), (8,2), (9,2), (10,2),
            (10,1), (11,1), (12,1), (12,2), (12,3),
            (12,4), (12,5), (12,6), (12,7), (11,7), 
            (10,7), (9,7), (9,6), (9,5), (8,5), (7,5),
            (6,5), (6,6), (5,6), (4,6), (4,7), (4,8), 
            (5,8), (6,8), (6,9)
        },
        new HashSet<(int, int)>
        {
            (5,1), (5,2), (5,3), (6,3), (7,3), (7,2),
            (8,2), (9,2), (10,2), (11,2), (11,3),
            (12,3), (12,4), (12,5), (12,6), (12,7),
            (11,7), (10,7), (10,6), (10,5), (9,5),
            (8,5), (7,5), (6,5), (5,5), (4,5), (3,5),
            (2,5), (1,5), (1,6), (1,7), (1,8), (2,8),
            (3,8), (4,8), (5,8), (6,8), (6,9)
        },
        new HashSet<(int, int)>
        {
            (1,2), (2,2), (3,2), (3,1), (4,1), (5,1),
            (6,1), (7,1), (8,1), (9,1), (10,1), (11,1),
            (12,1), (12,2), (12,3), (12,4), (12,5),
            (12,6), (11,6), (10,6), (10,5), (10,4),
            (10,3), (9,3), (8,3), (8,4), (8,5), (8,6),
            (7,6), (6,6), (5,6), (5,5), (5,4), (4,4),
            (3,4), (2,4), (2,5), (2,6), (2,7), (3,7),
            (3,8), (4,8), (5,8), (6,8), (6,9)
        },
        new HashSet<(int, int)>
        {
            (1,7), (2,7), (3,7), (4,7), (5,7), (5,6),
            (5,5), (5,4), (4,4), (3,4), (2,4), (2,3),
            (2,2), (3,2), (4,2), (5,2), (6,2), (6,3), 
            (7,3), (8,3), (9,3), (9,2), (10,2), (11,2), 
            (11,3), (11,4), (11,5), (10,5), (9,5), 
            (8,5), (8,6), (8,7), (8,8), (7,8), (7,9)
        },
        new HashSet<(int, int)>
        {
            (1,4), (2,4), (2,5), (2,6), (2,7), (2,8),
            (3,8), (4,8), (4,7), (4,6), (4,5), (4,4),
            (4,3), (4,2), (4,1), (5,1), (6,1), (6,2),
            (6,3), (7,3), (8,3), (9,3), (9,4), (10,4),
            (11,4), (11,5), (11,6), (11,7), (11,8),
            (10,8), (9,8), (8,8), (8,7), (8,6), (7,6),
            (6,6), (6,7), (6,8), (6,9)
        },
        new HashSet<(int, int)>
        {
            (1,8), (2,8), (3,8), (3,7), (3,6), (3,5),
            (3,4), (2,4), (2,3), (2,2), (3,2), (4,2),
            (4,3), (5,3), (6,3), (7,3), (7,2), (7,1),
            (8,1), (9,1), (9,2), (9,3), (10,3), (11,3),
            (11,4), (11,5), (11,6), (12,6), (12,7),
            (12,8), (11,8), (10,8), (10,7), (9,7),
            (9,6), (8,6), (7,6), (6,6), (6,7), (6,8),
            (6,9)
        }
    };

    public static HashSet<string> GeneratePattern()
    {
        Debug.Log("Static Tile Patterns");
        HashSet<(int, int)> firstTemp = patternHalf[Random.Range(0, patternHalf.Length)];
        HashSet<(int, int)> secondTemp = patternHalf[Random.Range(0, patternHalf.Length)];
        HashSet<string> firstHalf = new HashSet<string>(firstTemp.Select(i => i.ToString().Replace(" ", "")));
        HashSet<string> secondHalf;
        if (Random.Range(0,1) == 0)
        {
            secondHalf = new HashSet<string>(secondTemp.Select(i => (i.Item1, 18 - i.Item2).ToString().Replace(" ", "")));
        }
        else
        {
            secondHalf = new HashSet<string>(secondTemp.Select(i => (13 - i.Item1, 18 - i.Item2).ToString().Replace(" ", "")));
        }
        return new HashSet<string>(firstHalf.Concat(secondHalf));

        //HashSet<string> a = new HashSet<string>(patternHalf[6].Select(i => i.ToString().Replace(" ", "")));
        //HashSet<string> b = new HashSet<string>(patternHalf[1].Select(i => (13-i.Item1, 18-i.Item2).ToString().Replace(" ", "")));
        //HashSet<string> b = new HashSet<string>(patternHalf[6].Select(i => (i.Item1, 18 - i.Item2).ToString().Replace(" ", "")));
        //return new HashSet<string>(a.Concat(b));
    }    
}
