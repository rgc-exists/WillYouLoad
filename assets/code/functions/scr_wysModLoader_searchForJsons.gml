var pathToSearch = argument0
var jsonsSoFar = argument1

if(string_char_at(pathToSearch, string_length(pathToSearch)) == "/"){
    pathToSearch = string_delete(pathToSearch, string_length(pathToSearch), 1)
}

//show_message("Searching for: " + pathToSearch + "/*")

var jsonP = file_find_first(pathToSearch + "/*", fa_directory)
//NOTE TO SELF: IF THIS DOESNT WORK TRY TURNING "/*.json" INTO JUST "*.json"
//NOTE #2: Tried and it didnt work, maybe try again once more changes are made
var folderPaths = []


while(jsonP != ""){
    //show_message(jsonP)
    if (file_attributes(pathToSearch + "/" + jsonP, fa_directory))
    {
        array_push(folderPathsfolderPaths, pathToSearch + "/" + jsonP)
    } else {
        if(string_ends_with(jsonP, ".json")){
            array_push(jsonsSoFar, pathToSearch + "/" + jsonP)
        }
    }

    jsonP = file_find_next()   
}
file_find_close()
////show_message("A")



for(var fp = 0; fp < array_length(folderPaths); fp++){
    var folderPath = folderPaths[fp]
    jsonsSoFar = gml_Script_scr_wysModLoader_searchForJsons(folderPath, jsonsSoFar)
}

//show_message(jsonsSoFar)

return jsonsSoFar