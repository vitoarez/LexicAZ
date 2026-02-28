using UnityEngine;
using System.IO;
using SQLite4Unity3d;

public class DataBaseManager : MonoBehaviour
{
    public static SQLiteConnection DB;

    private static bool _initialized = false;

    private void Awake()
    {
        // Singleton: si ya hay una BD inicializada, nos destruimos.
        if (_initialized)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        InitDatabase();
        _initialized = true;
    }

    private void InitDatabase()
    {
        string fileName = "lexicaz.db";

        string sourcePath = Path.Combine(Application.streamingAssetsPath, fileName);
        string targetPath = Path.Combine(Application.persistentDataPath, fileName);

#if UNITY_EDITOR
        Debug.Log("EDITOR: copiando SIEMPRE BD desde StreamingAssets a: " + targetPath);
        File.Copy(sourcePath, targetPath, true);   // sobreescribir siempre
#elif UNITY_ANDROID
        Debug.Log("ANDROID: copiando SIEMPRE BD desde StreamingAssets a: " + targetPath);
        var www = new WWW(sourcePath);
        while (!www.isDone) { }

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("Error copiando BD desde StreamingAssets: " + www.error);
        }
        else
        {
            File.WriteAllBytes(targetPath, www.bytes);
        }
#else
        Debug.Log("PC: copiando SIEMPRE BD desde StreamingAssets a: " + targetPath);
        File.Copy(sourcePath, targetPath, true);
#endif

        long size = new FileInfo(targetPath).Length;
        Debug.Log("Tamaño BD copiada: " + size + " bytes");

        DB = new SQLiteConnection(targetPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("BD cargada correctamente desde: " + targetPath);

        LogTablas();
    }

    private void LogTablas()
    {
        try
        {
            var tablas = DB.Query<TableNameRow>("SELECT name FROM sqlite_master WHERE type='table';");
            if (tablas.Count == 0)
            {
                Debug.LogWarning("⚠ BD sin tablas (sqlite_master está vacío).");
            }
            foreach (var t in tablas)
            {
                Debug.Log("📂 Tabla en BD: " + t.name);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error listando tablas: " + ex);
        }
    }

    private class TableNameRow
    {
        public string name { get; set; }
    }
}
