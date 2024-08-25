global.editorTool_objects_obj = global.editorTool_objects_page * global.editorTool_objs_per_page


#orig#()

if tools_widnow_open && tools_window_tab == 0
{
    if(global.editorTool_objects_obj + global.editorTool_objs_per_page < ds_list_size(global.li_level_editor_database)){
        draw_sprite_ext(spr_lvledit_scroll_arrow, 0, 1920 - 600, 1080 - 300, 2, 2, 90, c_white, 1)

        var button_width = sprite_get_width(spr_lvledit_scroll_arrow) * 2
        var button_height = sprite_get_height(spr_lvledit_scroll_arrow) * 2

        var pressed = scr_ui_detectvmouse_square_lpress(1920 - 600 - button_height / 2, 1080 - 300 - button_width / 2, button_height, button_width)
        if(pressed){
            global.editorTool_objects_page += 1
        }
    }



    if(global.editorTool_objects_page > 0){
        draw_sprite_ext(spr_lvledit_scroll_arrow, 0, 600, 1080 - 300, 2, 2, -90, c_white, 1)

        button_width = sprite_get_width(spr_lvledit_scroll_arrow) * 2
        button_height = sprite_get_height(spr_lvledit_scroll_arrow) * 2

        pressed = scr_ui_detectvmouse_square_lpress(600 - button_height / 2, 1080 - 300 - button_width / 2, button_height, button_width)
        if(pressed){
            global.editorTool_objects_page -= 1
        }
    }
}