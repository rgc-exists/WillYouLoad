allModsPath = argument0




var modPaths = []
var modPath = file_find_first(allModsPath + "*", fa_directory)
while(modPath != ""){
    
    array_push(modPaths,  allModsPath + modPath)

    modPath = file_find_next()   
}
file_find_close()

global.wysModLoader_editorObjDatas = []

for(var mp = 0; mp < array_length(modPaths); mp++){
    var modPath = modPaths[mp]
    var jsonPaths = gml_Script_scr_wysModLoader_searchForJsons(modPath, [])
    //show_message("mod path: " + modPath)

    for(var j = 0; j < array_length(jsonPaths); j++){
        var jsonP = jsonPaths[j]
        //show_message(jsonP)
        var jsonFile = file_text_open_read(jsonP)
        var jsonStr = ""
        while(!file_text_eof(jsonFile)){
            jsonStr += file_text_read_string(jsonFile)
            file_text_readln(jsonFile)
        }

        var json_data = json_parse(jsonStr)
        if(variable_struct_exists(json_data, "sprites")){
            var sprites = json_data.sprites 
            for(var s = 0; s < array_length(sprites); s++){
                var sprite_data = sprites[s]
                var new_sprite = sprite_add(sprite_data.name, sprite_data.image_index, false, false, sprite_data.xorigin, sprite_data.yorigin)
                variable_global_set("global.wysModLoader_sprite_" + sprite_data.name, new_sprite)
            }

            //global sprite var format = global.wysModLoader_sprite_[SPRITE NAME]
        }
        if(variable_struct_exists(json_data, "editor_objs")){
            //show_message("Objects exists.")
            var objects = json_data.editor_objs 
            for(var s = 0; s < array_length(objects); s++){
                var object_data = objects[s]

                var new_object = [
                    "custom_tool_or_object_id", object_data.object_id,
                    "object_index_in_game", asset_get_index(object_data.gameObject_in_game),
                    "object_index_in_editor", asset_get_index(object_data.gameObject_in_editor),
                    "preview_sprite_index", asset_get_index(object_data.preview_sprite_while_hovering),
                    "preview_sprite_index_once_placed", asset_get_index(object_data.preview_sprite_index_once_placed),
                    "preview_image_index", object_data.preview_sprite_subframe_index,
                    "color_mode", object_data.color_mode,
                    "preview_color", object_data.color,
                    "preview_layer", "Minigames",
                    "can_be_copied", object_data.can_be_copied,
                    "quickrotation_script", gml_Script_toolrotate_impossible,
                    "placement_script", gml_Script_toolplace_fill_with_blocks,
                    "placement_offset_x", object_data.placement_offset_x,
                    "placement_offset_y", object_data.placement_offset_y,
                    "deletion_script", gml_Script_toolplace_delete_blocks_of_same_type,
                    "tool_properties", [],
                    "prepforplay", gml_Script_prepforplay_empty,
                ]

                array_push(global.wysModLoader_editorObjDatas, gml_Script_custom_modhelper_array_to_struct(new_object))
            }

            //global sprite var format = global.wysModLoader_sprite_[SPRITE NAME]
        }

    }
}

