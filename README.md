# Stardew Valley Auto Splitter

A LiveSplit autosplitter component for Stardew Valley

## Features

* [x] Able to add GLOBAL SETTINGS that are loaded on new game
* [x] Remove inconsistent save times (when the white text is visible)
* [x] Remove time while rebuilding graphics
    * This is usually very quick, but is also done a lot in some glitches in old versions
* [x] Remove time while the game is `\pause`d (introduced in 1.3+)
* [X] Automatically start the timer
* [X] Automatically split the timer at key events
* [X] Automatically stop the timer at run end
* [ ] Automatically reset the timer when starting a new run

### Version Support

* [x] Support for version 1.5.4
* [x] Support for version 1.4.0
* [x] Support for version 1.3.36
* [x] Support for version 1.2.33
* [ ] Support for version 1.11
* [ ] Support for version 1.07
* [x] Support for Steam
* [ ] Support for other vendors (GOG)

## Installing

* Copy Components/LiveSplit.StardewValley.dll into the Components folder in LiveSplit
* Add the Stardew Valley Auto Splitter component to the layout

## Getting Started

* Built in Visual Studio 2017 
* The missing LiveSplit.Core can be found next to the LiveSplit.exe executable
* the MemoryHelper project is used to compute and verify the offsets needed to inspect the game's memory
    * this is mostly done since the CLRMD dependency fails to inspect a process with a different pointer size
* When commiting to master, make sure to update the Components folder

## AutoSplit Support Table

|Version | DayStart | DayEnd | MinesFloor | Marriage | Bundles | CC | Joja | HatMouse | FourCandles |
|--|--|--|--|--|--|--|--|--|--|
| 1.2.33 | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| 1.3.36 | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| 1.4.0 | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ |
| 1.4.5 | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| 1.5.4 | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| 1.5.5 | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| 1.5.6 | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ |
| 1.5.6-compat | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ |
| 1.6.3 | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| 1.6.8 | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| 1.6.8-compat | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
