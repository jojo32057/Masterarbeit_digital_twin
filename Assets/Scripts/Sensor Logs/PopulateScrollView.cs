using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulateScrollView : MonoBehaviour
{
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    //[SerializeField] private int m_ItemsToGenerate;

    [SerializeField] private Color okColor = Color.green; // standart color "everything ok"
    [SerializeField] private Color warningColor = Color.red; // standart color "warning"
    int i = 0;

 

    public void writeToScrollView(string logContent) 
    {

        i = i + 1;

        var item_go = Instantiate(m_ItemPrefab);
        // do something with the instantiated item -- for instance
        TextMeshProUGUI textComponent = item_go.transform.Find("Button/Text (TMP)").GetComponent<TextMeshProUGUI>();
        textComponent.text = logContent;




        Color itemColor = (i % 5 == 0) ? warningColor : okColor; // logic for warning 
        
        Button buttonComponent = item_go.transform.Find("Button").GetComponent<Button>();
        ColorBlock cb = buttonComponent.colors;
        cb.normalColor = itemColor;
        buttonComponent.colors = cb;


        //parent the item to the content container
        item_go.transform.SetParent(m_ContentContainer);
        //reset the item's scale -> debug reasons
        item_go.transform.localScale = Vector2.one;



    } 
}
