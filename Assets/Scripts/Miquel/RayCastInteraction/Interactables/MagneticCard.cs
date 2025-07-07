using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


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

        // Set Interactable Type
        interactableType = InteractableType.MagneticCard;

        // Adding Default Card Perms 
        cardPerms.Add(CardPerms.Archeolab, true);
        cardPerms.Add(CardPerms.Biolab, false);
        cardPerms.Add(CardPerms.Geolab, false);
        cardPerms.Add(CardPerms.Nanolab, false);

        // Change bool to True to Change Card perms and Material
        AddPerms(initialCardPerm);


        // Event Triggers
        #region XR GRAB INTERACTABLE EVENTS
        GetComponent<XRGrabInteractable>().hoverEntered.AddListener(_ => 
        {
            HoverEnter(_.interactorObject.transform.gameObject);
        });

        GetComponent<XRGrabInteractable>().hoverExited.AddListener(_ =>
        {
            HoverExit(_.interactorObject.transform.gameObject);
        });

        GetComponent<XRGrabInteractable>().selectEntered.AddListener(_ =>
        {
            SelectEnter(_.interactorObject.transform.gameObject);
        });

        GetComponent<XRGrabInteractable>().selectExited.AddListener(_ => 
        { 
            SelectExit(_.interactorObject.transform.gameObject);
        });
        #endregion

    }

    public override void SelectEnter(GameObject interactor)
    {
        CardReader.toggleCardReaderRaycast.Invoke(true);
    }

    public override void SelectExit(GameObject interactor)
    {
        if (!isHovering && !isAttatch)
        {
            ReturnToPreviousPosition();
        }

        // Deactivate All Cardreaders
        CardReader.toggleCardReaderRaycast.Invoke(false);
    }

    public override void OnAttach(Transform attachPosition)
    {
        base.OnAttach(attachPosition);

        // This closes first popup
        PopupsManager.Instance.ClosePopup(0); // TEMP
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

        // Finds the Highest card level
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
