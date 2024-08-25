str_col = string_replace_all(argument0, " ", "")
str_col_type = string_copy(argument0, 0, 4)
first_comma_pos = string_pos(",", str_col)
last_comma_pos = string_last_pos(",", str_col)
value_1 = real(string_copy(str_col, 5, (first_comma_pos - 5)))
value_2 = real(string_copy(str_col, (first_comma_pos + 1), ((last_comma_pos - first_comma_pos) - 1)))
value_3 = real(string_copy(str_col, (last_comma_pos + 1), (string_length(str_col) - last_comma_pos)))
if ((str_col_type == "RGB:"))
    return make_color_rgb(value_1, value_2, value_3);
else if ((str_col_type == "HSV:"))
    return make_color_hsv(value_1, value_2, value_3);
else {
    show_message("Invalid custom color string: " + str_col)
    return c_black;
}