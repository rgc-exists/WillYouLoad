hacked_file = false
if ((time == 0))
{
    display_text = "LOADING"
    file = file_text_open_read(saveName)
    if ((room != level_editor))
        room_goto(level_editor)
    return false;
}
else if ((time <= 2))
    return false;
var levelData = ds_map_find_value(global.campaignMap, array_get(info, 0)).levels[info[1]]
if ((file == -1))
{
    clear_existing_level()
    show_debug_message("Level file does not exist, stopping loading and showing empty level.")
    return true;
}
if (levelData.isModded && (!global.isEditorModded))
{
    file_text_close(file)
    clear_existing_level()
    obj_level_editor.level_bound_x1 = 0
    obj_level_editor.level_bound_y1 = 0
    obj_level_editor.level_bound_x2 = 0
    obj_level_editor.level_bound_y2 = 0
    show_debug_message("Level is modded while the game is not, refusing to load!")
    return true;
}
clear_existing_level()
gameVersion = file_text_read_string(file)
file_text_readln(file)
file_text_readln(file)
file_text_read_string(file)
file_text_readln(file)
obj_level_editor.level_bound_x1 = 0
obj_level_editor.level_bound_y1 = 0
obj_level_editor.level_bound_x2 = file_text_read_real(file)
file_text_readln(file)
obj_level_editor.level_bound_y2 = file_text_read_real(file)
file_text_readln(file)
file_text_readln(file)
file_text_read_string(file)
file_text_readln(file)
listSize = file_text_read_real(file)
file_text_readln(file)
for (li = 0; li < listSize; li++)
{
    var toLoad = file_text_read_string(file)
    if ((toLoad == "spike_rusty"))
        toLoad = "spike"
    var missing_cur_object = false
    if(is_undefined(ds_map_find_value(global.map_level_editor_database, toLoad))){
        missing_cur_object = true
    }
    if(!missing_cur_object){
        dataBaseStruct = get_leveleditor_database_element(toLoad)
        file_text_readln(file)
        dataBaseStruct.image_angle = file_text_read_real(file)
        file_text_readln(file)
        dataBaseStruct.image_xscale = file_text_read_real(file)
        file_text_readln(file)
        dataBaseStruct.image_yscale = file_text_read_real(file)
        file_text_readln(file)
        arraySize = file_text_read_real(file)
        file_text_readln(file)
    } else {
        repeat(5) file_text_readln(file)
    }
    for (ti = 0; ti < arraySize; ti++)
    {
        if(!missing_cur_object){

            thsToolKey = file_text_read_string(file)
            file_text_readln(file)
            thsToolValue = file_text_read_real(file)
            file_text_readln(file)
            thsToolProp = ds_map_find_value(dataBaseStruct.ds_map_tool_properties, thsToolKey)
            if (!is_undefined(thsToolProp))
            {
                thsToolProp.value = thsToolValue
                if ((thsToolKey == "tex"))
                {
                    dataBaseStruct.preview_sprite_index = thsToolProp.CurrentPreviewSprite()
                    dataBaseStruct.preview_sprite_index_once_placed = thsToolProp.CurrentPreviewSprite()
                }
                leveleditor_set_hacked_if_invalid(thsToolProp)
            }
        } else {
            repeat(2) file_text_readln(file)
        }
    }
}
file_text_readln(file)
file_text_read_string(file)
file_text_readln(file)
for (li = 0; li < 10; li++)
{
    var slotToSet = file_text_read_string(file)
    if ((slotToSet == "spike_rusty"))
        slotToSet = "spike"
    if(is_undefined(ds_map_find_value(global.map_level_editor_database, slotToSet))){
        slotToSet = "wall"
    }
    ds_list_replace(obj_level_editor.li_quicktool_slots, li, get_leveleditor_database_element(slotToSet))
    file_text_readln(file)
}
file_text_readln(file)
file_text_read_string(file)
file_text_readln(file)
listSize = file_text_read_real(file)
file_text_readln(file)

