# Will You Load

Will You Load is a mod loader that makes it easy to create custom objects for modded Will You Snail levels! It also supports hooks, and other featurees seen in traditional modding, including C# mods for advanced users.

# NOTE

### This project is an UNFINISHED prototype. Not all features listed below have been implemented yet.

# Levels of Knowledge

### Features with checked checkboxes have been implemented, unchecked boxes mean the feature is unfinished or has not been started yet.

## Beginner Users

### With SIMPLE Json files you can:

- [ ] Add variations of "template objects" to the editor. You can change things such as their textures and some of their properties. For example, you can add basic solid "walls" with custom textures.

- [x] Add custom sprites (textures) to use in your custom objects

## Basic Users

### With Json files you can:

- [x] Add custom modded game objects - [x] Add those game objects to the WYS level editor

## Intermediate Users

### With Json files you can:

- [x] Add custom code and functionalities custom objects
- [ ] Add custom code and functionalities existing vanilla game objects - [x] Add custom functions that you can use throughout your code
- [ ] Add custom sounds to be called in your code
- [ ] Create template objects for others to use

## Advanced Users

### With Json files you can:

- [ ] "Hook" to existing functions and object events. This allows you to MODIFY other functions in the game, and call on their original code like a function.
- [ ] Preform "inline GML replacements", which replace all instances of a certain text in an existing GML code with a new text. ONLY WORKS IF THE CODE IS ABLE TO BE RECOMPILED REGULARLY. If you see an error, you will need to use a hook or, if you're an expert user, an inline hook/inline assembly hook.

## Expert Users

**Don't be ashamed if you can't understand this stuff. It is VERY advanced knowledge.**

### Inline Hooks

- [ ] Allows you to modify certain parts of code that normally will return errors while recompiling.
- [ ] Allows you to modify very specific parts of the GMAssembly to better support changes between vanilla game builds.
- [ ] Replaces the "to find" assembly with calling a function with the code from the file in it.

### Inline Assembly Hooks

- [ ] Preform "inline assembly replacements", which replace all instances of a certain text in the ASSEMBLY code with a new ASSEMBLY text.

### C# Extensions

- [ ] Allows you to write custom C# code, similar to traditional mods made with GMSL.

### C# Interop

- [ ] Just like normal GMSL mods, C# extensions allow you to write C# interop functions, letting you communicate between GML and C#.

# Planned Features

## Online Mod Sharing

An online level/mod sharing service SEPARATE from the steam workshop, probably using mod.io, or maybe GameBanana.\
This will allow users to share and download mods, as well as modded campaigns, without filling the vanilla steam workshop with incompatible campaigns.

## WYLoad Mod Editor UI (MAYBE)

A UI to create and edit mods even easier. No need to manage a bunch of json files, almost everything will be available in an intuitive user interface.\
This MAY or MAY NOT be created at some point down the line.

# TO DO: Short Term

- Finish adding all of the features promised in the current ReadMe file.
- Add a required modinfo.json file for every mod to have
- Make AS MANY THINGS AS POSSIBLE case insensitive
- String properties support (text instead of just numbers)
