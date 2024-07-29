using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class capnhatthanhmaubosshiepsi : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai,luongmautoida);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
