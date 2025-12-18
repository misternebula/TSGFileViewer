using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Loader : MonoBehaviour
    {
	    //public string STRToLoad = "frontend/frontend";
	    public List<string> STRsToLoad = new List<string>()
	    {
		    "frontend/frontend"
	    };

	    private void Start()
	    {
			SDBMHash.PrecomputeHashes();

			foreach (var item in STRsToLoad)
			{
				EAStreamFile.LoadSTRFile(item);
			}
	    }
    }
}
