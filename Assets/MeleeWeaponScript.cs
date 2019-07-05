using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MeleeWeaponScript : MonoBehaviour
{
    public TestEnemyScript tem;

    private Rigidbody rbweapon;

    public int dammagemultiplier;
    
    public CurrencySystem crs;

    // Start is called before the first frame update
    void Start()
    {
        tem = GameObject.FindObjectOfType<TestEnemyScript>();
        crs = GameObject.FindObjectOfType<CurrencySystem>();
        rbweapon = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("sfsgfdsgfdsgsgsgsggsagssgg");
            tem.takedamage(rbweapon.velocity.magnitude * dammagemultiplier);
            crs.OnWeaponHitAddCurrency();
        }
        
    }
}
