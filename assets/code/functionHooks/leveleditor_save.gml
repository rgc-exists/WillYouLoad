if campaign_is_readonly(info[0])
{
    show_debug_message("Cant save a readonly campaign's level!")
    return true;
}
if ((time == 0))
{
    display_text = "SAVING"
    file = file_text_open_write(saveName)
    return false;
}
var levelData = ds_map_find_value(global.campaignMap, array_get(info, 0)).levels[info[1]]
if (levelData.isModded && (!global.isEditorModded))
{
    show_debug_message("Can't save a modded level!")
    return true;
}



if(variable_global_exists("li_used_objs_database"))
    ds_list_clear(global.li_used_objs_database)
else
    global.li_used_objs_database = ds_list_create()
if(variable_global_exists("li_used_toolprops_database"))
    ds_list_clear(global.li_used_toolprops_database)
else
    global.li_used_toolprops_database = ds_list_create()

for(var o = 0; o < ds_list_size(global.li_level_editor_database); o++){
    var dataBaseStruct = ds_list_find_value(global.li_level_editor_database, o)
    if(ds_list_size(dataBaseStruct.li_placed_instances) > 0){
        ds_list_add(global.li_used_objs_database, dataBaseStruct)
        ds_list_add(global.li_used_toolprops_database, dataBaseStruct)
    } else if (scr_array_has(global.editor_tools_list, dataBaseStruct.custom_tool_or_object_id)){
        ds_list_add(global.li_used_toolprops_database, dataBaseStruct)
    }
}





var has_exploration_point = false
var player_tool_struct = undefined
var object_count_map = ds_map_create()
var uses_modded_elements = false
li = 0
while ((li < ds_list_size(global.li_used_objs_database)))
{
    dataBaseStruct = ds_list_find_value(global.li_used_objs_database, li)
    if dataBaseStruct.modded
    {
        uses_modded_elements = true
        break
    }
    else
        li++
}
file_text_write_string(file, ("2.09" + (uses_modded_elements ? "_MODDED" : "")))
file_text_writeln(file)
LVLX1 = obj_level_editor.level_bound_x1
LVLY1 = obj_level_editor.level_bound_y1
LVLW = (obj_level_editor.level_bound_x2 - obj_level_editor.level_bound_x1)
LVLH = (obj_level_editor.level_bound_y2 - obj_level_editor.level_bound_y1)
file_text_writeln(file)
file_text_write_string(file, "LEVEL DIMENSIONS:")
file_text_writeln(file)
file_text_write_real(file, LVLW)
file_text_writeln(file)
file_text_write_real(file, LVLH)
file_text_writeln(file)
file_text_writeln(file)
file_text_write_string(file, "TOOL DATA:")
file_text_writeln(file)
file_text_write_real(file, ds_list_size(global.li_used_toolprops_database))
file_text_writeln(file)
for (li = 0; li < ds_list_size(global.li_used_toolprops_database); li++)
{
    dataBaseStruct = ds_list_find_value(global.li_used_toolprops_database, li)
    file_text_write_string(file, dataBaseStruct.custom_tool_or_object_id)
    file_text_writeln(file)
    file_text_write_real(file, dataBaseStruct.image_angle)
    file_text_writeln(file)
    file_text_write_real(file, dataBaseStruct.image_xscale)
    file_text_writeln(file)
    file_text_write_real(file, dataBaseStruct.image_yscale)
    file_text_writeln(file)
    file_text_write_real(file, array_length(dataBaseStruct.tool_properties))
    file_text_writeln(file)
    for (ti = 0; ti < array_length(dataBaseStruct.tool_properties); ti++)
    {
        thsToolProp = dataBaseStruct.tool_properties[ti]
        file_text_write_string(file, thsToolProp.key)
        file_text_writeln(file)
        file_text_write_real(file, thsToolProp.value)
        file_text_writeln(file)
    }
}
file_text_writeln(file)
file_text_write_string(file, "QUICK SLOTS:")
file_text_writeln(file)
for (li = 0; li < 10; li++)
{
    dataBaseStruct = ds_list_find_value(obj_level_editor.li_quicktool_slots, li)
    file_text_write_string(file, dataBaseStruct.custom_tool_or_object_id)
    file_text_writeln(file)
}
file_text_writeln(file)


