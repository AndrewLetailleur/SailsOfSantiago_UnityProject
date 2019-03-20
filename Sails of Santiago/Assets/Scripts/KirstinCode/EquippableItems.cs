using UnityEngine;
//this is a hack attempt to get "a" equip items to work. If it works, thank Kirstin for the code.

public enum EquipType {
    Gunfire,
    Special,
    Hull,
    Sail,
}

public class EquipabbleItems : MonoBehaviour {//for the short term, since it does NOT let it be named as "Items" at the end
    public int GunLV = 1;
    public GameObject[] GunArray;
    private int GunMX;//after max amount of array

    public int SpecLV = 1;// = 5;
    public GameObject[] SpecArray;
    private int SpecMX;// = 3;

    public float Mx_Hull = 100;
    private float Cp_Hull = 400; // to set a "cap" at X4

    public float Mx_Sail = 100;
    private float Cp_Sail = 400; // to set a "cap" at X4



    private void Start()
    {
        GunMX = GunArray.Length;
        SpecMX = SpecArray.Length;
    }


    public float Upgrade_Sail(float value) {
        if (Mx_Sail + value >= Cp_Sail)
            Mx_Sail = Cp_Sail;
        else if (Mx_Sail + value <= 100)
            Mx_Sail = 100;
        //endif hack
        return Mx_Sail;
    }

    public float Upgrade_Hull(float value)
    {
        if (Mx_Hull + value >= Cp_Hull)
            Mx_Hull = Cp_Hull;
        else if (Mx_Hull + value <= 100)
            Mx_Hull = 100;
        //endif hack
        return Mx_Hull;

    }
    /*
    public void Upgrade_Gun()
    {
        if (GunLV++ <= GunMX) {
            GunLV++;
        }

    }*/
}

/* OLD CODE, WAS BUGGY
public enum EquipmentType
{
    Handgun,
    Bullet,
    Gun, 
    Bazooka,

}
[CreateAssetMenu]
public class EquippableItems : Items {

    public int StrengthBonus;
    public int AgilityBonus;
    public int IntellienceBonus;
    public int VitalityBonus;
    [Space]


}*/


