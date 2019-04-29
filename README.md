# Code Cleanup On Save

[![Build status](https://ci.appveyor.com/api/projects/status/bh50nba9a5o8y5sf?svg=true)](https://ci.appveyor.com/project/madskristensen/codecleanuponsave)

Automatically run one of the Code Clean profiles when saving the document. This ensures your code is always formatted correctly and follows your coding style conventions.

Download the extension at the
[Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CodeCleanupOnSave)
or try the
[CI build](http://vsixgallery.com/extension/66985471-b701-4851-a2d7-5a8bdce1e694/).

---------------------------------------

Code Cleanup is a new feature of Visual Studio 2019 that will automatically clean up your code file to make sure it is formatted correctly and that your coding style preferences are applied. 

This extension will perform the Code Cleanup automatically when the file is being saved.

### Configure Code Cleanup profiles

At the bottom of the C# editor, click the Configure Code Cleanup. 

![Code Cleanup Menu](art/code-cleanup-menu.png)

By default, **Profile 1** is executed on save by this extenions.

### Options
The options page allows you to chose between which profile to run automatically on save.

![Options](art/options.png)

## License
[Apache 2.0](LICENSE)
