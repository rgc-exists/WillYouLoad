#orig#()


if(variable_global_exists("map_extra_properties_database"))
    ds_map_clear(global.map_extra_properties_database)
else
    global.map_extra_properties_database = ds_map_create()
//show_message("modhelper_predatabase_override_me")
for(var o = 0; o < array_length(global.wysModLoader_editorObjDatas); o++){
    //show_message(global.wysModLoader_editorObjDatas[o])
    var lvleditor_database_addition = global.wysModLoader_editorObjDatas[o]
    modhelper_mark_object_as_modded(lvleditor_database_addition)
    ds_list_add(global.li_level_editor_database, lvleditor_database_addition)

    var color_mode = lvleditor_database_addition.color_mode
    var color =  lvleditor_database_addition.preview_color
    if (color_mode == "Custom"){

    } else if (color_mode == "Function"){
    } else if (color_mode == "Preset"){
    } else if (color_mode == "Color Scheme"){
    } else {
        show_message("Invalid color_mode for object " + lvleditor_database_addition.custom_tool_or_object_id + ": " + color_mode)
        color_mode = "Custom"
        color = "RGB: 0, 0, 0"
    }

    var extra_properties = gml_Script_custom_modhelper_array_to_struct([
        "color_mode", color_mode,
        "color", color,
    ])
    ds_map_add(global.map_extra_properties_database, lvleditor_database_addition.custom_tool_or_object_id, extra_properties)
}