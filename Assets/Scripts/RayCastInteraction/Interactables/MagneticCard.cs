using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;


// Permissions to access the Labs
public enum CardPerms { Archeolab, Biolab, Geolab, Nanolab }

public class MagneticCard : RaycastInteractable
{
    //private static MagneticCard instance;
    //public static MagneticCard Instance { get { if (instance == null) { instance = new MagneticCard(); } return instance;  } }

    Dictionary<CardPerms, bool> cardPerms = new Dictionary<CardPerms, bool>();

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
    }

    public override void Grab()
    {
        CardReader.toggleCardReaderRaycast.Invoke(true);
    }

    public override void Cancel()
    {
        CardReader.toggleCardReaderRaycast.Invoke(false);
    }

    public bool CheckPerm(CardPerms cardPerm)
    {
        return cardPerms[cardPerm];
    }    

    public void AddPerms(CardPerms cardPerm)
    {
        cardPerms[cardPerm] = true;
    }

    public void RemovePerms(CardPerms cardPerm)
    {
        cardPerms[cardPerm] = false;
    }
}
