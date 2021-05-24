using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicOptionMenu : Singleton<DynamicOptionMenu>
{
    #region Start function
    public void Start()
    {
        openedName = "";
        slotOpened = -1;
    }
    #endregion
    /// <summary>
    /// All the different types
    /// </summary>
    public enum MenuType
    {
        NONE,
        PLAYER,
        NPC,
        GAMEOBJECT,
        ITEM
    }

    #region Item interaction
    public int slotOpened { get; set; }
    #endregion

    #region Game objects filled in inspector
    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject background;
    [SerializeField]
    GameObject content;
    [SerializeField]
    GameObject menu_option_prefab;
    #endregion

    #region Option menu variables
    //The height of a option
    float optionHeight { get; set; }
    //Total height
    float height { get; set; }
    //Highest width of the text of the option
    float highestWidth { get; set; }
    //Checks if a menu is opened
    public bool menuOpened { get; set; }
    //The name of the object that is opened
    string openedName { get; set; }
    public GameObject opened_object { get; set; }
    //Menu type of the menu
    MenuType menuType { get; set; }
    #endregion

    /// <summary>
    /// Handles opening an option menu
    /// </summary>
    /// <param name="options">The options the menu will display</param>
    /// <param name="name">The name it will display with those options</param>
    public void Open(string[] options, GameObject clicked_object, MenuType type, string name = "N.V.T")
    {
        if(openedName.Equals(name))
        {
            Close();
            return;
        }
        //Loops though all the options and adds a button for them
        for (int index = 0; index < options.Length; index++)
        {
            //Spawns the button prefab
            GameObject menu_option = Instantiate(menu_option_prefab);
            //Adds the click function for an option
            menu_option.GetComponent<Button>().onClick.AddListener(() => HandleOption(menu_option.name));
            //Grabs the text component from the child
            Text textComponent = menu_option.GetComponentInChildren<Text>();
            //Sets the menu option as child of the content object
            menu_option.transform.SetParent(content.transform);
            //Sets the name of the menu option
            menu_option.name = options[index] + ":" + index;
            //Sets the text component to the correct thing
            textComponent.text = options[index] + " " + name;
        }

        //Grabs the height of a button
        optionHeight = content.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;

        //Calculated the new height of the background
        height = optionHeight * content.transform.childCount;

        //Sets the rect transform
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height + 22);
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(rectTransform.sizeDelta.x, height + 22);

        //Loops though all the children of content to grab the highest width
        foreach (Transform child in content.transform)
        {
            Text text = child.GetComponentInChildren<Text>();
            if (text.preferredWidth > highestWidth) highestWidth = text.preferredWidth + 6;

        }

        //Sets the size of the transfrom (main object)
        rectTransform.sizeDelta = new Vector2(highestWidth, height + 34);
        //Sets the size of the background
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(highestWidth, height + 22);
        //Sets the size of the devider line
        transform.GetChild(0).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(highestWidth, 2);
        //Sets the size of the content box
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(highestWidth, height);

        //Sets the size of all buttons
        for (int index = 0; index < content.transform.childCount; index++)
        {
            RectTransform rect = content.transform.GetChild(index).GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(highestWidth, rect.sizeDelta.y);
        }

        //Puts the menu in the correcet position
        transform.position = new Vector3(Input.mousePosition.x - (highestWidth / 2), Input.mousePosition.y + 5); // - 5
        
        menu.SetActive(true);
        menuOpened = true;
        openedName = name;
        opened_object = clicked_object;
        menuType = type;
    }

    /// <summary>
    /// Clicking handler of an menu option
    /// </summary>
    /// <param name="name">Name of the object</param>
    public void HandleOption(string name)
    {
        //Grabs what option you clicked
        int index = int.Parse(name[name.Length - 1].ToString());

        Debug.Log($"name: {name}, index: {index}, openedName: {openedName}");

        switch(menuType)
        {
            case MenuType.PLAYER:
                break;
            case MenuType.NPC:
                break;
            case MenuType.ITEM:
                break;
            case MenuType.GAMEOBJECT:
                break;
        }

        Close();
    }

    /// <summary>
    /// Closes an option menu
    /// </summary>
    public void Close(int test = 0)
    {
        //Debug.Log("test: " + test);
        //Removes all the children from the content object
        foreach(Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        //Sets the menu to false
        menu.SetActive(false);
        //Makes it so the menu is not open anymore
        menuOpened = false;
        //Sets the opened name to null
        openedName = "";
        //Reset the interactable if the menu type is a item
        if (menuType.Equals(MenuType.ITEM))
        {
            slotOpened = -1;
            //Client.Instance().local_player.interactable = null;
        }
        opened_object = null;
        //Resets the menu type to none
        menuType = MenuType.NONE;
    }
}
