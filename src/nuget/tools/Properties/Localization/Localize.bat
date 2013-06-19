@echo Off
setlocal
SET SolutionDir=%1
SET ProjectDir=%2

@Rem getting start time
for /F "tokens=1-4 delims=:.," %%a in ("%time%") do (
   set /A "start=(((%%a*60)+1%%b %% 100)*60+1%%c %% 100)*100+1%%d %% 100"
)

SET folder=%CD%
CD %SolutionDir%
SET SolutionDir=%CD%
CD %ProjectDir%
SET ProjectDir=%CD%
CD %folder%

SET ToolsDir=%~dp0
IF "%ProjectDir%" =="" SET ProjectDir=%ToolsDir%\..\..
IF "%SolutionDir%" =="" SET SolutionDir=%ToolsDir%\..\..\..
SET OutputDir=%ProjectDir%\Properties\Localization
SET ProjectFiles="%SolutionDir%\packages\projectfiles.txt"
SET FilteredProjectFiles="%SolutionDir%\packages\filteredprojectfiles.txt"
SET Languages="%ProjectDir%\Properties\Localization\Languages.txt"
SET ExcludeFiles="%ProjectDir%\Properties\Localization\Excludes.txt"
SET FileExtensions="%ProjectDir%\Properties\Localization\FileExtensions.txt"
SET PotMessages="%OutputDir%\newmessages.pot"
SET InputExtensions=
FOR /f "delims=" %%i in ('type "%FileExtensions%"') do (
SET InputExtensions=%%i
GOTO Program
)


:Program

SET GNU=%SolutionDir%\packages\GNU.Tools.1.0.0.1\tools
SET folder=%CD%
CD "%SolutionDir%"
dir %InputExtensions% /S /B > %ProjectFiles%
%GNU%\ExcludeLines.exe -s %ProjectFiles% -e %ExcludeFiles% -o %FilteredProjectFiles% -p %SolutionDir%
CD "%folder%"

ECHO Localizing into .po files of type %InputExtensions% from path: %ProjectDir% 


@rem Create a new .pot from source, place it in the English folder, and merge with the existing .po file in English

IF NOT EXIST "%OutputDir%" mkdir "%OutputDir%"

@rem echo xgett
"%GNU%\xgettext.exe" -k -k_ -k__q -k__ -k___ -k___q -kDisplayName -kDisplayNameAttribute -kRange -kRangeAttribute -kRequired -kRequiredAttribute -kRegularExpression -kRegularExpressionAttribute -kStringLength -kStringLengthAttribute -kCompare -kCompareAttribute -kEmail -kEmailAttribute -kGetText -kGetString -kGetHtml -kGetRaw -kFormatHtml -kFormatRaw -kGetRaw -kFormatRaw -k__h -k_s:1,2 -k__s:1,2 -k__hs:1,2 --from-code=UTF-8 -LC# --omit-header -o%PotMessages% -f%FilteredProjectFiles% --language=C# --sort-by-file
IF NOT EXIST %PotMessages% ECHO #No items found > %PotMessages%
IF NOT EXIST "%OutputDir%\messages.po" TYPE %PotMessages% >"%OutputDir%\messages.po"

"%GNU%\msgmerge.exe" --backup=none -U "%OutputDir%\messages.po" %PotMessages% -q

@rem echo Merge the newly created .pot file with the Other Language translations:

FOR /f "delims=" %%i in ('type "%Languages%"') do (
	if not exist "%OutputDir%\messages.%%i.po" (
		echo # PO Localization Tool for Visual Studio 2010, by laurentiu.macovei@dotnetwise.com, 2011.									> "%OutputDir%\messages.%%i.po"
		echo msgid ""									 >>"%OutputDir%\messages.%%i.po"
		echo msgstr ""									 >>"%OutputDir%\messages.%%i.po"
		echo "Language: %%i\n"							 >>"%OutputDir%\messages.%%i.po"
		echo "MIME-Version: 1.0\n"						 >>"%OutputDir%\messages.%%i.po"
		echo "Content-Type: text/plain; charset=UTF-8\n" >>"%OutputDir%\messages.%%i.po"
		echo "Content-Transfer-Encoding: 8bit\n"		 >>"%OutputDir%\messages.%%i.po"
		type %PotMessages% >> "%OutputDir%\messages.%%i.po"
	)
	"%GNU%\msgmerge.exe" --backup=none -U "%OutputDir%\messages.%%i.po" %PotMessages% --quiet --sort-by-file --lang=%%i
)

@rem Counting number of files

Set _File=%FilteredProjectFiles%
Set /a _Lines=0
For /f %%j in ('Type %_File%^|Find "" /v /c') Do Set /a _Lines=%%j


DEL /Q /F %ProjectFiles%
DEL /Q /F %FilteredProjectFiles%


@Rem getting end time
for /F "tokens=1-4 delims=:.," %%a in ("%time%") do (
   set /A "end=(((%%a*60)+1%%b %% 100)*60+1%%c %% 100)*100+1%%d %% 100"
)
rem Get elapsed time:
set /A elapsed=end-start

rem Show elapsed time:
set /A hh=elapsed/(60*60*100), rest=elapsed%%(60*60*100), mm=rest/(60*100), rest%%=60*100, ss=rest/100, cc=rest%%100
if %mm% lss 10 set mm=0%mm%
if %ss% lss 10 set ss=0%ss%
if %cc% lss 10 set cc=0%cc%

echo Localization completed. Scanned %_Lines% files in %hh%:%mm%:%ss%.%cc%

