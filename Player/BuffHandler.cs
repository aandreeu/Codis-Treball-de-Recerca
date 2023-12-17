using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public PlayerInventory inventory;
    public MeleeCombat meleeCombatScript;
    [SerializeField] bool IsBuffed;
    public float timer;
    

    private void Update()
    {
        //if (IsBuffed) //Si esta bufeat el temps,baixa. si aquest arriba a 0 deixa destar buffeat i tornem el valor buffejat al normal.
        //{
        //    timer -= Time.deltaTime;

        //    if(timer<=0) { 
        //        IsBuffed= false;
        //        float mult = inventory.inventory.Container[0].item.multiplier1;
        //        meleeCombatScript.DebuffDamageAttack(mult);
        //    }

        //}
        
        //if (Input.GetKeyDown(KeyCode.Alpha1) && !IsBuffed && timer<=0 && inventory.inventory.Container[0].amount > 0) 
        //    //Si apreta 1, no esta buffejat, el temps es menor que 0, i hi ha quantitat dallo a linventari
        //{
        //    timer= inventory.inventory.Container[0].item.EfectTime; //El temps es el temps dus de la droga
        //    IsBuffed= true;
        //    float mult = inventory.inventory.Container[0].item.multiplier1;
        //    meleeCombatScript.BuffDamageAttack(mult); //Passar el multiplicador
        //    inventory.inventory.Container[0].amount--; //Restar allo a linventari
        //}
    }

}
