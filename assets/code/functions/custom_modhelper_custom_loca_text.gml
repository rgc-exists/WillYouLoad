modhelper_comment("ARGS: key, value, [mood]")

var kkey = argument0
var ktrans = argument1

if(ds_map_find_value(global.loca_ds_map_text_translations, kkey) != -1){
    ds_map_delete(global.loca_ds_map_text_translations, kkey)
    ds_map_delete(global.loca_ds_map_text_moods, kkey)
}

ds_map_add(global.loca_ds_map_text_translations, kkey, ktrans)
ds_map_add(global.loca_ds_map_text_moods, kkey, "")

variable_struct_set(global.custom_loca_texts, kkey, ktrans)

return kkey


// TODO: (LOW PRIORITY) Add support for adding custom moods, AND adding them to global.custom_loca_texts