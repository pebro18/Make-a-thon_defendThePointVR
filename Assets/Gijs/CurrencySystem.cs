using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrencySystem : MonoBehaviour
{
    
    //each round you get building currency 
    public int Currency;
    
    // Start is called before the first frame update
    void Start()
    {
        Currency = 100000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWeaponHitAddCurrency()
    {
        Currency++;
    }
    
    public void StartOfRoundCurrency()
    {

    }

    public void OnEnemyDeathAddCurrency()
    {

    }
    
}
