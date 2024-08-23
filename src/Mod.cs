using GMSL;
using GMHooker;
using UndertaleModLib;
using WysApi.Api;
using UndertaleModLib.Models;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Runtime.InteropServices;
using System.Linq;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Compiler;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ComponentModel;
using willyouload;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks.Dataflow;
using WillYouLoad;
using Newtonsoft.Json;

namespace willyouload.src;

public class WillYouLoad : IGMSLMod
{

    public Dictionary<string, string> files = new Dictionary<string, string>();

    public static UndertaleData data;

    public static string baseDirectory = "UNDEFINED_BASE_DIRECTORY";
    public string appDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Will_You_Snail");
    public string assetPath;
    public string modsPath;
    public static bool failedHook = false;
    public void Load(UndertaleData moddingData, ModInfo modInfo)
    {

        data = moddingData;


        baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        assetPath = Path.Combine(baseDirectory, "assets");
        modsPath = Path.Combine(appDataDirectory, "Community_Mods");



        Console.WriteLine($"[WYSmodLoader]: Adding objects...");
        AddObjects();

        Console.WriteLine($"[WYSmodLoader]: Loading code from files...");
        files = LoadCodeFromFiles(Path.Combine(assetPath, "code"));




        Console.WriteLine($"[WYSmodLoader]: Adding code...");
        LoadCode();

        Console.WriteLine($"[WYSmodLoader]: Loading user mods...");
        LoadMods();

        Console.WriteLine($"[WYSmodLoader]: Adding code...");
        AddCodeAfterMods();

        data.FinalizeHooks();
    }


