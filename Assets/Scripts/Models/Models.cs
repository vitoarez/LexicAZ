using SQLite4Unity3d;

// Mapea con la tabla "Languages"
[Table("Languages")]
public class Language
{
    [PrimaryKey]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("code")]
    public string Code { get; set; }
}

// Mapea con la tabla "Groups"
[Table("Groups")]
public class WordGroup
{
    [PrimaryKey]
    [Column("id")]
    public int Id { get; set; }

    // Opcional: solo si quieres usar la columna category
    [Column("category")]
    public string Category { get; set; }
}

// Mapea con la tabla "Words" (IMPORTANTE: plural)
[Table("Words")]
public class Word
{
    [PrimaryKey]
    [Column("id")]
    public int Id { get; set; }

    [Column("language_id")]
    public int LanguageId { get; set; }

    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("text")]
    public string Text { get; set; }
}