var missing_objects = false
for (li = 0; li < listSize; li++)
{
    var dataBaseEntryToLoad = file_text_read_string(file)
    if ((dataBaseEntryToLoad == "spike_rusty"))
        dataBaseEntryToLoad = "spike"
    var missing_cur_object = false
    if(is_undefined(ds_map_find_value(global.map_level_editor_database, dataBaseEntryToLoad))){
        missing_objects = true
        missing_cur_object = true
    }
    if(!missing_cur_object) dataBaseStruct = get_leveleditor_database_element(dataBaseEntryToLoad)
    file_text_readln(file)
    instListSize = file_text_read_real(file)
    file_text_readln(file)
    for (ti = 0; ti < instListSize; ti++)
    {
        this_x = file_text_read_real(file)
        file_text_readln(file)
        this_y = file_text_read_real(file)
        file_text_readln(file)
        if(!missing_cur_object){
            thisInst = instance_create_layer(this_x, this_y, dataBaseStruct.preview_layer, dataBaseStruct.object_index_in_editor)
            thisInst.sprite_index = dataBaseStruct.preview_sprite_index_once_placed
            thisInst.image_index = dataBaseStruct.preview_image_index
            thisInst.image_speed = 0
            thisInst.image_blend = dataBaseStruct.preview_color
            thisInst.image_angle = file_text_read_real(file)
            file_text_readln(file)
            thisInst.image_xscale = file_text_read_real(file)
            file_text_readln(file)
            thisInst.image_yscale = file_text_read_real(file)
            file_text_readln(file)
            thisInst.toolStruct = dataBaseStruct
            ds_list_add(dataBaseStruct.li_placed_instances, thisInst)
        } else {
            repeat(3) file_text_readln(file)
        }
        prisize = file_text_read_real(file)
        file_text_readln(file)
        thisInst.map_properties = ds_map_create()
        for (pri = 0; pri < prisize; pri++)
        {
            if(!missing_cur_object){
                thisKey = file_text_read_string(file)
                file_text_readln(file)
                thisValue = file_text_read_real(file)
                file_text_readln(file)
                ds_map_add(thisInst.map_properties, thisKey, thisValue)
                thsToolProp = ds_map_find_value(dataBaseStruct.ds_map_tool_properties, thisKey)
                leveleditor_set_hacked_if_invalid(thsToolProp)
                if ((thisKey == "tex"))
                {
                    temp_map_properties = ds_map_create()
                    ds_map_add(temp_map_properties, "tex", thisValue)
                    script_execute(dataBaseStruct.prepforplay, thisInst, temp_map_properties, dataBaseStruct)
                    ds_map_destroy(temp_map_properties)
                }
            } else {
                repeat(2) file_text_readln(file)
            }
        }
        if(!missing_cur_object){
            for (var i = 0; i < array_length(dataBaseStruct.tool_properties); i++)
            {
                var tool_cur_prop = dataBaseStruct.tool_properties[i]
                if (tool_cur_prop.copy_property_to_placed_obj && (!(ds_map_exists(thisInst.map_properties, tool_cur_prop.key))))
                    ds_map_add(thisInst.map_properties, tool_cur_prop.key, tool_cur_prop.def_value)
            }
        }
    }
}
file_text_readln(file)
file_text_read_string(file)
file_text_readln(file)
listSize = file_text_read_real(file)
file_text_readln(file)
for (li = 0; li < listSize; li++)
{
    var missing_cur_object = false
    
    var fromID = file_text_read_string(file)
    if(is_undefined(ds_map_find_value(global.map_level_editor_database, fromID))){
        missing_cur_object = true
    }
    if(!missing_cur_object){
        toolFrom = get_leveleditor_database_element(fromID)
        file_text_readln(file)
        iFrom = file_text_read_real(file)
        file_text_readln(file)
        objFrom = ds_list_find_value(toolFrom.li_placed_instances, iFrom)
    } else {
        repeat(2) file_text_readln(file)
    }
    var toID = file_text_read_string(file)
    if(is_undefined(ds_map_find_value(global.map_level_editor_database, toID))){
        missing_cur_object = true
    }
    if(!missing_cur_object){
        toolTo = get_leveleditor_database_element(toID)
        file_text_readln(file)
        objTo = ds_list_find_value(toolTo.li_placed_instances, file_text_read_real(file))
        file_text_readln(file)
        lvlwire_create(objFrom, objTo)
    } else {
        repeat(2) file_text_readln(file)
    }
}
file_text_close(file)
if hacked_file
    show_debug_message("Level is hacked!")

if(missing_objects) {
    show_message("WARNING: This level contains objects from mods that you do not have installed! Please install the dependencies or the objects will simply be missing!")
}
return true;