#orig#()

WYSmodLoader_name = "WillYouLoad"

global.wysModLoader_path = "gmsl/mods/" + WYSmodLoader_name + "/"

var possiblePath = ""
var possibleModsPath = possiblePath + "Community_Mods/"
if(directory_exists(possibleModsPath)){

    global.wysModLoader_modsPath = possibleModsPath

    gml_Script_scr_wysModLoader_load_mods(global.wysModLoader_modsPath)
    
    global.isEditorModded = true
} else {
    show_error(possiblePath + " does not exist.", false)
}