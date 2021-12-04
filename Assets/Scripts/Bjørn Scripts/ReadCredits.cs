using System.Collections.Generic;
using UnityEngine;

public class ReadCredits : MonoBehaviour
{
    private List<string[]> credits_info = new List<string[]>();
    
    // Start is called before the first frame update
    void Start()
    {
        print(credits_info);
        ReadCreditsFile();
    }

    private void ReadCreditsFile()
    {
        var strReader = Resources.Load<TextAsset>("Credits/Credits");
        print(strReader);

        if (strReader != null)
        {
            var data = strReader.text.Split("\n");

            for (var i = 0; i < data.Length; i++)
            {
                credits_info.Add(data[i].Split(","));
            }

            var txt = "";
            for (var i = 0; i< credits_info.Count; i++)
            {
                for (var j = 0; j < credits_info[i].Length; j++)
                {
                    if (j == 0) txt += "";
                    else txt += "";
                }
            }
        }
    }
}
