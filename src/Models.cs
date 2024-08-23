using UndertaleModLib.Models;

class GameObjectData
{
    public UndertaleGameObject undertaleGameObject { get; set; }
    public List<CodeData> code { get; set; }
    public bool spriteIsCustom { get; set; }
}

class CodeData
{
    public string path { get; set; }
    public UndertaleCode undertaleCode { get; set; }
    public EventType eventType { get; set; }
    public uint eventSubType { get; set; }
}