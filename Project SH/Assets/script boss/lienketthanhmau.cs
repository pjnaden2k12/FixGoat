using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thanhmaubossgiapsat : MonoBehaviour
{
    public Image    _thanhMau;
    public  void CapNhatThanhMau(float luongmauhientai, float luongmautoida)
    {
        _thanhMau.fillAmount = luongmauhientai/luongmautoida;
    
    }

    // Start is called before the first frame update
    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