    public void LoadMods()
    {
        string[] modFolders = Directory.GetDirectories(modsPath);

        string scr_loadAssetsAsLocals_string = "";
        List<GameObjectData> gameObjects = new List<GameObjectData>();
        foreach (string modFolderPath in modFolders)
        {
            Console.WriteLine("Loading user mod " + modFolderPath);
            string[] modFiles = Directory.GetFileSystemEntries(modFolderPath);

            List<string> jsonPaths = new List<string>();
            List<string> spritePaths = new List<string>();
            List<string> soundPaths = new List<string>();


            foreach (string file in modFiles)
            {
                Console.WriteLine(file);
                if (file.EndsWith(".json"))
                {
                    jsonPaths.Add(file);
                }
            }


            foreach (string jsonF in jsonPaths)
            {
                Console.WriteLine("Loading json " + jsonF);
                string jsonString = File.ReadAllText(jsonF);
                var jsonData = JsonConvert.DeserializeObject<dynamic>(jsonString);
                string jsonFLocation = Path.GetDirectoryName(jsonF);
                if (jsonData != null)
                {
                    if (jsonData.sprites != null)
                    {
                        foreach (var sprite in jsonData.sprites)
                        {
                            string spritePath = FindFileInOneOfTheseDirectories(
                                new string[]{
                                    jsonFLocation,
                                    modFolderPath
                                },
                                sprite.path
                            );
                            if (spritePath != null)
                            {
                                spritePaths.Add(spritePath);
                                scr_loadAssetsAsLocals_string += sprite.name + " = global.wysModLoader_sprite_" + sprite.name + "\n";
                            }
                            else
                            {
                                Console.WriteLine("Could not find sprite path " + sprite.path + " in json " + jsonF);
                            }
                        }
                    }
                    if (jsonData.sounds != null)
                    {
                        foreach (var sound in jsonData.sounds)
                        {
                            string soundPath = FindFileInOneOfTheseDirectories(
                                new string[]{
                                    jsonFLocation,
                                    modFolderPath
                                },
                                sound.path
                            );

                            scr_loadAssetsAsLocals_string += sound.name + " = global.wysModLoader_sound_" + sound.name + "\n";
                            if (soundPath != null)
                            {
                                soundPaths.Add(soundPath);
                            }
                            else
                            {
                                Console.WriteLine("Could not find sprite path " + sound.path + " in json " + jsonF);
                            }
                        }
                    }
                    if (jsonData.game_objects != null)
                    {
                        foreach (var objData in jsonData.game_objects)
                        {
                            string parentName = objData.parent;
                            UndertaleGameObject parentId = data.GameObjects.ByName(parentName);

                            bool solid = objData.solid;

                            string spriteName = objData.sprite;
                            bool spriteIsCustom = false;

                            UndertaleSprite sprite = null;
                            if (spriteName != null)
                            {
                                sprite = data.Sprites.ByName(spriteName);
                                if (sprite == null)
                                {
                                    spriteIsCustom = true;
                                }
                            }
                            else
                            {
                            }

                            string objName = objData.name;
                            if (objName == null)
                            {
                                Console.WriteLine("One of the objects located in " + jsonF + " does not have a name.");
                                continue;
                            }

                            UndertaleGameObject newGameObject = new UndertaleGameObject
                            {
                                Name = data.Strings.MakeString(objName),
                                ParentId = parentId,
                                Solid = solid,
                                Sprite = sprite
                            };
                            data.GameObjects.Add(newGameObject);


                            List<CodeData> codeDatas = new List<CodeData>();

                            foreach (var code in objData.code)
                            {
                                string codePath = FindFileInOneOfTheseDirectories(
                                    new string[]{
                                        jsonFLocation,
                                        modFolderPath
                                    },
                                    code.path
                                );

                                if (codePath != null)
                                {
                                    string codeFContents = File.ReadAllText(codePath);
                                    var type = (EventType)Enum.Parse(typeof(EventType), code.Type);
                                    uint subtype;

                                    if (code.subtype == null) subtype = 0;
                                    else
                                    {
                                        try
                                        {
                                            subtype = (uint)Enum.Parse(FindType("UndertaleModLib.Models.EventSubtype" + code.Type), code.Subtype);
                                        }
                                        catch
                                        {
                                            subtype = uint.Parse(code.Subtype);
                                        }
                                    }


                                    UndertaleCode newCode = newGameObject.EventHandlerFor(type, subtype, data.Strings, data.Code, data.CodeLocals);

                                    newCode.ReplaceGML(codeFContents, data);

                                    codeDatas.Add(new CodeData
                                    {
                                        path = codePath,
                                        undertaleCode = newCode,
                                        eventType = type,
                                        eventSubType = subtype
                                    });

                                }
                                else
                                {
                                    Console.WriteLine("Could not find code file " + code.path + " in object " + objName);
                                }
                            }

                            GameObjectData newObjData = new GameObjectData
                            {
                                undertaleGameObject = newGameObject,
                                spriteIsCustom = spriteIsCustom,
                                code = codeDatas
                            };

                            gameObjects.Add(newObjData);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Could not load json: " + jsonF);
                }
            }


        }

        if (scr_loadAssetsAsLocals_string.Trim() == "")
        {
            scr_loadAssetsAsLocals_string = "var somethingBecauseHavingAnEmptyFunctionBreaksUMTApparently = 69420";
        }
        data.CreateFunction("scr_wysModLoader_loadAssetsAsLocals", scr_loadAssetsAsLocals_string, 0);

        foreach (GameObjectData obj in gameObjects)
        {
            UndertaleGameObject gameObject = obj.undertaleGameObject;

            string createCodeName = "gml_Object_" + gameObject.Name.Content + "_Create_0";

            if (data.Code.ByName(createCodeName) != null)
            {
                data.HookCode(createCodeName, "gml_Script_scr_wysModLoader_loadAssetsAsLocals()\n\n#orig#()");
            }
            else
            {
                gameObject.EventHandlerFor(EventType.Create, data)
                .ReplaceGML("gml_Script_scr_wysModLoader_loadAssetsAsLocals()", data);
            }
        }
    }

    private static Type? FindType(string qualifiedTypeName)
    {
        Type? t = Type.GetType(qualifiedTypeName);

        if (t != null) return t;

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            t = asm.GetType(qualifiedTypeName);
            if (t != null)
                return t;
        }
        return null;
    }


    public string FindFileInOneOfTheseDirectories(string[] directories, string path)
    {
        foreach (string dirPath in directories)
        {
            string newPath = Path.Combine(dirPath, path);
            if (File.Exists(newPath))
            {
                return newPath;
            }
        }
        return null;
    }
    private static void LoadCode()
    {

        var directoryJsonPath = Path.Combine(baseDirectory, "code", "directories.json");
        var directories = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(directoryJsonPath));
        if (directories == null) return;


        var handlers = SetupHandlers();

        foreach (var directory in directories)
        {
            var path = Path.Combine(baseDirectory, "code", directory.Key);
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Cant find {path} skipping...");
            }
            else
            {
                /*
                if (directory.Value == "custom_tools")
                {
                    foreach (string file in GetFilesRecursively(path))
                    {
                        var code = File.ReadAllText(file);
                        code = ReplaceMacros(code);

                        handlers["functions"].Invoke(code, file);
                    }
                }
                //Might add this at some point, but for now it's unsupported.
                */
                if (!handlers.ContainsKey(directory.Value))
                {
                    Console.WriteLine($"Path {path} has handler {directory.Value} which isn't in handlers");
                }
                else
                {
                    foreach (var file in Directory.GetFiles(path))
                    {
                        var code = File.ReadAllText(file);
                        code = ReplaceMacros(code);

                        handlers[directory.Value].Invoke(code, file);
                    }
                }
            }
        }


    }
    //Thanks to NameTheMPGuy for writing this code-loading code, originally used for Shellworks!
    private static Dictionary<string, Action<string, string>> SetupHandlers()
    {
        var handlers = new Dictionary<string, Action<string, string>>();

        handlers.Add("functions", (code, file) =>
        {
            var functionName = Path.GetFileNameWithoutExtension(file);

            MatchCollection matchList = Regex.Matches(code, @"(?<=argument)\d+");
            ushort argCount;
            if (matchList.Count > 0)
                argCount = (ushort)(matchList.Cast<Match>().Select(match => ushort.Parse(match.Value)).ToList().Max() + 1);
            else
                argCount = 0;
            Console.WriteLine("Creating new function " + functionName + " with argument count " + argCount.ToString());
            data.CreateFunction(functionName, code, argCount);
        });

        handlers.Add("codehooks", (code, file) =>
        {
            Console.WriteLine("Hooking to object code " + Path.GetFileNameWithoutExtension(file));
            data.HookCode(Path.GetFileNameWithoutExtension(file), code);
        });

        handlers.Add("functionhooks", (code, file) =>
        {
            Console.WriteLine("Hooking to function " + Path.GetFileNameWithoutExtension(file));
            data.HookFunction(Path.GetFileNameWithoutExtension(file), code);
        });

        handlers.Add("objectcode", (code, file) =>
        {
            if (file.EndsWith(".gml")) return;
            Console.WriteLine("Adding new object code " + Path.GetFileNameWithoutExtension(file));

            var objectFile = JsonConvert.DeserializeObject<ObjectFile>(code);
            if (objectFile == null) return;

            var type = (EventType)Enum.Parse(typeof(EventType), objectFile.Type);
            uint subtype;

            if (!objectFile.HasSubtype) subtype = uint.Parse(objectFile.Subtype);
            else subtype = (uint)Enum.Parse(FindType("UndertaleModLib.Models.EventSubtype" + objectFile.Type), objectFile.Subtype);


            string code_str = File.ReadAllText(Path.Combine(Path.GetDirectoryName(file), objectFile.File));
            code_str = ReplaceMacros(code_str);

            data.GameObjects.ByName(objectFile.Object)
                .EventHandlerFor(type, subtype, data.Strings, data.Code, data.CodeLocals)
                .ReplaceGML(code_str, data);
        });

        handlers.Add("inlinehooks", (code, file) =>
        {
            if (file.EndsWith(".gml")) return;

            var hookFile = JsonConvert.DeserializeObject<HookFile>(code);
            if (hookFile == null) return;

            Console.WriteLine("Adding inline hook(s) to " + file);


            foreach (HookFile_Hook hookData in hookFile.Hooks)
            {
                UndertaleCode undertaleCode = data.Code.ByName(hookFile.Script);

                string assembly_str = undertaleCode.Disassemble(data.Variables, data.CodeLocals.For(undertaleCode));
                List<string> lines = assembly_str.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                bool foundAtAll = false;
                for (var l = 0; l < lines.Count; l++)
                {
                    int cur_l = l;
                    int before = l;
                    var found = true;
                    for (int i = 0; i < hookData.Sig.Length && cur_l < lines.Count; i++)
                    {
                        if (lines[cur_l] == hookData.Sig[i]) { cur_l++; continue; }
                        found = false;
                        break;
                    }

                    if (!found) continue;

                    foundAtAll = true;
                    string fileContents = File.ReadAllText(Path.Combine(baseDirectory, "code", hookData.File));
                    string scriptName = Path.GetFileNameWithoutExtension(hookData.File);
                    string functionArgs = "";
                    for (int a = 0; a < hookData.InArgs.Length; a++)
                    {
                        var argument = hookData.InArgs[a];
                        if (!argument.StartsWith("local"))
                        {
                            if (a < hookData.InArgs.Length - 1)
                            {
                                functionArgs += argument.Replace("arg.", "") + ", ";
                            }
                            else
                            {
                                functionArgs += argument.Replace("arg.", "");
                            }
                        }
                    }
                    Console.WriteLine("Adding inlineHook function for " + Path.GetFileNameWithoutExtension(hookData.File));
                    string hookStr = "function " + scriptName + "(" + functionArgs + "){\n" + fileContents + "\n}";
                    if (data.Code.ByName(Path.GetFileNameWithoutExtension(hookData.File)) == null)
                    {
                        data.CreateLegacyScript(Path.GetFileNameWithoutExtension(hookData.File), hookStr, (ushort)hookData.InArgs.Length);
                    }
                    if (hookData.Type == "replace")
                    {
                        lines.RemoveRange(before, hookData.Sig.Length);
                    }
                    int insertionIndex = hookData.Type == "append" ? cur_l : before;
                    int curInsertIndex = insertionIndex;
                    foreach (var argument in hookData.InArgs)
                    {
                        lines.Insert(curInsertIndex, argument.StartsWith("local") ? $"pushloc.v {argument}" : $"push.v {argument}");
                        curInsertIndex++;
                    }
                    int argCount = hookData.InArgs.Length;
                    lines.Insert(curInsertIndex, $"call.i gml_Script_{scriptName}(argc={argCount})");
                    curInsertIndex++;
                    lines.Insert(curInsertIndex, "popz.v");
                    curInsertIndex++;
                    string assemblyStr_out = "";
                    foreach (string li in lines)
                    {
                        assemblyStr_out += li + "\n";
                    }
                    assemblyStr_out = ReplaceAssetsWithIndexes_ASM(assemblyStr_out);
                    undertaleCode.Replace(Assembler.Assemble(assemblyStr_out, data));
                    if (hookData.Type == "prepend")
                    {
                        l = curInsertIndex;
                    }
                }

                if (!foundAtAll)
                {

                    string assemblyLookedFor = "";
                    foreach (string l in hookData.Sig)
                    {
                        assemblyLookedFor += l + "\n";
                    }
                    Console.WriteLine("\n\nWARNING: could not find place to assembly hook for " + Path.GetFileNameWithoutExtension(file) + "\n" + assemblyLookedFor + "\n\n\n");
                    failedHook = true;
                }
            }
        });

        handlers.Add("inlineassemblyhooks", (code, file) =>
        {
            if (file.EndsWith(".gml") || file.EndsWith(".asm")) return;

            var hookFile = JsonConvert.DeserializeObject<HookFile_Asm>(code);
            if (hookFile == null) return;

            Console.WriteLine("Adding inline assembly hook(s) to " + file);


            foreach (HookFile_Hook_Asm hookData in hookFile.Hooks)
            {
                UndertaleCode undertaleCode = data.Code.ByName(hookFile.Script);

                string assembly_str = undertaleCode.Disassemble(data.Variables, data.CodeLocals.For(undertaleCode)).Replace("\r\n", "\n").Replace("\r", "\n");


                string find = hookData.ToFind;

                string replace = hookData.ToReplace;
                if (hookData.IsExternalFile)
                {
                    replace = File.ReadAllText(Path.Combine(baseDirectory, "code", hookData.File));
                }

                if (assembly_str.Contains(find))
                {
                    string assemblyStr_out = assembly_str.Replace(find, replace);

                    assemblyStr_out = ReplaceAssetsWithIndexes_ASM(assembly_str);

                    undertaleCode.Replace(Assembler.Assemble(assemblyStr_out, data));
                }
                else
                {
                    Console.WriteLine("\n\nWARNING: could not find place to assembly inline hook for " + Path.GetFileNameWithoutExtension(file) + "\n\n\n");
                    failedHook = true;
                }
            }
        });

        handlers.Add("inlinereplacements", (code, file) =>
        {
            if (file.EndsWith(".gml")) return;

            var hookFile = JsonConvert.DeserializeObject<ReplaceFile>(code);
            if (hookFile == null) return;

            Console.WriteLine("Adding inline replacement(s) for " + Path.GetFileNameWithoutExtension(file));


            foreach (ReplaceFile_Replacement replaceData in hookFile.Replacements)
            {
                UndertaleCode undertaleCode = data.Code.ByName(hookFile.Script);

                string assembly_str = undertaleCode.Disassemble(data.Variables, data.CodeLocals.For(undertaleCode)).Replace("\r\n", "\n").Replace("\r", "\n").Replace("\t", "").Replace(" ", "");
                GlobalDecompileContext globalDecompileContext = new GlobalDecompileContext(data, false);
                string decompiledStr = Decompiler.Decompile(undertaleCode, globalDecompileContext);


                string find = replaceData.ToFind;

                string replace = replaceData.ToReplace;

                if (replaceData.FindIsExternalFile)
                {
                    find = File.ReadAllText(Path.Combine(baseDirectory, "code", replaceData.FindFile)).Trim();
                }
                if (replaceData.ReplaceIsExternalFile)
                {
                    replace = File.ReadAllText(Path.Combine(baseDirectory, "code", replaceData.ReplaceFile)).Trim();
                }
                //find = find.Replace("\r\n", "\n").Replace("\r", "\n");
                //replace = replace.Replace("\r\n", "\n").Replace("\r", "\n");

                try
                {
                    string decompiledStr_out = decompiledStr.Replace(find, replace);

                    undertaleCode.ReplaceGmlSafe(decompiledStr_out, data);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                    Console.WriteLine("\n\nWARNING: could not find place to put inline replacement for " + Path.GetFileNameWithoutExtension(file) + "\n\n\n");
                    failedHook = true;
                }
            }
        });

        return handlers;
    }

    private static string ReplaceAssetsWithIndexes_ASM(string assemblyStr)
    {
        string assemblyStr_out = assemblyStr;

        Regex regex = new Regex("UNINITIALIZED_PATTERN");

        MatchCollection objMatches = Regex.Matches(assemblyStr_out, @"#OBJECT_INDEX#\((.*)\)");
        foreach (Match match in objMatches)
        {
            assemblyStr_out = assemblyStr_out.Replace(@$"#OBJECT_INDEX#({match.Groups[1].Value})", data.GameObjects.IndexOf(data.GameObjects.ByName(match.Groups[1].Value)).ToString());
        }

        MatchCollection spriteMatches = Regex.Matches(assemblyStr_out, @"#SPRITE_INDEX#\((.*)\)");
        foreach (Match match in spriteMatches)
        {
            assemblyStr_out = assemblyStr_out.Replace(@$"#SPRITE_INDEX#({match.Groups[1].Value})", data.Sprites.IndexOf(data.Sprites.ByName(match.Groups[1].Value)).ToString());

        }

        MatchCollection soundMatches = Regex.Matches(assemblyStr_out, @"#SOUND_INDEX#\((.*)\)");
        foreach (Match match in soundMatches)
        {
            assemblyStr_out = assemblyStr_out.Replace(@$"#SOUND_INDEX#({match.Groups[1].Value})", data.Sounds.IndexOf(data.Sounds.ByName(match.Groups[1].Value)).ToString());

        }

        MatchCollection stringMatces = Regex.Matches(assemblyStr_out, @"""(.*)""@(.*)$");
        foreach (Match match in stringMatces)
        {
            assemblyStr_out = assemblyStr_out.Replace(@$"""{match.Groups[1].Value}""@{match.Groups[2].Value}", @$"""{match.Groups[1].Value}""@{data.Strings.IndexOf(data.Strings.MakeString(match.Groups[1].Value))}");

        }


        return assemblyStr_out;
    }


    private static string ReplaceMacros(string code)
    {
        var macroJsonPath = Path.Combine(baseDirectory, "code", "macros.json");
        var macros = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(macroJsonPath));

        // We do a double pass to try to replace nested macros
        code = macros.Aggregate(code, (current, macro) => current.Replace(macro.Key, macro.Value));
        return macros.Aggregate(code, (current, macro) => current.Replace(macro.Key, macro.Value));
    }

    public static string[] GetFilesRecursively(string path)
    {
        List<string> pathsSoFar = new List<string>();

        foreach (var file in Directory.GetFiles(path))
        {
            pathsSoFar.Add(file);
        }

        foreach (var dir in Directory.GetDirectories(path))
        {
            pathsSoFar = GetFilesRecursively_Internal(dir, pathsSoFar);
        }

        return pathsSoFar.ToArray();
    }


    public static List<string> GetFilesRecursively_Internal(string path, List<string> pathsSoFar)
    {
        foreach (var file in Directory.GetFiles(path))
        {
            pathsSoFar.Add(file);
        }

        foreach (var dir in Directory.GetDirectories(path))
        {
            pathsSoFar = GetFilesRecursively_Internal(dir, pathsSoFar);
        }

        return pathsSoFar;
    }


    public void AddCodeAfterMods()
    {

    }

    public void AddObjects()
    {
    }



    public UndertaleGameObject NewObject(string objectName, UndertaleSprite sprite = null, bool visible = true, bool solid = false, bool persistent = false, UndertaleGameObject parentObject = null)
    {
        UndertaleString name = new UndertaleString(objectName);
        UndertaleGameObject newObject = new UndertaleGameObject()
        {
            Sprite = sprite,
            Persistent = persistent,
            Visible = visible,
            Solid = solid,
            Name = name,
            ParentId = parentObject
        };

        data.Strings.Add(name);
        data.GameObjects.Add(newObject);

        return newObject;
    }

    public UndertaleRoom.GameObject AddObjectToRoom(string roomName, UndertaleGameObject objectToAdd, string layerName)
    {
        UndertaleRoom room = GetRoomFromData(roomName);

        UndertaleRoom.GameObject object_inst = new UndertaleRoom.GameObject()
        {
            InstanceID = data.GeneralInfo.LastObj,
            ObjectDefinition = objectToAdd,
            X = -120,
            Y = -120
        };
        data.GeneralInfo.LastObj++;

        room.Layers.First(layer => layer.LayerName.Content == layerName).InstancesData.Instances.Add(object_inst);


        room.GameObjects.Add(object_inst);

        return object_inst;
    }


    public UndertaleGameObject GetObjectFromData(string name)
    {
        return data.GameObjects.ByName(name);
    }
    public UndertaleSprite GetSpriteFromData(string name)
    {
        return data.Sprites.ByName(name);
    }
    public UndertaleRoom GetRoomFromData(string name)
    {
        return data.Rooms.ByName(name);
    }
    public UndertaleCode GetObjectCodeFromData(string name)
    {
        return data.Code.ByName(name);
    }
    public UndertaleFunction GetFunctionFromData(string name)
    {
        return data.Functions.ByName(name);
    }
    public UndertaleScript GetScriptFromData(string name)
    {
        return data.Scripts.ByName(name);
    }
    public UndertaleSound GetSoundFromData(string name)
    {
        return data.Sounds.ByName(name);
    }
    public UndertaleVariable GetVariableFromData(string name)
    {
        return data.Variables.ByName(name);
    }



    public void HookFunctionFromFile(string path, string function)
    {
        string value = "";
        if (files.TryGetValue(path, out value))
        {
            Console.WriteLine($"[WYSmodLoader]: loading {path}");
            data.HookFunction(function, value);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't hook function {path}, it wasn't in the files dictionary.");
        }
    }
    public void CreateFunctionFromFile(string path, string function, ushort argumentCount = 0)
    {
        string value = "";
        if (files.TryGetValue(path, out value))
        {
            Console.WriteLine($"[WYSmodLoader]: loading {path}");
            data.CreateFunction(function, value, argumentCount);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't create function {path}, it wasn't in the files dictionary.");
        }
    }

    public void HookCodeFromFile(string path, string function)
    {
        string value = "";
        if (files.TryGetValue(path, out value))
        {
            Console.WriteLine($"[WYSmodLoader]: loading {path}");
            data.HookCode(function, value);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't hook object script {path}, it wasn't in the files dictionary.");
        }
    }


    public void CreateObjectCodeFromFile(string path, string objName, EventType eventType)
    {
        string value = "";
        UndertaleGameObject obj = data.GameObjects.ByName(objName);

        if (files.TryGetValue(path, out value))
        {
            obj.EventHandlerFor(eventType, data.Strings, data.Code, data.CodeLocals)
            .ReplaceGmlSafe(value, data);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't change/create object script {path}, it wasn't in the files dictionary.");
        }
    }

    public void CreateObjectCodeFromFile(string path, string objName, EventType eventType, EventSubtypeDraw EventSubtype)
    {
        string value = "";
        UndertaleGameObject obj = data.GameObjects.ByName(objName);

        if (files.TryGetValue(path, out value))
        {
            obj.EventHandlerFor(eventType, EventSubtype, data.Strings, data.Code, data.CodeLocals)
            .ReplaceGmlSafe(value, data);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't change/create object script {path}, it wasn't in the files dictionary.");
        }
    }
    public void CreateObjectCodeFromFile(string path, string objName, EventType eventType, uint EventSubtype)
    {
        string value = "";
        UndertaleGameObject obj = data.GameObjects.ByName(objName);

        if (files.TryGetValue(path, out value))
        {
            obj.EventHandlerFor(eventType, EventSubtype, data.Strings, data.Code, data.CodeLocals)
            .ReplaceGmlSafe(value, data);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't change/create object script {path}, it wasn't in the files dictionary.");
        }
    }
    public void CreateObjectCodeFromFile(string path, string objName, EventType eventType, EventSubtypeKey EventSubtype)
    {
        string value = "";
        UndertaleGameObject obj = data.GameObjects.ByName(objName);

        if (files.TryGetValue(path, out value))
        {
            obj.EventHandlerFor(eventType, EventSubtype, data.Strings, data.Code, data.CodeLocals)
            .ReplaceGmlSafe(value, data);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't change/create object script {path}, it wasn't in the files dictionary.");
        }
    }

    public void CreateObjectCodeFromFile(string path, string objName, EventType eventType, EventSubtypeMouse EventSubtype)
    {
        string value = "";
        UndertaleGameObject obj = data.GameObjects.ByName(objName);

        if (files.TryGetValue(path, out value))
        {
            obj.EventHandlerFor(eventType, EventSubtype, data.Strings, data.Code, data.CodeLocals)
            .ReplaceGmlSafe(value, data);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't change/create object script {path}, it wasn't in the files dictionary.");
        }
    }


    public void CreateObjectCodeFromFile(string path, string objName, EventType eventType, EventSubtypeOther EventSubtype)
    {
        string value = "";
        UndertaleGameObject obj = data.GameObjects.ByName(objName);

        if (files.TryGetValue(path, out value))
        {
            obj.EventHandlerFor(eventType, EventSubtype, data.Strings, data.Code, data.CodeLocals)
            .ReplaceGmlSafe(value, data);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't change/create object script {path}, it wasn't in the files dictionary.");
        }
    }

    public void CreateObjectCodeFromFile(string path, string objName, EventType eventType, EventSubtypeStep EventSubtype)
    {
        string value = "";
        UndertaleGameObject obj = data.GameObjects.ByName(objName);

        if (files.TryGetValue(path, out value))
        {
            obj.EventHandlerFor(eventType, EventSubtype, data.Strings, data.Code, data.CodeLocals)
            .ReplaceGmlSafe(value, data);
        }
        else
        {
            Console.WriteLine($"[WYSmodLoader]: Couldn't change/create object script {path}, it wasn't in the files dictionary.");
        }
    }

    public static Dictionary<string, string> LoadCodeFromFiles(string path)
    {
        Dictionary<string, string> files = new Dictionary<string, string>();
        string[] codeF = Directory.GetFiles(path, "*.gml");
        Console.WriteLine($"[WYSmodLoader]: Loading code from {path}");
        foreach (string f in codeF)
        {
            if (!files.ContainsKey(Path.GetFileName(f)))
            {
                files.Add(Path.GetFileName(f), File.ReadAllText(f));
            }
        }
        return files;
    }


}
