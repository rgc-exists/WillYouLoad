var return_value = #orig#()

var keys = variable_struct_get_names(global.custom_loca_texts)
for(var i = 0; i < array_length(keys); i++){
    var kkey = keys[i]
    var ktrans = variable_struct_get(global.custom_loca_texts, kkey)

    ds_map_add(global.loca_ds_map_text_translations, kkey, ktrans)
    ds_map_add(global.loca_ds_map_text_moods, kkey, "")
}

return return_value