file_text_write_string(file, "PLACED OBJECTS:")
file_text_writeln(file)
file_text_write_real(file, ds_list_size(global.li_used_objs_database))
file_text_writeln(file)
for (li = 0; li < ds_list_size(global.li_used_objs_database); li++)
{
    dataBaseStruct = ds_list_find_value(global.li_used_objs_database, li)
    file_text_write_string(file, dataBaseStruct.custom_tool_or_object_id)
    file_text_writeln(file)
    if ((dataBaseStruct.custom_tool_or_object_id == "exploration_point"))
        has_exploration_point = (ds_list_size(dataBaseStruct.li_placed_instances) > 0)
    else if ((dataBaseStruct.custom_tool_or_object_id == "player"))
        player_tool_struct = dataBaseStruct
    ds_map_set(object_count_map, dataBaseStruct.custom_tool_or_object_id, ds_list_size(dataBaseStruct.li_placed_instances))
    file_text_write_real(file, ds_list_size(dataBaseStruct.li_placed_instances))
    file_text_writeln(file)
    for (ti = 0; ti < ds_list_size(dataBaseStruct.li_placed_instances); ti++)
    {
        thisInst = ds_list_find_value(dataBaseStruct.li_placed_instances, ti)
        file_text_write_real(file, (thisInst.x - LVLX1))
        file_text_writeln(file)
        file_text_write_real(file, (thisInst.y - LVLY1))
        file_text_writeln(file)
        file_text_write_real(file, thisInst.image_angle)
        file_text_writeln(file)
        file_text_write_real(file, thisInst.image_xscale)
        file_text_writeln(file)
        file_text_write_real(file, thisInst.image_yscale)
        file_text_writeln(file)
        if (!(variable_instance_exists(thisInst, "map_properties")))
        {
            file_text_write_real(file, 0)
            file_text_writeln(file)
        }
        else if (!(ds_exists(thisInst.map_properties, 1)))
        {
            file_text_write_real(file, 0)
            file_text_writeln(file)
        }
        else
        {
            file_text_write_real(file, ds_map_size(thisInst.map_properties))
            file_text_writeln(file)
            thisKey = ds_map_find_first(thisInst.map_properties)
            for (pri = 0; pri < ds_map_size(thisInst.map_properties); pri++)
            {
                file_text_write_string(file, thisKey)
                file_text_writeln(file)
                var the_value = ds_map_find_value(thisInst.map_properties, thisKey)
                if ((typeof(the_value) == "string"))
                    show_error(((((("string where number should be! \nkey:" + thisKey) + "\nstruct:") + dataBaseStruct.custom_tool_or_object_id) + "\nvalue:") + the_value), true)
                file_text_write_real(file, the_value)
                file_text_writeln(file)
                thisKey = ds_map_find_next(thisInst.map_properties, thisKey)
            }
        }
    }
}
file_text_writeln(file)
file_text_write_string(file, "WIRES:")
file_text_writeln(file)
file_text_write_real(file, ds_list_size(global.lvl_wires))
file_text_writeln(file)
for (li = 0; li < ds_list_size(global.lvl_wires); li++)
{
    thisWire = ds_list_find_value(global.lvl_wires, li)
    fromObj = thisWire.from_obj
    fromObj_indexInList = ds_list_find_index(fromObj.toolStruct.li_placed_instances, fromObj)
    file_text_write_string(file, fromObj.toolStruct.custom_tool_or_object_id)
    file_text_writeln(file)
    file_text_write_real(file, fromObj_indexInList)
    file_text_writeln(file)
    toObj = thisWire.to_obj
    toObj_indexInList = ds_list_find_index(toObj.toolStruct.li_placed_instances, toObj)
    file_text_write_string(file, toObj.toolStruct.custom_tool_or_object_id)
    file_text_writeln(file)
    file_text_write_real(file, toObj_indexInList)
    file_text_writeln(file)
}
file_text_close(file)
var hasexplo = variable_struct_get(levelData, "hasExplorationPoint")
var levelico = variable_struct_get(levelData, "levelIcon")
var ismodded = variable_struct_get(levelData, "isModded")
var is_different = false
if is_undefined(hasexplo)
    hasexplo = ""
if is_undefined(levelico)
    levelico = ""
if is_undefined(ismodded)
    ismodded = ""
levelData.hasExplorationPoint = has_exploration_point
levelData.levelIcon = leveleditor_determine_level_icon(object_count_map, player_tool_struct)
levelData.isModded = uses_modded_elements
is_different = ((hasexplo != has_exploration_point) ? true : ((levelico != levelData.levelIcon) ? true : (ismodded != uses_modded_elements)))
if is_different
    save_leveleditor_campaign(info[0])
return true;