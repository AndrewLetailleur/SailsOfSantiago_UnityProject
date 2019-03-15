using UnityEngine;

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


}


