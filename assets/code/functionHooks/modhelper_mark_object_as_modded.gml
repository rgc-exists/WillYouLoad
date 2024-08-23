// There appears to be a bug with the original mark_object_as_modded mod helper.abs
// argument0.modded = true should be outside of the if statement if I'm not mistaken.

modhelper_comment("Please call this function ALL custom object structs, or object structs you've modified to have extra properties that won't work on a vanilla game!")
global.isEditorModded = true
if variable_struct_exists(argument0, "icons")
{
    if ((argument0.icons == -1))
        argument0.icons = ["modded"]
    else
        array_insert(argument0.icons, 0, "modded")
}
argument0.modded = true