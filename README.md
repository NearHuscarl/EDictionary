<p align="center">
  <img src="https://github.com/NearHuscarl/EDictionary/blob/master/demo/Logo.png"/>
</p>

<p align="center">
  <a href="https://github.com/NearHuscarl/E-Dictionary/blob/master/LICENSE.md"><img src="https://img.shields.io/badge/License-BSD_3--Clauses-blue.svg?longCache=true" alt="BSD 3-Clause License"></a>
  <a href="https://github.com/NearHuscarl/E-Dictionary/releases"><img src="https://img.shields.io/badge/Version-2.0.2-green.svg?longCache=true" alt="Version"></a>
</p>

EDitionary is a desktop application project for uni assignment written in WPF. It has basic feature (english translator) along with some add-ons to make learning English more convenient. Note: Some of the features is still in proof-of-concept state 

## Main Features

* Search 40k English words for definition
* Autocomplete when searching
* Words pronunciation
* Show other form of current word
* History

* EDictionary Learner: Periodically popup of a random word definition to help learning vocabulary
* EDictionary Dynamic: Search the currently selected text (by double click and press a trigger key)

## Screenshots

![Main window](https://github.com/NearHuscarl/EDictionary/blob/master/demo/Main.png)
![Learner](https://github.com/NearHuscarl/EDictionary/blob/master/demo/Learner.png)
![Dynamic demonstration](https://github.com/NearHuscarl/EDictionary/blob/master/demo/Dynamic.gif)

## Installation

#### Build from source
```bash
$ git clone --recursive https://github.com/NearHuscarl/EDictionary
```

#### Or download an [installer](https://github.com/NearHuscarl/EDictionary/releases) 

## Development Environment

The project is written in .NET Framework 4.6 and C# 6.0. Compiled using Visual Studio 14

### Libraries Used
* [Newtonsoft.Json](https://www.newtonsoft.com/json)
* [System.Data.SQLite](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki)
* [System.Data.SQLite.Core](https://www.nuget.org/packages/system.data.sqlite.core) (Need to fix a [sqlite-related error](https://stackoverflow.com/a/28092497/9449426))
* [StemmersNet](https://archive.codeplex.com/?p=stemmersnet)
* [FontAwesome.WPF](https://github.com/charri/Font-Awesome-WPF/blob/master/README-WPF.md)
* [NotifyIcon.Wpf](https://bitbucket.org/hardcodet/notifyicon-wpf)
* [avalonedit](http://avalonedit.net/)
* [MouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook)
* [LemmaGenerator](https://github.com/AlexPoint/LemmaGenerator)

## Related Work
* [oxford-dictionary-api](https://github.com/NearHuscarl/oxford-dictionary-api)
* [DynamicTranslator](https://github.com/DynamicTranslator/DynamicTranslator)
* [SmartVocabulary](https://github.com/al-develop/SmartVocabulary)

## License
**[BSD 3 Clauses](https://github.com/NearHuscarl/i3-quake/blob/master/LICENSE.md)**
