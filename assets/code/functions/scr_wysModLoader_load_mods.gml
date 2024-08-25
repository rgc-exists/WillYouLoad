allModsPath = argument0



var modPaths = []
var modPath = file_find_first(allModsPath + "*", fa_directory)
while(modPath != ""){
    
    array_push(modPaths,  allModsPath + modPath)

    modPath = file_find_next()   
}
file_find_close()

global.editor_tools_list = ["property_picker_tool", "path_tool", "wire_tool", "copy_tool", "move_tool", "delete_tool"]
global.wysModLoader_editorObjDatas = []

for(var mp = 0; mp < array_length(modPaths); mp++){
    var modPath = modPaths[mp]
    var jsonPaths = gml_Script_scr_wysModLoader_searchForJsons(modPath, [])

    for(var j = 0; j < array_length(jsonPaths); j++){
        var jsonP = jsonPaths[j]
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
                var new_sprite = sprite_add(modPath + "/" + sprite_data.path, sprite_data.image_index, false, false, sprite_data.xorigin, sprite_data.yorigin)
                variable_global_set("wysModLoader_sprite_" + sprite_data.name, new_sprite)
            }

            //global sprite var format = global.wysModLoader_sprite_[SPRITE NAME]
        }
    }
}

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
        
        if(variable_struct_exists(json_data, "editor_objs")){
            //show_message("Objects exists.")
            var objects = json_data.editor_objs 
            for(var s = 0; s < array_length(objects); s++){
                var object_data = objects[s]
                
                var preview_sprite = asset_get_index(object_data.preview_sprite_while_hovering)
                var preview_sprite_once_placed = asset_get_index(object_data.preview_sprite_once_placed)

                if(preview_sprite == -1){
                    preview_sprite = variable_global_get("wysModLoader_sprite_" + object_data.preview_sprite_while_hovering)
                }
                if(preview_sprite_once_placed == -1){
                    preview_sprite_once_placed = variable_global_get("wysModLoader_sprite_" + object_data.preview_sprite_once_placed)
                }

                if(variable_struct_exists(object_data, "is_tool")){
                    if(object_data.is_tool){
                        array_push(global.editor_tools_list, object_data.object_id)
                    }
                }

                var tool_properties = []
                if(variable_struct_exists(object_data, "properties")){
                    for(var p = 0; p < array_length(variable_struct_get_names(object_data.properties)); p++){
                        var names = variable_struct_get_names(object_data.properties)
                        var propName = names[p]
                        var propInfo = variable_struct_get(object_data.properties, propName)

                        var icon = spr_propico_instance
                        switch(propInfo.prop_type){
                            case "instance":
                                icon = spr_propico_instance
                                break;
                        }

                        var copy_property_to_placed_obj = true 
                        if(variable_struct_exists(propInfo, "copy_property_to_placed_obj")){
                            copy_property_to_placed_obj = propInfo.copy_property_to_placed_obj
                        }
                        
                        var prop = undefined
                        if(propInfo.value_type == "int"){
                            prop = modhelper_createprop_int(
                                propName,
                                copy_property_to_placed_obj,
                                propInfo.minimum,
                                propInfo.maximum,
                                gml_Script_custom_modhelper_custom_loca_text(
                                    string_replace_all(propInfo.prop_name, " ", ""),
                                    propInfo.prop_name
                                ),
                                icon,
                                propInfo.default_value,
                                gml_Script_custom_modhelper_custom_loca_text(
                                    string_replace_all(propInfo.tooltip, " ", ""),
                                    propInfo.tooltip
                                )
                            )
                            //modhelper_comment("ARGS: _key, _copy_property_to_placed_obj, _int_min, _int_max, _loca_string, _icon, _default_value, _looltip_loca_string")
                        }
                        if(!is_undefined(prop)){
                            array_push(tool_properties, prop)
                        }
                    }
                }

                var new_object = [
                    "custom_tool_or_object_id", object_data.object_id,
                    "object_index_in_game", asset_get_index(object_data.gameObject_in_game),
                    "object_index_in_editor", asset_get_index(object_data.gameObject_in_editor),
                    "preview_sprite_index", preview_sprite,
                    "preview_sprite_index_once_placed", preview_sprite_once_placed,
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
                    "tool_properties", tool_properties,
                    "prepforplay", gml_Script_prepforplay_automatic,
                ]

                array_push(global.wysModLoader_editorObjDatas, gml_Script_custom_modhelper_array_to_struct(new_object))
            }

        }

    }
}

