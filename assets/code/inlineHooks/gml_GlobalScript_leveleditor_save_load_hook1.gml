if string_ends_with(gameVersion, "_MODDED") && !global.isEditorModded
{
    return true;
}
return false;