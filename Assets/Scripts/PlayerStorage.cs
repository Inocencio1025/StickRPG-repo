using System.Collections;
using System.Collections.Generic;


public static class PlayerStorage {

    public static List<int> HealthStorage = new List<int>();

    public static List<int> RetrieveInfo()
    {
        return HealthStorage;
    }
}
