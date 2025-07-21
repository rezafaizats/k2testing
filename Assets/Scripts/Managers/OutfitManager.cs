using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Outfit
{
    public string outfitName;
    public List<GameObject> meshOutfit;
}

public class OutfitManager : MonoBehaviour
{
    public List<Outfit> outfits = new List<Outfit>();
    public UnityEvent<string> onOutfitChanged;
    private int currentOutfitIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var outfit in outfits)
            foreach (var mesh in outfit.meshOutfit)
                mesh.SetActive(false);

        foreach (var mesh in outfits[currentOutfitIndex].meshOutfit)
            mesh.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0)) NextOutfit();
        // if (Input.GetMouseButtonDown(1)) PreviousOutfit();
    }

    public void NextOutfit()
    {
        foreach (var mesh in outfits[currentOutfitIndex].meshOutfit)
            mesh.SetActive(false);
        currentOutfitIndex++;
        if (currentOutfitIndex >= outfits.Count) currentOutfitIndex = 0;
        for (int i = 0; i < outfits[currentOutfitIndex].meshOutfit.Count; i++)
        {
            outfits[currentOutfitIndex].meshOutfit[i].SetActive(true);
        }
        onOutfitChanged?.Invoke(outfits[currentOutfitIndex].outfitName);
    }

    public void PreviousOutfit()
    {
        foreach (var mesh in outfits[currentOutfitIndex].meshOutfit)
            mesh.SetActive(false);
        currentOutfitIndex--;
        if (currentOutfitIndex < 0) currentOutfitIndex = outfits.Count - 1;
        for (int i = 0; i < outfits[currentOutfitIndex].meshOutfit.Count; i++)
        {
            outfits[currentOutfitIndex].meshOutfit[i].SetActive(true);
        }
        onOutfitChanged?.Invoke(outfits[currentOutfitIndex].outfitName);
    }
}
