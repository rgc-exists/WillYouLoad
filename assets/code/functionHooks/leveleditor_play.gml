hacked_file = false
if ((time == 0))
{
    display_text = "LOADING"
    file = file_text_open_read(saveName)
    return false;
}
else if ((time == 1))
{
    room_goto(empty_black_room)
    return false;
}
else if ((time == 2))
{
    if ((file == -1))
    {
        file = file_text_open_read("fallback.lvl")
        time--
        return false;
    }
    gameVersion = file_text_read_string(file)
    file_text_readln(file)
    if string_ends_with(gameVersion, "_MODDED") && !global.isEditorModded
    {
        file_text_close(file)
        file = file_text_open_read("fallback.lvl")
        time--
        return false;
    }
    leveleditor_database_ini()
    ini_leveleditor_wires()
    file_text_readln(file)
    file_text_read_string(file)
    file_text_readln(file)
    lvl_width = file_text_read_real(file)
    file_text_readln(file)
    lvl_height = file_text_read_real(file)
    file_text_readln(file)
    room_set_width(level_editor_play_mode, lvl_width)
    room_set_height(level_editor_play_mode, lvl_height)
    file_text_readln(file)
    file_text_read_string(file)
    file_text_readln(file)
    listSize = file_text_read_real(file)
    file_text_readln(file)
    for (li = 0; li < listSize; li++)
    {
        var elementToPlace = file_text_read_string(file)
        if ((elementToPlace == "spike_rusty"))
            elementToPlace = "spike"
        
        var missing_cur_object = false
        if(is_undefined(ds_map_find_value(global.map_level_editor_database, elementToPlace))){
            missing_cur_object = true
        }
        if(!missing_cur_object){
            dataBaseStruct = get_leveleditor_database_element(elementToPlace)
            file_text_readln(file)
            dataBaseStruct.image_angle = file_text_read_real(file)
            file_text_readln(file)
            dataBaseStruct.image_xscale = file_text_read_real(file)
            file_text_readln(file)
            dataBaseStruct.image_yscale = file_text_read_real(file)
            file_text_readln(file)
        } else {
            repeat(4) file_text_readln(file)
        }
        arraySize = file_text_read_real(file)
        file_text_readln(file)
        for (ti = 0; ti < arraySize; ti++)
        {
            if(!missing_cur_object){
                thsToolKey = file_text_read_string(file)
                file_text_readln(file)
                thsToolValue = file_text_read_real(file)
                file_text_readln(file)
                thsToolProp = ds_map_find_value(dataBaseStruct.ds_map_tool_properties, thsToolKey)
                if is_undefined(thsToolProp)
                    show_debug_message((((("Property " + thsToolKey) + " in element ") + elementToPlace) + " is undefined."))
                else
                    thsToolProp.value = thsToolValue
            } else {
                repeat(2) file_text_readln(file)
            }
        }
    }
    room_goto(level_editor_play_mode)
    return false;
}
else if ((time == 3))
{
    playerToolStruct = get_leveleditor_database_element("player")
    lvlColorScheme = ds_map_find_value(playerToolStruct.ds_map_tool_properties, "ltyp").value
    lvlDarkMode = ds_map_find_value(playerToolStruct.ds_map_tool_properties, "ldrk").value
    lvlBgVisible = ds_map_find_value(playerToolStruct.ds_map_tool_properties, "bgvis").value
    wallVisible = ds_map_find_value(playerToolStruct.ds_map_tool_properties, "wlvis").value
    if lvlDarkMode
        instance_create_layer(0, 0, "FadeOutIn", obj_dark_level)
    switch lvlColorScheme
    {
        case 0:
            instance_create_layer(0, 0, "FadeOutIn", obj_levelstyler)
            break
        case 1:
            instance_create_layer(0, 0, "FadeOutIn", obj_levelstyler_disco)
            break
        case 2:
            instance_create_layer(0, 0, "FadeOutIn", obj_levelstyler_underwater)
            break
        case 3:
            instance_create_layer(0, 0, "FadeOutIn", obj_levelstyler_bubblegum)
            break
        case 4:
            instance_create_layer(0, 0, "FadeOutIn", obj_levelstyler_winter)
            break
        case 5:
            instance_create_layer(0, 0, "FadeOutIn", obj_levelstyler_brain)
            break
    }

    if ((lvlBgVisible == 0))
    {
        obj_levelstyler.enable_back_particle_spawn = false
        instance_destroy(obj_backdraw)
    }
    else if ((lvlBgVisible == 2))
    {
        obj_levelstyler.glitchy = true
        if instance_exists(obj_backdraw)
            obj_backdraw.glitchy = true
        else if (!instance_exists(obj_levelstyler_winter))
            show_error("Levelstyler creates no BG when attempting to switch to glitch mode!", true)
    }
    else if ((lvlBgVisible == 3))
    {
        obj_levelstyler.enable_back_particle_spawn = false
        instance_destroy(obj_backdraw)
        instance_create_layer(0, 0, "BackPatterns", obj_light_stars_background)
    }
    else if ((lvlBgVisible == 4))
    {
        obj_levelstyler.enable_back_particle_spawn = false
        instance_destroy(obj_backdraw)
        instance_create_layer(0, 0, "BackPatterns", obj_light_ocean_background)
    }
    obj_levelstyler.rythmic_changes_enabled = true
    lvlSquidVisible = ds_map_find_value(playerToolStruct.ds_map_tool_properties, "sv").value
    if (!lvlSquidVisible)
        instance_create_layer(0, 0, "FadeOutIn", obj_no_squid_in_this_level)
    leveleditor_spawn_music((lvlColorScheme == 2), playerToolStruct)
    instance_create_layer(0, 0, "PostProcessing", obj_post_processing_draw)
    global.conveyor_belt_speed = ds_map_find_value(get_leveleditor_database_element("conveyor").ds_map_tool_properties, "conveyspeed").value
    file_text_readln(file)
    file_text_read_string(file)
    file_text_readln(file)
    for (li = 0; li < 10; li++)
    {
        file_text_read_string(file)
        file_text_readln(file)
    }
    return false;
}
leveleditor_database_prepare()
ini_leveleditor_paths()
global.lvlchecksum = ""
temp_map_properties = ds_map_create()
file_text_readln(file)
file_text_read_string(file)
file_text_readln(file)
listSize = file_text_read_real(file)
file_text_readln(file)

