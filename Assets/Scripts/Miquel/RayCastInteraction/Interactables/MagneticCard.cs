using System.Collections.Generic;
using UnityEngine;


// Permissions to access the Labs, order in which the labs are unlocked. switch order if needed
public enum CardPerms { Archeolab = 0, Biolab = 1, Geolab = 2, Nanolab = 3 }

public class MagneticCard : AttachableObject
{
    //private static MagneticCard instance;
    //public static MagneticCard Instance { get { if (instance == null) { instance = new MagneticCard(); } return instance;  } }

    [SerializeField] CardPerms initialCardPerm;

    Dictionary<CardPerms, bool> cardPerms = new Dictionary<CardPerms, bool>();

    // This list must follow the CardPerms enum order
    [SerializeField] List<Material> cardMaterials = new List<Material>();

    protected override void Awake()
    {
        base.Awake();

        //instance = this;

        // Set Interactable Type
        interactableType = InteractableType.MagneticCard;

        // Adding Card Perms 
        cardPerms.Add(CardPerms.Archeolab, true);
        cardPerms.Add(CardPerms.Biolab, false);
        cardPerms.Add(CardPerms.Geolab, false);
        cardPerms.Add(CardPerms.Nanolab, false);

        // Change bool to True to Change Card perms and Material
        AddPerms(initialCardPerm);
    }

    public override void Grab()
    {
        CardReader.toggleCardReaderRaycast.Invoke(true);
    }

    public override void Cancel()
    {
        if (!isHovering && !isAttatch)
        {
            Debug.Log("ENTERED CANCEL CARD");
            ReturnToPreviousPosition();
        }

        CardReader.toggleCardReaderRaycast.Invoke(false);
    }

    public bool CheckPerm(CardPerms cardPerm)
    {
        return cardPerms[cardPerm];
    }    

    public void AddPerms(CardPerms cardPerm)
    {
        cardPerms[cardPerm] = true;
        ChangeMaterial(cardPerm);
    }

    public void RemovePerms(CardPerms cardPerm)
    {
        cardPerms[cardPerm] = false;
    }

    private void ChangeMaterial(CardPerms cardPerm)
    {
        CardPerms highestCardPerm = CardPerms.Archeolab;

        foreach (var perm in cardPerms)
        {
            if (perm.Value)
            {
                highestCardPerm = perm.Key;
            }
        }

        if ((int)highestCardPerm > (int)cardPerm)
        { return; }

        GetComponent<Renderer>().material = cardMaterials[(int)cardPerm];
    }
}
