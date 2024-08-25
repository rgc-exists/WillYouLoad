for (iprp = 0; iprp < ds_list_size(global.li_level_editor_database); iprp++)
{
    var lvleditor_database_element = ds_list_find_value(global.li_level_editor_database, iprp)

    var lvleditor_extraprops_element = ds_map_find_value(global.map_extra_properties_database, lvleditor_database_element.custom_tool_or_object_id)


    if(is_undefined(lvleditor_extraprops_element)){
        if is_string(lvleditor_database_element.preview_color)
        {   
            if variable_instance_exists(obj_levelstyler, lvleditor_database_element.preview_color)
                lvleditor_database_element.preview_color = variable_instance_get(obj_levelstyler, lvleditor_database_element.preview_color)
            else
                show_message(("Color variable does not exist in level styler: " + lvleditor_database_element.preview_color))
        }
        else if is_method(lvleditor_database_element.preview_color)
            lvleditor_database_element.preview_color = script_execute(lvleditor_database_element.preview_color)
        else
            show_message(("Unsupported color variable type:" + string(typeof(lvleditor_database_element.preview_color))))
    } else {
        if(lvleditor_extraprops_element.color_mode == "Function"){
            lvleditor_database_element.preview_color = script_execute(asset_get_index(lvleditor_extraprops_element.color))
        } else if(is_real(lvleditor_database_element.preview_color)){
            lvleditor_database_element.preview_color = lvleditor_database_element.preview_color
        } else if(lvleditor_extraprops_element.color_mode == "Color Scheme"){
            if variable_instance_exists(obj_levelstyler, lvleditor_database_element.preview_color)
                lvleditor_database_element.preview_color = variable_instance_get(obj_levelstyler, lvleditor_database_element.preview_color)
            else
                show_message(("Color variable does not exist in level styler: " + lvleditor_database_element.preview_color))
        }
        else if(lvleditor_extraprops_element.color_mode == "Custom") {
            lvleditor_database_element.preview_color = gml_Script_read_formatted_color_string(lvleditor_database_element.preview_color)
        } else if(lvleditor_extraprops_element.color_mode == "Preset") {
            show_message("The \"Preset\" color mode is not yet supported, though it may exist in the future.")
        } else {
            show_message("\"Preset\" is not a valid color mode.")

        }

    }
    if variable_struct_exists(lvleditor_database_element, "preview_layer")
    {
        if is_string(lvleditor_database_element.preview_layer){
            if(lvleditor_database_element.modded)
                lvleditor_database_element.preview_layer = lvleditor_database_element.preview_layer
            else
                lvleditor_database_element.preview_layer = layer_get_id(lvleditor_database_element.preview_layer)
        }
    }
}
for (iprp = 0; iprp < ds_list_size(global.li_level_editor_icons); iprp++)
{
    var lvleditor_icon_element = ds_list_find_value(global.li_level_editor_icons, iprp)
    if is_string(lvleditor_icon_element.preview_color)
    {
        if variable_instance_exists(obj_levelstyler, lvleditor_icon_element.preview_color)
            lvleditor_icon_element.preview_color = variable_instance_get(obj_levelstyler, lvleditor_icon_element.preview_color)
        else
            show_message(("Color variable does not exist in level styler: " + lvleditor_icon_element.preview_color))
    }
    else if is_method(lvleditor_database_element.preview_color)
        lvleditor_icon_element.preview_color = script_execute(lvleditor_icon_element.preview_color)
    else
        show_message(("Unsupported color variable type:" + string(typeof(lvleditor_icon_element.preview_color))))
}