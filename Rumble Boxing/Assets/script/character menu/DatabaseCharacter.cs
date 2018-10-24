using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseCharacter : MonoBehaviour {

    private static DatabaseCharacter databaseMachine = new DatabaseCharacter();
    public DatabaseCharacter()
    {
        loadDatabase();
    }

    public static DatabaseCharacter getInstance()
    {
        return databaseMachine;
    }

    private List<ArrayList> arrayOfCharacter;

    private void loadDatabase()
    {
        arrayOfCharacter = new List<ArrayList>()
        {
            new ArrayList() {"Name"},
            new ArrayList() {"Johny"},
            new ArrayList() {"Alex"},
            new ArrayList() {"Captain 'Steve' America"},
            new ArrayList() {"Mad 'Thonas' Titan"}

        };
    }

    public string getName(int _idCharacter)
    {
        return (string)arrayOfCharacter[_idCharacter][0];
    }
}
