:: This script registers "Microsoft.mshtml.dll" so that
:: it can be imported in .net applications. I needed to
:: do this when creating an add-on for IE11 in Windows 10
:: 64bit (jan/2019)
"%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"
cd "%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Community\Common7\IDE\PublicAssemblies"
regasm Microsoft.mshtml.dll
gacutil /i Microsoft.mshtml.dll
