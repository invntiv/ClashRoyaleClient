//Make sure to add this namespace, it is not included in the tutorial
using System.Text.RegularExpressions;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour {

    [SerializeField] private InputField username;
    [SerializeField] private InputField password;
    [SerializeField] private InputField repeatPassword;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Trying to register and account on the server
    public void RegisterAccount()
    {
        if (username.text == string.Empty) { Debug.Log("Please enter a username."); return; }
        if (password.text == string.Empty) { Debug.Log("Please enter a password."); return; }
        if (password.text != repeatPassword.text) { Debug.Log("Your passwords do not match!"); return; }

        if (!(validateCharacters(username.text) && validateCharacters(password.text)))
        {
            return;
        }
       

        ClientTCP.PACKAGE_NewAccount(username.text, password.text);
        Debug.Log("Sending Account Information to Server...");

    }

    private bool validateCharacters(string input)
    {   
        //// Check username and password strings for invalid characters to help prevent SQL Injection
        //   Check username
        Regex regex = new Regex("^[a-zA-Z0-9_]+$");
        if (!regex.IsMatch(username.text))
        {
            Debug.Log("Invalid Username! Please use only 'A-Z', '0-9', or '_'.");
            return false;
        }
        //  Check password
        else if (!regex.IsMatch(password.text))
        {
            Debug.Log("Invalid Password! Please use only 'A-Z, '0-9', or '_'.");
            return false;
        }
        else
        {
            return true;
        }
        
    }
}