var missing_objects = false
for (li = 0; li < listSize; li++)
{
    var itemToLoad = file_text_read_string(file)
    if ((itemToLoad == "spike_rusty"))
        itemToLoad = "spike"
    var missing_cur_object = false
    if(is_undefined(ds_map_find_value(global.map_level_editor_database, itemToLoad))){
        missing_cur_object = true
        missing_objects = true
    }
    if(!missing_cur_object) dataBaseStruct = get_leveleditor_database_element(itemToLoad)
    file_text_readln(file)
    instListSize = file_text_read_real(file)
    file_text_readln(file)
    global.lvlchecksum += (string(instListSize) + "|")
    for (ti = 0; ti < instListSize; ti++)
    {
        if(!missing_cur_object){
            this_x = file_text_read_real(file)
            file_text_readln(file)
            this_y = file_text_read_real(file)
            file_text_readln(file)
            var objectToSpawn = (is_method(dataBaseStruct.object_index_in_game) ? dataBaseStruct.object_index_in_game(dataBaseStruct) : dataBaseStruct.object_index_in_game)
            if ((itemToLoad == "spike") && lvlDarkMode)
                objectToSpawn = obj_rusty_spike
            if ((itemToLoad == "lava") && lvlDarkMode)
                objectToSpawn = 149
            thisInst = instance_create_layer(this_x, this_y, dataBaseStruct.preview_layer, objectToSpawn)
            thisInst.image_angle = file_text_read_real(file)
            file_text_readln(file)
            thisInst.image_xscale = file_text_read_real(file)
            file_text_readln(file)
            thisInst.image_yscale = file_text_read_real(file)
            file_text_readln(file)
            ds_list_add(dataBaseStruct.li_placed_instances, thisInst)
        } else {
            repeat(5) file_text_readln(file)
        }
        ds_map_clear(temp_map_properties)
        prisize = file_text_read_real(file)
        file_text_readln(file)
        for (pri = 0; pri < prisize; pri++)
        {
            if(!missing_cur_object){
                thisKey = file_text_read_string(file)
                file_text_readln(file)
                thisValue = file_text_read_real(file)
                file_text_readln(file)
                thsToolProp = ds_map_find_value(dataBaseStruct.ds_map_tool_properties, thisKey)
                leveleditor_set_hacked_if_invalid(thsToolProp)
                ds_map_add(temp_map_properties, thisKey, thisValue)
            } else {
                repeat(2) file_text_readln(file)
            }
        }
        
        if(!missing_cur_object) script_execute(dataBaseStruct.prepforplay, thisInst, temp_map_properties, dataBaseStruct)
    }
}
ds_map_destroy(temp_map_properties)
if (!is_allowed_checksum(global.lvlchecksum))
{
    show_error("It seems like this level was modified. Revert this level back into its original state to play.", true)
    game_end()
}
instance_create_layer(0, 0, "Player", obj_ai_level_editor)
scr_create_wall_lines()
global.collision_grid_filled = false
scr_colgrid_fill()
ds_list_clear(obj_in_community_level.li_power_connections_at_start)
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
        var structWireHFrom = -4
        var structWireHTo = -4
        if variable_struct_exists(toolFrom, "custom_wire_handler")
            structWireHFrom = toolFrom.custom_wire_handler
        if variable_struct_exists(toolTo, "custom_wire_handler")
            structWireHTo = toolTo.custom_wire_handler
        if ((structWireHTo == -4) && (structWireHFrom == -4))
            leveleditor_connectwires_default(objFrom, objTo, toolFrom, toolTo)
        else if ((structWireHFrom != -4) && (structWireHTo == -4))
            self.structWireHFrom(objFrom, objTo, toolFrom, toolTo)
        else if ((structWireHFrom == -4) && (structWireHTo != -4))
            self.structWireHTo(objTo, objFrom, toolTo, toolFrom)
        else if ((structWireHFrom != -4) && (structWireHTo != -4))
        {
            self.structWireHFrom(objFrom, objTo, toolFrom, toolTo)
            self.structWireHTo(objTo, objFrom, toolTo, toolFrom)
        }
    } else {
        repeat(2) file_text_readln(file)
    }
    

}
enemySpawnerStruct = get_leveleditor_database_element("td_enemy_spwaner")
bombSpawnerStruct = get_leveleditor_database_element("bomb_spawner")
gunStruct = get_leveleditor_database_element("playergun")
obj_in_community_level.bomb_degrees = ds_map_find_value(enemySpawnerStruct.ds_map_tool_properties, "bomb_degrees").value
obj_in_community_level.bomb_speed = ds_map_find_value(enemySpawnerStruct.ds_map_tool_properties, "bomb_speed").value
obj_in_community_level.bomb_p_grav = ds_map_find_value(bombSpawnerStruct.ds_map_tool_properties, "bomb_speed").value
obj_in_community_level.gun_speed_mult = ds_map_find_value(gunStruct.ds_map_tool_properties, "gun_speed").value
obj_in_community_level.gun_cooldown = ds_map_find_value(gunStruct.ds_map_tool_properties, "gun_cooldown").value
lvl_convertpaths()
scr_set_up_or_reset_power_grid()
if ((ds_map_find_value(get_leveleditor_database_element("conveyor").ds_map_tool_properties, "conveystate").value == 0))
    global.conveyor_belt_direction *= -1
scr_update_conveyor_visuals()
if instance_exists(obj_dark_level)
    scr_create_floor_lights()
if ((ds_list_size(playerToolStruct.li_placed_instances) == 0))
    instance_create_layer(0, 0, "Player", obj_player)
if (!wallVisible)
{
    with (obj_wall)
    {
        if ((object_index == obj_destructable_wall))
        {
        }
        else if ((object_index == obj_explosive_wall))
        {
        }
        else
            visible = false
    }
}
obj_player.first_spawn_in_community = true
file_text_close(file)

if(missing_objects) {
    show_message("WARNING: This level contains objects from mods that you do not have installed! Please install the dependencies or the objects will simply be missing!")
}
return true;